using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBlog.Domain.Entities
{
    public class Comment: IAggregateRoot
    {
        public int CommentId { get; set; }
        [Required(ErrorMessage = "Пустой комментарий")]
        [StringLength(250, ErrorMessage = "Комментарий не может быть более 250 символов", MinimumLength = 0)]
        public string Text { get; set; }
        public DateTimeOffset Date{ get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }

        [ForeignKey("UserProfile")]
        public int? UserProfilesId { get; set; }
        public UserProfile UserProfile { get; set; }
    }
}
