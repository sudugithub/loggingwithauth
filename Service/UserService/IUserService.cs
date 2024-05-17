using Data.Domain;

namespace Service.UserService
{
    public interface IUserService
    {
        Task<User> GetByEmail(string email);
    }
}
