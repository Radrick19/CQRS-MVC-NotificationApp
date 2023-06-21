using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace FastSchedule.MVC.ViewModels.LoginAndRegistration
{
    public class LoginViewModel
    {
        [DisplayName("Почта/Логин")]
        [StringLength(64)]
        public string EmailOrLogin { get; set; }

        [DisplayName("Пароль")]
        [StringLength(64)]
        public string Password { get; set; }
    }
}
