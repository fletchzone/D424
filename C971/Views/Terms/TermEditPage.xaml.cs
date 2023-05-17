using C971.Models;
using C971.Resources;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace C971.Views
{
    //edsnider. (2023, March 20). Github Local Notifications Plugin for Xamarin and Windows. Retrieved from Github localnotificationsplugin: Repo: https://github.com/edsnider/LocalNotificationsPlugin.
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermConstructPage : ContentPage
    {
        private TermPage TermPage;
        public TermConstructPage()
        {
            InitializeComponent();
            SaveEditButton.IsVisible = false;
        }

        public TermConstructPage(TermPage termPage)
        {
            InitializeComponent();
            TermPage = termPage;
            termTitle.Text = termPage.SelectedTerm.Title;
            startDateSelected.Date = termPage.SelectedTerm.StartDate;
            endDateSelected.Date = termPage.SelectedTerm.EndDate;
            SaveButton.IsVisible = false;
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (termTitle.Text == null || termTitle.Text == "")
                {
                    throw new Exception(AppResource.TermTitleRequired);
                }

                if (new DateTime(startDateSelected.Date.Year, startDateSelected.Date.Month, startDateSelected.Date.Day) > new DateTime(endDateSelected.Date.Year, endDateSelected.Date.Month, endDateSelected.Date.Day))
                {
                    throw new Exception(AppResource.StartDateEndDateOrder);
                }

                Term newTerm = new Term { Title = termTitle.Text, StartDate = startDateSelected.Date, EndDate = endDateSelected.Date };
                Globals.addTermToTermCollection(newTerm);
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
                if (termTitle.Text == "")
                {
                    throw new Exception(AppResource.TermTitleRequired);
                }

                if (new DateTime(startDateSelected.Date.Year, startDateSelected.Date.Month, startDateSelected.Date.Day) > new DateTime(endDateSelected.Date.Year, endDateSelected.Date.Month, endDateSelected.Date.Day))
                {
                    throw new Exception(AppResource.StartDateEndDateOrder);
                }

                TermPage termPage = TermPage;
                Term newTerm = new Term { Id = termPage.SelectedTerm.Id, Title = termTitle.Text, StartDate = startDateSelected.Date, EndDate = endDateSelected.Date };
                Globals.updateTermInTermCollection(termPage.SelectedTerm, newTerm);
                termPage.InitData(newTerm);
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