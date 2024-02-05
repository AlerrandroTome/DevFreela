using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DevFreelaDbContext _dbContext;

        public UserRepository(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<User>> GetAllAsync()
        {
            List<User> users = await _dbContext.Users.ToListAsync();
            return users;
        }

        public async Task<User> GetByEmailAndPasswordAsync(string username, string passwordHash)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email.Equals(username) 
                                                                    && u.Password.Equals(passwordHash));
        }

        public async Task<User> GetByIdAsync(int id)
        {
            User user = await _dbContext.Users.SingleOrDefaultAsync(p => p.Id == id);
            return user;
        }
    }
}
