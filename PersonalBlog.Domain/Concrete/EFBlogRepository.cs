using PersonalBlog.Domain.Abstract;
using PersonalBlog.Domain.Entities;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBlog.Domain.Concrete
{
    public class EFBlogRepository : IBlogsRepository
    {
        EFDbContext Context = new EFDbContext();
        public IEnumerable<Blog> Blogs
        {
            get { return Context.Blogs.Include(q => q.Posts).Include(q => q.User); }
        }

        public Blog DeleteBlog(int BlogId)
        {
            Blog dbEntry = Context.Blogs.Find(BlogId);
            if (dbEntry != null)
            {
                Context.Blogs.Remove(dbEntry);
                Context.SaveChanges();
            }
            return dbEntry;
        }

        public void SaveBlog(Blog Blog)
        {
            if (Blog.BlogId == 0)
            {
                Blog.DateOfCreate = DateTimeOffset.Now;
                Context.Blogs.Add(Blog);
            }
            else
            {
                Blog dbEntry = Context.Blogs.Find(Blog.BlogId);
                if (dbEntry != null)
                {
                    dbEntry.Name = Blog.Name;
                    if(Blog.ImageData!=null)
                    {
                    dbEntry.ImageData = Blog.ImageData;
                    dbEntry.ImageMimeType = Blog.ImageMimeType;
                    }
                    dbEntry.UserProfileId = Blog.UserProfileId;
                }
            }
            Context.SaveChanges();
        }
    }
}
