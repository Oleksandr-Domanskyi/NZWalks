using Microsoft.AspNetCore.Identity;

namespace NZwalks.API.Reposetories
{
    public interface ITokenRepositery
    {
       string CreatJWTToken(IdentityUser user, List<string> roles);
    }
}
