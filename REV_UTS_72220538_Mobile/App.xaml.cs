
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using REV_UTS_72220538_Mobile.Data;
using System.IO;
namespace REV_UTS_72220538_Mobile
{
    public partial class App : Application
    {
        private readonly ccService _ccService;
        public App(MainPage mainPage)
        {
            InitializeComponent();
            MainPage = new NavigationPage(mainPage); // Navigasi ke MainPage
        }
    }
}
