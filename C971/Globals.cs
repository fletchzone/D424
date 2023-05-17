using C971.Database;
using C971.Models;
using Plugin.LocalNotifications;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace C971
{
    public static class Globals
    {
        public static ObservableCollection<Term> Terms = new ObservableCollection<Term>();
        public static ObservableCollection<Course> Courses = new ObservableCollection<Course>();
        public static ObservableCollection<Assessment> Assessments = new ObservableCollection<Assessment>();

        public static void startupNotifications()
        {
            DateTime today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            SqliteDataService database = new SqliteDataService();
            database.Initialize();
            System.Collections.Generic.List<Course> courses = database.GetAllCourses();
            System.Collections.Generic.List<Assessment> assessments = database.GetAllAssessments();
            courses.ForEach(course =>
            {
                if (course.EnableNotifications && new DateTime(course.StartDate.Year, course.StartDate.Month, course.StartDate.Day) == today)
                {
                    CrossLocalNotifications.Current.Show("Course Start", $"{course.Title} is starting today");
                }
                if (course.EnableNotifications && new DateTime(course.EndDate.Year, course.EndDate.Month, course.EndDate.Day) == today)
                {
                    CrossLocalNotifications.Current.Show("Course End", $"{course.Title} is ending today");
                }
            });
            assessments.ForEach(test =>
            {
                if (test.EnableNotifications && new DateTime(test.StartDate.Year, test.StartDate.Month, test.StartDate.Day) == today)
                {
                    CrossLocalNotifications.Current.Show("Assessment Start", $"{test.Title} due date start is today");
                }
                if (test.EnableNotifications && new DateTime(test.EndDate.Year, test.EndDate.Month, test.EndDate.Day) == today)
                {
                    CrossLocalNotifications.Current.Show("Assessment End", $"{test.Title} due date end is today");
                }
            });
            database.Close();
        }

        public static void initializeTermsCollection()
        {
            SqliteDataService database = new SqliteDataService();
            database.Initialize();
            System.Collections.Generic.List<Term> terms = database.GetAllTerms();
            terms.ForEach(term => Terms.Add(term));
            database.Close();
        }

        public static void addTermToTermCollection(Term term)
        {
            SqliteDataService database = new SqliteDataService();
            database.Initialize();
            database.AddTerm(term);
            Terms.Add(term);
            database.Close();
        }

        public static void updateTermInTermCollection(Term oldTerm, Term newTerm)
        {
            System.Collections.Generic.List<Term> termList = Terms.ToList();
            Terms.Clear();

            SqliteDataService database = new SqliteDataService();
            database.Initialize();
            database.UpdateTerm(newTerm);

            int indexFound = termList.IndexOf(oldTerm);
            termList.RemoveAt(indexFound);
            termList.Insert(indexFound, newTerm);
            termList.ForEach(term => Terms.Add(term));

            database.Close();
        }

        public static void deleteTermFromTermCollection(Term term)
        {
            SqliteDataService database = new SqliteDataService();
            database.Initialize();
            database.DeleteTerm(term);
            Terms.Remove(term);
            database.Close();
        }

        public static void initializeCoursesCollection(int termId)
        {
            Courses.Clear();
            SqliteDataService database = new SqliteDataService();
            database.Initialize();
            System.Collections.Generic.List<Course> courses = database.GetCoursesByTermId(termId);
            courses.ForEach(course => Courses.Add(course));
            database.Close();
        }

        public static void addCourseToCourseCollection(Course course)
        {
            SqliteDataService database = new SqliteDataService();
            database.Initialize();
            database.AddCourse(course);
            Courses.Add(course);
            database.Close();
        }

        public static void updateCourseInCourseCollection(Course oldCourse, Course newCourse)
        {
            System.Collections.Generic.List<Course> courseList = Courses.ToList();
            Courses.Clear();

            SqliteDataService database = new SqliteDataService();
            database.Initialize();
            database.UpdateCourse(newCourse);

            int indexFound = courseList.IndexOf(oldCourse);
            courseList.RemoveAt(indexFound);
            courseList.Insert(indexFound, newCourse);
            courseList.ForEach(course => Courses.Add(course));

            database.Close();
        }

        public static void deleteCourseFromCourseCollection(Course course)
        {
            SqliteDataService database = new SqliteDataService();
            database.Initialize();
            database.DeleteCourse(course);
            Courses.Remove(course);
            database.Close();
        }

        public static void initializeAssessmentCollection(int courseId)
        {
            Assessments.Clear();
            SqliteDataService database = new SqliteDataService();
            database.Initialize();
            System.Collections.Generic.List<Assessment> courses = database.GetAssessmentsByCourseId(courseId);
            courses.ForEach(assessment => Assessments.Add(assessment));
            database.Close();
        }

        public static void addAssessmentToAssessmentCollection(Assessment assessment)
        {
            SqliteDataService database = new SqliteDataService();
            database.Initialize();
            database.AddAssessment(assessment);
            Assessments.Add(assessment);
            database.Close();
        }

        public static void updateAssessmentInAssessmentCollection(Assessment oldAssessment, Assessment newAssessment)
        {
            System.Collections.Generic.List<Assessment> assessmentList = Assessments.ToList();
            Assessments.Clear();

            SqliteDataService database = new SqliteDataService();
            database.Initialize();
            database.UpdateAssessment(newAssessment);

            int indexFound = assessmentList.IndexOf(oldAssessment);
            assessmentList.RemoveAt(indexFound);
            assessmentList.Insert(indexFound, newAssessment);
            assessmentList.ForEach(assessment => Assessments.Add(assessment));

            database.Close();
        }

        public static void deleteAssessmentFromAssessmentCollection(Assessment assessment)
        {
            SqliteDataService database = new SqliteDataService();
            database.Initialize();
            database.DeleteAssessment(assessment);
            Assessments.Remove(assessment);
            database.Close();
        }
    }
}
