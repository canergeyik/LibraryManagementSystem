using System;
using System.Collections.Generic;
using System.Windows;
using static LibraryManagementSystem.DbOperations;
using static LibraryManagementSystem.MainWindow;


namespace LibraryManagementSystem
{
    internal class AdminSettingsProcess
    {
        public static void AdminSettingsRefresh() //Reminder for admin of what is the current settings
        {
            int day = AdminSettingsDb("select studentIssueDay from[adminSettings]");
            int bookCount = AdminSettingsDb("select studentBookCount from[adminSettings]");
            main.txtAdminMaxBook.Text = bookCount.ToString();
            main.txtAdminIssueDays.Text = day.ToString();
        }
        public static void AdminSettingsUpdate()  //Updating settings
        {
            if (Int32.TryParse(main.txtAdminIssueDays.Text, out int x) == true && Int32.TryParse(main.txtAdminMaxBook.Text, out int b) == true) //Checking the values are integer or not
            {
                List<string> keys = new List<string>() { "@studentIssueDay", "@studentBookCount" };
                List<string> parameters = new List<string>() { main.txtAdminIssueDays.Text, main.txtAdminMaxBook.Text };
                string srQuery = "update adminSettings set studentIssueDay=@studentIssueDay, studentBookCount=@studentBookCount";
                db_Parameterized_Update_Delete_Insert(srQuery, keys, parameters);
            }
            else
                MessageBox.Show("Settings must be integer");
        }
    }
}
