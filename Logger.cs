using barangay_management_system;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace barangay_default
{
    public static class Logger
    {
        public static void LogAction(string userType, string username, string action)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(dbconString.connection))
                {
                    string query = "INSERT INTO logs (user_type, username, action) VALUES (@user_type, @username, @action)";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@user_type", userType);
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@action", action);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Optional: log to file or show a warning
                Console.WriteLine("Log error: " + ex.Message);
            }
        }
    }
}