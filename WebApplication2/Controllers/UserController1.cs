using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.ViewModels;

namespace WebApplication2.Controllers
{
  public class UserController : Controller
  {
    private UserManager<User> _userManager;
    public UserController(UserManager<User> userManager)
    {
      _userManager = userManager;
    }
    public IActionResult Index()
    {
      return View(_userManager.Users.ToList());
    }
    
    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
      User user = await _userManager.FindByIdAsync(id);
      if(user != null)
      {
        await _userManager.DeleteAsync(user);
      }
      return RedirectToAction("Index");
    }

    public IActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(RegisterViewModel model)
    {
      User user = new User
      {
        Email = model.Email,
        UserName = model.Email,
        BirthYear = model.Year
      };
      return View(user);
    }

    [HttpPost]

    public async Task<IActionResult> UpdatePassword(UpdatePasswordViewModel model)
    {
      if (ModelState.IsValid)
      {
        User user = await _userManager.FindByIdAsync(model.Id);
        if (user != null)
        {
          var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
          if (!result.Succeeded)
          {
            return Conflict();
          }
        }
      }
      return RedirectToAction("Index", "Home");
    }

  }
}
