
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Academy.DataLayer.Entities.Course;
using Academy.DataLayer.Entities.Order;
using Academy.DataLayer.Entities.Permission;
using Academy.DataLayer.Entities.User;
using Academy.DataLayer.Entities.Wallet;
using Academy.DataLayer.Entities.Order;

namespace Academy.DataLayer.Context
{
    public class AcademyContext:DbContext
    {
        public AcademyContext(DbContextOptions<AcademyContext> options):base(options)
        {

        }



        #region User
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole>  UserRoles { get; set; }
        public DbSet<UserDiscountCode> UserDiscountCodes { get; set; }
        #endregion
        #region Wallet
        public DbSet<WalletType> WalletTypes { get; set; }
        public DbSet<Wallet> Wallets { get; set; }

        #endregion
        #region Permission
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        #endregion

        #region Course
        public DbSet<CourseGroup> CourseGroups { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseLevel> CourseLevels { get; set; }
        public DbSet<CourseStatus> CourseStatuses { get; set; }
        public DbSet<CourseEpisode> CourseEpisodes { get; set; }
        public DbSet<UserCourse> userCourses { get; set; }
        public DbSet<CourseComment> CourseComments { get; set; }
        public DbSet<CourseVote> CourseVotes { get; set; }
        #endregion

        #region Order
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        #endregion



        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            //این تیکه باعث میشه که کسکید  کردن توسط انتیتی غیر فعال بشه و ما خودمون اونو کنترل کنیم
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
               .SelectMany(t => t.GetForeignKeys())
               .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;



            modelBuilder.Entity<User>().HasQueryFilter(u=>!u.IsDelete);
            modelBuilder.Entity<Role>().HasQueryFilter(r=>!r.IsDelete);
            modelBuilder.Entity<CourseGroup>().HasQueryFilter(c=>!c.IsDelete);


            //configure relation courses table with fluent
            modelBuilder.Entity<Course>()
                  .HasOne(c => c.CourseGroup)
                  .WithMany(c => c.Courses)
                  .HasForeignKey(m => m.GroupId);

            modelBuilder.Entity<Course>()
                 .HasOne(c => c.CourseGroupSub)
                 .WithMany(c => c.CoursesSub)
                 .HasForeignKey(m => m.ParentId);





            base.OnModelCreating(modelBuilder);
        }


    }
}
