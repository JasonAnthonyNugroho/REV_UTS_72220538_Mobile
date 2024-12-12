using REV_UTS_72220538_Mobile.Data;

namespace REV_UTS_72220538_Mobile.Pages;

public partial class InstructorList : ContentPage
{
    private readonly ccService _ccService;
    public InstructorList()
	{
        InitializeComponent();
        LoadInstructors();
    }
    private async void LoadInstructors()
    {
        var instructors = await _ccService.GetInstructorsAsync();
        InstructorsListView.ItemsSource = instructors;
    }

    private async void OnInstructorSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is Instructor selectedInstructor)
        {
            await Navigation.PushAsync(new CourseSelectionPage(_ccService, selectedInstructor));
        }
    }
}