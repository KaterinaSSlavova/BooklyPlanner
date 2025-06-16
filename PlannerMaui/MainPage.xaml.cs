using PlannerMaui.ViewModels;

namespace PlannerMaui
{
    public partial class MainPage : TabbedPage
    {
        public MainPage(ActiveTaskPage activeTaskPage, CompletedTaskPage completedTaskPage, CreateNewTaskPage newTaskPage)
        {
            InitializeComponent();

            Children.Add(newTaskPage);
            Children.Add(activeTaskPage);
            Children.Add(completedTaskPage);
        }
    }

}
