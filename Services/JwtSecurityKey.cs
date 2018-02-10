using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace TodoApi.Services
{
    public static class JwtSecurityKey
    {
        public static SymmetricSecurityKey Create(string secret)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        }
    }
}
