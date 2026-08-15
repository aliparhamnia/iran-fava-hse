using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hse.Platform.Workflow;

public interface IWorkflowEngine
{
    Task<WorkflowInstance> StartAsync(string entityType, Guid entityId, CancellationToken cancellationToken = default);

    Task<WorkflowInstance> TransitionAsync(
        string entityType,
        Guid entityId,
        string toState,
        string? comment = null,
        CancellationToken cancellationToken = default);

    Task<WorkflowInstance?> FindInstanceAsync(string entityType, Guid entityId, CancellationToken cancellationToken = default);
}
