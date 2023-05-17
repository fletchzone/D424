using C971.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace C971.Views
{
    //edsnider. (2023, March 20). Github Local Notifications Plugin for Xamarin and Windows. Retrieved from Github localnotificationsplugin: Repo: https://github.com/edsnider/LocalNotificationsPlugin.

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermsMainPage : ContentPage
    {
        public TermsMainPage()
        {
            Globals.initializeTermsCollection();
            InitializeComponent();
        }

        private async void AddTerm_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new TermConstructPage());
        }

        private async void Term_Clicked(object sender, EventArgs e)
        {
            BindableObject layout = (BindableObject)sender;
            Term term = (Term)layout.BindingContext;
            await Navigation.PushAsync(new TermPage(term));
        }
    }
}