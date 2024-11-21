using REV_UTS_72220538_Mobile.Pages;

namespace REV_UTS_72220538_Mobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(categoryPage), typeof(categoryPage));
            Routing.RegisterRoute(nameof(CoursePage), typeof(CoursePage));
        }

        
    
    }
}
