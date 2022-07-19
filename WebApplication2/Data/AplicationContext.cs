using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Data
{
  public class ApplicationContext : IdentityDbContext<User>
  {
    public DbSet<Work> Works { get; set; }
    public DbSet<Сomment> Comments { get; set; }
    public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options)
    {
      Database.EnsureCreated();
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      modelBuilder.Entity<Work>().ToTable("Works");
      modelBuilder.Entity<Сomment>().ToTable("Сomments");

      modelBuilder.Entity<Сomment>().HasKey(c => new { c.WorkId, c.UserId });
    }

  }
}
