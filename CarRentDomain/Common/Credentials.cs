using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace CarRent.Common
{
   public class Credentials
    {
        public Credentials(string email,string passwordHash)
        {
            Email = email;
            PasswordHash = passwordHash;
        }

        public static Credentials FromRawData(string email, string rawPassword)
        {
            using (var sha256=SHA256.Create())
            {
                var hashBytes=sha256.ComputeHash(Encoding.UTF8.GetBytes(rawPassword));
                var passwordHash = Encoding.UTF8.GetString(hashBytes);
                return new Credentials(email,passwordHash);
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Credentials credentials &&
                   Email == credentials.Email &&
                   PasswordHash == credentials.PasswordHash;
        }

        protected bool Equals(Credentials other)
        {
            return string.Equals(Email, other.Email) && string.Equals(PasswordHash, other.PasswordHash);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Email != null ? Email.GetHashCode() : 0) * 397) ^ (PasswordHash != null ? PasswordHash.GetHashCode() : 0);
            }
        }

        public string Email { get; }
        public string PasswordHash { get; }
        
        
    }
}
