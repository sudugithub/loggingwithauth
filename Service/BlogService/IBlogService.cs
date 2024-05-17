using Data.Domain;
using Service.Contract;

namespace Service.BlogService
{
    public interface IBlogService
    {
        Task<long> Create(BlogCreateContract blog);
        Task<Blog> GetById(long id);
        Task<BlogPaginationResponse> GetAll(int pageSize, int currentPage);
        Task<bool> Update(long id, BlogCreateContract blog);
        Task<bool> Delete(long id);
    }
}
