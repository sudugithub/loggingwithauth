using Data.Domain;

namespace Data.Repositories.UserRepository
{
    public interface IUserRepository
    {
        Task<User> GetByEmail(string email);
    }
}
