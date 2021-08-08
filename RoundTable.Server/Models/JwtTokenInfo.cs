namespace RoundTable.Server.Models
{
    public class JwtTokenInfo
    {
        /// <summary>
        /// 使用者 ID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Refresh Token
        /// </summary>
        public string RefreshToken { get; set; }
    }
}