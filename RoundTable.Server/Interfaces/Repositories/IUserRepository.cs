using System.Collections.Generic;
using RoundTable.Server.Data.Model;

namespace RoundTable.Server.Interfaces.Repositories
{
    public interface IUserRepository
    {
        /// <summary>
        /// 取得所有使用者
        /// </summary>
        /// <returns></returns>
        IEnumerable<User> GetAll();
        
        /// <summary>
        /// 取得使用者資訊
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        User GetById(int id);
    }
}