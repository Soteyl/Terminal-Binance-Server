using System.ComponentModel.DataAnnotations;

namespace CryptoTerminal.Models.Auth
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Не указан логин или пароль")]
        public string UserNameOrEmail { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
