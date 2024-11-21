namespace REV_UTS_72220538_Mobile
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            // Arahkan ke halaman berikutnya setelah menekan tombol login
            Application.Current.MainPage = new AppShell();
        }
    }

}
