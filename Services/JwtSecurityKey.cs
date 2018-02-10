using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace TodoApi.Services
{
    public static class JwtSecurityKey
    {
        public static SymmetricSecurityKey Create()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Superpupermegasecretkey909120313"));
        }
    }
}
