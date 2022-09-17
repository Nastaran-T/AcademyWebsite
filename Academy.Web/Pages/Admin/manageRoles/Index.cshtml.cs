using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Academy.Core.Security;
using Academy.Core.Services.Interfaces;
using Academy.DataLayer.Entities.User;

namespace Academy.Web.Pages.Admin.manageRoles
{
    [PermissionChecker(6)]
    public class IndexModel : PageModel
    {
        private IPermissionService _permissionService;
        public IndexModel(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public List<Role> Roles { get; set; }

        public void OnGet()
        {
            Roles = _permissionService.GetRoles();
        }
    }
}
