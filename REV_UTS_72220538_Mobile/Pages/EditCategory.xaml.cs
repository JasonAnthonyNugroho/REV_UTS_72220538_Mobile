using REV_UTS_72220538_Mobile.Data;
using System;
using Microsoft.Maui.Controls;
using REV_UTS_72220538_Mobile.Pages;  // Add this line
using Microsoft.Maui.Controls;

namespace REV_UTS_72220538_Mobile.Pages;

public partial class EditCategoryPage : ContentPage
{
 
        private readonly ccService _ccService;
        public category Category { get; set; }

        public EditCategoryPage(category selectedCategory, ccService ccService)
        {
            InitializeComponent();
            Category = selectedCategory;

            _ccService = ccService;

            // Set the BindingContext to the selected category to automatically bind the controls
            BindingContext = this;
        }

    // Save button click
    private async void OnSaveClicked(object sender, EventArgs e)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(Category.name) || string.IsNullOrWhiteSpace(Category.description))
            {
                await DisplayAlert("Validation", "Please fill in both the Name and Description.", "OK");
                return;
            }

            try
            {
                // Update the category via API
                await _ccService.UpdateCategoryAsync(Category);
                await DisplayAlert("Success", "Category updated successfully!", "OK");
                await Navigation.PopToRootAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }

        // Cancel button click
        private async void OnCancelClicked(object sender, EventArgs e)
        {
            bool confirmCancel = await DisplayAlert("Confirm", "Are you sure you want to cancel the changes?", "Yes", "No");
            if (confirmCancel)
            {
                await Navigation.PopAsync(); // Go back to the previous page without saving changes
            }
        }
    }
