using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace LinkHolder {
    public class AuthOptions {
        public const string ISSUER = "MyAuthServer";
        public const string AUDIENCE = "http://localhost:5000/";
        const string KEY = "F3Ffdsvs_33&fff$fdFFv+44fdfV__vvfdseEFfxVsKug";
        public const int LIFETIME = 10;
        public static SymmetricSecurityKey GetSymmetricSecurityKey() {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}