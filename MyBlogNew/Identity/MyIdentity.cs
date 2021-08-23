using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace MyBlogNew.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
    public class MyIdentity: IdentityDbContext<ApplicationUser>
    {
        public MyIdentity():base("sqlserverDB")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MyIdentity, Migrations.Configuration>());
            modelBuilder.Entity<IdentityUserRole>().HasKey(c => c.RoleId).HasKey(c => c.UserId);
            modelBuilder.Entity<IdentityUserLogin>().HasKey(c => c.UserId);
            base.OnModelCreating(modelBuilder);
        }
        public static MyIdentity Create()
        {
            return new MyIdentity();
        }
    }
}