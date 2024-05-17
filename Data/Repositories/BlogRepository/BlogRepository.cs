using Data.Domain;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.BlogRepository
{
    public class BlogRepository(Repository repository) : IBlogRepository
    {
        private readonly Repository _repository = repository;

        public async Task<long> Create(Blog blog)
        {
            await _repository.Blogs.AddAsync(blog);
            await _repository.SaveChangesAsync();

            return blog.Id;
        }

        public async Task<bool> Delete(Blog blog)
        {
            _repository.Blogs.Remove(blog);
            var affectedRow = await _repository.SaveChangesAsync();

            return affectedRow > 0;
        }

        public async Task<(List<Blog>, int, int)> GetAll(int pageSize, int currentPage)
        {
            var skip = (currentPage - 1) * pageSize;
            var totalCount = await _repository.Blogs.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            var data = await _repository.Blogs.Skip(skip).Take(pageSize).ToListAsync();

            return (data, totalCount, totalPages);
        }

        public async Task<Blog> GetById(long id)
        {
            return await _repository.Blogs.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> Update(Blog blog)
        {
            var existingBlog = await GetById(blog.Id);

            if (existingBlog != null)
            {
                existingBlog.Category = blog.Category;
                existingBlog.Title = blog.Title;
                existingBlog.Content = blog.Content;
                existingBlog.UpdatedById = blog.UpdatedById;
                existingBlog.CreatedTime = blog.UpdatedTime;

                var affectedRow = await _repository.SaveChangesAsync();

                return affectedRow > 0;
            }

            return false;
        }
    }
}
