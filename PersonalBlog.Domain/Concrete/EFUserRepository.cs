using PersonalBlog.Domain.Abstract;
using PersonalBlog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBlog.Domain.Concrete
{
    public class EFUserRepository:IUsersRepository
    {
        EFDbContext Context = new EFDbContext();

        public IEnumerable<User> Users
        {
            get { return Context.Users; }
        }
        public void SaveUsers(User user)
        {
            if (user.UserId == 0)
            {
                Context.Users.Add(user);
            }
            else
            {
                User dbEntry = Context.Users.Find(user.UserId);
                if (dbEntry != null)
                {
                    dbEntry.RoleId = 1;
                    dbEntry.Name = user.Name;
                    dbEntry.Password = user.Password;

                }
            }
            Context.SaveChanges();
        }

    }
}
