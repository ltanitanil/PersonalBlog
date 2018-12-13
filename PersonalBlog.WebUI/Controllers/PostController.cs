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
    public class PostController : Controller
    {
        private IRepository<Post> postsRepository;
        private IRepository<Comment> commentsRepository;
        private IRepository<UserProfile> usersProfileRepository;

        public int PageSize = 5;

        public PostController(IRepository<Post> repPosts, IRepository<Comment> repComment, IRepository<UserProfile> repUsersProfile)
        {
            postsRepository = repPosts;
            commentsRepository = repComment;
            usersProfileRepository = repUsersProfile;
        }
        // GET: Post
        public ViewResult List(string tag, string wordsearch, string filter, int page = 1)
        {
            string name = null;
            if (filter == "по заголовкам постов") name = wordsearch;
            if (filter == "по тегам") tag = wordsearch;
            PostsListViewModel<Post> model = new PostsListViewModel<Post>()
            {
                Collection = postsRepository.Get.OrderBy(p => p.PostId)
                                      .Where(p => (tag == null || p.Tags.Where(l => l.Name == tag).Count() == 1)
                                      && (name == null || (p.Title.Contains(name) || p.Description.Contains(name))))
                                      .Skip((page - 1) * PageSize)
                                      .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,

                    TotalItems = (tag == null && name == null) ? postsRepository.Get.Count() : postsRepository.Get
                    .Where(p => (tag == null || p.Tags.Where(l => l.Name == tag).Count() == 1) && (name == null || (p.Title.Contains(name) || p.Description.Contains(name)))).Count()
                },
                CurrentTag = tag

            };
            return View(model);
        }

        public ActionResult FullPost(int postId)
        {

            FullPost fullPost = new FullPost()
            {
                Post = postsRepository.Get.FirstOrDefault(p => p.PostId == postId),
                Comment = commentsRepository.Get.Where(p => p.PostId == postId),
            };
            fullPost.Post.Text=fullPost.Post.Text.Replace("\r\n", "<br>");
            ViewBag.CountComments = fullPost.Comment.Count();
            return View(fullPost);
        }
        
        [Authorize]
        [HttpPost]
        public ActionResult AddComment(Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.UserProfilesId = usersProfileRepository.Get.FirstOrDefault(p => p.Name == User.Identity.Name).UserProfileId;
                commentsRepository.Save(comment);
                return RedirectToAction("FullPost", new { comment.PostId});
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult DeleteComment(int commentId)
        {
            return RedirectToAction("FullPost");
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