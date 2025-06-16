using System.Collections.ObjectModel;
using System.Windows.Input;
using Application.Interfaces;
using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using Domain.Entities;
using Planner.ViewModels;

namespace PlannerMaui.ViewModels
{
    public class ActiveTaskViewModel: ObservableObject
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
            MarkAsComplete = new Command<int>(OnMarkAsComplete);
            ArchiveTask = new Command<int>(OnArchiveTask);
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

        public void OnMarkAsComplete(int Id)
        {
            ReadingTask? task = _taskService.GetTaskById(Id);
            _taskService.MarkTaskAsComplete(task);

            ReadingTaskViewModel? completedTask = Tasks.FirstOrDefault(t => t.Id == Id);  
            Tasks.Remove(completedTask);
        } 

        public void OnArchiveTask(int Id)
        {
            ReadingTask? task = _taskService.GetTaskById(Id);    
            _taskService.ArchiveTask(task);

            ReadingTaskViewModel? archivedTask = Tasks.FirstOrDefault(t => t.Id == Id);
            Tasks.Remove(archivedTask);
        }
    }
}
