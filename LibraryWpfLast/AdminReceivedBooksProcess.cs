using System.Collections.Generic;
using System.Data;
using System.Windows;
using static LibraryManagementSystem.DbOperations;
using static LibraryManagementSystem.HelperMethods;
using static LibraryManagementSystem.MainWindow;

namespace LibraryManagementSystem
{
    internal class AdminReceivedBooksProcess
    {
        public static void PendingBooksTable() //Loading the datagrid for Pending books
        {
            List<string> keys = new List<string>();
            List<string> parameters = new List<string>();
            string srQuery = "select * from [PendingBooks]";
            DataTable dt = db_Parameterized_Select_DataTable(srQuery, keys, parameters);
            RefreshDataGrid(dt, main.AdminBookPendDataGrid);
        }
        public static void ReceivedBooksTable() //Loading the datagrid for Received books
        {
            List<string> keys = new List<string>();
            List<string> parameters = new List<string>();
            string srQuery = "select * from [ReceivedBooks]";
            DataTable dt = db_Parameterized_Select_DataTable(srQuery, keys, parameters);
            RefreshDataGrid(dt, main.AdminReceivedDataGrid);
        }
        public static void AdminApprove() //Giving book process
        {
            if ((DataRowView)main.AdminBookPendDataGrid.SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Do you want to give permission to have this book to this user?", "Permission", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    List<string> keys = new List<string>() { "@bookName", "@bookAuthor", "@bookReceived", "@bookExpiration", "@whoReceived" };
                    List<string> parameters = new List<string>();
                    for (int i = 0; i < 5; i++)
                    {
                        parameters.Add(((DataRowView)main.AdminBookPendDataGrid.SelectedItem).Row.ItemArray[i].ToString());
                    }
                    string srQueryInsert = "insert into [ReceivedBooks] (bookName,bookAuthor,bookReceived,bookExpiration,whoReceived) values(@bookName,@bookAuthor,@bookReceived,@bookExpiration,@whoReceived) ";
                    db_Parameterized_Update_Delete_Insert(srQueryInsert, keys, parameters);
                    string srQueryDelete = "delete from [PendingBooks] where bookName=@bookName and whoReceived=@whoReceived ";
                    for (int i = 2; i < 4; i++) //Adjusting the keys and parameters
                    {
                        i--;
                        if (keys.Count != 2)
                        {
                            keys.RemoveAt(i);
                            parameters.RemoveAt(i);
                        }
                        else
                            break;
                    }
                    db_Parameterized_Update_Delete_Insert(srQueryDelete, keys, parameters);
                }
            }
            else
                MessageBox.Show("Please select a request first");
        }
        public static void PendingSearch() //Searching books in datagrid
        {
            List<string> keys = new List<string>() { "@bookName" };
            List<string> parameters = new List<string>() { "%" + main.AdminPendingBookSearch.Text.ToString() + "%" };
            string srQuery = "select * from [PendingBooks] where bookName like ";
            DataTable dt = db_Parameterized_Select_DataTable(srQuery + " @bookName ", keys, parameters);
            RefreshDataGrid(dt, main.AdminBookPendDataGrid);
        }
        public static void ReceivedSearch() //Searching books in datagrid
        {
            List<string> keys = new List<string>() { "@bookName" };
            List<string> parameters = new List<string>() { "%" + main.AdminReceivedBooksSearch.Text.ToString() + "%" };
            string srQuery = "select * from [ReceivedBooks] where bookName like ";
            DataTable dt = db_Parameterized_Select_DataTable(srQuery + "@bookName", keys, parameters);
            RefreshDataGrid(dt, main.AdminReceivedDataGrid);
        }
    }
}
