using C971.Models;
using C971.Resources;
using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace C971.Views
{
    //edsnider. (2023, March 20). Github Local Notifications Plugin for Xamarin and Windows. Retrieved from Github localnotificationsplugin: Repo: https://github.com/edsnider/LocalNotificationsPlugin.
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermPage : ContentPage
    {
        public Term SelectedTerm { get; set; }
        public TermPage(Term term)
        {
            Globals.initializeCoursesCollection(term.Id);
            InitializeComponent();
            SelectedTerm = term;
            InitData(term);
        }

        public void InitData(Term term)
        {
            navTitle.Text = term.Title;
            TermDateRange.Text = $"{term.StartDate.ToString("MM-dd-yyyy", DateTimeFormatInfo.InvariantInfo)} - {term.EndDate.ToString("MM-dd-yyyy", DateTimeFormatInfo.InvariantInfo)}";
        }

        private async void AddCourse_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (Globals.Courses.Count >= 6)
                {
                    throw new Exception(AppResource.SixTermsMax);
                }

                await Navigation.PushModalAsync(new CourseConstructPage(SelectedTerm.Id));
            }
            catch (Exception error)
            {
                await DisplayAlert("Alert", $"{error.Message}", "OK");
            }
        }

        private async void EditTerm_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new TermConstructPage(this));
        }

        private async void DeleteTerm_Clicked(object sender, EventArgs e)
        {
            Globals.deleteTermFromTermCollection(SelectedTerm);
            await Navigation.PopAsync();
        }

        private async void Course_Clicked(object sender, EventArgs e)
        {
            BindableObject layout = (BindableObject)sender;
            Course course = (Course)layout.BindingContext;
            await Navigation.PushAsync(new CoursePage(course));
        }
    }
}