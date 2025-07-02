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
    public partial class frmaddResidence : Form
    {
        public frmaddResidence()
        {
            InitializeComponent();

        }


        private void cboFhead_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboFhead.SelectedItem != null && cboFhead.SelectedItem.ToString() == "Yes")
            {
                
                txtFincome.Enabled = true;
                txtIincome.Enabled = false;
            }
            else if (cboFhead.SelectedItem != null && cboFhead.SelectedItem.ToString() == "No")
            {
                
                txtFincome.Enabled = false;
                txtIincome.Enabled = true;
            }
        }

        private void txtFincome_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void txtIincome_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtFname_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtMname_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtLname_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void cboSuffix_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
        private void UpdateFullName()
        {
            string firstName = txtFname.Text.Trim();
            string middleName = txtMname.Text.Trim();
            string lastName = txtLname.Text.Trim();
            string suffix = cboSuffix.SelectedItem?.ToString() ?? "";

            // Combine the values into the full name
            string fullName = $"{firstName} {middleName} {lastName} {suffix}".Trim();

            // Display the full name in the label
            lbFulname.Text = fullName;
        }

        private void frmaddResidence_Load(object sender, EventArgs e)
        {

        }


        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        private byte[] imageBytes;

        private void pictureBox1_Click(object sender, EventArgs e)
        {
           
        }
        private void button2_Click(object sender, EventArgs e)
        {
            
        }
        private void ClearFormFields()
        {
            // Clear text boxes
            txtFname.Clear();
            txtMname.Clear();
            txtLname.Clear();
            txtBplace.Clear();
            txtA.Clear();
            txtNationality.Clear();
            txtOccupation.Clear();
            txtReligion.Clear();
            txtLresidency.Clear();
            txtFincome.Clear();
            txtIincome.Clear();
            txtIdno.Clear();
            txtEmail.Clear();
            txtZip.Clear();
            txtBarangay.Clear();
            txtHno.Clear();
            txtStreet.Clear();
            txtMunicipality.Clear();
            txtContact.Clear();
            txtSenior.Clear();
            txtIndigent.Clear();

            // Reset combo boxes to default (you can set it to a specific default value or null)
            cboGender.SelectedIndex = -1;
            cboCstatus.SelectedIndex = -1;
            cboFhead.SelectedIndex = -1;
            
            cboTid.SelectedIndex = -1;
            cboPurokno.SelectedIndex = -1;
            cboRvoter.SelectedIndex = -1;
           
            cboPwd.SelectedIndex = -1;
            cboSparent.SelectedIndex = -1;
            cboSuffix.SelectedIndex = -1;  // Reset cboSuffix ComboBox

            // Clear PictureBox image
            pictureBox1.Image = null;  // Reset the image in pictureBox1
            // Reset DatePicker
            dtBdate.Value = DateTime.Now;
        }

        private string GeneratePassword()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, 8) // 8-character password
                                .Select(s => s[random.Next(s.Length)])
                                .ToArray());
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {

                e.Handled = true;
            }
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
           
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click_2(object sender, EventArgs e)
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

        private void dtBdate_ValueChanged_1(object sender, EventArgs e)
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


        private void cboFhead_SelectedIndexChanged_1(object sender, EventArgs e)
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

        private void txtFname_TextChanged_1(object sender, EventArgs e)
        {
            UpdateFullName();
        }

        private void txtFname_KeyPress_1(object sender, KeyPressEventArgs e)
        
            {
                if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ' && !char.IsControl(e.KeyChar))
                {

                    e.Handled = true;
                }
            }

        private void txtMname_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ' && !char.IsControl(e.KeyChar))
            {

                e.Handled = true;
            }
        }

        private void txtLname_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ' && !char.IsControl(e.KeyChar))
            {

                e.Handled = true;
            }
        }

        private void txtLresidency_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {

                e.Handled = true;
            }
        }

        private void txtFincome_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            // Allow only digits and control keys (like backspace)
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Prevent invalid input
            }
        }

        private void txtIdno_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {

                e.Handled = true;
            }
        }

        private void txtZip_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {

                e.Handled = true;
            }
        }

        private void txtHno_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {

                e.Handled = true;
            }
        }

        private void txtMunicipality_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ' && !char.IsControl(e.KeyChar))
            {

                e.Handled = true;
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

        private void txtFincome_TextChanged_1(object sender, EventArgs e)
        {
            // Check if the value entered in txtFincome is a valid number and below the threshold
            if (decimal.TryParse(txtFincome.Text, out decimal income) && income <= 12082)
            {
                txtIndigent.Text = "YES"; // Set txtIndigent to "Yes" if income is below threshold
            }
            else
            {
                txtIndigent.Text = "NO"; // Otherwise set it to "No"
            }
        }

        private void txtIincome_TextChanged_1(object sender, EventArgs e)
        {
            // Check if the value entered in txtIincome is a valid number and below the threshold
            if (decimal.TryParse(txtIincome.Text, out decimal income) && income <= 2500)
            {
                txtIndigent.Text = "YES"; // Set cboIndigent to "Yes" if income is below threshold
            }
            else
            {
                txtIndigent.Text = "NO"; // Otherwise set it to "No"
            }
        }

        private void txtContact_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {

                e.Handled = true;
            }
        }

        private void txtMname_TextChanged_1(object sender, EventArgs e)
        {
            UpdateFullName();
        }

        private void txtLname_TextChanged_1(object sender, EventArgs e)
        {
            UpdateFullName();
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

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Create a connection object using the dbconString connection
                using (MySqlConnection conn = new MySqlConnection(dbconString.connection))
                {
                    // Open the connection
                    conn.Open();

                    // Check if a resident with the same name and birthdate already exists
                    string checkQuery = @"SELECT COUNT(*) FROM Residents 
                                  WHERE FirstName = @FirstName AND LastName = @LastName AND DateOfBirth = @DateOfBirth";
                    using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn))
                    {
                        // Add parameters to the check query
                        checkCmd.Parameters.AddWithValue("@FirstName", txtFname.Text);
                        checkCmd.Parameters.AddWithValue("@LastName", txtLname.Text);
                        checkCmd.Parameters.AddWithValue("@DateOfBirth", dtBdate.Value);

                        // Execute the query to check if a record already exists
                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                        // If a resident with the same name and birthdate exists, show a message and return
                        if (count > 0)
                        {
                            MessageBox.Show("A resident with the same name and birthdate already exists.", "Duplicate Resident", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return; // Do not proceed with the insertion
                        }
                    }

                    // Prepare the SQL query to insert data into the Residents table
                    string query = @"INSERT INTO Residents (FirstName, MiddleName, LastName, Ext, PlaceOfBirth, DateOfBirth, Age, 
                 Sex, CivilStatus, Citizenship, Occupation, Religion, LengthOfResidency, FamilyHead, 
                 FamilyIncome, IndividualIncome, Indigent, TypeOfID, IDNumber, EmailAddress, ZipCode, 
                 Barangay, Purok, HouseNumber, Street, Municipality, Voters, Senior, Nationality, PWD, 
                 SingleParent, Contact, Image, Status, DateAdded) 
                 VALUES (@FirstName, @MiddleName, @LastName, @Ext, @PlaceOfBirth, @DateOfBirth, @Age, 
                 @Sex, @CivilStatus, @Citizenship, @Occupation, @Religion, @LengthOfResidency, 
                 @FamilyHead, @FamilyIncome, @IndividualIncome, @Indigent, @TypeOfID, @IDNumber, 
                 @EmailAddress, @ZipCode, @Barangay, @Purok, @HouseNumber, @Street, @Municipality, 
                 @Voters, @Senior, @Nationality, @PWD, @SingleParent, @Contact, @Image, @Status, NOW())";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        // Add parameters to the command
                        cmd.Parameters.AddWithValue("@FirstName", txtFname.Text);
                        cmd.Parameters.AddWithValue("@MiddleName", txtMname.Text);
                        cmd.Parameters.AddWithValue("@LastName", txtLname.Text);
                        cmd.Parameters.AddWithValue("@Ext", cboSuffix.Text);
                        cmd.Parameters.AddWithValue("@PlaceOfBirth", txtBplace.Text);
                        cmd.Parameters.AddWithValue("@DateOfBirth", dtBdate.Value);
                        cmd.Parameters.AddWithValue("@Age", txtA.Text); // Assuming txtA is where the age is displayed
                        cmd.Parameters.AddWithValue("@Sex", cboGender.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@CivilStatus", cboCstatus.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@Citizenship", txtNationality.Text);
                        cmd.Parameters.AddWithValue("@Occupation", txtOccupation.Text);
                        cmd.Parameters.AddWithValue("@Religion", txtReligion.Text);
                        cmd.Parameters.AddWithValue("@LengthOfResidency", txtLresidency.Text);
                        cmd.Parameters.AddWithValue("@FamilyHead", cboFhead.SelectedItem?.ToString() == "Yes" ? "Yes" : "No");
                        cmd.Parameters.AddWithValue("@FamilyIncome", txtFincome.Text);
                        cmd.Parameters.AddWithValue("@IndividualIncome", txtIincome.Text);
                        cmd.Parameters.AddWithValue("@Indigent", txtIndigent.Text);
                        cmd.Parameters.AddWithValue("@TypeOfID", cboTid.Text);
                        cmd.Parameters.AddWithValue("@IDNumber", txtIdno.Text);
                        cmd.Parameters.AddWithValue("@EmailAddress", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@ZipCode", txtZip.Text);
                        cmd.Parameters.AddWithValue("@Barangay", txtBarangay.Text);
                        cmd.Parameters.AddWithValue("@Purok", cboPurokno.Text);
                        cmd.Parameters.AddWithValue("@HouseNumber", txtHno.Text);
                        cmd.Parameters.AddWithValue("@Street", txtStreet.Text);
                        cmd.Parameters.AddWithValue("@Municipality", txtMunicipality.Text);
                        cmd.Parameters.AddWithValue("@Voters", cboRvoter.SelectedItem.ToString() == "Yes" ? "Yes" : "No");
                        cmd.Parameters.AddWithValue("@Senior", txtSenior.Text);
                        cmd.Parameters.AddWithValue("@Nationality", txtNationality.Text);
                        cmd.Parameters.AddWithValue("@PWD", cboPwd.SelectedItem.ToString() == "Yes" ? "Yes" : "No");
                        cmd.Parameters.AddWithValue("@SingleParent", cboSparent.SelectedItem.ToString() == "Yes" ? "Yes" : "No");
                        cmd.Parameters.AddWithValue("@Contact", txtContact.Text);

                        // Add the image byte array parameter
                        if (imageBytes != null && imageBytes.Length > 0)
                        {
                            cmd.Parameters.AddWithValue("@Image", imageBytes);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Image", DBNull.Value); // If no image is selected, store NULL
                        }

                        cmd.Parameters.AddWithValue("@Status", "ACTIVE");

                        // Execute the command to insert the resident data
                        cmd.ExecuteNonQuery();
                    }

                    // Generate the username and password for the new resident account
                    string fullName = txtFname.Text + " " + txtLname.Text; // Concatenate first and last name
                    string username = txtFname.Text.Substring(0, 3) + txtLname.Text.Substring(0, 3); // Example: "JohDoe"
                    string password = GeneratePassword(); // Use a method to generate a secure password

                    // Prepare the SQL query to insert account data, including FullName
                    string insertAccountQuery = "INSERT INTO Accounts (Username, Password, FullName, ResidentID) " +
                                                "VALUES (@Username, @Password, @FullName, LAST_INSERT_ID())";

                    // Create a command object for inserting account data
                    using (MySqlCommand cmdAccount = new MySqlCommand(insertAccountQuery, conn))
                    {
                        // Add parameters to the command
                        cmdAccount.Parameters.AddWithValue("@Username", username);
                        cmdAccount.Parameters.AddWithValue("@Password", password); // Password should be hashed before inserting in production apps
                        cmdAccount.Parameters.AddWithValue("@FullName", fullName); // Insert the full name

                        // Execute the command to insert the account data
                        cmdAccount.ExecuteNonQuery();
                    }

                    // Show a message box indicating success
                    MessageBox.Show("Resident and account added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Clear the form fields after successful insertion
                    ClearFormFields();
                }
            }
            catch (Exception ex)
            {
                // Show an error message if something goes wrong
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
