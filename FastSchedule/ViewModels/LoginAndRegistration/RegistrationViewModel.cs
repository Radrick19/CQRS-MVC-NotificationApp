using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FastSchedule.MVC.ViewModels.LoginAndRegistration
{
    public class RegistrationViewModel
    {
        [DisplayName("Логин")]
        [StringLength(64)]
        [RegularExpression("^[\\w.-]{0,19}[0-9a-zA-Z]$")]
        public string Login { get; set; }

        [DisplayName("Почта")]
        [StringLength(64)]
        [EmailAddress(ErrorMessage = "Неверно указан почтовый адрес")]
        public string Email { get; set; }

        [DisplayName("Пароль")]
        [StringLength(64)]
        public string Password { get; set; }

        [DisplayName("Повторите пароль")]
        [StringLength(64)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string PasswordConfirm { get; set; }
    }
}
