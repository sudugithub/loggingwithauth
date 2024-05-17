using Service.Contract;

namespace Service.AuthService
{
    public interface IAuthService
    {
        Task<string> Login(LoginContract loginContract);
    }
}
