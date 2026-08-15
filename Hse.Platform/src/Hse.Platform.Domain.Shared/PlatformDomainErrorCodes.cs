namespace Hse.Platform;

public static class PlatformDomainErrorCodes
{
    public const string EmployeeNumberAlreadyExists = "Platform:EmployeeNumberAlreadyExists";
    public const string EmployeeAlreadyTerminated = "Platform:EmployeeAlreadyTerminated";
    public const string WorkflowDefinitionNotFound = "Platform:WorkflowDefinitionNotFound";
    public const string WorkflowTransitionNotAllowed = "Platform:WorkflowTransitionNotAllowed";
    public const string WorkflowInstanceNotFound = "Platform:WorkflowInstanceNotFound";
}
