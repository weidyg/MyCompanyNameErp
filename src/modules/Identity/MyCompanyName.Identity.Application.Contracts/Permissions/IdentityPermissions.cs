using Volo.Abp.Reflection;

namespace MyCompanyName.Identity.Permissions
{
    public static class IdentityPermissions
    {
        public const string GroupName = "Identity";

        public static class Roles
        {
            public const string Default = GroupName + "_Roles";
            public const string Create = Default + "_Create";
            public const string Update = Default + "_Update";
            public const string Delete = Default + "_Delete";
            public const string ManagePermissions = Default + "_ManagePermissions";
        }

        public static class Users
        {
            public const string Default = GroupName + "_Users";
            public const string Create = Default + "_Create";
            public const string Update = Default + "_Update";
            public const string Delete = Default + "_Delete";
            public const string ManagePermissions = Default + "_ManagePermissions";
        }

        public static class UserLookup
        {
            public const string Default = GroupName + "_UserLookup";
        }

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(IdentityPermissions));
        }
    }
}