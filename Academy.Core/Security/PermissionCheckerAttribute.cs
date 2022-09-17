using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using Academy.Core.Services.Interfaces;

namespace Academy.Core.Security
{
    public class PermissionCheckerAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private IPermissionService _permissionService;
        private int _permissionId = 0;

        //ورودی میشه به جای آی دی اسم پرمیشن قرار داد
        public PermissionCheckerAttribute(int permissionId)
        {
            _permissionId = permissionId;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //در این جا ما می توانیم از context  سرویس هامون رو بگیریم 
            _permissionService =(IPermissionService) context.HttpContext.RequestServices.GetService(typeof(IPermissionService));

            //اول پک کنیم که کاربر احراز هویت شده باشه
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                string userName = context.HttpContext.User.Identity.Name;

                if (!_permissionService.checkPermissionUser(_permissionId,userName))
                {
                    context.Result = new RedirectResult("/Login?"+context.HttpContext.Request.Path);
                }

            }
            else 
            {
                context.Result = new RedirectResult("/Login");
            }

        }
    }
}
