using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalBlog.Domain.Abstract;
using PersonalBlog.Domain.Entities;


namespace PersonalBlog.Domain.Concrete
{
    public class EFPostRepository : IPostsRepository
    {
        private EFDbContext Context = new EFDbContext();

        public IEnumerable<Post> Posts
        {
            get
            {
                var a = Context.Posts.Include(j => j.Blog).Include(l => l.Tags);
                return a;
            }
        }

        public Post DeletePost(int PostId)
        {
            Post dbEntry = Context.Posts.Find(PostId);
            if (dbEntry != null)
            {
                Context.Posts.Remove(dbEntry);
                Context.SaveChanges();
            }
            return dbEntry;
        }

        public void SavePost(Post Post)
        {
            List<Tag> tags = new List<Tag>();
            if (Post.Tags != null)
            {
                foreach (var a in Post.Tags)
                {
                    if (a.Name != null)
                    {
                        tags.Add(a);
                    }
                }
            }
            Post.Tags = tags;
            if (Post.PostId == 0)
            {
                Post.DateOfCreate = DateTimeOffset.Now;
                Context.Posts.Add(Post);
            }
            else
            {
                Post dbEntry = Context.Posts.Find(Post.PostId);
                if (dbEntry != null)
                {
                    dbEntry.Title = Post.Title;
                    if (Post.ImageData != null)
                    {
                        dbEntry.ImageData = Post.ImageData;
                        dbEntry.ImageMimeType = Post.ImageMimeType;
                    }
                    dbEntry.Description = Post.Description;
                    dbEntry.BlogId = Post.BlogId;
                    dbEntry.Tags = Post.Tags;
                    dbEntry.Text = Post.Text;
                }
            }
            Context.SaveChanges();
        }
    }
}
