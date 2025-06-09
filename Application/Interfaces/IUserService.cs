using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces
{
    public interface IUserService
    {
        void Register(User user);
        void LogIn(User loggingUser);
        User? GetUserByUsername(string username);
        string ConvertToString(IFormFile image);
    }
}
