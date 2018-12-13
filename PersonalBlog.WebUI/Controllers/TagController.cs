using PersonalBlog.Domain.Abstract;
using PersonalBlog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonalBlog.WebUI.Controllers
{
    public class TagController : Controller
    {
        private IRepository<Tag> repository;
        public TagController(IRepository<Tag> tagsRepository)
        {
            repository = tagsRepository;
        }
        // GET: Tag
        public PartialViewResult MenuTag(string Tag)
        {
            ViewBag.SelectedTag = Tag;
            return PartialView(repository.Get.OrderByDescending(p=>p.TagId).Take(10).Select(x => x.Name));
        }
    }
}