using Sunton.Erp.Entities.System;

namespace Sunton.Erp.Identity
{
    internal class IdentityRoleNameChangedEvent
    {
        public IdentityRole IdentityRole { get; set; }
        public string OldName { get; set; }
    }
}