using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Model;

namespace DataInitializer
{
    public class DbInitializer : IDbInitializer
    {
        public void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>())
                {
                    context.Database.Migrate();

                    var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<ApplicationRole>>();
                    var userManager = serviceScope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
                }
            }
        }
    }
}
