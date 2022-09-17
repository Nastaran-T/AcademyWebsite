using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Academy.DataLayer.Entities.Permission
{
    public class Permission
    {
        [Key]
        public int PermissionId { get; set; }

        [Display(Name = "عنوان دسترسی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string PermissionTitle { get; set; }

       
        public int? ParentId { get; set; }

        #region Relations
        //اینجا پرنت آی دی را باید وصل کنیم به خود پرمیشن
        //یک پرمیشن می تواند چند تا  زیر مجموعه داشته باشد
        [ForeignKey("ParentId")]
        public List<Permission> Permissions { get; set; }
        public List<RolePermission> RolePermissions { get; set; }
        #endregion

    }
}
