using System.ComponentModel.DataAnnotations;

namespace RoundTable.Server.Data.Model
{
    /// <summary>
    /// 使用者資訊
    /// </summary>
    public class User
    {
        [Key]
        public int Id { get; set; }
        // 姓名
        public string UserName { get; set; }
        // 帳號
        public string Account { get; set; }
        // 密碼
        public string Password { get; set; }
    }
}