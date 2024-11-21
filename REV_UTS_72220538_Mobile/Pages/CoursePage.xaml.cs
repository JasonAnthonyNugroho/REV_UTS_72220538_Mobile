using REV_UTS_72220538_Mobile.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REV_UTS_72220538_Mobile.Pages;

public partial class coursePage : ContentPage
{
    private List<course> _allCourses;
    private List<category> _allCategories; // Add this for categories
    private readonly ccService _ccService;
    public coursePage()
    {
        InitializeComponent();
        _ccService = new ccService(new HttpClient());
        LoadCourses();
        LoadCategories(); // Load categories as well
    }
    private async Task LoadCourses()
    {
        try
        {
            // Fetch courses from your ccService
            var courses = await _ccService.GetCoursesAsync();  // Assuming GetCoursesAsync fetches courses from your API
            _allCourses = courses.ToList();  // Store the courses in a local variable
            CoursesCollectionView.ItemsSource = _allCourses;  // Bind the courses to the CollectionView
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to load courses: {ex.Message}", "OK");
        }
    }

    private async Task LoadCategories()
    {
        try
        {
            var categories = await _ccService.GetCategoriesAsync(); // Assuming you have this method in ccService
            _allCategories = categories.ToList();
            
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to load categories: {ex.Message}", "OK");
        }
    }

    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        var searchText = e.NewTextValue?.ToLower();

        if (string.IsNullOrEmpty(searchText))
        {
            // Reset to show all courses when the search bar is empty
            CoursesCollectionView.ItemsSource = _allCourses;
        }
        else
        {
            // Filter courses based on the search text
            var filteredCourses = _allCourses
                .Where(course => course.name.ToLower().Contains(searchText))
                .ToList();
            CoursesCollectionView.ItemsSource = filteredCourses;
        }
    }
    private async void OnAddCourseClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddCoursePage(_ccService)); // Navigate to AddCoursePage
    }

    private async void OnEditCourseClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var selectedCourse = button?.CommandParameter as course;

        if (selectedCourse != null)
        {
            // Navigasi ke halaman EditCourse dan kirim course yang dipilih
            await Navigation.PushAsync(new EditCourse(selectedCourse, _ccService));
        }
    }

    private async void OnDeleteCourseClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var selectedCourse = button?.CommandParameter as course;

        if (selectedCourse != null)
        {
            bool confirmDelete = await DisplayAlert("Confirm", "Are you sure you want to delete this course?", "Yes", "No");
            if (confirmDelete)
            {
                try
                {
                    // Delete course using ccService
                    await _ccService.DeleteCourseAsync(selectedCourse.courseId);
                    await DisplayAlert("Success", "Course deleted successfully.", "OK");
                }
                catch (Exception ex)
                {
                    // Show error if deletion fails
                    await DisplayAlert("Error", $"Failed to delete course: {ex.Message}", "OK");
                }
            }
        }
    }
    private async void OnRefreshCoursesClicked(object sender, EventArgs e)
{
    await LoadCourses();  // Re-fetch the courses when the refresh button is clicked
}

}