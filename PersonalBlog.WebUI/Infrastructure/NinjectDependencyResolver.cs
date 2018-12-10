﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using PersonalBlog.Domain.Abstract;
using PersonalBlog.Domain.Concrete;
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
            kernel.Bind<IPostsRepository>().To<EFPostRepository>();
            kernel.Bind<IBlogsRepository>().To<EFBlogRepository>();
            kernel.Bind<ITagsRepository>().To<EFTagRepository>();
            kernel.Bind<IUsersRepository>().To<EFUserRepository>();
            kernel.Bind<ICommentsRepository>().To<EFCommentsRepository>();
            kernel.Bind<IAuthProvider>().To<FormsAuthProvider>();
            kernel.Bind<IUsersProfileRepository>().To<EFUserProfileRepository>();
        }

    }
}