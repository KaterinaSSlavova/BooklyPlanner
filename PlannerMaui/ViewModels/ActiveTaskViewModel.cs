using System.Collections.ObjectModel;
using System.Windows.Input;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Planner.ViewModels;

namespace PlannerMaui.ViewModels
{
    public class ActiveTaskViewModel
    {
        private readonly IReadingTaskService _taskService;
        private readonly IMapper _mapper;

        public ICommand MarkAsComplete { get; }
        public ICommand ArchiveTask { get; }
        public ObservableCollection<ReadingTaskViewModel> Tasks { get; } = new();

        public ActiveTaskViewModel(IReadingTaskService taskService, IMapper mapper)
        {
             _taskService = taskService;
            _mapper = mapper;
            MarkAsComplete = new Command<ReadingTaskViewModel>(OnMarkAsComplete);
            ArchiveTask = new Command<ReadingTaskViewModel>(OnArchiveTask);
        }

        public async Task LoadActiveTasks()
        {
            List<ReadingTask>? tasks = await Task.Run(() => _taskService.LoadUserTasks(1));
            List<ReadingTaskViewModel> models = _mapper.Map<List<ReadingTaskViewModel>>(tasks);
            Tasks.Clear();
            foreach (var model in models)
            {
                if (!model.IsCompleted)
                    Tasks.Add(model);
            }
        }

        public void OnMarkAsComplete(ReadingTaskViewModel model)
        {
            ReadingTask task = _mapper.Map<ReadingTask>(model); 
            _taskService.MarkTaskAsComplete(task);
        } 

        public void OnArchiveTask(ReadingTaskViewModel model)
        {
            ReadingTask task = _mapper.Map<ReadingTask>(model);
            _taskService.ArchiveTask(task);
        }
    }
}
