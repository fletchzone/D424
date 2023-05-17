using C971.Database;
using C971.Models;
using C971.Views;
using System;
using Xamarin.Forms;

namespace C971
{
    public partial class App : Application
    {
        public App()
        {
            InitializeData();
            InitializeComponent();
            Globals.startupNotifications();

            MainPage = new NavigationPage(new TermsMainPage())
            {
                BarBackgroundColor = Color.FromHex("#002F51")
            };
        }

        private void InitializeData()
        {
            SqliteDataService db = new SqliteDataService();
            bool addData = db.Initialize();

            if (addData)
            {
                DateTime beginningMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                db.AddTerm(new Term { Title = "Spring", StartDate = beginningMonth, EndDate = beginningMonth.AddMonths(4).AddDays(-1) });
                db.AddCourse(new Course
                {
                    TermId = 1,
                    Title = "Underwater Basket Weaving",                    
                    Status = "In Progress",
                    Notes = "This is a hot note",
                    EnableNotifications = true,
                    InstructorName = "Chris Fletcher",
                    InstructorPhone = "503-545-2542",
                    InstructorEmail = "cfle103@wgu.edu", 
                    StartDate = beginningMonth,
                    EndDate = beginningMonth.AddMonths(1).AddDays(-1)
                });
                db.AddAssessment(new Assessment
                {
                    CourseId = 1,
                    Title = "Objective Assessment",
                    EnableNotifications = true,
                    Type = "Objective",
                    StartDate = beginningMonth.AddMonths(1).AddDays(-1),
                    EndDate = beginningMonth.AddMonths(1),
                });
                db.AddAssessment(new Assessment
                {
                    CourseId = 1,
                    Title = "Performance Assessment",
                    EnableNotifications = true,
                    Type = "Performance",                
                    StartDate = beginningMonth.AddMonths(1).AddDays(-1),
                    EndDate = beginningMonth.AddMonths(1),

                });

            }
        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
