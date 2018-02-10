using System;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Linq;


namespace TodoApi.Services
{
    public sealed class JwtTokenBuilder
    {

        private SecurityKey securityKey = null;
        private string issuer = "";
        private Dictionary<string, string> claims = new Dictionary<string, string>();


        public JwtTokenBuilder AddSecurityKey(SecurityKey securityKey)
        {
            this.securityKey = securityKey;
            return this;
        }


        public JwtTokenBuilder AddIssuer(string issuer)
        {
            this.issuer = issuer;
            return this;
        }

     
        public JwtTokenBuilder AddClaim(string type, string value)
        {
            this.claims.Add(type, value);
            return this;
        }


        public JwtToken Build()
        {
            EnsureArguments();

            var claims = new List<Claim>
            {
              new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }
            .Union(this.claims.Select(item => new Claim(item.Key, item.Value)));

            var token = new JwtSecurityToken(
                              issuer: issuer,
                              claims: claims,
                              signingCredentials: new SigningCredentials(
                                                securityKey,
                                                SecurityAlgorithms.HmacSha256));

            return new JwtToken(token);
        }

        #region " private "

        void EnsureArguments()
        {
            if (securityKey == null)
                throw new ArgumentNullException("Security Key");

            if (string.IsNullOrEmpty(this.issuer))
                throw new ArgumentNullException("Issuer");
        }

        #endregion
    }

}
