namespace RoundTable.Server.Data.Model
{
    public class User
    {
        public int Id { get; set; }
        // 姓名
        public string UserName { get; set; }
        // 帳號
        public string Account { get; set; }
        // 密碼
        public string Password { get; set; }
    }
}