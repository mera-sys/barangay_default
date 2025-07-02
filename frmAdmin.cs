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
    public partial class frmAdmin : Form
    {
        public frmAdmin()
        {
            InitializeComponent();
        }

        private void frmAdmin_Load(object sender, EventArgs e)
        {
            LoadAccountData("");

        }
        private void LoadAccountData(string searchTerm)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(dbconString.connection))
                {
                    conn.Open();
                    string query = @"SELECT id, Username, Password, role 
                             FROM users 
                             WHERE Username LIKE @search OR role LIKE @search";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@search", "%" + searchTerm + "%");

                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        guna2DataGridView1.Rows.Clear();

                        foreach (DataRow row in dt.Rows)
                        {
                            guna2DataGridView1.Rows.Add(
                                row["id"].ToString(),           // 0 - Account ID
                                row["Username"].ToString(),     // 1 - Username
                                "********",                     // 2 - Masked password
                                row["role"].ToString(),         // 3 - Role
                                row["Password"].ToString()      // 4 - Actual password (hidden)
                                                                // 5 - btnEdit already exists in grid
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (guna2DataGridView1.Columns[e.ColumnIndex].Name == "btnEdit" && e.RowIndex >= 0)
            {
                string accountId = guna2DataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                string username = guna2DataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                string realPassword = guna2DataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString(); // Hidden password column

                // Pass values directly to the constructor
                FrmUpdateAdmin updateForm = new FrmUpdateAdmin(accountId, username, realPassword);
                updateForm.ShowDialog();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim();
            LoadAccountData(searchTerm);
        }

        private void guna2DataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
    }
}
