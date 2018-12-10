using PersonalBlog.Domain.Abstract;
using PersonalBlog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBlog.Domain.Concrete
{
    public class EFUserProfileRepository : IUsersProfileRepository
    {
        EFDbContext Context = new EFDbContext();
        public IEnumerable<UserProfile> UsersProfile
        {
            get { return Context.UserProfiles; }
        }



        public void SaveUsersProfile(UserProfile user)
        {
            if (user.UserProfileId == 0)
            {
                Context.UserProfiles.Add(user);
            }
            else
            {
                UserProfile dbEntry = Context.UserProfiles.Find(user.UserProfileId);
                if (dbEntry != null)
                {
                    dbEntry.Name = user.Name;
                    dbEntry.Gender = user.Gender;
                    dbEntry.Interests = user.Interests;
                    dbEntry.Age = user.Age;
                    dbEntry.Country = user.Country;
                }
            }
            Context.SaveChanges();
        }
        
    }
}
