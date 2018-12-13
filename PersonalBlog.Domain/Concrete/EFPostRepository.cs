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
    public class EFPostRepository : IRepository<Post>
    {
        private EFDbContext Context;

        public EFPostRepository()
        {
            Context = new EFDbContext();
        }

        public IEnumerable<Post> Get
        {
            get
            {
                var a = Context.Posts.Include(j => j.Blog).Include(l => l.Tags);
                return a;
            }
        }

        public void Delete(int id)
        {
            Post dbEntry = Context.Posts.Find(id);
            if (dbEntry != null)
            {
                Context.Posts.Remove(dbEntry);
                Context.SaveChanges();
            }
        }

        public void Save(Post item)
        {
            List<Tag> tags = new List<Tag>();
            if (item.Tags != null)
            {
                foreach (var a in item.Tags)
                {
                    if (a.Name != null)
                    {
                        tags.Add(a);
                    }
                }
            }
            item.Tags = tags;
            if (item.PostId == 0)
            {
                item.DateOfCreate = DateTimeOffset.Now;
                Context.Posts.Add(item);
            }
            else
            {
                Post dbEntry = Context.Posts.Find(item.PostId);
                if (dbEntry != null)
                {
                    dbEntry.Title = item.Title;
                    if (item.ImageData != null)
                    {
                        dbEntry.ImageData = item.ImageData;
                        dbEntry.ImageMimeType = item.ImageMimeType;
                    }
                    dbEntry.Description = item.Description;
                    dbEntry.BlogId = item.BlogId;
                    dbEntry.Tags = item.Tags;
                    dbEntry.Text = item.Text;
                }
            }
            Context.SaveChanges();
        }
    }
}
