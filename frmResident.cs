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
using System.Security.Cryptography;  
namespace barangay_management_system
{
    public partial class frmResident : Form
    {
         MySqlConnection cn;
        MySqlCommand cm;
        frmResidentsList f;
        public string _id;
        public frmResident(frmResidentsList f)
        {
            InitializeComponent();
            cn = new  MySqlConnection(dbconString.connection);
            this.f = f; 

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you want to update this record?", var._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // Validate required fields
                    if (string.IsNullOrEmpty(txtLname.Text) || string.IsNullOrEmpty(txtFname.Text))
                    {
                        MessageBox.Show("Last name and First name are required.", var._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Validate numeric fields
                    if (!int.TryParse(txtA.Text, out int age))
                    {
                        MessageBox.Show("Please enter a valid numeric value for Age.", var._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (!decimal.TryParse(txtFincome.Text, out decimal fincome))
                    {
                        MessageBox.Show("Please enter a valid numeric value for Financial Income.", var._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (!int.TryParse(txtHno.Text, out int hnumber))
                    {
                        MessageBox.Show("Please enter a valid numeric value for House Number.", var._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Open SQL connection
                    if (cn.State != ConnectionState.Open)
                    {
                        cn.Open();
                    }

                    // Convert image to byte array
                    MemoryStream ms = new MemoryStream();
                    picImage.BackgroundImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    byte[] arrImage = ms.ToArray();


                    // Insert query
                    cm = new MySqlCommand("UPDATE tblResident SET lname = @lname, fname = @fname, mname = @mname, suffix = @suffix, bdate = @bdate, bplace = @bplace, age = @age, civilstatus = @civilstatus, gender = @gender, nationality = @nationality, religion = @religion, occupation = @occupation, lresidency = @lresidency, contact = @contact, pwd = @pwd, fincome = @fincome, indigent = @indigent, sparent = @sparent, rvoter = @rvoter, purok = @purok, fhead = @fhead, senior = @senior, hnumber = @hnumber, address = @address, street = @street, typid = @typid, idnumber = @idnumber, pic = @pic WHERE id = @id", cn);
                    // Add parameters
                    cm.Parameters.AddWithValue("@lname", txtLname.Text);
                    cm.Parameters.AddWithValue("@fname", txtFname.Text);
                    cm.Parameters.AddWithValue("@mname", txtMname.Text);
                    cm.Parameters.AddWithValue("@suffix", cboSuffix.Text ?? string.Empty);
                    cm.Parameters.AddWithValue("@bdate", dtBdate.Value);
                    cm.Parameters.AddWithValue("@bplace", txtBplace.Text);
                    cm.Parameters.AddWithValue("@age", age);
                    cm.Parameters.AddWithValue("@civilstatus", cboCstatus.Text);
                    cm.Parameters.AddWithValue("@gender", cboGender.Text);
                    cm.Parameters.AddWithValue("@nationality", txtNationality.Text);
                    cm.Parameters.AddWithValue("@religion", txtReligion.Text);
                    cm.Parameters.AddWithValue("@occupation", txtOccupation.Text);
                    cm.Parameters.AddWithValue("@lresidency", txtLresidency.Text);
                    cm.Parameters.AddWithValue("@contact", txtCnumber.Text);
                    cm.Parameters.AddWithValue("@pwd", cboPwd.Text);
                    cm.Parameters.AddWithValue("@fincome", fincome);
                    cm.Parameters.AddWithValue("@indigent", cboIndigent.Text);
                    cm.Parameters.AddWithValue("@sparent", cboSparent.Text);
                    cm.Parameters.AddWithValue("@rvoter", cboRvoter.Text);
                    cm.Parameters.AddWithValue("@purok", cboPurokno.Text);
                    cm.Parameters.AddWithValue("@senior", cboSenior.Text);
                    cm.Parameters.AddWithValue("@fhead", cboFhead.Text);
                    cm.Parameters.AddWithValue("@hnumber", hnumber);
                    cm.Parameters.AddWithValue("@address", txtAddress.Text);
                    cm.Parameters.AddWithValue("@street", txtStreet.Text);
                    cm.Parameters.AddWithValue("@typid", cboTid.Text);
                    cm.Parameters.AddWithValue("@idnumber", txtIdno.Text);
                    cm.Parameters.AddWithValue("@pic", arrImage);
                    cm.Parameters.AddWithValue("@id", _id);



                    // Execute query
                    cm.ExecuteNonQuery();
                    cn.Close();


                    MessageBox.Show("Record saved successfully", var._title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    f.Loadrecords();
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
                MessageBox.Show(ex.Message, var._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you want to save this record?", var._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // Validate required fields
                    if (string.IsNullOrEmpty(txtLname.Text) || string.IsNullOrEmpty(txtFname.Text))
                    {
                        MessageBox.Show("Last name and First name are required.", var._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Check for duplicates based on lname, fname, mname, suffix, and bdate
                    string checkQuery = "SELECT COUNT(*) FROM tblResident WHERE lname = @lname AND fname = @fname AND mname = @mname";
                    cm = new MySqlCommand(checkQuery, cn);
                    cm.Parameters.AddWithValue("@lname", txtLname.Text);
                    cm.Parameters.AddWithValue("@fname", txtFname.Text);
                    cm.Parameters.AddWithValue("@mname", txtMname.Text);


                    if (cn.State != ConnectionState.Open)
                    {
                        cn.Open();
                    }

                    object result = cm.ExecuteScalar();
                    int count = (result != null) ? Convert.ToInt32(result) : 0;


                    if (count > 0)
                    {
                        MessageBox.Show("A resident with the same name already exists.", var._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }


                    // Validate other fields
                    if (!int.TryParse(txtA.Text, out int age) || !decimal.TryParse(txtFincome.Text, out decimal fincome) || !int.TryParse(txtHno.Text, out int hnumber))
                    {
                        MessageBox.Show("Please ensure Age, Financial Income, and House Number are valid numbers.", var._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Convert image to byte array
                    MemoryStream ms = new MemoryStream();
                    picImage.BackgroundImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    byte[] arrImage = ms.GetBuffer();

                    // Insert into tblResident
                    cm = new MySqlCommand("INSERT INTO tblResident (lname, fname, mname, suffix, bdate, bplace, age, civilstatus, gender, nationality, religion, occupation, lresidency, contact, pwd, fincome, indigent, sparent, rvoter, purok, fhead, senior, hnumber, address, street, typid, idnumber, pic, date_inserted) " +
                                          "VALUES (@lname, @fname, @mname, @suffix, @bdate, @bplace, @age, @civilstatus, @gender, @nationality, @religion, @occupation, @lresidency, @contact, @pwd, @fincome, @indigent, @sparent, @rvoter, @purok, @fhead, @senior, @hnumber, @address, @street, @typid, @idnumber, @pic, @date_inserted);", cn);

                    cm.Parameters.AddWithValue("@lname", txtLname.Text);
                    cm.Parameters.AddWithValue("@fname", txtFname.Text);
                    cm.Parameters.AddWithValue("@mname", txtMname.Text);
                    cm.Parameters.AddWithValue("@suffix", cboSuffix.Text ?? string.Empty);
                    cm.Parameters.AddWithValue("@bdate", dtBdate.Value);
                    cm.Parameters.AddWithValue("@bplace", txtBplace.Text);
                    cm.Parameters.AddWithValue("@age", age);
                    cm.Parameters.AddWithValue("@civilstatus", cboCstatus.Text);
                    cm.Parameters.AddWithValue("@gender", cboGender.Text);
                    cm.Parameters.AddWithValue("@nationality", txtNationality.Text);
                    cm.Parameters.AddWithValue("@religion", txtReligion.Text);
                    cm.Parameters.AddWithValue("@occupation", txtOccupation.Text);
                    cm.Parameters.AddWithValue("@lresidency", txtLresidency.Text);
                    cm.Parameters.AddWithValue("@contact", txtCnumber.Text);
                    cm.Parameters.AddWithValue("@pwd", cboPwd.Text);
                    cm.Parameters.AddWithValue("@fincome", fincome);
                    cm.Parameters.AddWithValue("@indigent", cboIndigent.Text);
                    cm.Parameters.AddWithValue("@sparent", cboSparent.Text);
                    cm.Parameters.AddWithValue("@rvoter", cboRvoter.Text);
                    cm.Parameters.AddWithValue("@purok", cboPurokno.Text);
                    cm.Parameters.AddWithValue("@fhead", cboFhead.Text);
                    cm.Parameters.AddWithValue("@senior", cboSenior.Text);
                    cm.Parameters.AddWithValue("@hnumber", hnumber);
                    cm.Parameters.AddWithValue("@address", txtAddress.Text);
                    cm.Parameters.AddWithValue("@street", txtStreet.Text);
                    cm.Parameters.AddWithValue("@typid", cboTid.Text);
                    cm.Parameters.AddWithValue("@idnumber", txtIdno.Text);
                    cm.Parameters.AddWithValue("@pic", arrImage);
                    cm.Parameters.AddWithValue("@date_inserted", DateTime.Now);

                    // Execute the INSERT command
                    cm.ExecuteNonQuery();


                    // Retrieve the last inserted ID as an integer
                    cm = new MySqlCommand("SELECT LAST_INSERT_ID();", cn);
                    int lastInsertedId = Convert.ToInt32(cm.ExecuteScalar());

                    // Check if the resident record exists before inserting into tblaccountnew
                    cm.Dispose(); // Dispose the previous command
                    cm = new MySqlCommand("SELECT COUNT(*) FROM tblResident WHERE ID = @id", cn);
                    cm.Parameters.AddWithValue("@id", lastInsertedId);

                    int countResident = Convert.ToInt32(cm.ExecuteScalar());

                    if (countResident == 0)
                    {
                        MessageBox.Show("Resident record not found!", var._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Generate account
                    string fullName = $"{txtFname.Text} {txtMname.Text} {txtLname.Text}".Trim();
                    string username = GenerateUsername(txtFname.Text, txtLname.Text);
                    string password = GeneratePassword();

                    // Insert into tblaccountnew with the correct foreign key reference
                    cm.Dispose();
                    cm = new MySqlCommand("INSERT INTO tblaccountnew (username, password, role, residentid, name) VALUES (@username, @password, @role, @residentid, @name)", cn);
                    cm.Parameters.AddWithValue("@username", username);
                    string hashedPassword = HashPassword(password);
                    cm.Parameters.AddWithValue("@password", password);
                    cm.Parameters.AddWithValue("@role", "user");
                    cm.Parameters.AddWithValue("@residentid", lastInsertedId); // Use integer, not string
                    cm.Parameters.AddWithValue("@name", fullName);

                    cm.ExecuteNonQuery();


                    MessageBox.Show($"Record and account created successfully!\n\nUsername: {username}\nPassword: {password}",
                                    var._title, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    cn.Close();
                    Clear();
                    f.Loadrecords();
                }
            }
            catch (Exception ex)
            {
                if (cn.State == ConnectionState.Open) cn.Close();
                MessageBox.Show(ex.Message, var._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }
        private string GeneratePassword()
        {
            const string validChars = "ab290"; // Allowed characters
            StringBuilder password = new StringBuilder();
            Random rand = new Random();

            for (int i = 0; i < 5; i++) // Change 8 to 5 for a 5-character password
            {
                password.Append(validChars[rand.Next(validChars.Length)]);
            }

            return password.ToString();
        }

        // Method to generate a username
        private string GenerateUsername(string firstName, string lastName)
        {
            return (firstName.Substring(0, 1) + lastName).ToLower();  // Example: jdoe
        }
        public void Clear()
        {
            picImage.BackgroundImage = Image.FromFile(Application.StartupPath + @"\man1.jpg");
            txtLname.Clear();
            txtFname.Clear();
            txtMname.Clear();
            cboSuffix.SelectedIndex = -1; // Clear ComboBox
            dtBdate.Value = DateTime.Now; // Reset DateTimePicker to the current date
            txtBplace.Clear();
            txtA.Clear();
            cboCstatus.SelectedIndex = -1;
            cboGender.SelectedIndex = -1;
            txtNationality.Clear();
            txtReligion.Clear();
            txtOccupation.Clear();
            txtLresidency.Clear();
            txtCnumber.Clear();
            cboPwd.SelectedIndex = -1;
            txtFincome.Clear();
            cboIndigent.SelectedIndex = -1;
            cboSparent.SelectedIndex = -1;
            cboRvoter.SelectedIndex = -1;
            cboPurokno.SelectedIndex = -1;
            cboSenior.SelectedIndex = -1;
            txtHno.Clear();
            txtAddress.Clear();
            txtStreet.Clear();
            cboTid.SelectedIndex = -1;
            txtIdno.Clear();
            cboFhead.SelectedIndex = -1;
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            


        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.Filter = "Image File (*.png)|*.png|*.jpg|*.jpg|*.gif|*.gif";
                openFileDialog1.ShowDialog();
                picImage.BackgroundImage = Image.FromFile(openFileDialog1.FileName);
            }
            catch (Exception ex)
            {
                 MessageBox.Show(ex.Message,var._title, MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void dtBdate_ValueChanged(object sender, EventArgs e)
        {
            // Get the selected date from the DateTimePicker
            DateTime selectedDate = dtBdate.Value;

            // Calculate the age
            int age = CalculateAge(selectedDate);

            // Set the calculated age in the text box
            txtA.Text = age.ToString();

        }
        private int CalculateAge(DateTime birthDate)
        {
            DateTime today = DateTime.Today;

            // Check if the birthdate is in the future
            if (birthDate > today)
            {
                return 0; // Set age to 0 for future dates
            }

            int age = today.Year - birthDate.Year;

            // Adjust the age if the birthday hasn't occurred yet this year
            if (birthDate.Date > today.AddYears(-age)) age--;

            return age;
        }

        private void txtA_TextChanged(object sender, EventArgs e)
        {

        }

        private void cboGender_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void frmResident_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void picImage_Click(object sender, EventArgs e)
        {

        }
    }
}
