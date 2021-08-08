using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RoundTable.Server.Data;
using RoundTable.Server.Data.Model;
using RoundTable.Server.Interfaces.Repositories;

namespace RoundTable.Server.Handlers.Repositories
{
    public class UserAuthTokenRepository : IUserAuthTokenRepository
    {
        private readonly ApplicationDbContext _context;
        
        public UserAuthTokenRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public bool Add(UserAuthToken model, bool isRemoveOldToken)
        {
            using (var transaction  = this._context.Database.BeginTransaction() )
            {
                if (isRemoveOldToken)
                {
                    this.DeleteById(model.Id);
                }
                
                this._context.UserAuthTokens.Add(model);
                this._context.SaveChanges();

                transaction.Commit();
            }
            
            return true;
        }

        public bool DeleteById(int id)
        {
            var model = this._context.UserAuthTokens.FirstOrDefault(x => x.Id == id);
            if (model != null)
            {
                this._context.UserAuthTokens.Remove(model);
                this._context.SaveChanges();
            }
            

            return true;
        }

        public UserAuthToken GetById(int id)
        {
            return this._context.UserAuthTokens.AsQueryable().FirstOrDefault(x => x.Id == id);
        }

        public bool Update(UserAuthToken model)
        {
            var dbModel = this._context.UserAuthTokens.FirstOrDefault(x => x.Id == model.Id);

            if (dbModel != null)
            {
                dbModel.Expires = model.Expires;
                dbModel.Token = model.Token;
                dbModel.CreatedAt = model.CreatedAt;
                dbModel.CreatedByIp = model.CreatedByIp;

                this._context.UserAuthTokens.Update(dbModel);
                this._context.SaveChanges();

                return true;
            }


            return false;
        }
    }
}