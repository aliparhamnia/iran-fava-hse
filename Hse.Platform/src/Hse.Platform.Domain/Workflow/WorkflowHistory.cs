using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Hse.Platform.Workflow;

public class WorkflowHistory : CreationAuditedEntity<Guid>, IMultiTenant
{
    public Guid? TenantId { get; protected set; }

    public Guid InstanceId { get; protected set; }

    public string FromState { get; protected set; } = default!;

    public string ToState { get; protected set; } = default!;

    public string? Comment { get; protected set; }

    protected WorkflowHistory()
    {
    }

    public WorkflowHistory(Guid id, Guid instanceId, string fromState, string toState, string? comment)
        : base(id)
    {
        InstanceId = instanceId;
        FromState = Check.NotNullOrWhiteSpace(fromState, nameof(fromState), maxLength: WorkflowConsts.MaxStateLength);
        ToState = Check.NotNullOrWhiteSpace(toState, nameof(toState), maxLength: WorkflowConsts.MaxStateLength);
        Comment = comment;
    }
}
