using System.Collections.Generic;
using static LibraryManagementSystem.DbOperations;
using static LibraryManagementSystem.MainWindow;
using static LibraryManagementSystem.HelperMethods;
using System.Data;
using System.Windows;

namespace LibraryManagementSystem
{
    internal class LibraryIssueBookProcess
    {
        public static void IssueBookTable() //Loading the datagrid
        {
            List<string> key = new List<string>() { "@username" };
            List<string> parameter = new List<string>() { main.txtLoginUsername.Text };
            DataTable dt = db_Parameterized_Select_DataTable("select * from [ReceivedBooks] where whoReceived=@username", key, parameter);
            RefreshDataGrid(dt, main.LibraryIssueDataGrid);
        }
        public static void IssueBookButton()
        {
            if (main.LibraryIssueDataGrid.SelectedItem != null)
            {
                string[] rowValues = new string[5]; //getting row values
                List<string> keys = new List<string>() {"@bookName","@bookAuthor","@bookReceived","@bookExpiration","@whoReceived" };
                List<string> parameters = new List<string>() { };

                for (int i = 0; i < 5; i++)
                {
                    rowValues[i] = ((DataRowView)main.LibraryIssueDataGrid.SelectedItem).Row.ItemArray[i].ToString();
                    parameters.Add(rowValues[i]);
                }
                string srDeleteQuery = "delete from [ReceivedBooks] where bookName=@bookName and bookAuthor=@bookAuthor and bookReceived=@bookReceived and bookExpiration=@bookExpiration and whoReceived=@whoReceived";
                db_Parameterized_Update_Delete_Insert(srDeleteQuery, keys, parameters);
                string srQuantityQuery = $"update [Books] set bookQuantity=bookQuantity+1 where bookName='{rowValues[0]}'"; //Didn't use the parameterized because there is no user input
                keys.Clear();
                parameters.Clear();
                db_Parameterized_Update_Delete_Insert(srQuantityQuery, keys, parameters);
                IssueBookTable();
            }
            else
                MessageBox.Show("Select a book first");
        }
    }
}
