using Microsoft.Maui.Controls;
using REV_UTS_72220538_Mobile.Data;
using System;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.Maui.Storage;
using System.Linq;
using System.Text;
using REV_UTS_72220538_Mobile.Pages;

namespace REV_UTS_72220538_Mobile
{
    public partial class MainPage : ContentPage
    {
        private readonly authService _authService;
        private bool _isLoginMode = true;
        public static string AuthToken { get; private set; }

        public MainPage(authService authService)
        {
            InitializeComponent();
            _authService = authService;
        }

        private void OnToggleButtonClicked(object sender, EventArgs e)
        {
            _isLoginMode = !_isLoginMode;

            emailEntry.IsVisible = !_isLoginMode;
            confirmPasswordEntry.IsVisible = !_isLoginMode;

            toggleButton.Text = _isLoginMode ? "Switch to Register" : "Switch to Login";
        }

        private async void OnSubmitButtonClicked(object sender, EventArgs e)
        {
            if (_isLoginMode)
            {
                // Login
                var username = usernameEntry.Text;
                var password = passwordEntry.Text;

                var loginData = new
                {
                    userName = username,
                    password = password
                };

                var json = JsonConvert.SerializeObject(loginData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {
                    var response = await client.PostAsync("https://actbackendseervices.azurewebsites.net/api/login", content);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        AuthToken = JsonConvert.DeserializeObject<LoginModel>(responseContent).token;

                        // Save the token (e.g., in Preferences)
                        Preferences.Set("auth_token", AuthToken);

                        // Navigate to the next page
                        await Navigation.PushAsync(new FinalMainPage());
                    }
                    else
                    {
                        // Handle login failure
                        await DisplayAlert("Login Failed", "Invalid username or password", "OK");
                    }
                }
            }
            else
            {
                // Registration
                try
                {
                    var registerResult = await _authService.RegisterUserAsync(new UserRegistrationModel
                    {
                        Email = emailEntry.Text,
                        Username = usernameEntry.Text,
                        Password = passwordEntry.Text
                    });

                    if (registerResult)
                    {
                        await DisplayAlert("Success", "Registration successful!", "OK");
                        OnToggleButtonClicked(null, null); // Switch to login mode
                    }
                    else
                    {
                        await DisplayAlert("Error", "Registration failed.", "OK");
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
                }
            }
        }

        // Load the saved token (if any) when the app starts
        public static string GetSavedAuthToken()
        {
            return Preferences.Get("auth_token", string.Empty);
        }
    }
}
