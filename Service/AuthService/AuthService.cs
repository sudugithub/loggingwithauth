using JWT.Algorithms;
using JWT.Builder;
using Microsoft.Extensions.Configuration;
using Service.Contract;
using Service.UserService;
using Service.Utils;

namespace Service.AuthService
{
    public class AuthService(IUserService userService, IConfiguration configuration) : IAuthService
    {
        private readonly IUserService _userService = userService;
        private readonly string _JWTSecret = configuration["JWTSecret"]!;

        public async Task<string> Login(LoginContract loginContract)
        {
            var user = await _userService.GetByEmail(loginContract.Email);

            var result = PasswordHasher.Check(user.Password, loginContract.Password);

            if (!result)
            {
                throw new Exception("Password does not match.");
            }

            var param = new JWTUserParam
            {
                Id = user.Id,
                Email = loginContract.Email,
                Iat = DateTime.UtcNow,
            };
            return GenerateToken(param, 30);
        }

        private string GenerateToken(JWTUserParam param, int expiryInMinutes)
        {
            var token = JwtBuilder.Create()
                        .WithAlgorithm(new HMACSHA256Algorithm())
                        .WithSecret(_JWTSecret)
                        .AddClaim("exp", DateTimeOffset.UtcNow.AddMinutes(expiryInMinutes).ToUnixTimeSeconds())
                        .AddClaim("id", param.Id)
                        .AddClaim("data", param)
                        .AddClaim("issuedAt", new DateTimeOffset(param.Iat).ToUnixTimeSeconds())
                        .Encode();

            return token;
        }
    }
}
