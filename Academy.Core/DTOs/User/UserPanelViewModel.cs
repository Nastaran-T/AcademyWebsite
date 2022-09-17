using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Academy.Core.DTOs
{
    #region informationUser
    public class InformationUserViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime RegisterDate { get; set; }
        public int Wallet { get; set; }
    }
    #endregion




    #region SideBar Userpanel
    public class SideBarDataViewModel
    {
        public string UserName { get; set; }
        public DateTime RegisterDate { get; set; }

        public string ImageName { get; set; }
    }
    #endregion

    #region Edit UserProfile
    public class EditUserPanelViewModel
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string UserName { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
        public string Email { get; set; }


        public IFormFile UserAvatar { get; set; }

        public string AvatarName { get; set; }
    }
    #endregion

    #region ChangePassword UserProfile
    public class ChangePasswordUserPanelViewModel
    {
        [Display(Name = "پسورد فعلی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string OldPassword { get; set; }

        [Display(Name = " پسورد جدید")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string NewPassword { get; set; }

        [Display(Name = "تکرار پسورد جدید ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        [Compare("NewPassword", ErrorMessage = "کلمه های عبور مغایرت دارند")]
        public string RePassword { get; set; }
    }
    #endregion
}


