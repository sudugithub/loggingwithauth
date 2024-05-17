namespace Data.Domain
{
    public class Blog : AbstractEntity
    {
        public required string Title { get; set; }

        public required string Content { get; set; }
        
        public required string Category { get; set; }
    }
}
