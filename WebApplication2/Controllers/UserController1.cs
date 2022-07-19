using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.ViewModels;

namespace WebApplication2.Controllers
{
  [Authorize(Roles = "admin")]
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
      if (user != null)
      {
        await _userManager.DeleteAsync(user);
      }
      return RedirectToAction("Index");
    }

    [HttpGet]
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
      };
      var result = await _userManager.CreateAsync(user, model.Password);
      if (result.Succeeded)
      {
        return RedirectToAction("Index", "User");
      }
      else
      {
        foreach (var error in result.Errors)
        {
          ModelState.AddModelError(String.Empty, error.Description);
        }
      }
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
    [HttpGet]
    public IActionResult Edit(string? id)
    {
      if (id == null)
        return NotFound();
      var user = _userManager.Users.Where(x => x.Id == id).FirstOrDefault();
      if (user == null)
        return NotFound();

      return View(user);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(User user)
    {
      if (user == null)
        return NotFound();
      var result = await _userManager.UpdateAsync(user);
      if (result.Succeeded)
        return RedirectToAction("Index", "User");
      else
        foreach (var error in result.Errors)
        {
          ModelState.AddModelError(string.Empty, error.Description);
        };
      return Ok("Ошибка!");
    }
  }
}
