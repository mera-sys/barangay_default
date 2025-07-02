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
    public partial class FrmUpdateAdmin : Form
    {
        private string _id;
        public FrmUpdateAdmin(string accountId, string username, string password)
        {
            InitializeComponent();
            _id = accountId; // Proper assignment
            txtUsername.Text = username;
            txtPassword.Text = password;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(dbconString.connection))
                {
                    conn.Open();

                    string query = "UPDATE users SET Username = @username, Password = @password WHERE id = @accountId";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", txtUsername.Text.Trim());
                    cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());
                    cmd.Parameters.AddWithValue("@accountId", _id);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Account updated successfully.");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("No changes were made to the account.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating account: " + ex.Message);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frmupdateAccount_Load(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(dbconString.connection))
                {
                    conn.Open();
                    string query = "SELECT Username, Password FROM users WHERE id = @accountId";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@accountId", _id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtUsername.Text = reader["Username"].ToString();
                            txtPassword.Text = reader["Password"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading account: " + ex.Message);
            }
        }
    }
}