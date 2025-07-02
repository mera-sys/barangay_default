using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;
using barangay_management_system;

namespace barangay_default
{
    public partial class frmUpdateofficials : Form
    {
        private int officialId;
        public frmUpdateofficials(int id)
        {
            InitializeComponent();
            officialId = id;
            LoadOfficialData();
        }
        private void LoadOfficialData()
        {
            using (MySqlConnection conn = new MySqlConnection(dbconString.connection))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM barangay_officials WHERE id = @id";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", officialId);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Populate form controls with data
                                txtFname.Text = reader["fname"].ToString();
                                txtMname.Text = reader["mname"].ToString();
                                txtLname.Text = reader["lname"].ToString();
                                cboSuffix.Text = reader["suffix"].ToString();
                                cboPosition.Text = reader["position"].ToString();
                                CboChairmanship.Text = reader["chairmanship"].ToString();
                                dtStart.Value = Convert.ToDateTime(reader["term_start"]);
                                dtEnd.Value = Convert.ToDateTime(reader["term_end"]);
                                txtBplace.Text = reader["birth_place"].ToString();
                                txtNationality.Text = reader["nationality"].ToString();
                                dtDATEofbirth.Value = Convert.ToDateTime(reader["birth_date"]);
                                txtAge.Text = reader["age"].ToString();
                                cboCstatus.Text = reader["civil_status"].ToString();
                                txtReligion.Text = reader["religion"].ToString();
                                cboTid.Text = reader["tid"].ToString();
                                txtIdno.Text = reader["id_number"].ToString();
                                txtEmail.Text = reader["email"].ToString();

                                if (reader["image"] != DBNull.Value)
                                {
                                    byte[] imgBytes = (byte[])reader["image"];
                                    using (MemoryStream ms = new MemoryStream(imgBytes))
                                    {
                                        pictureBox1.Image = Image.FromStream(ms);
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading data: " + ex.Message);
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(dbconString.connection))
                {
                    conn.Open();

                    string updateQuery = @"
            UPDATE barangay_officials 
            SET fname = @fname, mname = @mname, lname = @lname, suffix = @suffix, 
                position = @position, chairmanship = @chairmanship, term_start = @term_start, 
                term_end = @term_end, birth_place = @birth_place, nationality = @nationality, 
                birth_date = @birth_date, age = @age, civil_status = @civil_status, 
                religion = @religion, tid = @tid, id_number = @id_number, email = @email
            WHERE id = @id";

                    using (MySqlCommand cmd = new MySqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@fname", txtFname.Text.Trim());
                        cmd.Parameters.AddWithValue("@mname", txtMname.Text.Trim());
                        cmd.Parameters.AddWithValue("@lname", txtLname.Text.Trim());
                        cmd.Parameters.AddWithValue("@suffix", cboSuffix.Text);
                        cmd.Parameters.AddWithValue("@position", cboPosition.Text);
                        cmd.Parameters.AddWithValue("@chairmanship", CboChairmanship.Text);
                        cmd.Parameters.AddWithValue("@term_start", dtStart.Value.Date);
                        cmd.Parameters.AddWithValue("@term_end", dtEnd.Value.Date);
                        cmd.Parameters.AddWithValue("@birth_place", txtBplace.Text.Trim());
                        cmd.Parameters.AddWithValue("@nationality", txtNationality.Text.Trim());
                        cmd.Parameters.AddWithValue("@birth_date", dtDATEofbirth.Value.Date);
                        cmd.Parameters.AddWithValue("@age", Convert.ToInt32(txtAge.Text));
                        cmd.Parameters.AddWithValue("@civil_status", cboCstatus.Text);
                        cmd.Parameters.AddWithValue("@religion", txtReligion.Text.Trim());
                        cmd.Parameters.AddWithValue("@tid", cboTid.Text);
                        cmd.Parameters.AddWithValue("@id_number", txtIdno.Text.Trim());
                        cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@id", officialId);

                        cmd.ExecuteNonQuery();
                    }

                    // Update image if it was changed
                    if (pictureBox1.Image != null)
                    {
                        using (MySqlConnection conn2 = new MySqlConnection(dbconString.connection))
                        {
                            conn2.Open();

                            string updateImageQuery = "UPDATE barangay_officials SET image = @image WHERE id = @id";

                            using (MySqlCommand cmd2 = new MySqlCommand(updateImageQuery, conn2))
                            {
                                using (MemoryStream ms = new MemoryStream())
                                {
                                    pictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                                    byte[] imageBytes = ms.ToArray();
                                    cmd2.Parameters.AddWithValue("@image", imageBytes);
                                }

                                cmd2.Parameters.AddWithValue("@id", officialId);
                                cmd2.ExecuteNonQuery();
                            }
                        }
                    }

                    // ✅ Log update action
                    Logger.LogAction("Admin", "CurrentUser", $"Updated official: {txtFname.Text} {txtLname.Text} (ID: {officialId})");

                    MessageBox.Show("Official details updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating official: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // Create an OpenFileDialog to allow the user to select an image file
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif",  // Allow common image formats
                Title = "Select Image"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Load the selected image into the PictureBox
                    pictureBox1.Image = Image.FromFile(openFileDialog.FileName);

                    // Optionally, you can update the UI or perform other actions here
                    // For example, you can set a flag to indicate the image has been changed
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}

