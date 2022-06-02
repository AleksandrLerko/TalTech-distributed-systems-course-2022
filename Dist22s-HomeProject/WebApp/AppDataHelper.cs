using System.Security.Claims;
using App.DAL.EF;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebApp;

public static class AppDataHelper
{
    public static void SetupData(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
    {
        using var serviceScope = app.
            ApplicationServices.
            GetRequiredService<IServiceScopeFactory>().
            CreateScope();

        using var context = serviceScope.
            ServiceProvider.
            GetService<AppDbContext>();

        if (context == null)
        {
            throw new ApplicationException("Problem in services. No db context.");
        }
        
        if (context.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory") return;

        if (configuration.GetValue<bool>("DataInitialization:DropDatabase"))
        {
            context.Database.EnsureDeleted();
        }

        if (configuration.GetValue<bool>("DataInitialization:MigrateDatabase"))
        {
            context.Database.Migrate();
        }

        if (configuration.GetValue<bool>("DataInitialization:SeedIdentity"))
        {
            using var userManager = serviceScope.ServiceProvider.GetService<UserManager<AppUser>>();
            using var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<AppRole>>();

            if (userManager == null || roleManager == null)
            {
                throw new NullReferenceException("userManager or roleManager cannot be null!!!");
            }
            
            var roles = new string[]
            {
                "admin",
                "manager",
                "user"
            };

            foreach (var roleInfo in roles)
            {
                var role = roleManager.FindByNameAsync(roleInfo).Result;
                if (role == null)
                {
                    var identityResult = roleManager.CreateAsync(new AppRole() {Name = roleInfo}).Result;

                    if (!identityResult.Succeeded)
                    {
                        throw new ApplicationException("Role creation failed.");
                    }
                }
            }

            var users = new (string username,string firstName, string lastName, string password ,string roles)[]
            {
                ("allerk@ttu.ee","Aleksandr", "Lerko", "FooBar1!", "user,admin"),
                ("allerk2@ttu.ee", "Aleksandr", "Lerko2", "FooBar1!", "user,admin"),
                ("manager@ttu.ee", "Manager", "TalTech", "FooBar1!", "user,manager"),
                ("user@ttu.ee", "User", "TTU", "FooBar1!", "user"),
                ("nouser@ttu.ee", "NoUser", "TTU", "FooBar1!", "")
            };

            foreach (var userInfo in users)
            {
                var user = userManager.FindByEmailAsync(userInfo.username).Result;

                if (user == null)
                {
                    user = new AppUser()
                    {
                        Email = userInfo.username,
                        UserName = userInfo.username,
                        FirstName = userInfo.firstName,
                        LastName = userInfo.lastName,
                        EmailConfirmed = true
                    };

                    var identityResult = userManager.CreateAsync(user, userInfo.password).Result;
                    identityResult =  userManager.AddClaimAsync(user, new Claim("aspnet.firstname",user.FirstName)).Result;
                    identityResult =  userManager.AddClaimAsync(user, new Claim("aspnet.lastname",user.LastName)).Result;


                    if (!identityResult.Succeeded)
                    {
                        throw new ApplicationException("Cannot create user!");
                    }
                }

                if (!string.IsNullOrWhiteSpace(userInfo.roles))
                {
                    var identityResultRole = userManager.AddToRolesAsync(user,
                            userInfo.roles.Split(",").Select(r => r.Trim()))
                        .Result;
                }
            }
            
            
        }        
        
        if (configuration.GetValue<bool>("DataInitialization:SeedData"))
        {
            
        }
    }
}