using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ToDo.WebAPI.Authentication;

public class AuthOptions
{
    private const string KEY = "6B3A1DDE-0249-49D1-A55B-740866DD1E14";

    internal const string ISSUER = "MyAuthServer";
    internal const string AUDIENCE = "MyAuthClient";

    internal static SymmetricSecurityKey GetSymmetricSecurityKey() =>
        new(Encoding.UTF8.GetBytes(KEY));
}