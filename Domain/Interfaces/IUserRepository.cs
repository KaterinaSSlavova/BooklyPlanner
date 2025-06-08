using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        void Register(User user);
        User? GetUserByUsername(string username);
        bool UsernameExists(string username);
        bool EmailExists(string email);
    }
}
