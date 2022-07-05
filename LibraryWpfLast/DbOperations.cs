using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace LibraryManagementSystem
{

    public static class DbOperations
    {
        public static string srConnectionString = "server=CANER;database=LibraryWpf; integrated security=SSPI;persist security info=False; Trusted_Connection=Yes;";

        public static int db_Parameterized_Update_Delete_Insert(string srQuery, List<string> keys, List<string> parameters) //For update, delete and inserting. It takes two list for parameters and keys to be sure the query is only string to avoid SQL injection
         {
            using ( SqlConnection connection = new SqlConnection(srConnectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(srQuery, connection))
                {
                    for (int i = 0; i < keys.Count(); i++)
                    {
                        cmd.Parameters.AddWithValue(keys[i], parameters[i]);
                    }
                    return cmd.ExecuteNonQuery(); // Returns nothing because we are only doing update, delete and insert.
                }
            }
        }
        public static DataTable db_Parameterized_Select_DataTable(string srQuery, List<string> keys, List<string> parameters)//For getting data from SQL. It takes two list for parameters and keys to be sure the query is only string to avoid SQL injection
        {
            DataTable dtResult = new DataTable();

            using (SqlConnection connection = new SqlConnection(srConnectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(srQuery, connection))
                {
                    for (int i = 0; i < keys.Count(); i++)
                    {
                        cmd.Parameters.AddWithValue(keys[i], parameters[i]);
                    }
                    using (SqlDataAdapter sqlAdapt = new SqlDataAdapter(cmd))
                    {
                        sqlAdapt.Fill(dtResult);
                        return dtResult; //Returns a datatable to fill datagrids.
                    }
                }
            }
        }
        public static DataRow db_Parameterized_Select_DataRow(string srQuery, List<string> keys, List<string> parameters)
        {
            DataRow row = null;

            using (SqlConnection connection = new SqlConnection(srConnectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(srQuery, connection))
                {
                    for (int i = 0; i < keys.Count(); i++)
                    {
                        cmd.Parameters.AddWithValue(keys[i], parameters[i]);
                    }
                    using (SqlDataAdapter DA = new SqlDataAdapter(cmd))
                    {
                        using (DataTable drTemp = new DataTable())
                        {
                            DA.Fill(0, 1, drTemp);
                            if (drTemp.Rows.Count > 0)
                                row = drTemp.Rows[0];
                            return row;
                        }
                    }
                }
            }
        }
        //Last two functions aren't parameterized because there is no input from user. So we don't have to use it
        public static int AdminSettingsDb(string srQuery) //Getting admin settings
        {
            using (SqlConnection con = new SqlConnection(srConnectionString))
            {
                con.Open();
                int settingValue = 0;
                using (SqlCommand cmd = new SqlCommand(srQuery, con))
                {
                    settingValue = (int)cmd.ExecuteScalar();
                }
                con.Close();
                return settingValue;
            }
        }
        public static int updateDeleteInsert(string srQuery) // update delete insert
        {
            using (SqlConnection connection = new SqlConnection(srConnectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(srQuery, connection))
                {
                    return cmd.ExecuteNonQuery();
                }
            }
        }
    }
}