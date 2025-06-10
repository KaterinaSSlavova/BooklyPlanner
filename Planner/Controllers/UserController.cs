using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Planner.ViewModels;
using Domain.Entities;
using AutoMapper;

namespace Planner.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper; 
        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult LogInForm()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LogIn(AccountLogIn loggingUser)
        {
			if (!ModelState.IsValid)
			{
				return View(loggingUser);
			}

            try
            {
				User user = _mapper.Map<User>(loggingUser);
				User? loggedUser = _userService.LogIn(user);
                Response.Cookies.Append("Username", loggedUser.Username, new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTimeOffset.UtcNow.AddDays(1)
                });

                Response.Cookies.Append("UserId", loggedUser.Id.ToString(), new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTimeOffset.UtcNow.AddMinutes(30)
                });
                return RedirectToAction("Index", "Home");
			}
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("LogInForm", "User");
        }

        [HttpGet]
        public IActionResult RegisterForm()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(AccountRegister newUser)
        {
            try
            {
                if (newUser.Image != null)
                {
                    string profilePicture = _userService.ConvertToString(newUser.Image);
                    newUser.Photo = profilePicture;
                }
                User user = _mapper.Map<User>(newUser);
                _userService.Register(user);
                TempData["Success"] = "Account created successfully!";
				return RedirectToActionPermanent("LogInForm", "User");
			}
            catch (Exception ex)
            {
				TempData["Error"] = ex.Message;
			}
            return RedirectToActionPermanent("RegisterForm", "User");
        }

		[HttpPost]
		public IActionResult LogOut()
		{
            Response.Cookies.Delete("Username");
			return RedirectToAction("LogInForm", "User");
		}
	}
}
