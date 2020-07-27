using System;
using System.Threading.Tasks;
// using FireSharp.Config;
// using FireSharp.Interfaces;
using Shared.Models;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Shared.Services
{
    public interface ICrypto
    {
        public string HashPassword(string password);
        public bool ArePasswordsEqual(string storedPassword, string modelPassword);
    }

    public class Crypto : ICrypto
    {
        public string HashPassword(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            var hash = pbkdf2.GetBytes(20);
            var hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            return Convert.ToBase64String(hashBytes);
        }

        public bool ArePasswordsEqual(string storedPassword, string modelPassword)
        {
            var hashBytes = Convert.FromBase64String(storedPassword);
            var salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            var pbkdf2 = new Rfc2898DeriveBytes(modelPassword, salt, 100000);
            var hash = pbkdf2.GetBytes(20);
            for (var i = 0; i < 20; i++)
                if (hashBytes[i + 16] != hash[i])
                    return false;
            return true;
        }
    }
}