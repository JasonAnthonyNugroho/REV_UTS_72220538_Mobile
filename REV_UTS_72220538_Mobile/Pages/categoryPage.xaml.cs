using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using REV_UTS_72220538_Mobile.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace REV_UTS_72220538_Mobile.Pages;

public partial class categoryPage : ContentPage
{
    private readonly ccService _ccService;
    private List<category> _allCategories;


    public categoryPage()
    {
        InitializeComponent(); // This method should work now
        _ccService = new ccService(new HttpClient());
        LoadCategories();
    }

    // Load Categories from API
    private async void LoadCategories()
    {
        var categories = await _ccService.GetCategoriesAsync();
        _allCategories = categories.ToList(); // Store all categories for filtering
        CategoriesCollectionView.ItemsSource = _allCategories;

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
            await Navigation.PushAsync(new EditCategoryPage(category, _ccService));
        }
    }
    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        var searchText = e.NewTextValue?.ToLower();

        // Filter categories based on search text
        if (string.IsNullOrEmpty(searchText))
        {
            CategoriesCollectionView.ItemsSource = _allCategories;
        }
        else
        {
            var filteredCategories = _allCategories
                .Where(category => category.name.ToLower().Contains(searchText))
                .ToList();
            CategoriesCollectionView.ItemsSource = filteredCategories;
        }
    }

    private async void OnEditCategoryClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var selectedCategory = button?.CommandParameter as category;

            if (selectedCategory != null)
            {
                // Navigate to EditCategoryPage with the selected category
                await Navigation.PushAsync(new EditCategoryPage(selectedCategory, _ccService));
            }
            else
            {
                await DisplayAlert("Error", "No category selected for editing.", "OK");
            }
        }
    public async void OnAddCategory(object sender, EventArgs e) {
        await Navigation.PushAsync(new AddCategoryPage(_ccService));

    }
    // Handle Delete button click
    private async void OnDeleteCategoryClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var selectedCategory = button?.CommandParameter as category;

        if (selectedCategory != null)
        {
            bool confirm = await DisplayAlert("Confirm", "Are you sure you want to delete this category?", "Yes", "No");
            if (confirm)
            {
                try
                {
                    // Delete the category
                    await _ccService.DeleteCategoryAsync(selectedCategory.categoryId);
                    // Refresh the category list after deletion
                    LoadCategories();
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", ex.Message, "OK");
                }
            }
        }
        else
        {
            await DisplayAlert("Error", "Failed to identify the selected category for deletion.", "OK");
        }
    }



}
