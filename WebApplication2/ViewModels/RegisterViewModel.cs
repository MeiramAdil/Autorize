using System.ComponentModel.DataAnnotations;

namespace WebApplication2.ViewModels
{
  public class RegisterViewModel
  {
    [Required]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Required]
    [Display(Name = "Год рождения")]
    public int Year { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }
    [Required]
    [Compare("Password", ErrorMessage = "Пароли не совподают")]
    [DataType(DataType.Password)]
    [Display(Name = "Подтверждение пароля")]
    public string PasswordConfirm { get; set; }
  }
}
