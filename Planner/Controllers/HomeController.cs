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
                
        public HomeController(ILogger<HomeController> logger, IReadingTaskService readingTaskService, IMapper mapper)
        {
            _logger = logger;
            _readingTaskService = readingTaskService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            int userId = HttpContext.Session.GetInt32("UserId").Value;
            List<ReadingTask> tasks = _readingTaskService.LoadUserTasks(userId);
            List<ReadingTaskViewModel> model = _mapper.Map<List<ReadingTaskViewModel>>(tasks);
            return View(model);
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
                task.UserId = HttpContext.Session.GetInt32("UserId").Value;
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
        public IActionResult MarkAsComplete(int id)
        {
            ReadingTask task = _readingTaskService.GetTaskById(id);
            _readingTaskService.MarkTaskAsComplete(task);
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
