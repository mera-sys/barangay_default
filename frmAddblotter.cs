using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using barangay_management_system;
using MySql.Data.MySqlClient;

namespace barangay_default
{
    public partial class frmAddblotter : Form
    {

        public frmAddblotter()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.frmAddblotter_Load_1);
        


        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(dbconString.connection))
                {
                    conn.Open();
                    string[] nameParts = txtcomplainant.Text.Trim().Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    string lastName = nameParts.Length > 0 ? nameParts[0] : "";
                    string firstName = nameParts.Length > 1 ? nameParts[1] : "";
                    string middleName = nameParts.Length > 2 ? nameParts[2] : "";
                    string ext = nameParts.Length > 3 ? nameParts[3] : ""; 

                    string query = @"INSERT INTO blotter_record 
    (first_name, middle_name, last_name, ext, statement, respodent, involved_not_resident, statement_person, 
    date_incident, date_reported, type_of_incident, location_incident, status, remarks, date_added)
    VALUES 
    (@first_name, @middle_name, @last_name, @ext, @statement, @respodent, @involved_not_resident, @statement_person, 
    @date_incident, @date_reported, @type_of_incident, @location_incident, @status, @remarks, NOW())";


                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@first_name", firstName);
                        cmd.Parameters.AddWithValue("@middle_name", middleName);
                        cmd.Parameters.AddWithValue("@last_name", lastName);
                        cmd.Parameters.AddWithValue("@ext", ext);
                        cmd.Parameters.AddWithValue("@statement", txtStatement.Text);
                        cmd.Parameters.AddWithValue("@respodent", txtRespondent.Text);
                        cmd.Parameters.AddWithValue("@involved_not_resident", txtInvolved.Text);
                        cmd.Parameters.AddWithValue("@statement_person", txtInvoledstatement.Text);
                        cmd.Parameters.AddWithValue("@date_incident", dtIncident.Value.Date);
                        cmd.Parameters.AddWithValue("@date_reported", dtreporteddate.Value.Date);
                        cmd.Parameters.AddWithValue("@type_of_incident", txtIncident.Text);
                        cmd.Parameters.AddWithValue("@location_incident", txtLocation.Text);
                        cmd.Parameters.AddWithValue("@status", cboStatus.Text);
                        cmd.Parameters.AddWithValue("@remarks", cboRemarks.Text);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Insert Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ClearFields()
        {
            txtcomplainant.Clear();
            txtStatement.Clear();
            txtRespondent.Clear();
            txtInvolved.Clear();
            txtInvoledstatement.Clear();
            dtIncident.Value = DateTime.Now;
            dtreporteddate.Value = DateTime.Now;
            txtIncident.Clear();
            txtLocation.Clear();
            cboStatus.SelectedIndex = -1;
            cboRemarks.SelectedIndex = -1;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void txtcomplainant_TextChanged(object sender, EventArgs e)
        {
            string input = txtcomplainant.Text.Trim();

            if (input.Length < 2) return;

            try
            {
                // Run database query on a background thread
                await Task.Run(() => FetchSuggestions(input));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to fetch suggestions: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void FetchSuggestions(string input)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(dbconString.connection))
                {
                    conn.Open();
                    string query = @"SELECT LastName, FirstName, MiddleName, Ext 
                             FROM residents 
                             WHERE LastName LIKE @search OR FirstName LIKE @search";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@search", $"%{input}%");

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            AutoCompleteStringCollection suggestions = new AutoCompleteStringCollection();

                            while (reader.Read())
                            {
                                string last = reader["LastName"].ToString();
                                string first = reader["FirstName"].ToString();
                                string middle = reader["MiddleName"].ToString();
                                string ext = reader["Ext"].ToString();

                                string fullName = $"{last}, {first} {middle} {ext}".Replace("  ", " ").Trim();
                                suggestions.Add(fullName);
                            }

                            // Update the AutoComplete source on the UI thread
                            this.Invoke((Action)(() =>
                            {
                                txtcomplainant.AutoCompleteCustomSource = suggestions;
                            }));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching suggestions: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmAddblotter_Load_1(object sender, EventArgs e)
        {
            txtcomplainant.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtcomplainant.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtcomplainant.AutoCompleteCustomSource = new AutoCompleteStringCollection();
        }

        private void txtcomplainant_Enter(object sender, EventArgs e)
        {
            string input = txtcomplainant.Text.Trim();

            
            if (input.Length < 2) return;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(dbconString.connection))
                {
                    conn.Open();
                    string query = @"SELECT LastName, FirstName, MiddleName, Ext 
                             FROM residents 
                             WHERE LastName LIKE @search OR FirstName LIKE @search";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@search", $"%{input}%");

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            AutoCompleteStringCollection suggestions = new AutoCompleteStringCollection();

                            while (reader.Read())
                            {
                                string last = reader["LastName"].ToString();
                                string first = reader["FirstName"].ToString();
                                string middle = reader["MiddleName"].ToString();
                                string ext = reader["Ext"].ToString();

                                string fullName = $"{last}, {first} {middle} {ext}".Replace("  ", " ").Trim();
                                suggestions.Add(fullName);
                            }

                           
                            txtcomplainant.AutoCompleteCustomSource = suggestions;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to fetch suggestions: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
