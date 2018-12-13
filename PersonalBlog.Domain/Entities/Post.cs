
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBlog.Domain.Entities
{
    public class Post: IAggregateRoot
    {
        public int PostId { get; set; }
        [Required(ErrorMessage = "Введите название")]
        [StringLength(150, ErrorMessage = "Длина названия поста должна составлять от {2} до {1} символов", MinimumLength = 1)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Введите описание")]
        [StringLength(300, ErrorMessage = "Длина может составлять не более {1} символов", MinimumLength = 1)]
        public string Description { get; set; }
        [Required(ErrorMessage = "Введите текст")]
        [StringLength(3000, ErrorMessage = "Длина может составлять не более {1} символов")]
        public string Text{ get; set; }
        public DateTimeOffset DateOfCreate { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }

        public List<Tag> Tags { get; set; }
    }  
}
