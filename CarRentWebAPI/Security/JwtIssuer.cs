using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace CarRentWebAPI.Security
{
    public class JwtIssuer : IJwIssuer
    {

        public JwtIssuer(SecuritySettings securitySettings)
        {
            _securitySettings = securitySettings;
        }

        public string IssueJwt(string role, int? id)
        {
            var claims = new[]
            {
                new Claim(Claims.Roles.RoleClaim, role),
                new Claim(Claims.IClaim, id?.ToString()??String.Empty)
            };
            var credentials = new SigningCredentials(
                _securitySettings.EncryptionKey,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(issuer: _securitySettings.Issue,
                claims: claims,
                expires: DateTime.Now.Add(_securitySettings.ExpirationPeriod),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private readonly SecuritySettings _securitySettings;

    }
}
