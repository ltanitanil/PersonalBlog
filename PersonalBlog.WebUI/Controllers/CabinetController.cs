using PersonalBlog.Domain.Abstract;
using PersonalBlog.Domain.Entities;
using PersonalBlog.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonalBlog.WebUI.Controllers
{
    [Authorize]
    public class CabinetController : Controller
    {
        private IRepository<Blog> blogsRepository;
        private IRepository<Post> postsRepository;
        private IRepository<UserProfile> usersProfileRepository;
        private IRepository<Tag> tagsRepository;

        public CabinetController(IRepository<Blog> blogRep, IRepository<UserProfile> usersProfileRep, IRepository<Post> postRep, IRepository<Tag> tagRep)
        {
            blogsRepository = blogRep;
            usersProfileRepository = usersProfileRep;
            postsRepository = postRep;
            tagsRepository = tagRep;
        }
        // GET: BlogManager
        public ActionResult Manager()
        {
            BlogManager Blogs = new BlogManager()
            {
                Blogs = blogsRepository.Get.Where(p => p.User.Name == User.Identity.Name),
                BlogCount = blogsRepository.Get.Where(p => p.User.Name == User.Identity.Name).Count()
            };
            return View(Blogs);
        }
        public ActionResult PostManager(int blogId)
        {
            ViewBag.BlogId = blogId;
            return View(postsRepository.Get.Where(p => p.BlogId == blogId));
        }

        [HttpPost]
        public ActionResult BlogDelete(int BlogId)
        {
            return RedirectToAction("Manager");
        }
        [HttpGet]
        public ViewResult BlogEdit(int BlogId)
        {
            Blog blog = blogsRepository.Get
              .FirstOrDefault(p => p.BlogId == BlogId);
            if (blog.User.Name == User.Identity.Name) { return View(blog); }
            return View();
        }
        [HttpPost]
        public ActionResult BlogEdit(Blog Blog, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                Blog.UserProfileId = usersProfileRepository.Get.FirstOrDefault(p => p.Name == User.Identity.Name).UserProfileId;
                if (image != null)
                {
                    Blog.ImageMimeType = image.ContentType;
                    Blog.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(Blog.ImageData, 0, image.ContentLength);
                }
                blogsRepository.Save(Blog);
                return RedirectToAction("Manager");
            }
            else
            {
                return View(Blog);
            }
        }
        public ViewResult BlogCreate()
        {
            return View("BlogEdit", new Blog());
        }

        public FileContentResult GetImage(int BlogId)
        {
            Blog prod = blogsRepository.Get.FirstOrDefault(p => p.BlogId == BlogId);
            if (prod != null)
            {
                return File(prod.ImageData, prod.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
        public ViewResult PostCreate(int blogId)
        {
            return View("PostEdit", new Post() {  BlogId=blogId,Tags = new List<Tag>() });
        }
        [HttpGet]
        public ViewResult PostEdit(int postId)
        {
            Post Post = postsRepository.Get
            .FirstOrDefault(p => p.PostId == postId);

            return View(Post);
        }
        [HttpPost]
        public ActionResult PostEdit(Post postManager, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    postManager.ImageMimeType = image.ContentType;
                    postManager.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(postManager.ImageData, 0, image.ContentLength);
                }
                tagsRepository.Delete(postManager.PostId);
                postsRepository.Save(postManager);
                return RedirectToAction("PostManager",new { blogId=postManager.BlogId});
            }
            else
            {
                return View(postManager);
            }
        }
        [HttpPost]
        public ActionResult PostDelete(int PostId)
        {
            return RedirectToAction("PostManager");
        }
        public FileContentResult GetImagePost(int PostId)
        {
            Post post = postsRepository.Get.FirstOrDefault(p => p.PostId == PostId);
            if (post != null)
            {
                return File(post.ImageData, post.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}