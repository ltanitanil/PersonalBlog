using PersonalBlog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBlog.Domain.Abstract
{
    public interface ITagsRepository
    {
        IEnumerable<Tag> Tags { get;}
        void DeleteTag(int PostId);
    }
}
