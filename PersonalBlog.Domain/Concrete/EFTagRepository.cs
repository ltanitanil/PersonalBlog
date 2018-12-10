using PersonalBlog.Domain.Abstract;
using PersonalBlog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBlog.Domain.Concrete
{
    public class EFTagRepository : ITagsRepository
    {
        EFDbContext Context = new EFDbContext();
        public IEnumerable<Tag> Tags
        {
            get { return Context.Tags; }
        }

        public void DeleteTag(int PostId)
        {
            List<Tag> tags = Context.Tags.Where(p => p.PostId == PostId).ToList();
            foreach (var a in tags)
            {
                Context.Tags.Remove(a);
            }
            Context.SaveChanges();
        }
    }
}
