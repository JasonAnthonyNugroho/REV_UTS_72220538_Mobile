using REV_UTS_72220538_Mobile.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Maui.Controls;

namespace REV_UTS_72220538_Mobile
{
    public partial class CategoryPage : ContentPage
    {
        private readonly ccService _ccService;

        public CategoryPage(ccService ccService)
        {
            InitializeComponent();
            _ccService = ccService;
            LoadCategories();
        }

        // Load Categories from API
        private async void LoadCategories()
        {
            try
            {
                var categories = await _ccService.GetCategoriesAsync();
                CategoryCollectionView.ItemsSource = categories.ToList();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        // Refresh Categories when button clicked
        private void OnRefreshClicked(object sender, EventArgs e)
        {
            LoadCategories();
        }

        // Handle Edit button click
        private async void OnEditClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var category = button?.BindingContext as category;

            if (category != null)
            {
                // Navigate to EditCategoryPage (You will need to implement this page)
                await Navigation.PushAsync(new EditCategoryPage(category));
            }
        }

        // Handle Delete button click
        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var category = button?.BindingContext as category;

            if (category != null)
            {
                bool confirm = await DisplayAlert("Confirm", "Are you sure you want to delete this category?", "Yes", "No");
                if (confirm)
                {
                    try
                    {
                        await _ccService.DeleteCategoryAsync(category.categoryId);
                        LoadCategories(); // Reload list after deletion
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Error", ex.Message, "OK");
                    }
                }
            }
        }
    }
}
