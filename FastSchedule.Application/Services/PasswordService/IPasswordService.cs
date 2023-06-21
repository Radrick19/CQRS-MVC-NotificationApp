using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastSchedule.Application.Services.PasswordService
{

    public interface IPasswordService
    {
        string GetHash(string password, string salt);
        string GenerateSalt();
    }
}
