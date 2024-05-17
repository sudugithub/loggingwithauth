using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Data.Utils.UserContext
{
    public class UserContext(IHttpContextAccessor contextAccessor) : IUserContext
    {
        public string Id { get; } = GetClaimValue<string>("id", contextAccessor.HttpContext);

        private static T? GetClaimValue<T>(string claimName, HttpContext? context)
        {
            var userClaims = context?.User.Claims.ToArray<Claim>();

            if (userClaims == null || userClaims.Length == 0)
                return default;

            var value = userClaims.FirstOrDefault(x => x.Type == claimName)?.Value;

            return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}
