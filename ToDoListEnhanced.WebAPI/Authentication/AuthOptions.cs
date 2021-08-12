using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ToDoListEnhanced.WebAPI.Authentication
{
    public class AuthOptions
    {
        public const string ISSUER = "ToDoListApiServer";
        public const string AUDIENCE = "ToDoListClient";
        const string KEY = "todolistcrypt_secretkey!791";
        public const int LIFETIME = 60;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
