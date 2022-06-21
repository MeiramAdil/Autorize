using System.ComponentModel.DataAnnotations;

namespace WebApplication2.ViewModels
{
  public class LoginViewModel
  {
    [Required]
    [Display(Name ="Email")]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name ="Пароль")]
    public string Password { get; set; }
    [Display(Name = "Запомнить ли вас?")]
    public bool RememberMe { get; set; }
  }
}
