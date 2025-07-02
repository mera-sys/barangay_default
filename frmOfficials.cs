using barangay_management_system;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace barangay_default
{
    public partial class frmOfficials : Form
    {
        byte[] imageBytes;


        public frmOfficials()
        {
            InitializeComponent();
        }

        private void btnAddoffcials_Click(object sender, EventArgs e)
        {
            // Basic validation
            if (string.IsNullOrWhiteSpace(txtfullname.Text) || string.IsNullOrWhiteSpace(txtLname.Text) || imageBytes == null)
            {
                MessageBox.Show("Please complete required fields and select an image.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(dbconString.connection))
                {
                    string query = @"INSERT INTO barangay_officials 
            (fname, mname, lname, suffix, position, chairmanship, term_start, term_end, birth_place, 
            nationality, birth_date, age, civil_status, religion, tid, id_number, email, image)
            VALUES
            (@fname, @mname, @lname, @suffix, @position, @chairmanship, @term_start, @term_end, 
            @birth_place, @nationality, @birth_date, @age, @civil_status, @religion, @tid, @id_number, @email, @image)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@fname", txtfullname.Text.Trim());
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
                        cmd.Parameters.AddWithValue("@image", imageBytes);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                // Add log after successful insert
                Logger.LogAction("Admin", "CurrentUser", $"Added official: {txtfullname.Text} {txtLname.Text}");

                MessageBox.Show("Official added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ClearForm()
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is TextBox txt) txt.Clear();
                else if (ctrl is ComboBox cb) cb.SelectedIndex = -1;
            }

            dtStart.Value = DateTime.Today;
            dtEnd.Value = DateTime.Today;
            dtDATEofbirth.Value = DateTime.Today;
            pictureBox1.Image = null;
            imageBytes = null;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                openFileDialog.Title = "Select an Image";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = new Bitmap(openFileDialog.FileName);
                    imageBytes = File.ReadAllBytes(openFileDialog.FileName);
                }
            }
        }

        private void dtDATEofbirth_ValueChanged(object sender, EventArgs e)
        {
            // Get the selected date from the DateTimePicker
            DateTime birthDate = dtDATEofbirth.Value;

            // Calculate age
            int age = DateTime.Now.Year - birthDate.Year;

            // Adjust for birthdates later in the year
            if (birthDate > DateTime.Now.AddYears(-age))
            {
                age--;
            }

            // Display age in txtA
            txtAge.Text = age.ToString();

            // Set txtA text color to white
            txtAge.ForeColor = Color.White;

        }
        private bool isFetching = false; // Prevent infinite loop


        private void txtfullname_TextChanged(object sender, EventArgs e)
        {
            if (isFetching || string.IsNullOrWhiteSpace(txtfullname.Text)) return;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(dbconString.connection))
                {
                    conn.Open();

                    string query = @"SELECT FirstName, MiddleName, contact, LastName, ext, PlaceOfBirth, nationality, DateOfBirth, 
                                civilstatus, religion, TypeOfID, IdNumber, emailaddress, Image 
                         FROM Residents
                         WHERE FirstName LIKE @FirstName LIMIT 1";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@FirstName", txtfullname.Text.Trim() + "%");

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFetching = true;

                                txtfullname.Text = reader["FirstName"].ToString();
                                txtMname.Text = reader["MiddleName"].ToString();
                                txtLname.Text = reader["LastName"].ToString();
                                cboSuffix.Text = reader["ext"].ToString();
                                txtBplace.Text = reader["PlaceOfBirth"].ToString();
                                txtNationality.Text = reader["nationality"].ToString();
                                txtContact.Text = reader["contact"].ToString();

                                if (DateTime.TryParse(reader["DateOfBirth"].ToString(), out DateTime birthDate))
                                {
                                    dtDATEofbirth.Value = birthDate;

                                    int age = DateTime.Now.Year - birthDate.Year;
                                    if (birthDate > DateTime.Now.AddYears(-age)) age--;
                                    txtAge.Text = age.ToString();
                                }

                                cboCstatus.Text = reader["civilstatus"].ToString();
                                txtReligion.Text = reader["religion"].ToString();
                                cboTid.Text = reader["TypeOfID"].ToString();
                                txtIdno.Text = reader["IdNumber"].ToString();
                                txtEmail.Text = reader["emailaddress"].ToString();

                                // 🔽 Image processing
                                if (!reader.IsDBNull(reader.GetOrdinal("Image")))
                                {
                                    byte[] imgBytes = (byte[])reader["Image"];
                                    using (MemoryStream ms = new MemoryStream(imgBytes))
                                    {
                                        pictureBox1.Image = Image.FromStream(ms);
                                        imageBytes = imgBytes; // If you want to reuse/save this image
                                    }
                                }
                                else
                                {
                                    pictureBox1.Image = null; // Clear image if no photo in DB
                                    imageBytes = null;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching resident data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                isFetching = false;
            }
        }
    }
}