using Microsoft.OData.Edm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using static LibraryManagementSystem.DbOperations;
using static LibraryManagementSystem.HelperMethods;
using static LibraryManagementSystem.MainWindow;

namespace LibraryManagementSystem
{
    internal class AdminLibraryManagement
    {
        public static List<System.Windows.Controls.TextBox> txtBoxes = new List<System.Windows.Controls.TextBox> { main.txtAdminLibraryBookName, main.txtAdminLibraryBookAuthor, main.txtAdminLibraryBookCategory, main.txtAdminLibraryBookYear, main.txtAdminLibraryBookPages, main.txtAdminLibraryBookQuantity };

        public static void AdminLibraryDataGrid() //Loading Books to the data grid
        {
            List<string> keys = new List<string>();
            List<string> parameters = new List<string>();
            string srQuery = "select * from [Books]";
            DataTable dt = db_Parameterized_Select_DataTable(srQuery, keys, parameters);
            RefreshDataGrid(dt, main.AdminLibraryDataGrid);
        }
        public static void SelectedRow() //Fill the information labels about book with seperating the selected row to in ItemArray 
        {
            string[] rowValues = new string[6]; // Declare an array for the row's cell values
            if ((DataRowView)main.AdminLibraryDataGrid.SelectedItem != null)
            {
                for (int i = 0; i < 6; i++)
                {
                    rowValues[i] = ((DataRowView)main.AdminLibraryDataGrid.SelectedItem).Row.ItemArray[i].ToString();
                    txtBoxes[i].Text = rowValues[i];
                }
            }
            else // Emptying the txtboxes to avoid crash
            {
                for (int i = 0; i < 6; i++)
                {
                    txtBoxes[i].Text = null;
                }
            }
        }
        public static void AdminLibraryAddButton() //Adding a book to Library
        {
            List<string> keys = new List<string>() { "@bookName" };
            List<string> parameters = new List<string>() { txtBoxes[0].Text };
            string srQuery = "select * from [Books] where bookName=@bookName";
            DataTable dt = db_Parameterized_Select_DataTable(srQuery, keys, parameters);
            if (dt.Rows.Count == 0)//Checking if there is a book with same name in database
            {
                if (ValidationCheck() == true) //Checking the textboxes are filled in correct format
                {
                    for (int i = 1; i < 6; i++)
                    {
                        parameters.Add(txtBoxes[i].Text.ToString());
                    }
                    Convert.ToDateTime(parameters[3]);
                    parameters[3] = String.Format("{0:yyyy-MM-dd}", parameters[3]); //Formatting the date to append to database
                    keys = new List<string>() { "@bookName", "@bookAuthor", "@bookCategory", "@bookYear", "@bookPages", "@bookQuantity" };
                    string srQueryInsert = "insert into [Books] (bookName,bookAuthor,bookCategory,bookYear,bookPages,bookQuantity) values(@bookName,@bookAuthor,@bookCategory,@bookYear,@bookPages,@bookQuantity)";
                    db_Parameterized_Update_Delete_Insert(srQueryInsert, keys, parameters);
                }
            }
            else
                MessageBox.Show("This book exist in library");
        }
        public static void AdminLibraryUpdateButton()
        {
            List<string> keys = new List<string>() { "@bookName" };
            List<string> parameters = new List<string>() { txtBoxes[0].Text.ToString(), };
            string srQuery = "select * from [Books] where bookName=@bookName";
            DataTable dt = db_Parameterized_Select_DataTable(srQuery, keys, parameters);
            if (dt.Rows.Count != 0)// if there is a book to edit informations about 
            {
                if (ValidationCheck() == true)//Checking the textboxes are filled in correct format
                {
                    for (int i = 1; i < 6; i++) //Adding the book information that we will make changes about to parameters as a string
                    {
                        parameters.Add(txtBoxes[i].Text.ToString());
                    }
                    Convert.ToDateTime(parameters[3]);
                    parameters[3] = String.Format("{0:yyyy-MM-dd}", parameters[3]); //Formatting the date to append to database
                    parameters.Add(txtBoxes[0].Text.ToString());
                    keys = new List<string>() { "@bookName", "@bookAuthor", "@bookCategory", "@bookYear", "@bookPages", "@bookQuantity", "@bookNameSearch" };
                    string srQueryInsert = "update [Books] set bookName=@bookName,bookAuthor=@bookAuthor,bookCategory=@bookCategory,bookYear=@bookYear,bookPages=@bookPages, bookQuantity=@bookQuantity where bookname=@bookNameSearch";
                    db_Parameterized_Update_Delete_Insert(srQueryInsert, keys, parameters);
                }
            }
            else
                MessageBox.Show("This book doesn't exist in library");
        }
        public static bool ValidationCheck() //Checking the textboxes are filled in correct format
        {
            if (txtBoxes[1].Text.All(char.IsDigit) == true)
            {
                MessageBox.Show("Author name can not contain number");
                return false;
            }

            if (txtBoxes[2].Text.ToString().All(char.IsDigit) == true)
            {
                MessageBox.Show("Category name can not contain number");
                return false;
            }
            if (Date.TryParse(txtBoxes[3].Text.ToString(), out Date result4) == false)
            {
                MessageBox.Show("Your date format has to be YYYY-MM-DD");
                return false;
            }
            if (int.TryParse(txtBoxes[4].Text.ToString().Trim(), out int result5) == false)
            {
                MessageBox.Show("Your page has to be number");
                return false;
            }

            if (int.TryParse(txtBoxes[5].Text.ToString().Trim(), out int result6) == false)
            {
                MessageBox.Show("Your Quantity has to be number");
                return false;
            }
            return true;
        }
        public static void AdminLibrarySearch() //Searching books in datagrid
        {
            List<string> keys = new List<string>() { "@bookName" };
            List<string> parameters = new List<string>() { "%" + main.AdminLibrarySearch.Text.ToString() + "%" };
            string srQuery = "select * from [Books] where bookName like ";
            DataTable dt = db_Parameterized_Select_DataTable(srQuery + " @bookName ", keys, parameters);
            RefreshDataGrid(dt, main.AdminLibraryDataGrid);
        }

    }
}
