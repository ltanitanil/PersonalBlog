using PersonalBlog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBlog.Domain.Abstract
{
    public interface IUsersRepository
    {
        IEnumerable<User> Users { get;}
        void SaveUsers(User user);
    }
}
