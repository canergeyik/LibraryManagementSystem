using System.Collections.Generic;
using System.Data;
using System.Windows;
using static LibraryManagementSystem.DbOperations;
using static LibraryManagementSystem.HelperMethods;
using static LibraryManagementSystem.MainWindow;

namespace LibraryManagementSystem
{
    internal class AdminTeacherRankUpgrade
    {

        public static void AdminTeacherTable() //Loading the datagrid for users that who wants to get a permission to be teacher
        {
            List<string> keys = new List<string>();
            List<string> parameters = new List<string>();
            string srQuery = "select username,email from [Users] where userRank= 'Teacher(Pending)'";
            DataTable dt = db_Parameterized_Select_DataTable(srQuery, keys, parameters);
            RefreshDataGrid(dt, main.AdminTeachersDataGrid);
        }
        public static void AdminRankUpgradeButton()
        {
            if ((DataRowView)main.AdminTeachersDataGrid.SelectedItem != null)
            {
                string[] rowValues = new string[2];

                for (int i = 0; i < 2; i++)
                {
                    rowValues[i] = ((DataRowView)main.AdminTeachersDataGrid.SelectedItem).Row.ItemArray[i].ToString();
                }
                List<string> keys = new List<string>() { "@username" };
                List<string> parameters = new List<string>() { rowValues[0] };
                string srQuery = $"update [Users] set userRank='Teacher' where username =@username";
                db_Parameterized_Update_Delete_Insert(srQuery, keys, parameters);
            }
            else
                MessageBox.Show("Please select a user first");

        }
        public static void TeacherSearch() //Searching in datagrid
        {
            List<string> keys = new List<string>() { "@username" };
            List<string> parameters = new List<string>() { "%" + main.AdminTeacherSearch.Text.ToString() + "%" };
            string srQuery = "select username,email from [Users] where username like ";
            DataTable dt = db_Parameterized_Select_DataTable(srQuery + " @username " + "and userRank='Teacher(Pending)'", keys, parameters);
            RefreshDataGrid(dt, main.AdminTeachersDataGrid);
        }
    }
}
