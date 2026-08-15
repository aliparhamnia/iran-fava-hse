using Hse.Platform.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Hse.Platform.Permissions;

public class PlatformPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var group = context.AddGroup(PlatformPermissions.GroupName, L("Permission:Platform"));

        var employees = group.AddPermission(PlatformPermissions.Employees.Default, L("Permission:Employees"));
        employees.AddChild(PlatformPermissions.Employees.Create, L("Permission:Employees.Create"));
        employees.AddChild(PlatformPermissions.Employees.Update, L("Permission:Employees.Update"));

        var workflow = group.AddPermission(PlatformPermissions.Workflow.Default, L("Permission:Workflow"));
        workflow.AddChild(PlatformPermissions.Workflow.Manage, L("Permission:Workflow.Manage"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<PlatformResource>(name);
    }
}
