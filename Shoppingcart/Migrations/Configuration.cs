 using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Shoppingcart.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
namespace Shoppingcart.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Shoppingcart.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(
            new RoleStore<IdentityRole>(context));
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }

            var userManager = new UserManager<ApplicationUser>(
            new UserStore<ApplicationUser>(context));
            if (!context.Users.Any(u => u.Email == "glance16540@gmail.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "glance16540@gmail.com",
                    Email = "glance16540@gmail.com",
                    FirstName ="Garrett",
                    LastName = "Lance",
                }, "Tuckerandhobbes1");
            }
            var userId = userManager.FindByEmail("glance16540@gmail.com").Id;
            userManager.AddToRole(userId, "Admin");
        }
    }
}
