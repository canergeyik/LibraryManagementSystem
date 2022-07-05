using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using static LibraryManagementSystem.MainWindow;

namespace LibraryManagementSystem
{
    internal static class HelperMethods
    {
        public static void TabChanging(TabItem Tab)
        {
            Tab.Visibility = Visibility.Visible;
            Tab.Focus();
            Tab.Visibility = Visibility.Hidden;
        }
        public static List<string> PassHashing(string password) //This method using for Registiration and Pass changing
        {
            Random random = new Random();
            int number = random.Next(1000, 9999);
            string randomNumber = number.ToString();
            password = password + randomNumber; //We are adding random number to pass because some atackers can try easy password to crack it.
            //https://stackoverflow.com/questions/70740536/how-to-hash-password-using-sha-256
            var sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            var HashedPassword = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                HashedPassword.Append(bytes[i].ToString("x2"));
            }
            string lastHashed = HashedPassword.ToString();
            List<string> result = new List<string>() { lastHashed, randomNumber };
            return result;
        }
        public static string PassHashing(string password, string number) //This method using for checking the pass is correct
        {
            string newpass = password + number;
            //https://stackoverflow.com/questions/70740536/how-to-hash-password-using-sha-256
            var sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(newpass));
            var HashedPassword = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                HashedPassword.Append(bytes[i].ToString("x2"));
            }
            string lastHashed = HashedPassword.ToString();
            return lastHashed;
        }
        public static void RefreshDataGrid(DataTable table, DataGrid gridName) //Data grid refresh
        {
            DataView dataView = new DataView(table);
            gridName.ItemsSource = dataView;
        }
        public static void PopulateComboBox() //populating combobox for User Library search
        {
            main.cmbReceiveSearchFilter.Items.Add("All");
            main.cmbReceiveSearchFilter.Items.Add("Name");
            main.cmbReceiveSearchFilter.Items.Add("Author");
            main.cmbReceiveSearchFilter.Items.Add("Category");
        }
        //public static void parameterizedPaginatedDataGrid(string Query, List<string> keys, List<string> parameters, DataGrid dataGrid,Label lbl)
        //{
        //    int currentPage = Convert.ToInt32(lbl.Content);
        //    DataTable dataTable = new DataTable();
        //    DataTable newDataTable = new DataTable();
        //    dataTable = db_Parameterized_Select_DataTable(Query, keys, parameters);
        //    if (dataTable.Rows.Count > 0)
        //    {
        //        newDataTable = dataTable.Select().Skip((currentPage - 1) * 8).Take(8).CopyToDataTable();
        //    }
        //    DataView dataView = new DataView(newDataTable);
        //    dataGrid.ItemsSource = dataView;
        //}
        //public static void NextPageButton()
        //{

        //}
        //public static void PreviousPageButton()
        //{

        //}
        //public static void LastPageButton()
        //{

        //}
        //public static void FirstPageButton(Label lbl, List<Button> buttons)
        //{

        //}
    }


}
