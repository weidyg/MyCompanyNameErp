using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Users;

namespace MyCompanyName.Efx.Web.Components
{
    public class TenantSwitchViewComponent : AbpViewComponent
    {
        private readonly ITenantStore _tenantStore;
        private readonly ICurrentUser _currentUser;
        public TenantSwitchViewComponent(
            ITenantStore tenantStore,
            ICurrentUser currentUser
            )
        {
            _tenantStore = tenantStore;
            _currentUser = currentUser;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var linkTenantIds = _currentUser.GetLinkTenantIds();
            var allTenants = new List<TenantInfo>();
            foreach (var tenantIdStr in linkTenantIds)
            {
                Guid.TryParse(tenantIdStr, out Guid tenantId);
                var tenant = await _tenantStore.FindAsync(tenantId);
                allTenants.Add(new TenantInfo(tenant.Id, tenant.Name));
            }
            var currentTenantId = _currentUser.TenantId;
            var model = new TenantSwitchViewComponentModel
            {
                CurrentTenant = allTenants.Find(f => f.Id == currentTenantId),
                OtherTenants = allTenants.FindAll(f => f.Id != currentTenantId)
            };
            return View("~/Components/Toolbar/TenantSwitch/Default.cshtml", model);
        }
    }

    public class TenantSwitchViewComponentModel
    {
        public TenantInfo CurrentTenant { get; set; }

        public List<TenantInfo> OtherTenants { get; set; }

        public string TenantChange(TenantInfo tenant, string encodedPathQuery)
        {
            return $"/MyCompanyName/Tenant/Switch?id={tenant.Id}&returnUrl={WebUtility.UrlEncode(encodedPathQuery)}";
        }
    }

    public class TenantInfo
    {
        public TenantInfo(Guid? id, string name)
        {
            Id = id;
            Name = name;
            DisplayName = name;
        }
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }

    }
}
