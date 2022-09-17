using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Academy.DataLayer.Entities.User
{
    public class UserRole
    {
        public UserRole()
        {

        }

        [Key]
        public int UR_ID { get; set; }
        public int RoleID { get; set; }
        public int UserID { get; set; }


        #region Relations
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }

        #endregion
    }
}
