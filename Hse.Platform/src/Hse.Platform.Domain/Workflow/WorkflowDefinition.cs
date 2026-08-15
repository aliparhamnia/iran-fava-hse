using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Hse.Platform.Workflow;

public class WorkflowDefinition : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    public Guid? TenantId { get; protected set; }

    public string EntityType { get; protected set; } = default!;

    public int Version { get; protected set; }

    public bool IsActive { get; protected set; }

    public ICollection<WorkflowStateRecord> States { get; protected set; } = new List<WorkflowStateRecord>();

    public ICollection<WorkflowTransitionRecord> Transitions { get; protected set; } = new List<WorkflowTransitionRecord>();

    protected WorkflowDefinition()
    {
    }

    public WorkflowDefinition(Guid id, string entityType, int version = 1)
        : base(id)
    {
        EntityType = Check.NotNullOrWhiteSpace(entityType, nameof(entityType), maxLength: WorkflowConsts.MaxEntityTypeLength);
        Version = version;
        IsActive = true;
    }

    public void AddState(string code, string displayName, bool isInitial = false, bool isTerminal = false)
    {
        if (States.Any(x => x.Code == code))
        {
            return;
        }

        States.Add(new WorkflowStateRecord(code, displayName, isInitial, isTerminal));
    }

    public void AddTransition(string from, string to, string? requiredPermission = null)
    {
        if (Transitions.Any(x => x.FromState == from && x.ToState == to))
        {
            return;
        }

        Transitions.Add(new WorkflowTransitionRecord(from, to, requiredPermission));
    }

    public string GetInitialState()
    {
        return States.FirstOrDefault(x => x.IsInitial)?.Code
               ?? throw new BusinessException(PlatformDomainErrorCodes.WorkflowDefinitionNotFound);
    }

    public bool CanTransition(string from, string to)
    {
        return Transitions.Any(x => x.FromState == from && x.ToState == to);
    }

    public WorkflowTransitionRecord? FindTransition(string from, string to)
    {
        return Transitions.FirstOrDefault(x => x.FromState == from && x.ToState == to);
    }
}
