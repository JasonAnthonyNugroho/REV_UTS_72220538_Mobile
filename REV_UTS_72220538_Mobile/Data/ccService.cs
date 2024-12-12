using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http.Headers;


namespace REV_UTS_72220538_Mobile.Data
{
    public class ccService
    {
        private readonly HttpClient _httpClient;
        private const string CoursesEndpoint = "/api/courses";
        private const string CategoriesEndpoint = "/api/categories";

        public ccService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://actbackendseervices.azurewebsites.net");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", MainPage.AuthToken);
        }
        public async Task AddCategoryAsync(category category)
        {
            var content = new StringContent(JsonSerializer.Serialize(category), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(CategoriesEndpoint, content);
            response.EnsureSuccessStatusCode();
        }
        
        //public async Task<IEnumerable<course>> GetCoursesAsync()
        //{
        //    var response = await _httpClient.GetAsync(CoursesEndpoint);
        //    response.EnsureSuccessStatusCode();
        //    return await response.Content.ReadFromJsonAsync<IEnumerable<course>>();
        //}

        public async Task<course> GetCourseByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{CoursesEndpoint}/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<course>(content);
        }

        public async Task AddCourseAsync(course course)
        {
            var content = new StringContent(JsonSerializer.Serialize(course), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(CoursesEndpoint, content);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateCourseAsync(course course)
        {
            var content = new StringContent(JsonSerializer.Serialize(course), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{CoursesEndpoint}/{course.courseId}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteCourseAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{CoursesEndpoint}/{id}");
            response.EnsureSuccessStatusCode();
        }

        // Category Services
        public async Task<IEnumerable<category>> GetCategoriesAsync()
        {
            var response = await _httpClient.GetAsync(CategoriesEndpoint);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<category>>();
        }
        
        public async Task<category> GetCategoryByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{CategoriesEndpoint}/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<category>(content);
        }

       

        public async Task UpdateCategoryAsync(category category)
        {
            var content = new StringContent(JsonSerializer.Serialize(category), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{CategoriesEndpoint}/{category.categoryId}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{CategoriesEndpoint}/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<List<course>> GetCoursesByNameAsync(string courseName)
        {
            var response = await _httpClient.GetAsync($"{CoursesEndpoint}/search/{courseName}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<course>>() ?? new List<course>();
            }
            throw new Exception($"Failed to load courses: {response.ReasonPhrase}");
        }

        public async Task<List<Instructor>> GetInstructorsAsync()
        {
            var response = await _httpClient.GetAsync("api/instructors");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Instructor>>();
            }
            return new List<Instructor>();
        }

        public async Task<List<course>> GetCoursesAsync()
        {
            var response = await _httpClient.GetAsync("api/courses");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<course>>();
            }
            return new List<course>();
        }

        public async Task<bool> AddEnrollmentAsync(Enrollment enrollment)
        {
            var response = await _httpClient.PostAsJsonAsync("api/enrollments", enrollment);
            return response.IsSuccessStatusCode;
        }

    }

}
