using REV_UTS_72220538_Mobile.Data;

namespace REV_UTS_72220538_Mobile.Pages;

public partial class CourseSelectionPage : ContentPage
{
    private readonly ccService _ccService;
    private readonly Instructor _instructor;
    public CourseSelectionPage(ccService ccService, Instructor instructor)
    {
        InitializeComponent();
        _ccService = ccService;
        _instructor = instructor;
        LoadCourses();
    }

    private async void LoadCourses()
    {
        var courses = await _ccService.GetCoursesAsync();
        CoursesListView.ItemsSource = courses;
    }

    private async void OnCourseSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is course selectedCourse)
        {
            var enrollment = new Enrollment
            {
                InstructorId = _instructor.InstructorId,
                CourseId = selectedCourse.courseId
            };

            var success = await _ccService.AddEnrollmentAsync(enrollment);
            if (success)
            {
                await DisplayAlert("Success", "Instructor assigned to course.", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "Failed to assign instructor.", "OK");
            }
        }
    }
}