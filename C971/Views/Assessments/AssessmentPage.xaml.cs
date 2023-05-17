using C971.Models;
using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace C971.Views
{    //edsnider. (2023, March 20). Github Local Notifications Plugin for Xamarin and Windows. Retrieved from Github localnotificationsplugin: Repo: https://github.com/edsnider/LocalNotificationsPlugin.
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AssessmentPage : ContentPage
    {
        private Assessment SelectedAssessment;
        public AssessmentPage(Assessment assessment)
        {
            InitializeComponent();
            SelectedAssessment = assessment;
            InitData(assessment);
        }

        public void InitData(Assessment assessment)
        {
            navTitle.Text = assessment.Title;
            AssessmentDateRange.Text = $"{assessment.StartDate.ToString("MM-dd-yyyy", DateTimeFormatInfo.InvariantInfo)} - {assessment.EndDate.ToString("MM-dd-yyyy", DateTimeFormatInfo.InvariantInfo)}";
            TypeSelection.Text = assessment.Type;
            notificationsEnabled.Text = assessment.EnableNotifications ? "ON" : "OFF";
        }

        private async void EditAssessment_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AssessmentConstructPage(this, SelectedAssessment));
        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            Globals.deleteAssessmentFromAssessmentCollection(SelectedAssessment);
            await Navigation.PopAsync();
        }
    }
}