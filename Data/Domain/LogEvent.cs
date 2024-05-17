namespace Data.Domain
{
    public class LogEvent: AbstractEntity
    {
        public required string Method { get; set; }

        public required string EndPoint { get; set; }
        
        public required string RequestBody { get; set; }
        
        public required string ResponseBody { get; set; }
        
        public required int StatusCode { get; set; }
    }
}
