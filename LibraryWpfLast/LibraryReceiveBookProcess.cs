using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using static LibraryManagementSystem.DbOperations;
using static LibraryManagementSystem.MainWindow;
using static LibraryManagementSystem.HelperMethods;


namespace LibraryManagementSystem
{
    internal class LibraryReceiveBookProcess
    {
        public static void ReceiveBook()
        {
            if (main.LibraryReceiveDataGrid.SelectedItem != null)
            {
                DateTime receivedDate = DateTime.Now;
                string formattedReceived = String.Format("{0:yyyy-MM-dd}", receivedDate);
                if (main.lblProfileRank.Content.ToString() == "Student" || main.lblProfileRank.Content.ToString() == "Teacher(Pending)")
                {
                    if (BookQuantityCheck() == true && IsBookAlreadyTaken() == true && StudentBookCountCheck() == true)
                    {
                        int day = AdminSettingsDb("select studentIssueDay from[adminSettings]");
                        DateTime expirationDate = receivedDate.AddDays(day);
                        string formattedExpirationDate = String.Format("{0:yyyy-MM-dd}", expirationDate).ToString();
                        string srQueryInsert = "insert into [PendingBooks] (bookName,bookAuthor,bookReceived,bookExpiration,whoReceived) values(@bookName,@bookAuthor,@bookReceived,@bookExpiration,@whoReceived) ";
                        List<string> insertKeys = new List<string>() { "@bookName", "@bookAuthor", "@bookReceived", "@bookExpiration", "@whoReceived" };
                        List<string> insertParameters = new List<string>() { main.lblBookName.Content.ToString(), main.lblBookAuthor.Content.ToString(), formattedReceived.ToString(), formattedExpirationDate.ToString(), main.lblProfileName.Content.ToString() };
                        db_Parameterized_Update_Delete_Insert(srQueryInsert, insertKeys, insertParameters);
                        updateDeleteInsert($"update [Books] set bookQuantity=bookQuantity-1 where bookName='{main.lblBookName.Content}'");
                        MessageBox.Show("Your request has been sent");
                        LibraryInform();
                    }
                }
                else
                {
                    if (BookQuantityCheck() == true && IsBookAlreadyTaken() == true)
                    {
                        string srQueryInsert = "insert into [PendingBooks] (bookName,bookAuthor,bookReceived,whoReceived) values(@bookName,@bookAuthor,@bookReceived,@whoReceived) ";
                        List<string> insertKeys = new List<string>() { "@bookName", "@bookAuthor", "@bookReceived", "@whoReceived" };
                        List<string> insertParameters = new List<string>() { main.lblBookName.Content.ToString(), main.lblBookAuthor.Content.ToString(), formattedReceived.ToString(), main.lblProfileName.Content.ToString() };
                        db_Parameterized_Update_Delete_Insert(srQueryInsert, insertKeys, insertParameters);
                        MessageBox.Show("Your request has been sent");
                        updateDeleteInsert($"update [Books] set bookQuantity=bookQuantity-1 where bookName='{main.lblBookName.Content}'");
                    }
                }
            }
            else
                MessageBox.Show("Select a book First");
        }
        public static void LibrarySearchBook()
        {
            List<string> keys = new List<string>() { };
            List<string> parameters = new List<string>() { };
            string srQuery = "select * from [Books] where ";
            DataTable dt = new DataTable();
            if (main.LibraryReceiveExistCheck.IsChecked == true)
            {
                switch (main.cmbReceiveSearchFilter.SelectedItem)
                {
                    case "All":
                        keys.Add("@bookName");
                        keys.Add("@bookAuthor");
                        keys.Add("@bookCategory");
                        for (int i = 0; i < 3; i++)
                        {
                            parameters.Add("%"+main.txtLibraryReceiveSearch.Text+"%");
                        }
                        dt =db_Parameterized_Select_DataTable(srQuery + " bookName like " + "@bookName"+" or "+ "bookAuthor like " + "@bookAuthor"+ " or "+ "bookCategory like "  + "@bookCategory" +" and bookQuantity>0", keys, parameters);
                        RefreshDataGrid(dt,main.LibraryReceiveDataGrid);
                        break;
                    case "Name":
                        keys.Add("@bookName");
                        parameters.Add("%"+main.txtLibraryReceiveSearch.Text+"%");
                        dt=db_Parameterized_Select_DataTable(srQuery + " bookName like " + "@bookName "+" and bookQuantity>0", keys, parameters);
                        RefreshDataGrid(dt, main.LibraryReceiveDataGrid);
                        break;
                    case "Author":
                        keys.Add("@bookAuthor");
                        parameters.Add("%" + main.txtLibraryReceiveSearch.Text + "%");
                        dt=db_Parameterized_Select_DataTable(srQuery + " bookAuthor like "  + "@bookAuthor" +" and bookQuantity>0", keys, parameters);
                        RefreshDataGrid(dt, main.LibraryReceiveDataGrid);

                        break;
                    case "Category":
                        keys.Add("@bookCategory");
                        parameters.Add("%" + main.txtLibraryReceiveSearch.Text + "%");
                        dt=db_Parameterized_Select_DataTable(srQuery + " bookCategory like "  + "@bookCategory" +" and bookQuantity>0", keys, parameters);
                        RefreshDataGrid(dt, main.LibraryReceiveDataGrid);

                        break;
                }
            }
            else
            {
                switch (main.cmbReceiveSearchFilter.SelectedItem)
                {
                    case "All":
                        keys.Add("@bookName");
                        keys.Add("@bookAuthor");
                        keys.Add("@bookCategory");
                        for (int i = 0; i < 3; i++)
                        {
                            parameters.Add("%" + main.txtLibraryReceiveSearch.Text + "%");
                        }
                        dt =db_Parameterized_Select_DataTable(srQuery + " bookName like  "  + "@bookName" + " or " + "bookAuthor like "  + "@bookAuthor"  + " or " + "bookCategory like  "  + "@bookCategory" , keys, parameters);
                        RefreshDataGrid(dt, main.LibraryReceiveDataGrid);

                        break;
                    case "Name":
                        keys.Add("@bookName");
                        parameters.Add("%" + main.txtLibraryReceiveSearch.Text + "%");
                        dt=db_Parameterized_Select_DataTable(srQuery + "  bookName like  "  + "@bookName" , keys, parameters);
                        RefreshDataGrid(dt, main.LibraryReceiveDataGrid);

                        break;
                    case "Author":
                        keys.Add("@bookAuthor");
                        parameters.Add("%" + main.txtLibraryReceiveSearch.Text + "%");
                        dt=db_Parameterized_Select_DataTable(srQuery + " bookAuthor like  "  + "@bookAuthor" , keys, parameters);
                        RefreshDataGrid(dt, main.LibraryReceiveDataGrid);

                        break;
                    case "Category":
                        keys.Add("@bookCategory");
                        parameters.Add("%" + main.txtLibraryReceiveSearch.Text + "%");
                        dt=db_Parameterized_Select_DataTable(srQuery + "  bookCategory like  " + "@bookCategory" , keys, parameters);
                        RefreshDataGrid(dt, main.LibraryReceiveDataGrid);

                        break;
                }
            }
        }
        public static void SelectionChangedLibrary()
        {
            var labels = new List<System.Windows.Controls.Label> { main.lblBookName, main.lblBookAuthor, main.lblBookPages, main.lblBookYear, main.lblBookCategory, main.lblBookQuantity };

            if ((DataRowView)main.LibraryReceiveDataGrid.SelectedItem != null)
            {
                string[] rowValues = new string[6];

                for (int i = 0; i < 6; i++)
                {
                    rowValues[i] = ((DataRowView)main.LibraryReceiveDataGrid.SelectedItem).Row.ItemArray[i].ToString();
                    labels[i].Content = rowValues[i];
                }
            }
            else
            {
                for (int i = 0; i < 6; i++)
                {
                    labels[i].Content = null;
                }
            }
        }
        public static bool BookQuantityCheck()
        {
            List<string> keyForQuantity = new List<string>() { "@bookName" };
            List<string> parametersForQuantity = new List<string>() { main.lblBookName.Content.ToString() };
            string srQueryForCheck = "select bookQuantity from [Books] where bookName=@bookName";
            DataRow row = db_Parameterized_Select_DataRow(srQueryForCheck, keyForQuantity, parametersForQuantity);
            if ((int)row[0] > 0)
            {
                return true;
            }
            MessageBox.Show("Book is out of stock");
            return false;
        }
        public static bool IsBookAlreadyTaken()
        {
            List<string> keys = new List<string>() { "@bookName", "@whoReceived" };
            List<string> parameters = new List<string>() { main.lblBookName.Content.ToString(), main.txtLoginUsername.Text };
            string srQueryPending = "select * from [PendingBooks] where bookName=@bookName and whoReceived=@whoReceived";
            string srQueryReceived = "select * from [ReceivedBooks] where bookName=@bookName and whoReceived=@whoReceived";
            DataRow pendingBooksRow = db_Parameterized_Select_DataRow(srQueryPending, keys, parameters);
            DataRow receivedBooksRow = db_Parameterized_Select_DataRow(srQueryReceived, keys, parameters);
            if (pendingBooksRow != null)
            {
                MessageBox.Show("You already sent a request");
                return false;
            }
            if (receivedBooksRow != null)
            {
                MessageBox.Show("You already have this book");
                return false;
            }
            return true;
        }
        public static bool StudentBookCountCheck()
        {
            List<string> keys = new List<string>() { "@whoReceived" };
            List<string> parameters = new List<string>() { main.txtLoginUsername.Text };
            int receivedBookCount = db_Parameterized_Select_DataTable("select * from [ReceivedBooks] where whoReceived=@whoReceived ", keys, parameters).Rows.Count;
            int maxBookCount = AdminSettingsDb("select studentBookCount from adminSettings");
            if (receivedBookCount >= maxBookCount)
            {
                MessageBox.Show($"You can receive only {maxBookCount} books");
                return false;
            }
            return true;
        }
        public static void LibraryInform()
        {
            List<string> key = new List<string>();
            List<string> value = new List<string>();
            string srQuery = "select * from adminSettings";
            DataRow dr = db_Parameterized_Select_DataRow(srQuery, key, value);
            main.libraryInform.Content = $" You can hold only {dr["studentBookCount"]} books for {dr["studentIssueDay"]} days";
            key.Add("@whoReceived");
            value.Add(main.lblProfileName.Content.ToString());
            srQuery = "select * from ReceivedBooks where whoReceived=@whoReceived";
            DataTable dt = db_Parameterized_Select_DataTable(srQuery, key, value);
            if (dt.Rows.Count > (int)dr[0])
                main.lblBooksLeft.Content = "You already have maximum book that you can hold at the same time";
            else
                main.lblBooksLeft.Content = $"You can have {(int)dr[0]-dt.Rows.Count} more books";
        }
    }
}
