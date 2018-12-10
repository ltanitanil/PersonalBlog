using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBlog.Domain.Entities
{
    public class Blog
    {
        public int BlogId { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        [StringLength(50, ErrorMessage = "Длина имени блога должна составлять от {2} до {1} символов", MinimumLength = 1)]
        public string Name { get; set; }
        public DateTimeOffset DateOfCreate { get; set; }
        public byte[] ImageData { get; set; }

        public string ImageMimeType { get; set; }



        public int UserProfileId { get; set; }
        public UserProfile User { get; set; }

        public List<Post> Posts { get; set; }
    }
}
