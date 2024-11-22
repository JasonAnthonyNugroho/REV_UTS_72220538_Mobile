using REV_UTS_72220538_Mobile.Data;
using System;
using System.Threading.Tasks;

namespace REV_UTS_72220538_Mobile.Pages;

public partial class AddCategoryPage : ContentPage
{
    private readonly ccService _ccService;
    public category NewCategory { get; set; }

    public AddCategoryPage(ccService ccService)
    {
        InitializeComponent();
        _ccService = ccService;
        NewCategory = new category(); // Initialize NewCategory

        BindingContext = this; // Bind to this page's context
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NewCategory.name) || string.IsNullOrWhiteSpace(NewCategory.description))
        {
            await DisplayAlert("Validation", "Please fill in both the Name and Description.", "OK");
            return;
        }

        try
        {
            // Add the new category
            await _ccService.AddCategoryAsync(NewCategory);
            await DisplayAlert("Success", "Category added successfully!", "OK");
            await Navigation.PopAsync(); // Go back to CategoryPage
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Error adding category: {ex.Message}", "OK");
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