using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace barangay_management_system
{
    class dbconString
    {
        // Connection string for MySQL (XAMPP)
        public static string connection = "server=localhost; database=barangaydb; user=root; password=;";

        // Method to establish a connection
        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connection);
        }
    }
}
