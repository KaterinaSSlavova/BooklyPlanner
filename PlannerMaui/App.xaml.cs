namespace PlannerMaui
{
    public partial class App : Microsoft.Maui.Controls.Application
    {
        public App(ActiveTaskPage activeTask, CompletedTaskPage completedTask)
        {
            InitializeComponent();
            MainPage = new MainPage(activeTask, completedTask);  
        }
    }
}
