using PersonalBlog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalBlog.WebUI.Models
{
    public class BlogManager
    {
        public IEnumerable<Blog> Blogs { get; set; }
        public int BlogCount { get; set; }
    }
}