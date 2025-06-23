using PlannerMaui.ViewModels;

namespace PlannerMaui
{
    public partial class App : Microsoft.Maui.Controls.Application
    {
        public App(ActiveTaskPage activeTask, CompletedTaskPage completedTask, CreateNewTaskPage newTaskPage)
        {
            InitializeComponent();
            MainPage = new MainPage(activeTask, completedTask, newTaskPage);  
        }
    }
}
