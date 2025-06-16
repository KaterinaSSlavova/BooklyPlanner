using PlannerMaui.ViewModels;

namespace PlannerMaui
{
    public partial class App : Microsoft.Maui.Controls.Application
    {
        public App(ActiveTaskPage activeTask, CompletedTaskPage completedTask, CreateNewTaskPage newTaskPAge)
        {
            InitializeComponent();
            MainPage = new MainPage(activeTask, completedTask, newTaskPAge);  
        }
    }
}
