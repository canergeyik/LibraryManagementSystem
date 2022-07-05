using System.Collections.Generic;
using System.Data;
using System.Windows;
using static LibraryManagementSystem.AdminPage;
using static LibraryManagementSystem.DbOperations;
using static LibraryManagementSystem.HelperMethods;
using static LibraryManagementSystem.LibraryProfile;
using static LibraryManagementSystem.MainWindow;

namespace LibraryManagementSystem
{
    internal class LoginProcess
    {
        public static void LoginCheck()
        {
            List<string> key = new List<string>() { "@username", "@email" };
            List<string> parameters = new List<string>() { main.txtLoginUsername.Text, main.txtLoginUsername.Text };
            string srQuery = "select * from [Users] where username=@username or email=@email";
            DataRow row = db_Parameterized_Select_DataRow(srQuery, key, parameters);
            if (row != null)
            {
                string a = main.PassBoxLogin.Password;
                string passwordNumber = row[4].ToString();
                string passControl = PassHashing(main.PassBoxLogin.Password, passwordNumber);
                if (passControl == row[1].ToString())
                {
                    if (row[3].ToString() == "Admin")
                    {
                        TabChanging(main.AdminTab);
                        AdminPageNotifications();
                    }
                    else
                    {
                        ProfileStatus();
                        TabChanging(main.LibraryTab);
                    }
                }
                else
                {
                    MessageBox.Show("Your password is wrong");
                }
            }
            else
            {
                MessageBox.Show("Your username or email is wrong");

            }
        }
    }
}
