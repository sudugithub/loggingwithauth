using Data.Domain;
using Data.Repositories.BlogRepository;
using Data.Utils.UserContext;
using Service.Contract;
using Service.Exceptions;

namespace Service.BlogService
{
    public class BlogService(IBlogRepository blogRepository, IUserContext userContext) : IBlogService
    {
        private readonly IUserContext _userContext = userContext;
        private readonly IBlogRepository _blogRepository = blogRepository;

        public async Task<long> Create(BlogCreateContract contract)
        {
            var blog = new Blog
            {
                Category = contract.Category,
                Content = contract.Content,
                Title = contract.Title,
                CreatedById = long.Parse(_userContext.Id),
                CreatedTime = DateTime.Now,
            };
            return await _blogRepository.Create(blog);
        }

        public async Task<bool> Delete(long id)
        {
            var blog = await GetById(id);

            return await _blogRepository.Delete(blog);
        }

        public async Task<BlogPaginationResponse> GetAll(int pageSize, int currentPage)
        {
            var (data, count, pages) = await _blogRepository.GetAll(pageSize, currentPage);

            return new BlogPaginationResponse { Blogs = data, PageCount = pages, TotalCount = count };
        }

        public async Task<Blog> GetById(long id)
        {
            ArgumentNullException.ThrowIfNull(nameof(id));
            return await _blogRepository.GetById(id) ?? throw new NotFoundException("Blog not found");
        }

        public async Task<bool> Update(long id, BlogCreateContract contract)
        {
            await GetById(id);
            var blog = new Blog
            {
                Id = id,
                Category = contract.Category,
                Content = contract.Content,
                Title = contract.Title,
                UpdatedById = long.Parse(_userContext.Id),
                UpdatedTime = DateTime.UtcNow,
            };

            return await _blogRepository.Update(blog);
        }
    }
}
