using C971.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
/*
 oysteinkrog. (2023, March 20). Github SQLite.Net-PCL. Retrieved from Github SQLite.Net-PCL Repo: https://github.com/oysteinkrog/SQLite.Net-PCL.
 */
namespace C971.Database
{
    public class SqliteDataService
    {
        private SQLiteConnection database;
        public bool Initialize()
        {
            bool dbTablesCreated = false;
            if (database == null)
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "C971Dbd.db3");
                database = new SQLiteConnection(dbPath);
            }

            CreateTablesResult create = database.CreateTables<Term, Course, Assessment>();

            foreach (KeyValuePair<Type, CreateTableResult> result in create.Results)
            {
                dbTablesCreated = dbTablesCreated || result.Value == CreateTableResult.Created;
            }

            return dbTablesCreated;
        }

        public void AddTerm(Term term)
        {
            database.Insert(term);
        }

        public List<Term> GetAllTerms()
        {
            return database.Table<Term>().ToList();
        }

        public List<Course> GetAllCourses()
        {
            return database.Table<Course>().ToList();
        }

        public List<Assessment> GetAllAssessments()
        {
            return database.Table<Assessment>().ToList();
        }

        public int UpdateTerm(Term term)
        {
            return database.Update(term);
        }

        public int DeleteTerm(Term term)
        {
            return database.Delete(term);
        }

        public void AddCourse(Course course)
        {
            database.Insert(course);
        }

        public List<Course> GetCoursesByTermId(int termId)
        {
            string query = $"SELECT * FROM course WHERE course.TermId={termId}";
            return database.Query<Course>(query);
        }

        public void Close()
        {
            database.Close();
        }

        public int UpdateCourse(Course course)
        {
            return database.Update(course);
        }

        public int DeleteCourse(Course course)
        {
            return database.Delete(course);
        }

        public void AddAssessment(Assessment assessment)
        {
            database.Insert(assessment);
        }

        public List<Assessment> GetAssessmentsByCourseId(int courseId)
        {
            string query = $"SELECT * FROM assessment WHERE assessment.CourseId={courseId}";
            return database.Query<Assessment>(query);
        }

        public int UpdateAssessment(Assessment assessment)
        {
            return database.Update(assessment);
        }

        public int DeleteAssessment(Assessment assessment)
        {
            return database.Delete(assessment);
        }
    }
}
