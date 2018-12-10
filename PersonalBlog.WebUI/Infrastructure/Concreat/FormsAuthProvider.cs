using PersonalBlog.Domain.Abstract;
using PersonalBlog.Domain.Entities;
using PersonalBlog.WebUI.Infrastructure.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace PersonalBlog.WebUI.Infrastructure.Concreat
{

    public class FormsAuthProvider : IAuthProvider
    {
        IUsersRepository usersRepository;
        public FormsAuthProvider(IUsersRepository repository)
        {
            usersRepository = repository;
        }
        public bool Authenticate(string login, string password)
        {
            User user = usersRepository.Users.FirstOrDefault(p =>p.Name==login&&p.Password==password);
            
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(login, true);
                return true;
            }
            return false;
        }

        public void LogOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}