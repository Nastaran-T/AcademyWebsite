using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Academy.DataLayer.Entities.Wallet
{
    public class Wallet
    {
        public Wallet()
        {

        }


        [Key]
        public int WalletId { get; set; }

        [Display(Name = "نوع تراکنش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int TypeId { get; set; }

        [Display(Name = "کاربر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int UserID { get; set; }

        [Display(Name = "مبلغ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Amount { get; set; }

        [Display(Name = "شرح")]
        [MaxLength(500, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Description { get; set; }

        [Display(Name = "تایید شده")]
        public bool IsPay { get; set; }

        [Display(Name = "تاریخ و ساعت")]
        public DateTime CreateDate { get; set; }

        #region Relations
        //هر تراکنش متعلق به یک کاربر است
        public virtual User.User User { get; set; }

        //هر تراکنش یا واریز است یا پرداشت
        public WalletType WalletType { get; set; }
        #endregion
    }
}
