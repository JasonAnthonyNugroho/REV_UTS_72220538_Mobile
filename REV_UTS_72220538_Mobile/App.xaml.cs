
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using REV_UTS_72220538_Mobile.Data;
using System.IO;
namespace REV_UTS_72220538_Mobile
{
    public partial class App : Application
    {
        public static DatabaseService Database { get; private set; }

        public App()
        {
            InitializeComponent();

            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "categories.db3");
            Database = new DatabaseService(dbPath);

            MainPage = new MainPage(); // Atur halaman utama
        }
    }
}
