using Application.Interfaces;
using AutoMapper;
using PlannerMaui.ViewModels;

namespace PlannerMaui;

public partial class ActiveTaskPage : ContentPage
{
	private ActiveTaskViewModel _viewModel;
	public ActiveTaskPage(ActiveTaskViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		BindingContext = _viewModel;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadActiveTasks();
    }
}