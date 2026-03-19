using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Restaurant_Reservation_System.Dal.Repositories
{
    public interface IPasswordHasher
    {
        Task<string> HashPassword(string password);
        Task<bool> VerifyPassword(string password, string hashedPassword);
    }

    public class PasswordHasher : IPasswordHasher
    {
        public async Task<string> HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException("Password Can't be Null or Empty");

            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        public async Task<bool> VerifyPassword(string password, string hashedPassword)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException("Password Can't be Null or Empty");
            if (string.IsNullOrWhiteSpace(hashedPassword))
                throw new ArgumentNullException("Password Can't be Null or Empty");

            var hashedInputPassword = await HashPassword(password);
            return hashedInputPassword == hashedPassword;
        }
    }
}
