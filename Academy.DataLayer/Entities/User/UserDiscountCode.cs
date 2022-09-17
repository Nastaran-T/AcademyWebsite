using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Academy.DataLayer.Entities.Order;

namespace Academy.DataLayer.Entities.User
{
    //برای اینکه بدونیم هر کاربر از چه کد تخفیفی استفاده کرده 
        public class UserDiscountCode
        {
            [Key]
            public int UD_Id { get; set; }
            public int UserId { get; set; }
            public int DiscountId { get; set; }


            public User User { get; set; }
            public Discount Discount { get; set; }
        }
    
}
