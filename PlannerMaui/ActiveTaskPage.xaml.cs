using PlannerMaui.ViewModels;

namespace PlannerMaui;

public partial class ActiveTaskPage : ContentPage
{
	private ActiveTaskViewModel _viewModel;
    private bool shouldRefresh = false;
    public ActiveTaskPage(ActiveTaskViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		BindingContext = _viewModel;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        shouldRefresh = true;
        Refresh();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        shouldRefresh = false;
    }

    private async Task Refresh()
    {
        while (shouldRefresh)
        {
            await MainThread.InvokeOnMainThreadAsync(() => _viewModel.LoadActiveTasks());
            await Task.Delay(TimeSpan.FromSeconds(1));
        }
    }
}