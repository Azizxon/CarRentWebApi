using System;
using System.Text;
using CarRent.Common;
using Microsoft.IdentityModel.Tokens;

namespace CarRentWebAPI.Security
{
    public class SecuritySettings
    {
        public SecuritySettings(string issue,
            TimeSpan expirationPeriod,
            Credentials adminCredentials,
            string encryptionKey)
        {
            Issue = issue;
            ExpirationPeriod = expirationPeriod;
            AdminCredentials = adminCredentials;
            EncryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(encryptionKey));
        }

        public string Issue { get; }
        public TimeSpan ExpirationPeriod { get; }
        public Credentials AdminCredentials { get; }

        public SymmetricSecurityKey EncryptionKey { get; }
    }
}
