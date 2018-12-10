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
    public class BlogManagerController : Controller
    {
        private IBlogsRepository blogsRepository;
        private IPostsRepository postsRepository;
        private IUsersProfileRepository usersProfileRepository;
        private ITagsRepository tagsRepository;

        public BlogManagerController(IBlogsRepository blogRep, IUsersProfileRepository usersProfileRep, IPostsRepository postRep, ITagsRepository tagRep)
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
                Blogs = blogsRepository.Blogs.Where(p => p.User.Name == User.Identity.Name),
                BlogCount = blogsRepository.Blogs.Where(p => p.User.Name == User.Identity.Name).Count()
            };
            return View(Blogs);
        }
        public ActionResult PostManager(int blogId)
        {
            ViewBag.BlogId = blogId;
            return View(postsRepository.Posts.Where(p => p.BlogId == blogId));
        }

        [HttpPost]
        public ActionResult BlogDelete(int BlogId)
        {
            Blog deletedBlog = blogsRepository.DeleteBlog(BlogId);
            return RedirectToAction("Manager");
        }
        [HttpGet]
        public ViewResult BlogEdit(int BlogId)
        {
            Blog blog = blogsRepository.Blogs
              .FirstOrDefault(p => p.BlogId == BlogId);
            if (blog.User.Name == User.Identity.Name) { return View(blog); }
            return View();
        }
        [HttpPost]
        public ActionResult BlogEdit(Blog Blog, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                Blog.UserProfileId = usersProfileRepository.UsersProfile.FirstOrDefault(p => p.Name == User.Identity.Name).UserProfileId;
                if (image != null)
                {
                    Blog.ImageMimeType = image.ContentType;
                    Blog.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(Blog.ImageData, 0, image.ContentLength);
                }
                blogsRepository.SaveBlog(Blog);
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
            Blog prod = blogsRepository.Blogs.FirstOrDefault(p => p.BlogId == BlogId);
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
            Post Post = postsRepository.Posts
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
                tagsRepository.DeleteTag(postManager.PostId);
                postsRepository.SavePost(postManager);
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
            Post deletedPost = postsRepository.DeletePost(PostId);
            return RedirectToAction("PostManager", new { blogId = deletedPost.BlogId });
        }
        public FileContentResult GetImagePost(int PostId)
        {
            Post post = postsRepository.Posts.FirstOrDefault(p => p.PostId == PostId);
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