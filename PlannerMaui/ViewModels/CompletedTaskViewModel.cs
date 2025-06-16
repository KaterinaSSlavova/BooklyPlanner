using Application.Interfaces;
using AutoMapper;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Planner.ViewModels;
using Domain.Entities;
using CommunityToolkit.Mvvm.ComponentModel;

namespace PlannerMaui.ViewModels
{
    public class CompletedTaskViewModel: ObservableObject
    {
        private readonly IReadingTaskService _taskService;
        private readonly IMapper _mapper;

        public ICommand MarkAsComplete { get; }
        public ICommand ArchiveTask { get; }
        public ObservableCollection<ReadingTaskViewModel> Tasks { get; } = new();

        public CompletedTaskViewModel(IReadingTaskService taskService, IMapper mapper)
        {
            _taskService = taskService;
            _mapper = mapper;

            MarkAsComplete = new Command<int>(OnMarkAsComplete);
            ArchiveTask = new Command<int>(OnArchiveTask);
        }

        public async Task LoadCompletedTasks()
        {
            List<ReadingTask>? tasks = await Task.Run(() => _taskService.LoadUserTasks(1));
            List<ReadingTaskViewModel> models = _mapper.Map<List<ReadingTaskViewModel>>(tasks);
            Tasks.Clear();
            foreach (var model in models)
            {
                if (model.IsCompleted)
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
