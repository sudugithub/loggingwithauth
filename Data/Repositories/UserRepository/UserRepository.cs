using Data.Domain;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.UserRepository
{
    public class UserRepository(Repository repository) : IUserRepository
    {
        private readonly Repository _repository = repository;

        public async Task<User> GetByEmail(string email)
        {
            return await _repository.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
        }
    }
}
