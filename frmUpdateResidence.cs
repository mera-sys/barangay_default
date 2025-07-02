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
using barangay_management_system;
using MySql.Data.MySqlClient;

namespace barangay_default
{
    public partial class frmUpdateResidence : Form
    {
        private byte[] imageBytes;
        private object residentId;
        public string ResidentID { get; set; }

        public frmUpdateResidence(string residentID)
        {
            InitializeComponent();
            ResidentID = residentID;
        }
        

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void UpdateFullName()
        {
            string firstName = txtFname.Text.Trim();
            string middleName = txtMname.Text.Trim();
            string lastName = txtLname.Text.Trim();
            string suffix = cboSuffix.SelectedItem?.ToString() ?? ""; // Use an empty string if no suffix is selected

            // Combine the values into the full name
            string fullName = $"{firstName} {middleName} {lastName} {suffix}".Trim();

            // Display the full name in the label
            lbFulname.Text = fullName;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // Create a new OpenFileDialog to allow the user to select an image file
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif"; // Image file types
                openFileDialog.Title = "Select an Image";

                // If the user selects an image, load it into the PictureBox and convert it to bytes
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Load the selected image into PictureBox1
                    pictureBox1.Image = new Bitmap(openFileDialog.FileName);

                    // Convert the image to byte array (for storing in the database)
                    imageBytes = File.ReadAllBytes(openFileDialog.FileName);
                }
            }
        }

        private void dtBdate_ValueChanged(object sender, EventArgs e)
        {
            // Get the selected date from the DateTimePicker
            DateTime birthDate = dtBdate.Value;

            // Calculate age
            int age = DateTime.Now.Year - birthDate.Year;

            // Adjust for birthdates later in the year
            if (birthDate > DateTime.Now.AddYears(-age))
            {
                age--;
            }

            // Display age in txtA
            txtA.Text = age.ToString();

            // Set txtA text color to white
            txtA.ForeColor = Color.White;


            txtSenior.Text = (age >= 60) ? "YES" : "NO";
        }

        private void txtFname_TextChanged(object sender, EventArgs e)
        {
            UpdateFullName();
        }

        private void txtFname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ' && !char.IsControl(e.KeyChar))
            {

                e.Handled = true;
            }
        }

        private void txtMname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ' && !char.IsControl(e.KeyChar))
            {

                e.Handled = true;
            }
        }

        private void txtMname_TextChanged(object sender, EventArgs e)
        {
            UpdateFullName();
        }

        private void txtLname_TextChanged(object sender, EventArgs e)
        {
            UpdateFullName();
        }

        private void txtLname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ' && !char.IsControl(e.KeyChar))
            {

                e.Handled = true;
            }
        }

        private void cboSuffix_TextChanged(object sender, EventArgs e)
        {
            UpdateFullName();
        }

        private void txtOccupation_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ' && !char.IsControl(e.KeyChar))
            {

                e.Handled = true;
            }
        }

        private void txtLresidency_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {

                e.Handled = true;
            }
        }

        private void cboFhead_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboFhead.SelectedItem != null && cboFhead.SelectedItem.ToString() == "Yes")
            {
                // If Family Head is Yes, enable the Family Income field and disable the Individual Income field
                txtFincome.Enabled = true;
                txtIincome.Enabled = false;
            }
            else if (cboFhead.SelectedItem != null && cboFhead.SelectedItem.ToString() == "No")
            {
                // If Family Head is No, enable the Individual Income field and disable the Family Income field
                txtFincome.Enabled = false;
                txtIincome.Enabled = true;
            }
        }

        private void txtFincome_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only digits and control keys (like backspace)
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Prevent invalid input
            }
        }

        private void txtFincome_TextChanged(object sender, EventArgs e)
        {
            // Check if the value entered in txtFincome is a valid number and below the threshold
            if (decimal.TryParse(txtFincome.Text, out decimal income) && income <= 12082)
            {
                txtIndigent.Text = "Yes"; // Set txtIndigent to "Yes" if income is below threshold
            }
            else
            {
                txtIndigent.Text = "No"; // Otherwise set it to "No"
            }
        }

        private void txtIincome_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only digits and control keys (like backspace)
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Prevent invalid input
            }
        }

        private void txtIincome_TextChanged(object sender, EventArgs e)
        {
            // Check if the value entered in txtIincome is a valid number and below the threshold
            if (decimal.TryParse(txtIincome.Text, out decimal income) && income <= 2500)
            {
                txtIndigent.Text = "Yes"; // Set cboIndigent to "Yes" if income is below threshold
            }
            else
            {
                txtIndigent.Text = "No"; // Otherwise set it to "No"
            }
        }

        private void txtIdno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {

                e.Handled = true;
            }
        }

        private void txtZip_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {

                e.Handled = true;
            }
        }

        private void txtHno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {

                e.Handled = true;
            }
        }

        private void txtMunicipality_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ' && !char.IsControl(e.KeyChar))
            {

                e.Handled = true;
            }
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if any required fields are null
                if (string.IsNullOrEmpty(txtFname.Text) || string.IsNullOrEmpty(txtLname.Text) ||
                    string.IsNullOrEmpty(txtBplace.Text) || string.IsNullOrEmpty(txtNationality.Text) ||
                    string.IsNullOrEmpty(txtOccupation.Text) || cboGender.SelectedItem == null ||
                    cboCstatus.SelectedItem == null)
                {
                    MessageBox.Show("Please fill all required fields.");
                    return;
                }

                // Ensure the database connection is valid
                using (MySqlConnection conn = new MySqlConnection(dbconString.connection))
                {
                    conn.Open();
                    string query = "UPDATE residents SET FirstName = @FirstName, MiddleName = @MiddleName, LastName = @LastName, Ext = @Ext, " +
                 "PlaceOfBirth = @PlaceOfBirth, DateOfBirth = @DateOfBirth, Sex = @Sex, CivilStatus = @CivilStatus, " +
                 "Citizenship = @Citizenship, Occupation = @Occupation, Religion = @Religion, LengthOfResidency = @LengthOfResidency, " +
                 "FamilyHead = @FamilyHead, FamilyIncome = @FamilyIncome, IndividualIncome = @IndividualIncome, " +
                 "Indigent = @Indigent, TypeOfID = @TypeOfID, IDNumber = @IDNumber, EmailAddress = @EmailAddress, " +
                 "ZipCode = @ZipCode, Barangay = @Barangay, Purok = @Purok, HouseNumber = @HouseNumber, Street = @Street, " +
                 "Municipality = @Municipality, Voters = @Voters, Senior = @Senior, PWD = @PWD, SingleParent = @SingleParent, " +
                 "Contact = @Contact WHERE ResidentID = @ResidentID";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        // Add parameters and check for null values
                        cmd.Parameters.AddWithValue("@FirstName", txtFname.Text ?? "");
                        cmd.Parameters.AddWithValue("@MiddleName", txtMname.Text ?? "");
                        cmd.Parameters.AddWithValue("@LastName", txtLname.Text ?? "");
                        cmd.Parameters.AddWithValue("@Ext", cboSuffix.Text ?? "");
                        cmd.Parameters.AddWithValue("@PlaceOfBirth", txtBplace.Text ?? "");
                        cmd.Parameters.AddWithValue("@DateOfBirth", dtBdate.Value);
                        cmd.Parameters.AddWithValue("@Sex", cboGender.SelectedItem?.ToString() ?? "");
                        cmd.Parameters.AddWithValue("@CivilStatus", cboCstatus.SelectedItem?.ToString() ?? "");
                        cmd.Parameters.AddWithValue("@Citizenship", txtNationality.Text ?? "");
                        cmd.Parameters.AddWithValue("@Occupation", txtOccupation.Text ?? "");
                        cmd.Parameters.AddWithValue("@Religion", txtReligion.Text ?? "");
                        cmd.Parameters.AddWithValue("@LengthOfResidency", txtLresidency.Text ?? "");
                        cmd.Parameters.AddWithValue("@FamilyHead", cboFhead.SelectedItem?.ToString() ?? "No");
                        cmd.Parameters.AddWithValue("@FamilyIncome", txtFincome.Text ?? "0");
                        cmd.Parameters.AddWithValue("@IndividualIncome", txtIincome.Text ?? "0");
                        cmd.Parameters.AddWithValue("@Indigent", txtIndigent.Text ?? "No");
                        cmd.Parameters.AddWithValue("@TypeOfID", cboTid.SelectedItem?.ToString() ?? "");
                        cmd.Parameters.AddWithValue("@IDNumber", txtIdno.Text ?? "");
                        cmd.Parameters.AddWithValue("@EmailAddress", txtEmail.Text ?? "");
                        cmd.Parameters.AddWithValue("@ZipCode", txtZip.Text ?? "");
                        cmd.Parameters.AddWithValue("@Barangay", txtBarangay.Text ?? "");
                        cmd.Parameters.AddWithValue("@Purok", cboPurokno.Text ?? "");
                        cmd.Parameters.AddWithValue("@HouseNumber", txtHno.Text ?? "");
                        cmd.Parameters.AddWithValue("@Street", txtStreet.Text ?? "");
                        cmd.Parameters.AddWithValue("@Municipality", txtMunicipality.Text ?? "");
                        cmd.Parameters.AddWithValue("@Voters", cboRvoter.SelectedItem?.ToString() ?? "");
                        cmd.Parameters.AddWithValue("@Senior", txtSenior.Text ?? "");
                        cmd.Parameters.AddWithValue("@PWD", cboPwd.SelectedItem?.ToString() ?? "");
                        cmd.Parameters.AddWithValue("@SingleParent", cboSparent.SelectedItem?.ToString() ?? "");
                        cmd.Parameters.AddWithValue("@Contact", txtContact.Text ?? "");
                        cmd.Parameters.AddWithValue("@ResidentID", ResidentID);  // Use the provided ResidentID to identify the record

                        // Execute the query
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Resident details updated successfully.");
                        }
                        else
                        {
                            MessageBox.Show("Update failed.");
                        }
                    }
                }
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show("A required value was not set: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frmUpdateResidence_Load(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(dbconString.connection))
            {
                conn.Open();
                string query = "SELECT * FROM residents WHERE ResidentID = @ResidentID";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ResidentID", ResidentID);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtFname.Text = reader["FirstName"].ToString();
                            txtMname.Text = reader["MiddleName"].ToString();
                            txtLname.Text = reader["LastName"].ToString();
                            cboSuffix.Text = reader["Ext"].ToString();
                            txtBplace.Text = reader["PlaceOfBirth"].ToString();
                            dtBdate.Value = Convert.ToDateTime(reader["DateOfBirth"]);
                            cboGender.Text = reader["Sex"].ToString();
                            cboCstatus.Text = reader["CivilStatus"].ToString();
                            txtNationality.Text = reader["Citizenship"].ToString();
                            txtOccupation.Text = reader["Occupation"].ToString();
                            txtReligion.Text = reader["Religion"].ToString();
                            txtLresidency.Text = reader["LengthOfResidency"].ToString();
                            cboFhead.Text = reader["FamilyHead"].ToString();
                            txtFincome.Text = reader["FamilyIncome"].ToString();
                            txtIincome.Text = reader["IndividualIncome"].ToString();
                            txtIndigent.Text = reader["Indigent"].ToString();
                            cboTid.Text = reader["TypeOfID"].ToString();
                            txtIdno.Text = reader["IDNumber"].ToString();
                            txtEmail.Text = reader["EmailAddress"].ToString();
                            txtZip.Text = reader["ZipCode"].ToString();
                            txtBarangay.Text = reader["Barangay"].ToString();
                            cboPurokno.Text = reader["Purok"].ToString();
                            txtHno.Text = reader["HouseNumber"].ToString();
                            txtStreet.Text = reader["Street"].ToString();
                            txtMunicipality.Text = reader["Municipality"].ToString();
                            cboRvoter.Text = reader["Voters"].ToString();
                            txtSenior.Text = reader["Senior"].ToString();
                            cboPwd.Text = reader["PWD"].ToString();
                            cboSparent.Text = reader["SingleParent"].ToString();
                            txtContact.Text = reader["Contact"].ToString();

                            // Load image if it exists
                            if (reader["Image"] != DBNull.Value)
                            {
                                byte[] imageData = (byte[])reader["Image"];
                                using (MemoryStream ms = new MemoryStream(imageData))
                                {
                                    pictureBox1.Image = Image.FromStream(ms);
                                }
                            }

                            UpdateFullName();
                        }
                    }
                }
            }
        }
    }
 }
 
    

