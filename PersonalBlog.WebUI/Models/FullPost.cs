using PersonalBlog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalBlog.WebUI.Models
{
    public class FullPost
    {
        public Post Post { get; set; }
        public IEnumerable<Comment> Comment { get; set; }
    }
}