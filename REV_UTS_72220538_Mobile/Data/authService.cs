using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.Maui.Storage;
namespace REV_UTS_72220538_Mobile.Data
{
    public class authService
    {
        private readonly HttpClient _httpClient;
        private const string UsersEndpoint = "api/users";
        private const string LoginEndpoint = "api/login";

        public authService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://actbackendseervices.azurewebsites.net/");
        }

        // Registrasi pengguna baru
        public async Task<bool> RegisterUserAsync(UserRegistrationModel user)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(UsersEndpoint, user);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Registration Error: {ex.Message}");
                return false;
            }
        }

        // Login dan dapatkan token
        public async Task<string> LoginAsync(LoginModel credentials)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(LoginEndpoint, credentials);
                if (response.IsSuccessStatusCode)
                {
                    var loginResponse = await response.Content.ReadFromJsonAsync<LoginRespondModel>();
                    var token = loginResponse?.Token;

                    // Simpan token di SecureStorage
                    if (token != null)
                    {
                        await SecureStorage.SetAsync("auth_token", token);
                    }

                    return token;
                }
                else
                {
                    Console.WriteLine($"Login failed: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login Error: {ex.Message}");
                return null;
            }
        }

        // Tambahkan token ke semua permintaan
        public async Task AddAuthHeaderAsync()
        {
            var token = await SecureStorage.GetAsync("auth_token");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            else
            {
                Console.WriteLine("No token found for authorization.");
            }
        }
    }
}
