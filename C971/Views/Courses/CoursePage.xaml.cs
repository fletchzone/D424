using C971.Models;
using System;
using System.Globalization;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace C971.Views
{
    //edsnider. (2023, March 20). Github Local Notifications Plugin for Xamarin and Windows. Retrieved from Github localnotificationsplugin: Repo: https://github.com/edsnider/LocalNotificationsPlugin.
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CoursePage : ContentPage
    {
        public Course SelectedCourse { get; set; }
        public CoursePage(Course course)
        {
            InitializeComponent();
            InitData(course);
        }

        public void InitData(Course course)
        {
            SelectedCourse = course;
            navTitle.Text = course.Title;
            CourseDateRange.Text = $"{course.StartDate.ToString("MM-dd-yyyy", DateTimeFormatInfo.InvariantInfo)} - {course.EndDate.ToString("MM-dd-yyyy", DateTimeFormatInfo.InvariantInfo)}";
            statusSelection.Text = course.Status;
            instructorsName.Text = course.InstructorName;
            instructorsPhone.Text = course.InstructorPhone;
            instructorsEmail.Text = course.InstructorEmail;
            notes.Text = course.Notes;
            notificationsEnabled.Text = course.EnableNotifications ? "ON" : "OFF";
        }

        private async void ShareNotes_Clicked(object sender, EventArgs e)
        {
            await Share.RequestAsync($"Notes from {navTitle.Text}:\n{notes.Text}");
        }

        private async void CourseAssessments_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AssessmentsPage(SelectedCourse));
        }

        private async void EditCourse_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new CourseConstructPage(this));
        }

        private async void DeleteCourse_Clicked(object sender, EventArgs e)
        {
            Globals.deleteCourseFromCourseCollection(SelectedCourse);
            await Navigation.PopAsync();
        }
    }
}