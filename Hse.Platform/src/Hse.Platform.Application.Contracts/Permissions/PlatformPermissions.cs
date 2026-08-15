namespace Hse.Platform.Permissions;

public static class PlatformPermissions
{
    public const string GroupName = "Platform";

    public static class Employees
    {
        public const string Default = GroupName + ".Employees";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
    }

    public static class Workflow
    {
        public const string Default = GroupName + ".Workflow";
        public const string Manage = Default + ".Manage";
    }
}
