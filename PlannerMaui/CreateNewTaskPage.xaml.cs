namespace PlannerMaui.ViewModels;

public partial class CreateNewTaskPage : ContentPage
{
	private readonly CreateReadingTaskViewModel _viewModel;
	public CreateNewTaskPage(CreateReadingTaskViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
        BindingContext = _viewModel;
    }
}