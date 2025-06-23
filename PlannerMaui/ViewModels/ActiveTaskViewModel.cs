using System.Collections.ObjectModel;
using System.Windows.Input;
using Application.Interfaces;
using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using Domain.Entities;
using Planner.ViewModels;
using System.Linq;

namespace PlannerMaui.ViewModels
{
    public class ActiveTaskViewModel: ObservableObject
    {
        private readonly IApiTaskClient _taskClient;
        private readonly IMapper _mapper;

        public ICommand MarkAsComplete { get; }
        public ICommand ArchiveTask { get; }

        public ObservableCollection<ReadingTaskViewModel> Tasks { get; } = new();

        public ActiveTaskViewModel(IMapper mapper, IApiTaskClient taskClient)
        {
            _taskClient = taskClient;
            _mapper = mapper;
            MarkAsComplete = new Command<int>(async (id) => await OnMarkAsComplete(id));
            ArchiveTask = new Command<int>(async (id) => await OnArchiveTask(id));
        }

        public async Task LoadActiveTasks()
        {
            List<ReadingTask>? tasks = await _taskClient.LoadUserTasks(1);
            var newTasks = tasks.Where(t => !t.IsCompleted).ToList();
            var newIds = newTasks.Select(t => t.Id).OrderBy(id => id);
            var currentIds = Tasks.Select(t => t.Id).OrderBy(id => id);

            if (!newIds.SequenceEqual(currentIds))
            {
                List<ReadingTaskViewModel> models = _mapper.Map<List<ReadingTaskViewModel>>(tasks);
                Tasks.Clear();
                foreach (var model in models)
                {
                    if (!model.IsCompleted)
                        Tasks.Add(model);
                }
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