using Domain.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Application.Interfaces;

namespace Application.Services
{
    public class UserServices: IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserServices(IUserRepository userRepository)
        {
             _userRepository = userRepository;
        }

        public void Register(User user)
        {
            ValidateUser(user);
            _userRepository.Register(user); 
        }

        public User? LogIn(User loggingUser)
        {
            User? storedUser = _userRepository.GetUserByUsername(loggingUser.Username);
            if (storedUser == null)
                throw new ArgumentException($"User with username '{loggingUser.Username}' was not found!");
            if(storedUser.Password != loggingUser.Password)
                throw new ArgumentException($"Wrong password! Please try again!");
            return storedUser;
        }

        public User? GetUserByUsername(string username)
        {
            return _userRepository.GetUserByUsername(username);
        }

        public string ConvertToString(IFormFile image)
        {
            using (var ms = new MemoryStream())
            {
                image.CopyTo(ms);
                byte[] imageBytes = ms.ToArray();
                return Convert.ToBase64String(imageBytes);
            }
        }

        private void ValidateUser(User user)
        {
            if (user == null) throw new ArgumentNullException("Invalid data!");
            if (_userRepository.UsernameExists(user.Username)) throw new ArgumentException("Username already exists!");
            if (_userRepository.EmailExists(user.Email)) throw new ArgumentException("Email already exists!");
        }
    }
}
