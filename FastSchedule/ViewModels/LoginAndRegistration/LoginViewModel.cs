using System.ComponentModel;
using System.Runtime.InteropServices;

namespace FastSchedule.MVC.ViewModels.LoginAndRegistration
{
    public class LoginViewModel
    {
        [DisplayName("Почта/Логин")]
        public string EmailOrLogin { get; set; }

        [DisplayName("Пароль")]
        public string Password { get; set; }

        public LoginViewModel(string emailOrLogin, string password)
        {
            EmailOrLogin = emailOrLogin;
            Password = password;
        }
    }
}
