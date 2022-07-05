using System.Collections.Generic;
using System.Data;
using static LibraryManagementSystem.DbOperations;
using static LibraryManagementSystem.HelperMethods;
using static LibraryManagementSystem.MainWindow;
namespace LibraryManagementSystem
{
    internal class AdminUsersProcess
    {
        public static void AdminUsersTable()
        {
            List<string> keys = new List<string>();
            List<string> parameters = new List<string>();
            string srQuery = "select username,email,userRank from [Users]";
            DataTable dt = db_Parameterized_Select_DataTable(srQuery, keys, parameters);
            RefreshDataGrid(dt, main.AdminUsersDataGrid);
        }
        public static void UserSearch() //Searching in datagrid
        {
            List<string> keys = new List<string>() { "@username" };
            List<string> parameters = new List<string>() { "%" + main.AdminUserSearch.Text.ToString() + "%" };
            string srQuery = "select username,email,userRank from [Users] where username like ";
            DataTable dt = db_Parameterized_Select_DataTable(srQuery + " @username", keys, parameters);
            RefreshDataGrid(dt, main.AdminUsersDataGrid);
        }
    }
}
