using REV_UTS_72220538_Mobile.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;

namespace REV_UTS_72220538_Mobile.Pages;

public partial class EditCourse : ContentPage
{
    private readonly int _courseId;
    private readonly ccService _ccService;
    private int _selectedCategoryId;
    public EditCourse(course selectedCourse, ccService ccService)
    {
        InitializeComponent();
        _courseId = selectedCourse.courseId;
        _ccService = new ccService(new HttpClient());
        LoadCourse();
        LoadCategories();

        BindingContext = this;

    }
    private async void LoadCategories()
    {
        var categories = await _ccService.GetCategoriesAsync();
        categoryPicker.ItemsSource = categories.ToList();
        categoryPicker.ItemDisplayBinding = new Binding("name");

        var selectedCategory = categories.FirstOrDefault(c => c.categoryId == _selectedCategoryId);
        if (selectedCategory != null)
        {
            categoryPicker.SelectedItem = selectedCategory;
        }
    }
    private async void LoadCourse()
    {
        var course = await _ccService.GetCourseByIdAsync(_courseId);
        if (course != null)
        {
            courseIdLabel.Text = course.courseId.ToString();
            nameEntry.Text = course.name;
            imageNameEntry.Text = course.imageName;
            durationEntry.Text = course.duration.ToString();
            descriptionEditor.Text = course.description;
            _selectedCategoryId = course.categoryId;
        }
    }

    private async void OnEditCourseClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var selectedCourse = button?.CommandParameter as course;

        if (selectedCourse != null)
        {
            // Navigate to EditCourse page and pass the selected course and the ccService
            await Navigation.PushAsync(new EditCourse(selectedCourse, _ccService));
        }
    }
    private async void OnSaveClicked(object sender, EventArgs e)
    {
        var selectedCategory = categoryPicker.SelectedItem as category;

        if (selectedCategory != null)
        {
            var updatedCourse = new course
            {
                courseId = _courseId,
                name = nameEntry.Text,
                imageName = imageNameEntry.Text,
                duration = int.Parse(durationEntry.Text),
                description = descriptionEditor.Text,
                categoryId = selectedCategory.categoryId
            };

            await _ccService.UpdateCourseAsync(updatedCourse);
            await DisplayAlert("Success", "Course updated successfully.", "OK");
            
        }
        else
        {
            await DisplayAlert("Error", "Please select a category.", "OK");
        }
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        bool confirmCancel = await DisplayAlert("Confirm", "Are you sure you want to cancel changes?", "Yes", "No");
        if (confirmCancel)
        {
            await Navigation.PopAsync(); // Go back to previous page without saving
        }
    }
}