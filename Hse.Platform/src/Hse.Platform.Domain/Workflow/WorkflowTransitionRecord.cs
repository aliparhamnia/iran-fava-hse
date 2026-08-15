using Volo.Abp;
using Volo.Abp.Domain.Values;

namespace Hse.Platform.Workflow;

public class WorkflowTransitionRecord : ValueObject
{
    public string FromState { get; private set; } = default!;

    public string ToState { get; private set; } = default!;

    public string? RequiredPermission { get; private set; }

    private WorkflowTransitionRecord()
    {
    }

    public WorkflowTransitionRecord(string from, string to, string? requiredPermission)
    {
        FromState = Check.NotNullOrWhiteSpace(from, nameof(from), maxLength: WorkflowConsts.MaxStateLength);
        ToState = Check.NotNullOrWhiteSpace(to, nameof(to), maxLength: WorkflowConsts.MaxStateLength);
        RequiredPermission = requiredPermission;
    }

    protected override System.Collections.Generic.IEnumerable<object> GetAtomicValues()
    {
        yield return FromState;
        yield return ToState;
        yield return RequiredPermission ?? string.Empty;
    }
}
