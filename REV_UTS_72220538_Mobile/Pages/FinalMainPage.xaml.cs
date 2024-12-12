using Microsoft.Maui.Controls;
using REV_UTS_72220538_Mobile.Data;
using REV_UTS_72220538_Mobile.Pages;
using System.Net.Http;
using System.Net.Http.Json;

namespace REV_UTS_72220538_Mobile.Pages
{
    public partial class FinalMainPage : ContentPage
    {
        private readonly ccService _ccService;
        private readonly HttpClient _httpClient;
        public FinalMainPage()
        {
            InitializeComponent();  // This should work if the class is correctly defined
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://actbackendseervices.azurewebsites.net/api/")
            };
            LoadUserRoleAsync();
        }
        private async void LoadUserRoleAsync()
        {
            try
            {
                // Simulate API Call
                var roles = await _httpClient.GetFromJsonAsync<List<string>>("roles");

                if (roles != null && roles.Any())
                {
                    RoleLabel.Text = $"Role: {roles.First()}";
                }
                else
                {
                    RoleLabel.Text = "Role: Unknown";
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error fetching roles: {ex.Message}");
                RoleLabel.Text = "Role: Error loading";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                RoleLabel.Text = "Role: Error";
            }
        }
        private async void OnCategoryPageClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new categoryPage());
        }

        private async void OnCoursePageClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new coursePage());
        }
        private async void OnInstructorList(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new InstructorList());
        }
    }
}