using Azure.Core;
using SHA3.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FastSchedule.Application.Services.PasswordService
{
    public class PasswordService : IPasswordService
    {
        // Хэширует пароль с солью
        public string GetHash(string password, string salt)
        {
            string hash;
            using (var shaAlg = Sha3.Sha3256())
            {
                string inputPassHash = Convert.ToBase64String(shaAlg.ComputeHash(Encoding.UTF8.GetBytes(password)));
                hash = Convert.ToBase64String(shaAlg.ComputeHash(Encoding.UTF8.GetBytes(inputPassHash + salt)));
            }
            return hash;
        }

        // Генерирует соль из текущего даты и времени
        public string GenerateSalt()
        {
            return DateTime.Now.GetHashCode().ToString();
        }
    }
}
