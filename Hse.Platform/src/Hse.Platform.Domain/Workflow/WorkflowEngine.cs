using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Guids;

namespace Hse.Platform.Workflow;

public class WorkflowEngine : DomainService, IWorkflowEngine
{
    private readonly IRepository<WorkflowDefinition, Guid> _definitions;
    private readonly IRepository<WorkflowInstance, Guid> _instances;
    private readonly IRepository<WorkflowHistory, Guid> _histories;
    private readonly IGuidGenerator _guidGenerator;

    public WorkflowEngine(
        IRepository<WorkflowDefinition, Guid> definitions,
        IRepository<WorkflowInstance, Guid> instances,
        IRepository<WorkflowHistory, Guid> histories,
        IGuidGenerator guidGenerator)
    {
        _definitions = definitions;
        _instances = instances;
        _histories = histories;
        _guidGenerator = guidGenerator;
    }

    public async Task<WorkflowInstance> StartAsync(string entityType, Guid entityId, CancellationToken cancellationToken = default)
    {
        var existing = await FindInstanceAsync(entityType, entityId, cancellationToken);
        if (existing != null)
        {
            return existing;
        }

        var definition = await GetActiveDefinitionAsync(entityType);
        var instance = new WorkflowInstance(
            _guidGenerator.Create(),
            definition.Id,
            entityType,
            entityId,
            definition.GetInitialState());

        await _instances.InsertAsync(instance, cancellationToken: cancellationToken);
        return instance;
    }

    public async Task<WorkflowInstance> TransitionAsync(
        string entityType,
        Guid entityId,
        string toState,
        string? comment = null,
        CancellationToken cancellationToken = default)
    {
        var instance = await FindInstanceAsync(entityType, entityId, cancellationToken)
                       ?? throw new BusinessException(PlatformDomainErrorCodes.WorkflowInstanceNotFound);

        var definition = await _definitions.GetAsync(instance.DefinitionId, cancellationToken: cancellationToken);
        if (!definition.CanTransition(instance.CurrentState, toState))
        {
            throw new BusinessException(PlatformDomainErrorCodes.WorkflowTransitionNotAllowed)
                .WithData("From", instance.CurrentState)
                .WithData("To", toState);
        }

        var from = instance.CurrentState;
        instance.MoveTo(toState);

        await _histories.InsertAsync(
            new WorkflowHistory(_guidGenerator.Create(), instance.Id, from, toState, comment),
            cancellationToken: cancellationToken);

        await _instances.UpdateAsync(instance, cancellationToken: cancellationToken);
        return instance;
    }

    public async Task<WorkflowInstance?> FindInstanceAsync(string entityType, Guid entityId, CancellationToken cancellationToken = default)
    {
        return await _instances.FirstOrDefaultAsync(
            x => x.EntityType == entityType && x.EntityId == entityId,
            cancellationToken: cancellationToken);
    }

    private async Task<WorkflowDefinition> GetActiveDefinitionAsync(string entityType)
    {
        var definitions = await _definitions.GetListAsync(x => x.EntityType == entityType && x.IsActive);
        var definition = definitions.OrderByDescending(x => x.Version).FirstOrDefault();
        if (definition == null)
        {
            throw new BusinessException(PlatformDomainErrorCodes.WorkflowDefinitionNotFound)
                .WithData("EntityType", entityType);
        }

        return definition;
    }
}
