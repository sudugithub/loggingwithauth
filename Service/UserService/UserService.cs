using Data.Domain;
using Data.Repositories.UserRepository;
using Service.Exceptions;

namespace Service.UserService
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<User> GetByEmail(string email)
        {
            var user = await _userRepository.GetByEmail(email) ?? throw new NotFoundException("User not found");

            return user;
        }
    }
}
