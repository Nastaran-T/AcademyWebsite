using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Academy.DataLayer.Entities.User;

namespace Academy.DataLayer.Entities.Permission
{
    //این انتیتی برای اینه که هر نقش چه دسترسی هایی دارد
    public class RolePermission
    {
        [Key]
        public int RP_Id { get; set; }
        public int PermissionId { get; set; }
        public int RoleID { get; set; }

        #region Relations
        public Permission Permission { get; set; }
        public Role Role { get; set; }
        #endregion
    }
}
