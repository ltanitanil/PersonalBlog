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
        IRepository<User> usersRepository;
        IRepository<UserProfile> usersProfileRepository;

        public AccountController(IAuthProvider provider, IRepository<User> users, IRepository<UserProfile> usersProfile)
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
                    return Redirect(returnUrl ?? Url.Action("Manager", "Cabinet"));
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
        [HttpGet]
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
                User user = usersRepository.Get.FirstOrDefault(u => u.Name == model.Name);
                if (user == null)
                {
                    User userSave = new User { Name = model.Name, Password = model.Password, UserId = 0, RoleId = 1 };
                    usersRepository.Save(userSave);
                    UserProfile userProfileSafe = new UserProfile { Name = model.Name, Age = model.Age, Gender = model.Gender, Country = model.Country };
                    usersProfileRepository.Save(userProfileSafe);
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