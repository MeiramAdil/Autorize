using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WebApplication2.Data;

namespace WebApplication2.Models
{
  public static class Roleinitializer
  {
    public static async Task InitializeAsync(this ApplicationContext context)
    {
      var roleManager = new RoleStore<IdentityRole>(context);
      var userManager = new UserStore<User>(context);

      string adminEmail = "admin@gmail.com";
      var role = roleManager.Roles.FirstOrDefault(x => x.Name == "admin");
      
      if (role == null)
      {
        await roleManager.CreateAsync(new IdentityRole("admin"));
      }
      if (await roleManager.FindByNameAsync("user") == null)
      {
        await roleManager.CreateAsync(new IdentityRole("user"));
      }
      if (await userManager.FindByNameAsync(adminEmail) == null)
      {
        User admin = new User { Email = adminEmail, UserName = adminEmail, };

        var password = new PasswordHasher<User>();
        var hashed = password.HashPassword(admin, "_Aa123456");
        admin.PasswordHash = hashed;
  
        IdentityResult result = await userManager.CreateAsync(admin);
        if (result.Succeeded)
        {
          await userManager.AddToRoleAsync(admin, "admin");
        }
      }
    }
  }
}

