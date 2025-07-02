using barangay_management_system;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace barangay_default
{
    public partial class frmListofOfficial : Form
    {
        public frmListofOfficial()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmListofOfficial_Load);
            guna2DataGridView1.Rows.Add("Item 1");
            guna2DataGridView1.Rows[0].Cells["colStatus"].Value = "ACTIVE";
            guna2DataGridView1.Rows[0].Cells["colStatus"].Style.BackColor = Color.LightGreen;
        }
        public void Loadofficial()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(dbconString.connection))
                {
                    conn.Open();
                    string query = @"SELECT id, image, fname, mname, lname, suffix, birth_date, age, position, chairmanship, status, id_number FROM barangay_officials";


                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        guna2DataGridView1.Rows.Clear();

                        while (reader.Read())
                        {
                            // Handle image
                            Image img = null;
                            if (reader["image"] != DBNull.Value)
                            {
                                byte[] imgBytes = (byte[])reader["image"];
                                using (MemoryStream ms = new MemoryStream(imgBytes))
                                {
                                    Image originalImg = Image.FromStream(ms);
                                    img = new Bitmap(originalImg, new Size(50, 50));
                                }
                            }

                            // Build full name
                            string fullName = $"{reader["fname"]} {reader["mname"]} {reader["lname"]} {reader["suffix"]}".Trim();

                            // Add to DataGridView
                            guna2DataGridView1.Rows.Add(
                                img, // Image
                                fullName, // Name
                                reader["position"].ToString(), // Position
                                reader["chairmanship"].ToString(), // Chairmanship
                                Convert.ToDateTime(reader["birth_date"]).ToShortDateString(), // Birth Date
                                reader["age"].ToString(), // Age
                                reader["status"].ToString(), // status from DB


                              
                            
                                reader["id"].ToString() // Make sure colID exists and matches this position
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading officials: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmListofOfficial_Load(object sender, EventArgs e)
        {
            Loadofficial();
        }

        private void guna2DataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {

            if (e.RowIndex >= 0 && e.ColumnIndex == guna2DataGridView1.Columns["colStatus"].Index)
            {
                var cell = guna2DataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                string status = cell.Value?.ToString();

                Color backgroundColor;
                if (status == "ACTIVE")
                {
                    backgroundColor = Color.LightGreen;
                }
                else if (status == "INACTIVE")
                {
                    backgroundColor = Color.LightCoral;
                }
                else
                {
                    backgroundColor = Color.White;
                }


                int borderRadius = 5;
                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddArc(e.CellBounds.X, e.CellBounds.Y, borderRadius, borderRadius, 180, 90);
                    path.AddArc(e.CellBounds.X + e.CellBounds.Width - borderRadius, e.CellBounds.Y, borderRadius, borderRadius, 270, 90);
                    path.AddArc(e.CellBounds.X + e.CellBounds.Width - borderRadius, e.CellBounds.Y + e.CellBounds.Height - borderRadius, borderRadius, borderRadius, 0, 90);
                    path.AddArc(e.CellBounds.X, e.CellBounds.Y + e.CellBounds.Height - borderRadius, borderRadius, borderRadius, 90, 90);
                    path.CloseFigure();


                    using (SolidBrush brush = new SolidBrush(backgroundColor))
                    {
                        e.Graphics.FillPath(brush, path);
                    }


                    TextRenderer.DrawText(e.Graphics, status, e.CellStyle.Font, e.CellBounds, Color.Black, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                }


                e.Handled = true;
            }
        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                guna2DataGridView1.Rows.Clear();

                string query = @"
            SELECT image, fname, mname, lname, suffix, position, chairmanship, birth_date, age, email, status, 
            FROM barangay_officials 
            WHERE fname LIKE @search OR mname LIKE @search OR lname LIKE @search";

                using (MySqlConnection conn = new MySqlConnection(dbconString.connection))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@search", "%" + txtSearch.Text.Trim() + "%");

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Image img = null;
                                if (reader["image"] != DBNull.Value)
                                {
                                    byte[] imgBytes = (byte[])reader["image"];
                                    using (MemoryStream ms = new MemoryStream(imgBytes))
                                    {
                                        img = new Bitmap(Image.FromStream(ms), new Size(50, 50));
                                    }
                                }

                                string fullName = $"{reader["fname"]} {reader["mname"]} {reader["lname"]} {reader["suffix"]}".Trim();
                                guna2DataGridView1.Rows.Add(
                                    img,
                                    fullName,
                                    reader["position"].ToString(),
                                    reader["chairmanship"].ToString(),
                                    Convert.ToDateTime(reader["birth_date"]).ToShortDateString(),
                                    reader["age"].ToString(),
                                    reader["status"].ToString(), // status from DB
                                    "",
                                    "",
                                    "",
                                    "ACTIVE",
                                    "..."
                                );
                            }
                        }
                    }
                }

                guna2DataGridView1.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Search error: " + ex.Message);
            }
        }

        private void guna2DataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

            e.Cancel = true;
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0 && e.RowIndex < guna2DataGridView1.Rows.Count)
            {

                if (e.ColumnIndex == guna2DataGridView1.Columns["colStatus"].Index)
                {
                    var row = guna2DataGridView1.Rows[e.RowIndex];
                    var cell = row.Cells["colStatus"];
                    string currentStatus = cell.Value?.ToString();
                    string newStatus = currentStatus == "ACTIVE" ? "INACTIVE" : "ACTIVE";
                    string residentID = row.Cells["colID"].Value?.ToString();


                    cell.Value = newStatus;
                    cell.Style.BackColor = newStatus == "ACTIVE" ? Color.Green : Color.Red;


                    if (e.RowIndex >= 0 && e.RowIndex < guna2DataGridView1.Rows.Count)
                    {
                        guna2DataGridView1.InvalidateCell(e.ColumnIndex, e.RowIndex);
                    }

                    // Update the status in the database
                    try
                    {
                        using (MySqlConnection conn = new MySqlConnection(dbconString.connection))
                        {
                            conn.Open();
                            string updateQuery = "UPDATE barangay_officials SET status = @status WHERE id_number = @id";

                            using (MySqlCommand cmd = new MySqlCommand(updateQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@status", newStatus);
                                cmd.Parameters.AddWithValue("@id", residentID);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Database update failed: " + ex.Message);
                    }
                }
                // Check if the clicked column is the "btnView" button column (assuming the column name is "btnView")
                else if (e.ColumnIndex == guna2DataGridView1.Columns["btnView"].Index)
                {
                    var row = guna2DataGridView1.Rows[e.RowIndex];
                    string idValue = row.Cells["colID"].Value?.ToString();

                    if (int.TryParse(idValue, out int officialId))
                    {
                        frmUpdateofficials updateForm = new frmUpdateofficials(officialId);
                        updateForm.ShowDialog();
                        Loadofficial(); // Refresh after closing update form
                    }
                    else
                    {
                        MessageBox.Show("Unable to get official ID.");
                    }
                }
            }
        }
    }
}