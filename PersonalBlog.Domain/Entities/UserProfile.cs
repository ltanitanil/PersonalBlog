using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBlog.Domain.Entities
{
    public class UserProfile
    {
        [Key]
        [Column("UserProfileId")]

        public int UserProfileId { get; set; }

        public string Name { get; set; }
        public string Country { get; set; }
        public int Age { get; set; }
        public string Interests { get; set; }
        public string AboutUser { get; set; }
        public string Gender { get; set; }
        public byte[] ImageData { get; set; }

        public string ImageMimeType { get; set; }


        public List<Blog> Blogs { get; set; }

        public List<Comment> Comments { get; set; }
    }
}
