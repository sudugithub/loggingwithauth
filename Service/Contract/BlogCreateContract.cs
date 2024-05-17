namespace Service.Contract
{
    public class BlogCreateContract
    {
        public required string Title { get; set; }
        public required string Content { get; set; }
        public required string Category { get; set; }
    }
}
