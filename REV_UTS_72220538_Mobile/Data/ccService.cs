﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace REV_UTS_72220538_Mobile.Data
{
    public class ccService
    {
        private readonly HttpClient _httpClient;
        private const string CoursesEndpoint = "api/Courses";
        private const string CategoriesEndpoint = "api/v1/Categories";

        public ccService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://actualbackendapp.azurewebsites.net/");
        }
        public async Task AddCategoryAsync(category category)
        {
            var content = new StringContent(JsonSerializer.Serialize(category), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(CategoriesEndpoint, content);
            response.EnsureSuccessStatusCode();
        }
        
        public async Task<IEnumerable<course>> GetCoursesAsync()
        {
            var response = await _httpClient.GetAsync(CoursesEndpoint);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<course>>();
        }

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
    }

}
