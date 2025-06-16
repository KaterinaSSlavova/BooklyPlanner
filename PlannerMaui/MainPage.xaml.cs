namespace PlannerMaui
{
    public partial class MainPage : TabbedPage
    {

        public MainPage(ActiveTaskPage activeTaskPage, CompletedTaskPage completedTaskPage)
        {
            InitializeComponent();
            
            Children.Add(activeTaskPage);
            Children.Add(completedTaskPage);
        }
    }

}
