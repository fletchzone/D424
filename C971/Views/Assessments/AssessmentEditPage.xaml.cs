using C971.Models;
using C971.Resources;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace C971.Views
{
    //edsnider. (2023, March 20). Github Local Notifications Plugin for Xamarin and Windows. Retrieved from Github localnotificationsplugin: Repo: https://github.com/edsnider/LocalNotificationsPlugin.

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AssessmentConstructPage : ContentPage
    {
        private int CourseID;
        private Assessment SelectedAssessment;
        private AssessmentPage AssessmentPageRef;
        public AssessmentConstructPage(int courseId)
        {
            InitializeComponent();
            CourseID = courseId;
            SaveEditButton.IsVisible = false;
        }

        public AssessmentConstructPage(AssessmentPage assessmentPage, Assessment assessment)
        {
            InitializeComponent();
            CourseID = assessment.CourseId;
            AssessmentPageRef = assessmentPage;
            SelectedAssessment = assessment;
            assessmentTitle.Text = assessment.Title;
            startDateSelected.Date = assessment.StartDate;
            endDateSelected.Date = assessment.EndDate;
            typePicker.SelectedItem = assessment.Type;
            notificationSwitch.IsToggled = assessment.EnableNotifications;
            SaveButton.IsVisible = false;
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (assessmentTitle.Text == null || assessmentTitle.Text == "")
                {
                    throw new Exception(AppResource.AssessmentTitleRequired);
                }

                if (new DateTime(startDateSelected.Date.Year, startDateSelected.Date.Month, startDateSelected.Date.Day) > new DateTime(endDateSelected.Date.Year, endDateSelected.Date.Month, endDateSelected.Date.Day))
                {
                    throw new Exception(AppResource.StartDateEndDateOrder);
                }

                if (typePicker.SelectedItem == null)
                {
                    throw new Exception(AppResource.AssessmentTypeRequired);
                }

                if (Globals.Assessments.Any())
                {
                    if (Globals.Assessments.First().Type == typePicker.SelectedItem.ToString())
                    {
                        throw new Exception(AppResource.AssessmentType2Max);
                    }
                }

                Assessment newAssessment = new Assessment
                {
                    CourseId = CourseID,
                    Title = assessmentTitle.Text,
                    StartDate = startDateSelected.Date,
                    EndDate = endDateSelected.Date,
                    Type = typePicker.SelectedItem.ToString(),
                    EnableNotifications = notificationSwitch.IsToggled
                };
                Globals.addAssessmentToAssessmentCollection(newAssessment);
                await Navigation.PopModalAsync();
            }
            catch (Exception error)
            {
                await DisplayAlert("Alert", $"{error.Message}", "OK");
            }
        }

        private async void SaveEditButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (assessmentTitle.Text == "")
                {
                    throw new Exception(AppResource.AssessmentTitleRequired);
                }

                if (new DateTime(startDateSelected.Date.Year, startDateSelected.Date.Month, startDateSelected.Date.Day) > new DateTime(endDateSelected.Date.Year, endDateSelected.Date.Month, endDateSelected.Date.Day))
                {
                    throw new Exception(AppResource.StartDateEndDateOrder);
                }

                if (Globals.Assessments.Where(test => test.Id != SelectedAssessment.Id).First().Type == typePicker.SelectedItem.ToString())
                {
                    throw new Exception(AppResource.AssessmentType2Max);
                }

                Assessment newAssessment = new Assessment
                {
                    Id = SelectedAssessment.Id,
                    CourseId = CourseID,
                    Title = assessmentTitle.Text,
                    StartDate = startDateSelected.Date,
                    EndDate = endDateSelected.Date,
                    Type = typePicker.SelectedItem.ToString(),
                    EnableNotifications = notificationSwitch.IsToggled
                };
                Globals.updateAssessmentInAssessmentCollection(SelectedAssessment, newAssessment);
                AssessmentPageRef.InitData(newAssessment);
                await Navigation.PopModalAsync();
            }
            catch (Exception error)
            {
                await DisplayAlert("Alert", $"{error.Message}", "OK");
            }
        }

        private async void CancelButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}