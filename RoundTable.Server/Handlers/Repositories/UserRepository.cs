using System.Collections.Generic;
using System.Linq;
using RoundTable.Server.Data;
using RoundTable.Server.Data.Model;

namespace RoundTable.Server.Handlers.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAll()
        {
            return this._context.Users.ToList();
        }

        public User GetById(int id)
        {
            return this._context.Users.FirstOrDefault(x => x.Id == id);
        }
    }
}