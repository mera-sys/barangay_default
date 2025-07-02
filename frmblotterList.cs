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
    public partial class frmblotterList : Form
    {
        public frmblotterList()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.frmblotterList_Load);


        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmAddblotter f = new frmAddblotter();
            f.ShowDialog();
        }
        private void LoadBlotterRecords()
        {
            try
            {
                guna2DataGridView1.Rows.Clear();

                using (MySqlConnection conn = new MySqlConnection(dbconString.connection))
                {
                    conn.Open();
                    string query = @"SELECT blotter_id,involved_not_resident, status, remarks, type_of_incident, location_incident, date_incident, date_reported 
                             FROM blotter_record ORDER BY date_reported DESC";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                guna2DataGridView1.Rows.Add(
                                    reader["blotter_id"].ToString(),
                                    reader["involved_not_resident"].ToString(),
                                    reader["status"].ToString(), 
                                    reader["remarks"].ToString(),
                                    reader["type_of_incident"].ToString(),
                                    reader["location_incident"].ToString(),
                                    Convert.ToDateTime(reader["date_incident"]).ToString("yyyy-MM-dd"),
                                    Convert.ToDateTime(reader["date_reported"]).ToString("yyyy-MM-dd")
                                );
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading blotter records: " + ex.Message);
            }
        }
        private void frmblotterList_Load(object sender, EventArgs e)
        {
            LoadBlotterRecords();
        }
        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var blotterIdCell = guna2DataGridView1.Rows[e.RowIndex].Cells["colBlotterNumber"].Value;

                if (blotterIdCell == null)
                {
                    MessageBox.Show("Blotter ID is missing. Please check the record.", "Invalid Blotter ID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string blotterId = blotterIdCell.ToString();

                if (e.ColumnIndex == guna2DataGridView1.Columns["btnUpdate"].Index)
                {
                    // Update button clicked
                    frmBlotterupdate updateForm = new frmBlotterupdate(blotterId);
                    updateForm.ShowDialog();
                }
                else if (e.ColumnIndex == guna2DataGridView1.Columns["btnPrint"].Index)
                {
                    // Print button clicked
                    frmReport reportForm = new frmReport();
                    reportForm.PreviewBlotter(blotterId); // Pass the blotter ID
                    reportForm.ShowDialog();
                }
            }
        }
            private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string searchQuery = metroTextBox1.Text.Trim();

                if (!string.IsNullOrEmpty(searchQuery))
                {
                  
                    using (MySqlConnection conn = new MySqlConnection(dbconString.connection))
                    {
                        conn.Open();
                        string query = @"SELECT blotter_id,involved_not_resident, status, type_of_incident, location_incident, date_incident, date_reported
                                 FROM blotter_record
                                 WHERE blotter_id LIKE @searchQuery OR involved_not_resident LIKE @searchQuery OR
                                       status LIKE @searchQuery OR
                                       type_of_incident LIKE @searchQuery OR
                                       location_incident LIKE @searchQuery
                                 ORDER BY date_reported DESC";

                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@searchQuery", "%" + searchQuery + "%");

                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                guna2DataGridView1.Rows.Clear();
                                while (reader.Read())
                                {
                                    guna2DataGridView1.Rows.Add(
                                        reader["blotter_id"].ToString(),
                                          reader["involved_not_resident"].ToString(),
                                        reader["status"].ToString(),
                                        reader["type_of_incident"].ToString(),
                                        reader["location_incident"].ToString(),
                                        Convert.ToDateTime(reader["date_incident"]).ToString("yyyy-MM-dd"),
                                        Convert.ToDateTime(reader["date_reported"]).ToString("yyyy-MM-dd")
                                    );
                                }
                            }
                        }
                    }
                }
                else
                {
                    // If the search box is empty, load all records
                    LoadBlotterRecords();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during search: " + ex.Message);
            }
        }

        private void metroTextBox1_TextChanged(object sender, EventArgs e)
        
            {
                try
                {
                    string searchQuery = metroTextBox1.Text.Trim();

                    if (!string.IsNullOrEmpty(searchQuery))
                    {

                        using (MySqlConnection conn = new MySqlConnection(dbconString.connection))
                        {
                            conn.Open();
                            string query = @"SELECT blotter_id, involved_not_resident, status, type_of_incident, location_incident, date_incident, date_reported
                                 FROM blotter_record
                                 WHERE blotter_id LIKE @searchQuery OR involved_not_resident LIKE @searchQuery OR
                                       status LIKE @searchQuery OR
                                       type_of_incident LIKE @searchQuery OR
                                       location_incident LIKE @searchQuery
                                 ORDER BY date_reported DESC";

                            using (MySqlCommand cmd = new MySqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@searchQuery", "%" + searchQuery + "%");

                                using (MySqlDataReader reader = cmd.ExecuteReader())
                                {
                                    guna2DataGridView1.Rows.Clear();
                                    while (reader.Read())
                                    {
                                        guna2DataGridView1.Rows.Add(
                                            reader["blotter_id"].ToString(),
                                               reader["involved_not_resident"].ToString(),
                                            reader["status"].ToString(),
                                            reader["type_of_incident"].ToString(),
                                            reader["location_incident"].ToString(),
                                            Convert.ToDateTime(reader["date_incident"]).ToString("yyyy-MM-dd"),
                                            Convert.ToDateTime(reader["date_reported"]).ToString("yyyy-MM-dd")
                                        );
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        // If the search box is empty, load all records
                        LoadBlotterRecords();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error during search: " + ex.Message);
                }
            }

        private void frmblotterList_Load_1(object sender, EventArgs e)
        {

        }
    }
}
