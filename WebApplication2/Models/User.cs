using Microsoft.AspNetCore.Identity;

namespace WebApplication2.Models
{
  public class User : IdentityUser
  {
    public int BirthYear { get; set; }
  }
}
