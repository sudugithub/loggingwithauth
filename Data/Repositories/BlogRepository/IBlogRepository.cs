using Data.Domain;

namespace Data.Repositories.BlogRepository
{
    public interface IBlogRepository
    {
        Task<long> Create(Blog blog);
        Task<Blog> GetById(long id);
        Task<(List<Blog>, int, int)> GetAll(int pageSize, int currentPage);
        Task<bool> Update(Blog blog);
        Task<bool> Delete(Blog blog);
    }
}
