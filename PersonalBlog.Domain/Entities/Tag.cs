using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBlog.Domain.Entities
{
    public class Tag : IAggregateRoot
    {
        public int TagId { get; set; }
        public string Name { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
