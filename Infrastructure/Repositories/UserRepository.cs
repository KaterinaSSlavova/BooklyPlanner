using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository:IUserRepository
    {
        private readonly DBContext _context;
        public UserRepository(DBContext context)
        {
            _context = context;
        }

        public void Register(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        
        public User? GetUserByUsername(string username)
        {
            return _context.Users
                .AsNoTracking()
                .FirstOrDefault(u => u.Username == username);
        }

        public bool UsernameExists(string username)
        {
            return _context.Users.Any(u => u.Username == username);
        }

        public bool EmailExists(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }
    }
}
