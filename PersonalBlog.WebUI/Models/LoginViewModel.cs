using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PersonalBlog.WebUI.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Введите логин")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
    public class RegisterModel
    {
        [Required(ErrorMessage ="Введите имя")]
        [StringLength(20, ErrorMessage = "Длина имени должна быть от {2} до {1} символов", MinimumLength = 5)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [StringLength(20, ErrorMessage = "Длина пароля должна быть от {2} до {1} символов", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Повторите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        public string Gender { get; set; }

        [Required(ErrorMessage = "Укажите возраст")]
        [Range(1, 100, ErrorMessage = "Недопустимый возраст")]
        public int Age { get; set; }
        public string Country { get; set; }
    }
}