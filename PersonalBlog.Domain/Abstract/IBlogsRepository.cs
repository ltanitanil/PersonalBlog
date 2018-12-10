using PersonalBlog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBlog.Domain.Abstract
{
    public interface IBlogsRepository
    {
        IEnumerable<Blog> Blogs { get; }
        void SaveBlog(Blog Blog);
        Blog DeleteBlog(int BlogId);
    }
}
