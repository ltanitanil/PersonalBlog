using PersonalBlog.Domain.Abstract;
using PersonalBlog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBlog.Domain.Concrete
{
    public class EFUserRepository : IRepository<User>
    {
        private EFDbContext Context;

        public EFUserRepository()
        {
            Context = new EFDbContext();
        }

        public IEnumerable<User> Get
        {
            get { return Context.Users; }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Save(User item)
        {
            if (item.UserId == 0)
            {
                Context.Users.Add(item);
            }
            else
            {
                User dbEntry = Context.Users.Find(item.UserId);
                if (dbEntry != null)
                {
                    dbEntry.RoleId = 1;
                    dbEntry.Name = item.Name;
                    dbEntry.Password = item.Password;

                }
            }
            Context.SaveChanges();
        }
    }
}
