using Microsoft.Owin;
using Owin;
using Booktopia.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
[assembly: OwinStartupAttribute(typeof(Booktopia.Startup))]
namespace Booktopia
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createAdminUserAndApplicationRoles();
        }
        private void createAdminUserAndApplicationRoles()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            if (!roleManager.RoleExists("Administrator"))
            {
                var role = new IdentityRole();
                role.Name = "Administrator";
                roleManager.Create(role);
                var user = new ApplicationUser();
                user.UserName = "radusorin@admin.com";
                user.Email = "radusorin@admin.com";
                var adminCreated = UserManager.Create(user, "radminsorin");
                if (adminCreated.Succeeded)
                {
                    UserManager.AddToRole(user.Id, "Administrator");
                }
                var user2 = new ApplicationUser();
                user2.UserName = "pirvudaniel@admin.com";
                user2.Email = "pirvudaniel@admin.com";
                var admin2Created = UserManager.Create(user2, "padmindaniel");
                if (admin2Created.Succeeded)
                {
                    UserManager.AddToRole(user2.Id, "Administrator");
                }
            }
            if (!roleManager.RoleExists("Colaborator"))
            {
                var role = new IdentityRole();
                role.Name = "Colaborator";
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("User"))
            {
                var role = new IdentityRole();
                role.Name = "User";
                roleManager.Create(role);
            }
        }
    }
}
