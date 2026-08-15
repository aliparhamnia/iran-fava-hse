using Volo.Abp;
using Volo.Abp.Domain.Values;

namespace Hse.Platform.Workflow;

public class WorkflowStateRecord : ValueObject
{
    public string Code { get; private set; } = default!;

    public string DisplayName { get; private set; } = default!;

    public bool IsInitial { get; private set; }

    public bool IsTerminal { get; private set; }

    private WorkflowStateRecord()
    {
    }

    public WorkflowStateRecord(string code, string displayName, bool isInitial, bool isTerminal)
    {
        Code = Check.NotNullOrWhiteSpace(code, nameof(code), maxLength: WorkflowConsts.MaxStateLength);
        DisplayName = Check.NotNullOrWhiteSpace(displayName, nameof(displayName), maxLength: WorkflowConsts.MaxDisplayNameLength);
        IsInitial = isInitial;
        IsTerminal = isTerminal;
    }

    protected override System.Collections.Generic.IEnumerable<object> GetAtomicValues()
    {
        yield return Code;
        yield return DisplayName;
        yield return IsInitial;
        yield return IsTerminal;
    }
}
