using PlannerMaui.ViewModels;

namespace PlannerMaui;

public partial class ActiveTaskPage : ContentPage
{
	private ActiveTaskViewModel _viewModel;
    private bool _isPolling = false;
    private CancellationTokenSource _pollingTokenSource;
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
        StartPolling();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        StopPolling();
    }

    private void StartPolling()
    {
        if (_isPolling) return;

        _isPolling = true;
        _pollingTokenSource = new CancellationTokenSource();

        Task.Run(async () =>
        {
            while (!_pollingTokenSource.IsCancellationRequested)
            {
                await MainThread.InvokeOnMainThreadAsync(() => _viewModel.LoadActiveTasks());
              
                await Task.Delay(TimeSpan.FromSeconds(10));
            }
        }, _pollingTokenSource.Token);
    }

    private void StopPolling()
    {
        _isPolling = false;
        _pollingTokenSource?.Cancel();
    }

}