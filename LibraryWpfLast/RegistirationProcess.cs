using System.Collections.Generic;
using System.Windows;
using static LibraryManagementSystem.MainWindow;
using static LibraryManagementSystem.DbOperations;
using static LibraryManagementSystem.HelperMethods;


namespace LibraryManagementSystem
{
    internal static class RegistirationProcess
    {
        static string AllowedCharUpper = "ZXCVBNMASDFGHJKLQWERTYUIOP";
        static string AllowedNumber = "1234567890";
        static string AllowedSigns = "!'^+%&'/()=?_é<>£#$½{[]}\\|.,;`:";
        static string AllowedCharLower = AllowedCharUpper.ToLower();
        static int MinUsernameLength = 6;
        static int MaxUsernameLength = 15;
        static int MinPasswordLength = 5;
        static int MaxPasswordLength = 20;
        public static bool PasswordCheck(string password, string passwordRe)
        {
            if (password == passwordRe)
            {
                if (password.Length < MinPasswordLength || password.Length > MaxPasswordLength)
                {
                    MessageBox.Show("Password's length has to be between 5 and 20");
                    return false;
                }
                int counterSign = 0;
                int counterUpper = 0;
                int counterLower = 0;
                int counterNumber = 0;
                for (int i = 0; i < password.Length; i++)
                {
                    if (AllowedCharLower.Contains(password[i]) || AllowedNumber.Contains(password[i]) || AllowedCharUpper.Contains(password[i]) || AllowedSigns.Contains(password[i]))
                    {
                        if (AllowedCharUpper.Contains(password[i]))
                        {
                            counterUpper++;
                        }
                        if (AllowedNumber.Contains(password[i]))
                        {
                            counterNumber++;
                        }
                        if (AllowedCharLower.Contains(password[i]))
                        {
                            counterLower++;
                        }
                        if (AllowedSigns.Contains(password[i]))
                            counterSign++;
                    }
                    else
                    {
                        MessageBox.Show($" Password can not contain {password[i]} character");
                        return false;
                    }
                }
                if (counterUpper == 0 || counterLower == 0 || counterNumber == 0 || counterSign == 0)
                {
                    MessageBox.Show("Your password must contain 1 number, 1 upper, 1 sign and 1 lower character.");
                    return false;
                }
                else
                    return true;
            }
            else
            {
                MessageBox.Show("Your passwords are not matching");
                return false;
            }
        }
        public static bool UsernameRegisterCheck()
        {   
            string username= main.txtRegisterUsername.Text;
            List<string> key = new List<string>() { "@username" };
            List<string> parameters = new List<string>() { username };
            if (db_Parameterized_Select_DataRow("select * from Users where username= @username", key, parameters) == null)
            {
                if (username.Length < MinUsernameLength || username.Length > MaxUsernameLength)
                {
                    MessageBox.Show("Username's length has to be between 6 and 15");
                    return false;
                }
                for (int i = 0; i < username.Length; i++)
                {
                    if (AllowedCharLower.Contains(username[i]) || AllowedNumber.Contains(username[i]) || AllowedCharUpper.Contains(username[i]))
                        continue;
                    else
                    {
                        MessageBox.Show($"Username can not contain {username[i]} character");
                        return false;
                    }
                }
                return true;
            }
            else
            {
                MessageBox.Show("your username already in use");
                return false;
            }
        }
        public static bool EmailCheck()
        {   
            string email = main.txtRegisterEmail.Text;
            if (!email.Contains('@'))
            {
                email = email + "@toros.edu.tr";
                List<string> key = new List<string>() {"@email"};
                List<string> parameters = new List<string>() { email};
                if (db_Parameterized_Select_DataRow("select * from Users where email= @email", key, parameters) == null)
                    return true;            
                else
                {
                    MessageBox.Show("your email already in use");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Your email will automaticly have @toros.edu.tr");
                return false;
            }
        }
        public static void RegistrationSuccesfull()
        {
            if (PasswordCheck(main.PassBoxRegister.Password,main.PassBoxRegisterRe.Password) == true && UsernameRegisterCheck() == true && EmailCheck() == true)
            {
                List<string> passInform = PassHashing(main.PassBoxRegister.Password);
                string hashedPassword = passInform[0];
                string hashedNumber =passInform[1];
                string srQuery = "insert into [Users] (username,password,email,userRank,passNumber) values (@username,@password,@email,@userRank,@passNumber)";
                List<string> keys = new List<string> { "@username", "@password", "@email", "@userRank", "@passNumber" };
                List<string> parameters = new List<string>() { main.txtRegisterUsername.Text, hashedPassword, main.txtRegisterEmail.Text + "@toros.edu.tr", hashedNumber };
                if (main.StudentRadioButton.IsChecked == true)
                {
                    parameters.Insert(3, "Student");
                }
                if (main.TeacherRadioButton.IsChecked == true)
                {
                    parameters.Insert(3, "Teacher(Pending)");
                }
                db_Parameterized_Update_Delete_Insert(srQuery, keys,parameters);
            }
        }
    } 
    }

