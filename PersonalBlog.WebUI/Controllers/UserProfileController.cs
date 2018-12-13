using PersonalBlog.Domain.Abstract;
using PersonalBlog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonalBlog.WebUI.Controllers
{
    public class UserProfileController : Controller
    {
        IRepository<UserProfile> usersProfileR;

        public UserProfileController(IRepository<UserProfile> repository)
        {
            usersProfileR = repository;
        }
        // GET: UserProfile
        public ViewResult Index()
        {
            UserProfile userProfile = usersProfileR.Get.FirstOrDefault(p => p.Name == User.Identity.Name);
            return View(userProfile);
        }
    }
}