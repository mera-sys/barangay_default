using barangay_management_system;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace barangay_default
{
    public partial class frmLogs : Form
    {
        public frmLogs()
        {
            InitializeComponent();
        }

        private void frmLogs_Load(object sender, EventArgs e)
        {
            LoadLogs();
        }
        private void LoadLogs()
        {
            using (MySqlConnection conn = new MySqlConnection(dbconString.connection))
            {
                try
                {
                    conn.Open();
                    string query = @"
                SELECT 
                    id,
                    CONCAT(user_type, ' ', username, ' ', action) AS message,
                    timestamp AS date
                FROM logs
                ORDER BY timestamp DESC";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        MessageBox.Show("Rows fetched: " + dt.Rows.Count.ToString()); // Debug

                        // Ensure column mappings
                        guna2DataGridView1.AutoGenerateColumns = false;
                        guna2DataGridView1.Columns["id"].DataPropertyName = "id";
                        guna2DataGridView1.Columns["message"].DataPropertyName = "message";
                        guna2DataGridView1.Columns["date"].DataPropertyName = "date";

                        // Bind
                        guna2DataGridView1.DataSource = dt;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading logs: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
