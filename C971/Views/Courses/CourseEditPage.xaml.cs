using C971.Models;
using C971.Resources;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace C971.Views
{    //edsnider. (2023, March 20). Github Local Notifications Plugin for Xamarin and Windows. Retrieved from Github localnotificationsplugin: Repo: https://github.com/edsnider/LocalNotificationsPlugin.
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CourseConstructPage : ContentPage
    {
        private int TermID;
        private CoursePage CoursePage;
        public CourseConstructPage(int termId)
        {
            InitializeComponent();
            TermID = termId;
            SaveEditButton.IsVisible = false;
        }

        public CourseConstructPage(CoursePage coursePage)
        {
            InitializeComponent();
            CoursePage = coursePage;
            Course course = coursePage.SelectedCourse;
            TermID = course.TermId;
            courseTitle.Text = course.Title;
            startDateSelected.Date = course.StartDate;
            endDateSelected.Date = course.EndDate;
            statusPicker.SelectedItem = course.Status;
            instructorName.Text = course.InstructorName;
            instructorPhone.Text = course.InstructorPhone;
            instructorEmail.Text = course.InstructorEmail;
            courseNotes.Text = course.Notes;
            notificationSwitch.IsToggled = course.EnableNotifications;
            SaveButton.IsVisible = false;
        }


        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                bool emailInvalid = isInvalidEmail(instructorEmail.Text);
                if (courseTitle.Text == null || courseTitle.Text == "")
                {
                    throw new Exception(AppResource.CourseTitleRequired);
                }

                if (new DateTime(startDateSelected.Date.Year, startDateSelected.Date.Month, startDateSelected.Date.Day) > new DateTime(endDateSelected.Date.Year, endDateSelected.Date.Month, endDateSelected.Date.Day))
                {
                    throw new Exception(AppResource.StartDateEndDateOrder);
                }

                if (statusPicker.SelectedItem == null)
                {
                    throw new Exception(AppResource.CourseStatusRequired);
                }

                if (
                        instructorName.Text == null || instructorName.Text == "" ||
                        instructorPhone.Text == null || instructorPhone.Text == "" ||
                        instructorEmail.Text == null || instructorEmail.Text == ""
                    )
                {
                    throw new Exception(AppResource.CourseInstrRequired);
                }

                if (emailInvalid)
                {
                    throw new Exception(AppResource.EmailRequired);
                }

                if (courseNotes.Text == null)
                {
                    courseNotes.Text = "";
                }

                Course newCourse = new Course
                {
                    TermId = TermID,
                    Title = courseTitle.Text,
                    StartDate = startDateSelected.Date,
                    EndDate = endDateSelected.Date,
                    Status = statusPicker.SelectedItem.ToString(),
                    InstructorName = instructorName.Text,
                    InstructorPhone = instructorPhone.Text,
                    InstructorEmail = instructorEmail.Text,
                    Notes = courseNotes.Text,
                    EnableNotifications = notificationSwitch.IsToggled
                };
                Globals.addCourseToCourseCollection(newCourse);
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
                bool emailInvalid = isInvalidEmail(instructorEmail.Text);
                if (courseTitle.Text == "")
                {
                    throw new Exception(AppResource.CourseTitleRequired);
                }

                if (new DateTime(startDateSelected.Date.Year, startDateSelected.Date.Month, startDateSelected.Date.Day) > new DateTime(endDateSelected.Date.Year, endDateSelected.Date.Month, endDateSelected.Date.Day))
                {
                    throw new Exception(AppResource.StartDateEndDateOrder);
                }

                if (instructorName.Text == "" || instructorPhone.Text == "" || instructorEmail.Text == "")
                {
                    throw new Exception(AppResource.CourseInstrRequired);
                }

                if (emailInvalid)
                {
                    throw new Exception(AppResource.EmailRequired);
                }

                CoursePage coursePage = CoursePage;
                Course newCourse = new Course
                {
                    Id = coursePage.SelectedCourse.Id,
                    TermId = TermID,
                    Title = courseTitle.Text,
                    StartDate = startDateSelected.Date,
                    EndDate = endDateSelected.Date,
                    Status = statusPicker.SelectedItem.ToString(),
                    InstructorName = instructorName.Text,
                    InstructorPhone = instructorPhone.Text,
                    InstructorEmail = instructorEmail.Text,
                    Notes = courseNotes.Text,
                    EnableNotifications = notificationSwitch.IsToggled
                };
                Globals.updateCourseInCourseCollection(coursePage.SelectedCourse, newCourse);
                coursePage.InitData(newCourse);
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

        private bool isInvalidEmail(string email)
        {
            try
            {
                System.Net.Mail.MailAddress addr = new System.Net.Mail.MailAddress(email);
                return !(addr.Address == email);
            }
            catch
            {
                return true;
            }
        }
    }
}