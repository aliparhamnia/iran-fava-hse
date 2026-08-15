using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Hse.Platform.Workflow;

public class WorkflowInstance : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    public Guid? TenantId { get; protected set; }

    public Guid DefinitionId { get; protected set; }

    public string EntityType { get; protected set; } = default!;

    public Guid EntityId { get; protected set; }

    public string CurrentState { get; protected set; } = default!;

    protected WorkflowInstance()
    {
    }

    public WorkflowInstance(Guid id, Guid definitionId, string entityType, Guid entityId, string currentState)
        : base(id)
    {
        DefinitionId = definitionId;
        EntityType = Check.NotNullOrWhiteSpace(entityType, nameof(entityType), maxLength: WorkflowConsts.MaxEntityTypeLength);
        EntityId = entityId;
        CurrentState = Check.NotNullOrWhiteSpace(currentState, nameof(currentState), maxLength: WorkflowConsts.MaxStateLength);
    }

    public void MoveTo(string toState)
    {
        CurrentState = Check.NotNullOrWhiteSpace(toState, nameof(toState), maxLength: WorkflowConsts.MaxStateLength);
    }
}
