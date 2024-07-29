using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accapt.WpfServies
{
    public class JwtHelper
    {
        public static string GetUsernameFromToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                var jwtToken = tokenHandler.ReadJwtToken(token);

                var usernameClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "userName")?.Value;

                return usernameClaim;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error extracting username from token: {ex.Message}");
                return null;
            }
        }
    }
}
