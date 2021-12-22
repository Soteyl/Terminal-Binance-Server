using System.ComponentModel.DataAnnotations;

namespace CryptoTerminal.Models.Auth
{
    using Models.Database;

    public class RegisterModel
    {
        [Required(ErrorMessage = "Не указана почта.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан логин.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Не указан пароль.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
