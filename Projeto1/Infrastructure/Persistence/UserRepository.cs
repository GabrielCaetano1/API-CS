using Microsoft.EntityFrameworkCore;
using Projeto1.Domain.Entities;
using Projeto1.Domain.Enums;
using Projeto1.Domain.Interfaces;

namespace Projeto1.Infrastructure.Persistence
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;

        public UserRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _db.Users.FindAsync(id);
        }
        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task<List<User>> GetUsersAsync(UserType? type = null)
        {
            var query = _db.Users.AsQueryable();
            if (type.HasValue)
            {
                query = query.Where(u => u.Type == type.Value);
            }
            return await query.ToListAsync();
        }
        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _db.Users.AnyAsync(u => u.Email == email);
        }
        public async Task AddAsync(User user)
        {
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
        }
        public async Task UpdateAsync(User user)
        {
            _db.Users.Update(user);
            await _db.SaveChangesAsync();
        }
        public async Task DeleteAsync(User user)
        {
            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
        }
    }
}
