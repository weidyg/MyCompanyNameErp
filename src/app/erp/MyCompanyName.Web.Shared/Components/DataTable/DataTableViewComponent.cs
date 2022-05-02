using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Volo.Abp.AspNetCore.Mvc;

namespace MyCompanyName.Web.Shared.Components
{
    public class DataTableViewComponent : AbpViewComponent
    {

        public IViewComponentResult Invoke(string id, List<ColumnTpl> templates)
        {
            var model = new DataTableViewComponentModel { Id = id, Templates = templates ?? new List<ColumnTpl>() };
            return View("~/Components/DataTable/Default.cshtml", model);
        }
    }

    public class DataTableViewComponentModel
    {
        public string Id { get; set; }
        public List<ColumnTpl> Templates { get; set; }
    }

    public class ColumnTpl
    {
        public ColumnTpl(string name, Func<dynamic, object> func, dynamic dynamic = null)
        {
            Name = name;
            Template = func.Invoke(dynamic);
        }
        public string Name { get; set; }
        public IHtmlContent Template { get; set; }
    }
}
