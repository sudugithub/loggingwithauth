using Data.Domain;

namespace Service.Contract
{
    public class BlogPaginationResponse
    {
        public List<Blog>? Blogs { get; set; }
        public int TotalCount { get; set; }
        public int PageCount { get; set; }
    }
}
