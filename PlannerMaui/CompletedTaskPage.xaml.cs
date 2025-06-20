using PlannerMaui.ViewModels;

namespace PlannerMaui;

public partial class CompletedTaskPage : ContentPage
{
    private CompletedTaskViewModel _viewModel;
    public CompletedTaskPage(CompletedTaskViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadCompletedTasks();
    }
}