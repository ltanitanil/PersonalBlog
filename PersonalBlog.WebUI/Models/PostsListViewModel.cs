using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PersonalBlog.Domain.Entities;

namespace PersonalBlog.WebUI.Models
{
    public class PostsListViewModel<T>
    {
        public IEnumerable<T> Collection { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentTag { get; set; }
    }
}