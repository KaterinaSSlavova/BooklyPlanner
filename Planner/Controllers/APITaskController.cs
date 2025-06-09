using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Planner.ViewModels;
using Domain.Entities;

namespace Planner.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class APITaskController : ControllerBase
    {
        private readonly IReadingTaskService _readingTaskService;
        private readonly IUserService _userService;

        public APITaskController(IReadingTaskService readingTaskService, IUserService userService)
        {
             _readingTaskService = readingTaskService;  
            _userService = userService;
        }

        [HttpPost("from-bookly")]
        public IActionResult CreateTaskFromBookly([FromBody]BooklyToPlanner dto)
        {
            User? user = _userService.GetUserByUsername(dto.Username);
            if (user == null)
                return BadRequest("User not found!");

            ReadingTask readingTask = new ReadingTask(dto.DueDate, new Book(dto.Title, dto.Author, dto.Image, dto.Pages));
            readingTask.UserId = user.Id;
            _readingTaskService.CreateTask(readingTask);
            HttpContext.Session.SetInt32("UserId", user.Id);
            return Ok(new { message = "Reading task created successfully." });
        }
    }
}
