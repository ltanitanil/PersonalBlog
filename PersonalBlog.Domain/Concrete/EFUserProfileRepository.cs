using PersonalBlog.Domain.Abstract;
using PersonalBlog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBlog.Domain.Concrete
{
    public class EFUserProfileRepository : IRepository<UserProfile>
    {
        private EFDbContext Context;

        public EFUserProfileRepository()
        {
            Context = new EFDbContext();
        }

        public IEnumerable<UserProfile> Get
        {
            get
            {
                return Context.UserProfiles;
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Save(UserProfile item)
        {
            if (item.UserProfileId == 0)
            {
                Context.UserProfiles.Add(item);
            }
            else
            {
                UserProfile dbEntry = Context.UserProfiles.Find(item.UserProfileId);
                if (dbEntry != null)
                {
                    dbEntry.Name = item.Name;
                    dbEntry.Gender = item.Gender;
                    dbEntry.Interests = item.Interests;
                    dbEntry.Age = item.Age;
                    dbEntry.Country = item.Country;
                }
            }
            Context.SaveChanges();
        }
    }
}
