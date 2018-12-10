using PersonalBlog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBlog.Domain.Abstract
{
    public interface IPostsRepository
    {
        IEnumerable<Post> Posts { get;}
        void SavePost(Post Post);
        Post DeletePost(int PostId);
    }
}
