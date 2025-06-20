using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API
{
    [ApiController]
    [Route("api/tasks")]
    public class TaskController: ControllerBase
    {
        private readonly IReadingTaskService _taskService;
        public TaskController(IReadingTaskService taskService)
        {
             _taskService = taskService;
        }

        [HttpGet("by-id/{taskId}")]
        public IActionResult GetTaskById(int taskId)
        {
            try
            {
                ReadingTask task = _taskService.GetTaskById(taskId);
                if (task == null)
                {
                    return NotFound();
                }
                return Ok(task);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("create-task")]
        public IActionResult CreateTask([FromBody]ReadingTask task)
        {
            try
            {
                _taskService.CreateTask(task);
                return CreatedAtAction(nameof(GetTaskById), new {taskId = task.Id}, task);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("by-user/{userId}")]
        public IActionResult LoadUserTasks(int userId)
        {
            try
            {
                List<ReadingTask>? tasks = _taskService.LoadUserTasks(userId);
                if (tasks.Count <= 0)
                    return NotFound();
                return Ok(tasks);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("mark-as-complete")]
        public IActionResult MarkTaskAsComplete(ReadingTask task)
        {
            try
            {
                _taskService.MarkTaskAsComplete(task);
                return Ok(task);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("archive-task")]
        public IActionResult ArchiveTask(ReadingTask task)
        {
            try
            {
                _taskService.ArchiveTask(task);
                return Ok(task);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
