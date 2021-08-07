using System;
using System.ComponentModel.DataAnnotations;

namespace RoundTable.Server.Data.Model
{
    /// <summary>
    /// 使用者授權 Token 資訊
    /// </summary>
    public class UserAuthToken
    {
        // User ID
        [Key]
        public int Id { get; set; }

        /// <summary>
        ///  Auth jwt token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 有效期限日期時間
        /// </summary>
        public DateTime Expires { get; set; }

        /// <summary>
        /// 建立日期時間
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// 建立 Token IP Address
        /// </summary>
        public string CreatedByIp { get; set; }
        
    }
}