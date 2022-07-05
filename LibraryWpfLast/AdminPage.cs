using System.Collections.Generic;
using System.Data;
using static LibraryManagementSystem.DbOperations;
using static LibraryManagementSystem.MainWindow;

namespace LibraryManagementSystem
{
    internal class AdminPage
    {
        public static void AdminPageNotifications() //Reminder for admin if there is any requests from users
        {
            List<string> key = new List<string>();
            List<string> parameter = new List<string>();
            string srQuery = "select * from PendingBooks";
            DataTable dt = db_Parameterized_Select_DataTable(srQuery, key, parameter);
            main.ReceivedBookCount.Content = dt.Rows.Count;
            srQuery = "select * from Users where userRank='Teacher(Pending)'";
            dt = db_Parameterized_Select_DataTable(srQuery, key, parameter);
            main.TeacherRequestCount.Content = dt.Rows.Count;
        }
    }
}
