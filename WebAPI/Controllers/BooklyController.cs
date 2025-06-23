using System.Diagnostics;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using ViewModels;
namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/book-tasks")]
    public class BooklyController : ControllerBase
    {
        private readonly IApiTaskClient _taskClient;
        public BooklyController(IApiTaskClient taskClient)
        {
            _taskClient = taskClient;
        }

        [HttpPost("from-bookly")]
        public async Task<IActionResult> CreateTask([FromBody] BooklyToPlanner dto)
        {
            try
            {
                if (dto == null)
                {
                    return BadRequest("Invalid input");
                }

                ReadingTask task = new ReadingTask(dto.DueDate, new Book(dto.Title, dto.Author, dto.Image, dto.Pages));
                task.UserId = 1;
                await _taskClient.CreateTask(task);
                return Ok(task);
            }
            catch(Exception ex)
            {
                Console.WriteLine("EXCEPTION: " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
