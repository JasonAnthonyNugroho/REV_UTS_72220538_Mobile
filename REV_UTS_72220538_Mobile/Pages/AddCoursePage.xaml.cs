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

public partial class AddCoursePage : ContentPage
{
    private readonly int _courseId;
    private readonly ccService _ccService;
    private int _selectedCategoryId;
    public AddCoursePage(ccService ccService)
    {
        InitializeComponent();
        _ccService = new ccService(new HttpClient());
        LoadCategories();
    }

    private async void LoadCategories()
    {
        var categories = await _ccService.GetCategoriesAsync();
        categoryPicker.ItemsSource = categories.ToList();
        categoryPicker.ItemDisplayBinding = new Binding("name");
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        var selectedCategory = categoryPicker.SelectedItem as category;

        if (selectedCategory != null)
        {
            // Create a new course object with entered data
            var newCourse = new course
            {
                name = nameEntry.Text,
                imageName = imageNameEntry.Text,
                duration = int.TryParse(durationEntry.Text, out int duration) ? duration : 0,
                description = descriptionEditor.Text,
                categoryId = selectedCategory.categoryId // Store only the category ID
            };

            await _ccService.AddCourseAsync(newCourse);
            await DisplayAlert("Success", "Course created successfully.", "OK");
            await Navigation.PushAsync(new coursePage());
        }
        else
        {
            await DisplayAlert("Error", "Please select a category.", "OK");
        }
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        bool confirmCancel = await DisplayAlert("Confirm", "Are you sure you want to cancel?", "Yes", "No");
        if (confirmCancel)
        {
            await Navigation.PopAsync(); // Go back to previous page without saving
        }
    }

}