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
    public class EFCommentsRepository : ICommentsRepository
    {
        EFDbContext Context = new EFDbContext(); 
        public IEnumerable<Comment> Comments
        {
            get {return Context.Comments.Include(p=>p.UserProfile); }
        }

        public void SaveComment(Comment comment)
        {
            if (comment.CommentId == 0)
            {
                comment.Date = DateTimeOffset.Now;
                Context.Comments.Add(comment);
            }
            Context.SaveChanges();
        }
        public Comment DeleteComment(int CommentId)
        {
            Comment dbEntry = Context.Comments.Find(CommentId);
            if (dbEntry != null)
            {
                Context.Comments.Remove(dbEntry);
                Context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
