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
        private readonly IApiTaskClient _taskClient;
        private readonly IMapper _mapper;

        public ICommand MarkAsComplete { get; }
        public ICommand ArchiveTask { get; }
        public ObservableCollection<ReadingTaskViewModel> Tasks { get; } = new();

        public CompletedTaskViewModel(IApiTaskClient taskClient, IMapper mapper)
        {
            _taskClient = taskClient;
            _mapper = mapper;

            MarkAsComplete = new Command<int>(async (id) => await OnMarkAsComplete(id));
            ArchiveTask = new Command<int>(async (id) => await OnArchiveTask(id));
        }

        public async Task LoadCompletedTasks()
        {
            List<ReadingTask>? tasks = await _taskClient.LoadUserTasks(1);
            List<ReadingTaskViewModel> models = _mapper.Map<List<ReadingTaskViewModel>>(tasks);
            Tasks.Clear();
            foreach (var model in models)
            {
                if (model.IsCompleted)
                    Tasks.Add(model);
            }
        }

        public async Task OnMarkAsComplete(int Id)
        {
            ReadingTask? task = await _taskClient.GetTaskById(Id);
            await _taskClient.MarkAsComplete(task);

            ReadingTaskViewModel? completedTask = Tasks.FirstOrDefault(t => t.Id == Id);
            Tasks.Remove(completedTask);
        }

        public async Task OnArchiveTask(int Id)
        {
            ReadingTask? task = await _taskClient.GetTaskById(Id);
            await _taskClient.ArchiveTask(task);

            ReadingTaskViewModel? archivedTask = Tasks.FirstOrDefault(t => t.Id == Id);
            Tasks.Remove(archivedTask);
        }
    }
}
