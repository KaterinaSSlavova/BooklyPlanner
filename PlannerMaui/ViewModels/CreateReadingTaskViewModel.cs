using System.Windows.Input;
using Application.Interfaces;
using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using Domain.Entities;
using Planner.ViewModels;

namespace PlannerMaui.ViewModels
{
    public class CreateReadingTaskViewModel: ObservableObject
    {
        private readonly IApiTaskClient _taskClient;
        private readonly IMapper _mapper;
        public CreateTaskViewModel Model { get; set; } = new CreateTaskViewModel
        {
            Book = new BookViewModel()  
        };
        public ICommand CreateTaskCommand { get; }

        public CreateReadingTaskViewModel(IApiTaskClient taskClient, IMapper mapper)
        {
            _taskClient = taskClient;
            _mapper = mapper;
            CreateTaskCommand = new Command(OnCreateTask);
        }

        public void OnCreateTask()
        {
            ReadingTask task = _mapper.Map<ReadingTask>(Model);
            task.UserId = 1;
            _taskClient.CreateTask(task);

            Model = new CreateTaskViewModel { Book = new BookViewModel() };
            OnPropertyChanged(nameof(Model));
        }
    }
}
