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
    public partial class frmBlotterupdate : Form
    {

        private string blotterId;
        public frmBlotterupdate(string blotterId)
        {
            InitializeComponent();
            this.blotterId = blotterId;
            LoadBlotterRecord(); 
        }
        private void LoadBlotterRecord()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(dbconString.connection))
                {
                    conn.Open();
                    string query = @"SELECT * FROM blotter_record WHERE blotter_id = @blotter_id";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@blotter_id", blotterId);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Populate the form controls with the data from the database
                                txtcomplainant.Text = reader["complainant_not_residence"].ToString();
                                txtStatement.Text = reader["statement"].ToString();
                                txtRespondent.Text = reader["respodent"].ToString();
                                txtInvolved.Text = reader["involved_not_resident"].ToString();
                                txtInvoledstatement.Text = reader["statement_person"].ToString();
                                dtIncident.Value = Convert.ToDateTime(reader["date_incident"]);
                                dtreporteddate.Value = Convert.ToDateTime(reader["date_reported"]);
                                txtIncident.Text = reader["type_of_incident"].ToString();
                                txtLocation.Text = reader["location_incident"].ToString();
                                cboStatus.Text = reader["status"].ToString();
                                cboRemarks.Text = reader["remarks"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading blotter record: " + ex.Message, "Load Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(dbconString.connection))
                {
                    conn.Open();
                    string query = @"UPDATE blotter_record SET
                             complainant_not_residence = @complainant_not_residence,
                             statement = @statement,
                             respodent = @respodent,
                             involved_not_resident = @involved_not_resident,
                             statement_person = @statement_person,
                             date_incident = @date_incident,
                             date_reported = @date_reported,
                             type_of_incident = @type_of_incident,
                             location_incident = @location_incident,
                             status = @status,
                             remarks = @remarks
                             WHERE blotter_id = @blotter_id";  // Don't update blotter_id

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        // Add parameters for all fields that are being updated
                        cmd.Parameters.AddWithValue("@complainant_not_residence", txtcomplainant.Text);
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
                        cmd.Parameters.AddWithValue("@blotter_id", blotterId); // Ensure blotter_id remains the same

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Blotter record updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close(); // Close the form after update
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating blotter record: " + ex.Message, "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}