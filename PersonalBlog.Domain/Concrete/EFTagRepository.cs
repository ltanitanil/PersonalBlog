using PersonalBlog.Domain.Abstract;
using PersonalBlog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBlog.Domain.Concrete
{
    public class EFTagRepository : IRepository<Tag>
    {
        private EFDbContext Context;

        public EFTagRepository()
        {
            Context = new EFDbContext();
        }

        public IEnumerable<Tag> Get
        {
            get { return Context.Tags; }
        }

        public void Delete(int id)
        {
            List<Tag> tags = Context.Tags.Where(p => p.PostId == id).ToList();
            foreach (var a in tags)
            {
                Context.Tags.Remove(a);
            }
            Context.SaveChanges();     
        }


        public void Save(Tag item)
        {
            throw new NotImplementedException();
        }
    }
}
