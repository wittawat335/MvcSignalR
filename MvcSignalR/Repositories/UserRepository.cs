using Microsoft.EntityFrameworkCore;
using MvcSignalR.Entities;

namespace MvcSignalR.Repositories
{
    public class UserRepository
    {
        private readonly SignalRnotiContext _dbContext;

        public UserRepository(SignalRnotiContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<User> GetList()
        {
            return _dbContext.Users.ToList();
        }

        public async Task<User> GetUserDetails(string username, string password)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(user => user.Username == username && user.Password == password);
        }
    }
}
