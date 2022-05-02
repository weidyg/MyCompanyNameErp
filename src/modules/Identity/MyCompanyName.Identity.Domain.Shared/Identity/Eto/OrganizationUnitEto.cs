﻿using System;

namespace MyCompanyName.Identity
{
    [Serializable]
    public class OrganizationUnitEto
    {
        public Guid Id { get; set; }

        public Guid? TenantId { get; set; }

        public string Code { get; set; }

        public string DisplayName { get; set; }
    }
}
