using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Academy.DataLayer.Entities.Course
{
    /*این جدول برای این است که که درس های هر کاربر را که خریداری کرده 
     * را ذخیره کنیم که ببینیم هر کاربر چه درسی را خریداری کرده که بتونه دانلودش کنه
     */
    public class UserCourse
    {
        [Key]
        public int UC_Id { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }


        public Course Course { get; set; }
        public User.User User { get; set; }
    }

}
