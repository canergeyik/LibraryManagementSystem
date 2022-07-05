using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static LibraryManagementSystem.HelperMethods;
using static LibraryManagementSystem.RegistirationProcess;
using static LibraryManagementSystem.LoginProcess;
using static LibraryManagementSystem.LibraryProfile;
using static LibraryManagementSystem.LibraryIssueBookProcess;
using static LibraryManagementSystem.LibraryReceiveBookProcess;
using static LibraryManagementSystem.AdminUsersProcess;
using static LibraryManagementSystem.AdminSettingsProcess;
using static LibraryManagementSystem.AdminTeacherRankUpgrade;
using static LibraryManagementSystem.AdminReceivedBooksProcess;
using static LibraryManagementSystem.AdminLibraryManagement;
using System.IO;
using Microsoft.OData.Edm;
using static LibraryManagementSystem.AdminPage;


namespace LibraryManagementSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow main;
        public MainWindow()
        {
            main = this;
            InitializeComponent();
            PopulateComboBox();
            LoginTab.Focus();
            AdminTab.Visibility = Visibility.Hidden;
            LibraryProfileTab.Visibility = Visibility.Hidden;
            AdminLibraryTab.Visibility = Visibility.Hidden;
            AdminReceivedTab.Visibility = Visibility.Hidden;
            SettingsTab.Visibility = Visibility.Hidden;
            AdminTeachersTab.Visibility = Visibility.Hidden;
            LoginTab.Visibility = Visibility.Hidden;
            RegisterTab.Visibility = Visibility.Hidden;
            LibraryTab.Visibility = Visibility.Hidden;
            LibraryReceiveTab.Visibility = Visibility.Hidden;
            LibraryIssueTab.Visibility = Visibility.Hidden;
            AdminUsersTab.Visibility = Visibility.Hidden;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            LoginCheck();
        }

        private void ToRegisterButton_Click(object sender, RoutedEventArgs e)
        {
            TabChanging(RegisterTab);

        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            RegistrationSuccesfull();
        }

        private void ToLoginScreen_Click(object sender, RoutedEventArgs e)
        {
            TabChanging(LoginTab);
        }

        private void ToLibraryProfileButton_Click(object sender, RoutedEventArgs e)
        {
            TabChanging(LibraryProfileTab);
        }

        private void updatePassButton_Click(object sender, RoutedEventArgs e)
        {
            ProfilePassChange();
        }

        private void ToLibraryIssueButton_Click(object sender, RoutedEventArgs e)
        {
            TabChanging(LibraryIssueTab);
            IssueBookTable();
        }

        private void LibraryIssueButton_Click(object sender, RoutedEventArgs e)
        {
            IssueBookButton();
        }

        private void txtLibraryReceiveSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            LibrarySearchBook();
        }

        private void ToLibraryReceiveButton_Click(object sender, RoutedEventArgs e)
        {
            LibraryInform();
            TabChanging(LibraryReceiveTab);
            cmbReceiveSearchFilter.SelectedIndex = 0;
        }

        private void LibraryReceiveDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectionChangedLibrary();
        }

        private void LibraryReceiveButton_Click(object sender, RoutedEventArgs e)
        {
            ReceiveBook();
        }

        private void LogOutLibraryButton_Click(object sender, RoutedEventArgs e)
        {
            TabChanging(LoginTab);
            txtLoginUsername.Clear();
            PassBoxLogin.Clear();
        }
        private void LibraryPanelClick(object sender, RoutedEventArgs e)
        {
            TabChanging(LibraryTab);
        }
        private void AdminPanelClick(object sender, RoutedEventArgs e)
        {
            TabChanging(AdminTab);
            AdminPageNotifications();
        }

        private void ToAdminUsersButton_Click(object sender, RoutedEventArgs e)
        {
            TabChanging(AdminUsersTab);
            AdminUsersTable();
        }

        private void ToAdminSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            TabChanging(SettingsTab);
            AdminSettingsRefresh();
        }

        private void AdminSettingsUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            AdminSettingsUpdate();
            AdminSettingsRefresh();

        }

        private void ToAdminRequestsButton_Click(object sender, RoutedEventArgs e)
        {
            TabChanging(AdminTeachersTab);
            AdminTeacherTable();

        }

        private void AdminTeacherRankUpgButton_Click(object sender, RoutedEventArgs e)
        {
            AdminRankUpgradeButton();
            AdminTeacherTable();
        }

        private void ToAdminReceivedButton_Click(object sender, RoutedEventArgs e)
        {
            TabChanging(AdminReceivedTab);
            PendingBooksTable();
            ReceivedBooksTable();
        }

        private void ToAdminManagementButton_Click(object sender, RoutedEventArgs e)
        {
            TabChanging(AdminLibraryTab);
            AdminLibraryDataGrid();
        }

        private void AdminLibraryAddButton_Click(object sender, RoutedEventArgs e)
        {
            AdminLibraryAddButton();
            AdminLibraryDataGrid();

        }

        private void AdminLibraryUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            AdminLibraryUpdateButton();
            AdminLibraryDataGrid();

        }

        private void AdminLibraryDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedRow();
        }



        private void AdminGiveBookButton_Click(object sender, RoutedEventArgs e)
        {
            AdminApprove();
            PendingBooksTable();
            ReceivedBooksTable();
        }

        private void AdminUserSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UserSearch();
        }

        private void AdminTeacherSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            TeacherSearch();
        }

        private void AdminLibrarySearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            AdminLibrarySearch();
        }

        private void AdminPendingBookSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            PendingSearch();
        }

        private void AdminReceivedBooksSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            ReceivedSearch();
        }
        /*
        private void asd_Click(object sender, RoutedEventArgs e)
        {
            List<string> categoryList = new List<string>();
            List<string> authorList = new List<string>();
            List<string> nameList = new List<string>();
            List<int> quantityList = new List<int>();
            List<int> pages = new List<int>();
            List<string> dateList = new List<string>();

            foreach (var Line in File.ReadLines(@"C:\Users\Caner\Desktop\library project\LibraryManagementSystem\LibraryManagementSystem\books.txt"))
            {
                if (Line.Length < 10)
                    continue;
                List<string> EachBookSeperated = Line.Split("$$$$$").ToList();
                if (EachBookSeperated.Count < 3)
                    continue;
                categoryList.Add(EachBookSeperated[0].Trim());
                authorList.Add(EachBookSeperated[1].Trim());
                nameList.Add(EachBookSeperated[2].Trim());
                Random random = new Random();
                quantityList.Add(random.Next(0, 9));
            }
            for (int i = 0; i < categoryList.Count; i++)
            {
                Random random = new Random();
                string[] randomCategoryArray = new string[] { "History", "Crime", "Politics", "Health", "Fiction", "Culture", "Pedagogy" };
                if (categoryList[i] == "")
                {
                    categoryList[i] = randomCategoryArray[random.Next(0, 6)];
                }
                int a = random.Next(1000, 2000);
                int b = random.Next(1, 13);
                int c = random.Next(1, 28);
                string d = $"{a}-{b}-{c}";
                d = String.Format("{0:yyyy-MM-dd}", d);
                dateList.Add(d);
                pages.Add(random.Next(100, 1000));
            }
            for (int i = 0; i < nameList.Count; i++)
            {
                string DataQuery = ($"insert into Books (bookName,bookAuthor,bookCategory,bookYear,bookPages,bookQuantity) values (@BookName,@Author,@Category,@bookYear,'{pages[i]}','{quantityList[i]}')");
                List<string> keys = new List<string>() { "@BookName", "@Author", "@Category", "@bookYear" };
                List<string> parameters = new List<string>() { nameList[i], authorList[i], categoryList[i], dateList[i] };
                DbOperations.db_Parameterized_Update_Delete_Insert(DataQuery, keys, parameters);
            }
        
         } 
        */
    }
}
