using System.Diagnostics;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Planner.Models;
using Planner.ViewModels;
using Domain.Entities;
using AutoMapper;

namespace Planner.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IReadingTaskService _readingTaskService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
                
        public HomeController(ILogger<HomeController> logger, IReadingTaskService readingTaskService, IMapper mapper, IUserService userService)
        {
            _logger = logger;
            _readingTaskService = readingTaskService;
            _mapper = mapper;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            string cookie = Request.Cookies["UserId"];
            if(int.TryParse(cookie, out int userId))
            {
                List<ReadingTask> tasks = _readingTaskService.LoadUserTasks(userId);
                List<ReadingTaskViewModel> model = _mapper.Map<List<ReadingTaskViewModel>>(tasks);
                return View(model);
            }
            else
            {
                return RedirectToAction("LogInForm", "User");
            }
        }

        [HttpGet]
        public IActionResult CreateTask()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaveTask(CreateTaskViewModel model)
        {
            try
            {
                ReadingTask task = _mapper.Map<ReadingTask>(model);
                task.UserId = int.Parse(Request.Cookies["UserId"]);
                _readingTaskService.CreateTask(task);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("CreateTask", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> MarkAsComplete(int id)
        {
            ReadingTask? task = _readingTaskService.GetTaskById(id);
            _readingTaskService.MarkTaskAsComplete(task);

            PlannerToBooklyDTO dto = new PlannerToBooklyDTO()
            {
                Username = Request.Cookies["Username"],
                Title = task.Book.Title,
                Author = task.Book.Author,
                Image = task.Book.Image,
                Pages = task.Book.Pages
            };
            using HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7268");
            try
            {
                var response = await client.PostAsJsonAsync("/api/books/mark-as-completed", dto);
                if (!response.IsSuccessStatusCode)
                {
                    TempData["Warning"] = "Task was marked as completed but book was not moved to Have Read shelf.";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Bookly connection failed: {ex.Message}";
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Archive(int id)
        {
            ReadingTask? task = _readingTaskService.GetTaskById(id);
            _readingTaskService.ArchiveTask(task);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
