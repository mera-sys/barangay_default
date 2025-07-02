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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(dbconString.connection))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT role FROM users WHERE username = @username AND password = @password";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);

                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            string role = result.ToString();
                            MessageBox.Show("Login Successful! Role: " + role, "Welcome", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            
                            Logger.LogAction(role.ToUpper(), username, "LOGIN");

                            this.Hide();
                            Form1 mainForm = new Form1(username, role);
                            mainForm.ShowDialog();
                            this.Show();

                            
                            Logger.LogAction(role.ToUpper(), username, "LOGOUT");
                        }
                        else
                        {
                            MessageBox.Show("Invalid credentials.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void checkBoxShowpassword_CheckedChanged(object sender, EventArgs e)
        {
            
            txtPassword.PasswordChar = checkBoxShowpassword.Checked ? '\0' : '*';
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            

        }
    }
}
