using System.Collections.Generic;
using System.Data;
using System.Windows;
using static LibraryManagementSystem.DbOperations;
using static LibraryManagementSystem.HelperMethods;
using static LibraryManagementSystem.MainWindow;
using static LibraryManagementSystem.RegistirationProcess;

namespace LibraryManagementSystem
{
    internal static class LibraryProfile
    {
        public static void ProfileStatus() // Loading information about user
        {
            List<string> key = new List<string>() { "@username" };
            List<string> parameter = new List<string>() { main.txtLoginUsername.Text };
            DataRow row = db_Parameterized_Select_DataRow("select * from [Users] where username = @username", key, parameter);
            main.lblProfileName.Content = row[0].ToString();
            main.lblProfileEmail.Content = row[2].ToString();
            main.lblProfileRank.Content = row[3].ToString();

        }
        public static void ProfilePassChange() //Pass Changing
        {
            if (main.PassBoxProfilePass.Password != null && main.PassBoxProfileRePass.Password != null && PasswordCheck(main.PassBoxProfileRePass.Password, main.PassBoxProfileRePass.Password) == true) //checking not null and in correct format
            {
                List<string> hashedPass = PassHashing(main.PassBoxProfilePass.Password);
                List<string> key = new List<string>() { "@username", "@password", "@passNumber" };
                List<string> parameter = new List<string>() { main.txtLoginUsername.Text, hashedPass[0], hashedPass[1] };
                db_Parameterized_Update_Delete_Insert("update [Users] set password=@password , passNumber=@passNumber where username = @username", key, parameter);
                MessageBox.Show("Your password changed succesfully");
            }
            else
                MessageBox.Show("Your passwords must be same and can not be null");
        }
    }
}
