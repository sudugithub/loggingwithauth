namespace Service.Contract
{
    public class JWTUserParam
    {
        public required long Id { get; set; }
        public required string Email { get; set; }
        public required DateTime Iat { get; set; }
    }
}
