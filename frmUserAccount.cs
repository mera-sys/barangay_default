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
    public partial class frmUserAccount : Form
    {
        
        public frmUserAccount()
        {
            InitializeComponent();
        }

        private void frmUserAccount_Load(object sender, EventArgs e)
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
                    string query = "SELECT AccountID, Username, Password, ResidentID, FullName FROM accounts WHERE FullName LIKE @search";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@search", "%" + searchTerm + "%");

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    guna2DataGridView1.Rows.Clear();

                    foreach (DataRow row in dt.Rows)
                    {
                        guna2DataGridView1.Rows.Add(
                            row["AccountID"].ToString(),     // 0
                            row["Username"].ToString(),      // 1
                            "********",                      // 2 - display masked
                            row["ResidentID"].ToString(),    // 3
                            row["FullName"].ToString(),      // 4
                            row["Password"].ToString()       
                        );
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
     
            if (e.RowIndex >= 0 && guna2DataGridView1.Columns[e.ColumnIndex].Name == "btnEdit")
            {
                
                string accountId = guna2DataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                string username = guna2DataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                string realPassword = guna2DataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString(); 

                
                frmupdateAccount updateForm = new frmupdateAccount();
                updateForm.SetAccountData(accountId, username, realPassword); 
                updateForm.ShowDialog();
            }
        }
        private void guna2DataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim();
            LoadAccountData(searchTerm);
        }
    }
}
