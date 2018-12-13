using PersonalBlog.Domain.Abstract;
using PersonalBlog.Domain.Entities;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBlog.Domain.Concrete
{
    public class EFCommentsRepository : IRepository<Comment>
    {
        private EFDbContext Context;

        public EFCommentsRepository()
        {
            Context = new EFDbContext();
        }

        public IEnumerable<Comment> Get
        {
            get { return Context.Comments.Include(p => p.UserProfile); }
        }

        public void Delete(int id)
        {
            Comment dbEntry = Context.Comments.Find(id);
            if (dbEntry != null)
            {
                Context.Comments.Remove(dbEntry);
                Context.SaveChanges();
            }
        }

        public void Save(Comment item)
        {
            if (item.CommentId == 0)
            {
                item.Date = DateTimeOffset.Now;
                Context.Comments.Add(item);
            }
            Context.SaveChanges();
        }
    }
}
