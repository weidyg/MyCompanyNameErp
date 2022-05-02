namespace MyCompanyName.Identity
{
    public static class IdentityErrorCodes
    {
        public const string UserSelfDeletion = "Identity:010001";
        public const string MaxAllowedOuMembership = "Identity:010002";
        public const string ExternalUserPasswordChange = "Identity:010003";
        public const string DuplicateOrganizationUnitDisplayName = "Identity:010004";
        public const string StaticRoleRenaming = "Identity:010005";
        public const string StaticRoleDeletion = "Identity:010006";
        public const string UsersCanNotChangeTwoFactor = "Identity:010007";
        public const string CanNotChangeTwoFactor = "Identity:010008";
        public const string SystemAdminUserDeletion = "Identity:010009";

        public const string ClientTypeEmpty = "Identity:010010";
        public const string UserCompanyEmpty = "Identity:010011";
        public const string NotFindUserTenant = "Identity:010012";
    }
}
