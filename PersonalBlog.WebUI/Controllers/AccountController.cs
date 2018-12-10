using PersonalBlog.Domain.Abstract;
using PersonalBlog.Domain.Entities;
using PersonalBlog.WebUI.Infrastructure.Abstract;
using PersonalBlog.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonalBlog.WebUI.Controllers
{
    public class AccountController : Controller
    {
        IAuthProvider authProvider;
        IUsersRepository usersRepository;
        IUsersProfileRepository usersProfileRepository;

        public AccountController(IAuthProvider provider, IUsersRepository users, IUsersProfileRepository usersProfile)
        {
            authProvider = provider;
            usersRepository = users;
            usersProfileRepository = usersProfile;
        }

        public ViewResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (authProvider.Authenticate(model.UserName, model.Password))
                {
                    return Redirect(returnUrl ?? Url.Action("Manager", "BlogManager"));
                }
                else
                {
                    ModelState.AddModelError("notLog", "Неверный логин или пароль");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = usersRepository.Users.FirstOrDefault(u => u.Name == model.Name);
                if (user == null)
                {
                    User userSave = new User { Name = model.Name, Password = model.Password, UserId = 0, RoleId = 1 };
                    usersRepository.SaveUsers(userSave);
                    UserProfile userProfileSafe = new UserProfile { Name = model.Name, Age = model.Age, Gender = model.Gender, Country = model.Country };
                    usersProfileRepository.SaveUsersProfile(userProfileSafe);
                    if (authProvider.Authenticate(userSave.Name, userSave.Password))
                    {
                        return RedirectToAction("List", "Post");
                    }
                }
                else
                {
                    ModelState.AddModelError("Reg", "Пользователь с таким логином уже существует");
                }
            }

            return View(model);
        }
        public ActionResult LogOut()
        {
            authProvider.LogOut();
            return RedirectToAction("list", "Post");
        }

        
    }
}