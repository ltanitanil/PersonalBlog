using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using PersonalBlog.Domain.Abstract;
using PersonalBlog.Domain.Concrete;
using PersonalBlog.Domain.Entities;
using PersonalBlog.WebUI.Infrastructure.Abstract;
using PersonalBlog.WebUI.Infrastructure.Concreat;

namespace PersonalBlog.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver()
        {
            kernel = new StandardKernel();
            AddBinding();
        }


        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        public void AddBinding()
        {
            kernel.Bind<IRepository<Post>>().To<EFPostRepository>();
            kernel.Bind<IRepository<Comment>>().To<EFCommentsRepository>();
            kernel.Bind<IRepository<UserProfile>>().To<EFUserProfileRepository>();
            kernel.Bind<IRepository<Blog>>().To<EFBlogRepository>();
            kernel.Bind<IRepository<User>>().To<EFUserRepository>();
            kernel.Bind<IRepository<Tag>>().To<EFTagRepository>();
            
            kernel.Bind<IAuthProvider>().To<FormsAuthProvider>();
            
        }

    }
}