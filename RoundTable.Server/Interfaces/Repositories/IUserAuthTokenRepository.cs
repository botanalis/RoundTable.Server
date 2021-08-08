using RoundTable.Server.Data.Model;

namespace RoundTable.Server.Interfaces.Repositories
{
    public interface IUserAuthTokenRepository
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <param name="isRemoveOldToken">清除舊 Token</param>
        /// <returns></returns>
        bool Add(UserAuthToken model, bool isRemoveOldToken);
        
        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="id"> ID</param>
        /// <returns></returns>
        bool DeleteById(int id);

        /// <summary>
        /// 取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UserAuthToken GetById(int id);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool Update(UserAuthToken model);
    }
}