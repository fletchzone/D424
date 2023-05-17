using C971.Models;
using C971.Resources;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace C971.Views
{    //edsnider. (2023, March 20). Github Local Notifications Plugin for Xamarin and Windows. Retrieved from Github localnotificationsplugin: Repo: https://github.com/edsnider/LocalNotificationsPlugin.
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AssessmentsPage : ContentPage
    {
        public Course SelectedCourse { get; set; }
        public AssessmentsPage(Course course)
        {
            Globals.initializeAssessmentCollection(course.Id);
            InitializeComponent();
            SelectedCourse = course;
            navTitle.Text = $"{course.Title} Assessments";
        }

        private async void AddAssessment_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (Globals.Assessments.Count >= 2)
                {
                    throw new Exception(AppResource.AssessmentType2Max);
                }
                await Navigation.PushModalAsync(new AssessmentConstructPage(SelectedCourse.Id));
            }
            catch (Exception error)
            {
                await DisplayAlert("Alert", $"{error.Message}", "OK");
            }
        }

        private async void Assessment_Clicked(object sender, EventArgs e)
        {
            BindableObject layout = (BindableObject)sender;
            Assessment assessment = (Assessment)layout.BindingContext;
            await Navigation.PushAsync(new AssessmentPage(assessment));
        }
    }
}