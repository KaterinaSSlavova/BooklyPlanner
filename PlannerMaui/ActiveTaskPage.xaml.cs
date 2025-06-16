using Application.Interfaces;
using AutoMapper;
using PlannerMaui.ViewModels;

namespace PlannerMaui;

public partial class ActiveTaskPage : ContentPage
{
	private ActiveTaskViewModel _viewModel;
	private IMapper _mapper;
	private IReadingTaskService _taskService;
	public ActiveTaskPage(IReadingTaskService taskService, IMapper mapper)
	{
		InitializeComponent();
		_mapper = mapper;
		_taskService = taskService;
		_viewModel = new ActiveTaskViewModel(_taskService, _mapper);
		BindingContext = _viewModel;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadActiveTasks();
    }
}