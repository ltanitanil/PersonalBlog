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
    public class EFBlogRepository : IRepository<Blog>
    {
        private EFDbContext Context;

        public EFBlogRepository()
        {
            Context = new EFDbContext();
        }

        public IEnumerable<Blog> Get
        {
            get { return Context.Blogs.Include(q => q.Posts).Include(q => q.User); }
        }

        public void Delete(int id)
        {
            Blog dbEntry = Context.Blogs.Find(id);
            if (dbEntry != null)
            {
                Context.Blogs.Remove(dbEntry);
                Context.SaveChanges();
            }
        }

        public void Save(Blog item)
        {
            if (item.BlogId == 0)
            {
                item.DateOfCreate = DateTimeOffset.Now;
                Context.Blogs.Add(item);
            }
            else
            {
                Blog dbEntry = Context.Blogs.Find(item.BlogId);
                if (dbEntry != null)
                {
                    dbEntry.Name = item.Name;
                    if (item.ImageData != null)
                    {
                        dbEntry.ImageData = item.ImageData;
                        dbEntry.ImageMimeType = item.ImageMimeType;
                    }
                    dbEntry.UserProfileId = item.UserProfileId;
                }
            }
            Context.SaveChanges();
        }
    }
}
