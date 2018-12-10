using PersonalBlog.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonalBlog.WebUI.Controllers
{
    public class TagController : Controller
    {
        private ITagsRepository repository;
        public TagController(ITagsRepository tagsRepository)
        {
            repository = tagsRepository;
        }
        // GET: Tag
        public PartialViewResult MenuTag(string Tag)
        {
            ViewBag.SelectedTag = Tag;
            return PartialView(repository.Tags.OrderByDescending(p=>p.TagId).Take(10).Select(x => x.Name));
        }
    }
}