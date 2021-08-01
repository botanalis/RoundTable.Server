namespace RoundTable.Server.Models
{
    public class JwtAuthOptions
    {
        public const string SectionName = "JWTAuth";
        public string Secret { get; set; }
    }
}