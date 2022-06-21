using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Data
{
  public class ApplicationContext : IdentityDbContext<User>
  {
    public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options)
    {

    }
  }
}
