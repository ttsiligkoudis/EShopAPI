using Microsoft.EntityFrameworkCore;
using DataModels;
using Context;
using Repositories.Helpers;

namespace Repositories.Users
{
    public class UserRepository : IUserRepository, IDisposable
    {
        private readonly AppDbContext _context = null;

        public UserRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                }
            }
        }

        public async Task<List<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUser(int id)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetUser(string email, string password)
        {
            var user = await _context.Users
                .SingleOrDefaultAsync(u => u.Email == email);

            var isPasswordValid = Crypt.VerifyPassword(password, user?.Password);

            return isPasswordValid ? user : null;
        }

        public async Task<User> GetUser(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(u =>
                u.Email == email);
        }

        public async Task<User> Post(User user)
        {
            user.Password = Crypt.HashPassword(user.Password);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> Put(User user)
        {
            _context.Users.Attach(user);
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<object> Delete(int id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            return null;
        }
    }
}
