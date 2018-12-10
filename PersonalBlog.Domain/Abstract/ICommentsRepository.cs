using PersonalBlog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBlog.Domain.Abstract
{
    public interface ICommentsRepository
    {
        IEnumerable<Comment> Comments { get; }
        void SaveComment(Comment comment);
        Comment DeleteComment(int CommentId);
    }
}
