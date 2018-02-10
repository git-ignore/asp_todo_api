using System;
using System.IdentityModel.Tokens.Jwt;

namespace TodoApi.Services
{
    public sealed class JwtToken
    {
        JwtSecurityToken token;

        internal JwtToken(JwtSecurityToken token)
        {
            this.token = token;
        }

        public DateTime ValidTo => token.ValidTo;
        public string Value => new JwtSecurityTokenHandler().WriteToken(token);
    }
}
