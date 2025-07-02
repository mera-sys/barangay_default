using barangay_management_system;
using Microsoft.Reporting.WinForms;
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
    public partial class frmCertificate : Form
    {
        

        public frmCertificate()
        {
            InitializeComponent();
            cboCertificateType.SelectedIndexChanged += cboCertificateType_SelectedIndexChanged;
        }

        private void frmCertificate_Load(object sender, EventArgs e)
        {


        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (this.IsDisposed || !this.IsHandleCreated) return;

            string input = txtResident.Text.Trim();

            if (string.IsNullOrWhiteSpace(input))
            {
                MessageBox.Show("Please enter a valid resident name.");
                return;
            }

            if (cboCertificateType.SelectedItem == null)
            {
                MessageBox.Show("Please select a certificate type.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtAmount.Text))
            {
                MessageBox.Show("Please fill in the Amount field.");
                return;
            }

            // Only check for remarks if the certificate type is not "Barangay Business"
            if (cboCertificateType.SelectedItem.ToString() != "Barangay Business" && cboRemarks.SelectedItem == null)
            {
                MessageBox.Show("Please select a remarks option.");
                return;
            }

            try
            {
                string lastName = "", firstName = "", middleName = "", ext = "";
                string[] parts = input.Split(',');

                if (parts.Length < 2)
                {
                    MessageBox.Show("Please enter the name in the format: LastName, FirstName MiddleName Ext");
                    return;
                }

                lastName = parts[0].Trim();
                string[] nameParts = parts[1].Trim().Split(' ');

                firstName = nameParts.Length > 0 ? nameParts[0].Trim() : "";
                middleName = nameParts.Length > 1 ? nameParts[1].Trim() : "";
                ext = nameParts.Length > 2 ? nameParts[2].Trim() : "";

                // Get only the middle initial
                string middleInitial = "";
                if (!string.IsNullOrWhiteSpace(middleName))
                {
                    middleInitial = middleName.Substring(0, 1).ToUpper(); // Get first letter and make it uppercase
                }

                using (MySqlConnection conn = new MySqlConnection(dbconString.connection))
                {
                    conn.Open();

                    // Check for Indigent status
                    string indigentCheckQuery = @"SELECT Indigent FROM residents 
                WHERE LastName = @last AND FirstName = @first 
                AND (@middle = '' OR MiddleName = @middle)
                AND (@ext = '' OR Ext = @ext)";

                    using (MySqlCommand checkIndigentCmd = new MySqlCommand(indigentCheckQuery, conn))
                    {
                        checkIndigentCmd.Parameters.AddWithValue("@last", lastName);
                        checkIndigentCmd.Parameters.AddWithValue("@first", firstName);
                        checkIndigentCmd.Parameters.AddWithValue("@middle", middleName);
                        checkIndigentCmd.Parameters.AddWithValue("@ext", ext);

                        var indigentResult = checkIndigentCmd.ExecuteScalar();
                        string indigent = "NO"; // Default if NULL or empty

                        if (indigentResult != null && indigentResult != DBNull.Value)
                        {
                            indigent = indigentResult.ToString().Trim().ToUpper();
                        }

                        if (cboCertificateType.SelectedItem.ToString() == "Barangay Indigency" && indigent != "YES")
                        {
                            MessageBox.Show("Resident is not marked as indigent. Cannot generate Barangay Indigency certificate.");
                            return;
                        }
                    }

                    // Retrieve full resident details
                    string residentQuery = @"SELECT * FROM residents 
                WHERE LastName = @last AND FirstName = @first 
                AND (@middle = '' OR MiddleName = @middle)
                AND (@ext = '' OR Ext = @ext)";

                    string dob = "", age = "", civilStatus = "", sex = "";

                    using (MySqlCommand cmd = new MySqlCommand(residentQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@last", lastName);
                        cmd.Parameters.AddWithValue("@first", firstName);
                        cmd.Parameters.AddWithValue("@middle", middleName);
                        cmd.Parameters.AddWithValue("@ext", ext);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                MessageBox.Show("Resident not found.");
                                return;
                            }

                            dob = reader["DateOfBirth"] != DBNull.Value ? Convert.ToDateTime(reader["DateOfBirth"]).ToString("yyyy-MM-dd") : "";
                            age = reader["Age"].ToString();
                            civilStatus = reader["CivilStatus"].ToString();
                            sex = reader["Sex"].ToString();
                        }
                    }

                    string remarks = cboRemarks.SelectedItem?.ToString(); // Safe get remarks
                    string referenceNumber = txtReference.Text.Trim();

                    if (string.IsNullOrWhiteSpace(referenceNumber))
                    {
                        MessageBox.Show("Please enter a Reference Number.");
                        return;
                    }

                    // Generate Permit Number if needed
                    string permitNumber = "";
                    if (cboCertificateType.SelectedItem.ToString() == "Barangay Business")
                    {
                        string permitQuery = "SELECT MAX(PermitNumber) FROM brgycertificate WHERE CertificateType = 'Barangay Business'";
                        using (MySqlCommand permitCmd = new MySqlCommand(permitQuery, conn))
                        {
                            var permitResult = permitCmd.ExecuteScalar();
                            int permitNumberInt = (permitResult != DBNull.Value && permitResult != null) ? Convert.ToInt32(permitResult) + 1 : 1;

                            // Format permit number
                            string firstPart = permitNumberInt.ToString("D3");
                            string secondPart = ((permitNumberInt % 100) + 1).ToString("D2");

                            permitNumber = firstPart + "-" + secondPart;
                        }
                    }

                    // Prepare Insert Query
                    string insertQuery = "";

                    if (cboCertificateType.SelectedItem.ToString() == "Barangay Certificate")
                    {
                        insertQuery = @"INSERT INTO brgycertificate 
                    (LastName, FirstName, MiddleName, Ext, DateOfBirth, Age, CivilStatus, 
                    DateIssued, CertificateType, Purpose, Amount, Remarks, Sex, ReferenceNumber)
                    VALUES 
                    (@last, @first, @middle, @ext, @dob, @age, @civil, 
                    @issued, @certType, @purpose, @amount, @remarks, @sex, @refNo)";
                    }
                    else if (cboCertificateType.SelectedItem.ToString() == "Barangay Indigency")
                    {
                        insertQuery = @"INSERT INTO brgycertificate 
                    (LastName, FirstName, MiddleName, Ext, DateOfBirth, Age, CivilStatus, 
                    DateIssued, CertificateType, Amount, Remarks, Sex, ReferenceNumber)
                    VALUES 
                    (@last, @first, @middle, @ext, @dob, @age, @civil, 
                    @issued, @certType, @amount, @remarks, @sex, @refNo)";
                    }
                    else if (cboCertificateType.SelectedItem.ToString() == "Barangay Business")
                    {
                        insertQuery = @"INSERT INTO brgycertificate 
                    (LastName, FirstName, MiddleName, Ext, DateOfBirth, Age, CivilStatus, 
                    DateIssued, CertificateType, Amount, Sex, ReferenceNumber, Natureofbusiness, PermitNumber)
                    VALUES 
                    (@last, @first, @middle, @ext, @dob, @age, @civil, 
                    @issued, @certType, @amount, @sex, @refNo, @Natureofbusiness, @permitNumber)";
                    }

                    using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn))
                    {
                        insertCmd.Parameters.AddWithValue("@last", lastName);
                        insertCmd.Parameters.AddWithValue("@first", firstName);
                        insertCmd.Parameters.AddWithValue("@middle", middleInitial);
                        insertCmd.Parameters.AddWithValue("@ext", ext);
                        insertCmd.Parameters.AddWithValue("@dob", dob);
                        insertCmd.Parameters.AddWithValue("@age", age);
                        insertCmd.Parameters.AddWithValue("@civil", civilStatus);
                        insertCmd.Parameters.AddWithValue("@issued", dtpDateIssued.Value.ToString("yyyy-MM-dd"));
                        insertCmd.Parameters.AddWithValue("@certType", cboCertificateType.SelectedItem.ToString());
                        insertCmd.Parameters.AddWithValue("@amount", txtAmount.Text.Trim());
                        insertCmd.Parameters.AddWithValue("@sex", sex);
                        insertCmd.Parameters.AddWithValue("@refNo", referenceNumber);

                        if (cboCertificateType.SelectedItem.ToString() == "Barangay Business")
                        {
                            insertCmd.Parameters.AddWithValue("@Natureofbusiness", txtNameofBusiness.Text);
                            insertCmd.Parameters.AddWithValue("@permitNumber", permitNumber);
                        }
                        else
                        {
                            insertCmd.Parameters.AddWithValue("@purpose", txtPurpose.Text.Trim());
                            insertCmd.Parameters.AddWithValue("@remarks", remarks);
                        }

                        int result = insertCmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            long lastInsertedId = insertCmd.LastInsertedId;
                            MessageBox.Show("Certificate successfully generated and saved.");

                            // Generate appropriate report
                            if (cboCertificateType.SelectedItem.ToString() == "Barangay Indigency")
                            {
                                GenerateReport3(lastInsertedId.ToString());
                            }
                            else if (cboCertificateType.SelectedItem.ToString() == "Barangay Business")
                            {
                                GenerateReport4(lastInsertedId.ToString());
                            }
                            else
                            {
                                GenerateReport(lastInsertedId.ToString());
                            }
                        }
                        else
                        {
                            MessageBox.Show("Failed to insert certificate. Please try again.");
                        }
                    }
                }
            }
            catch (AccessViolationException ave)
            {
                MessageBox.Show("Access violation error: " + ave.Message, "Memory Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void GenerateReport4(string CertificateID)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(dbconString.connection))
                {
                    conn.Open();

                    string query = "SELECT * FROM brgycertificate WHERE CertificateID = @CertificateID";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@CertificateID", CertificateID);

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds, "brgycertificate");  // Fill the DataSet with the table 'brgycertificate'

                    // Ensure that the correct dataset and table are used in the report
                    ReportDataSource rds = new ReportDataSource("DataSet1", ds.Tables["brgycertificate"]);

                    reportViewer1.LocalReport.DataSources.Clear();
                    reportViewer1.LocalReport.DataSources.Add(rds);
                    reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Report\Report4.rdlc";  // Default report for others

                    reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                    reportViewer1.ZoomMode = ZoomMode.PageWidth;
                    reportViewer1.RefreshReport();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load report: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerateReport(string CertificateID)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(dbconString.connection))
                {
                    conn.Open();

                    string query = "SELECT * FROM brgycertificate WHERE CertificateID = @CertificateID";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@CertificateID", CertificateID);

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds, "brgycertificate");  // Fill the DataSet with the table 'brgycertificate'

                    // Ensure that the correct dataset and table are used in the report
                    ReportDataSource rds = new ReportDataSource("brgycertificate", ds.Tables["brgycertificate"]);

                    reportViewer1.LocalReport.DataSources.Clear();
                    reportViewer1.LocalReport.DataSources.Add(rds);
                    reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Report\Report2.rdlc";  // Default report for others

                    reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                    reportViewer1.ZoomMode = ZoomMode.PageWidth;
                    reportViewer1.RefreshReport();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load report: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void GenerateReport3(string CertificateID)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(dbconString.connection))
                {
                    conn.Open();

                    string query = "SELECT * FROM brgycertificate WHERE CertificateID = @CertificateID";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@CertificateID", CertificateID);

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds, "brgycertificate");  // Fill the DataSet with the table 'brgycertificate'

                    // 👇 This should match the RDLC Report's expected dataset name, e.g., "DataSet1"
                    ReportDataSource rds = new ReportDataSource("DataSet1", ds.Tables["brgycertificate"]);

                    reportViewer1.LocalReport.DataSources.Clear();
                    reportViewer1.LocalReport.DataSources.Add(rds);
                    reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Report\Report3.rdlc";  // Report for Barangay Indigency
                    reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                    reportViewer1.ZoomMode = ZoomMode.PageWidth;
                    reportViewer1.RefreshReport();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load report: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void txtResident_Enter(object sender, EventArgs e)
        {

        }

        private void txtResident_TextChanged(object sender, EventArgs e)
        {
            string input = txtResident.Text.Trim();

            if (input.Length < 2)
                return; // wait until at least 2 characters

            try
            {
                using (MySqlConnection conn = new MySqlConnection(dbconString.connection))
                {
                    conn.Open();

                    string query = @"SELECT LastName, FirstName, MiddleName, Ext 
                             FROM residents
                             WHERE LastName LIKE @search 
                                OR FirstName LIKE @search 
                                OR MiddleName LIKE @search 
                                OR Ext LIKE @search";

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

                                string fullName = $"{last}, {first}";

                                if (!string.IsNullOrWhiteSpace(middle))
                                    fullName += $" {middle}";

                                if (!string.IsNullOrWhiteSpace(ext))
                                    fullName += $" {ext}";

                                suggestions.Add(fullName.Trim());
                            }

                            txtResident.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                            txtResident.AutoCompleteSource = AutoCompleteSource.CustomSource;
                            txtResident.AutoCompleteCustomSource = suggestions;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to fetch suggestions: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       
        private void txtResident_Leave(object sender, EventArgs e)
        {

        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void cboCertificateType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check if there is a selected item in the ComboBox
            if (cboCertificateType.SelectedItem == null) return;

            string selectedType = cboCertificateType.SelectedItem.ToString();

            // Reset all fields to be editable initially
            txtNameofBusiness.Enabled = true;
            txtPurpose.Enabled = true;
            cboRemarks.Enabled = true;

            // Handle each certificate type
            if (selectedType == "Barangay Certificate" || selectedType == "Barangay Indigency")
            {
                txtNameofBusiness.Enabled = false;
            }

            if (selectedType == "Barangay Indigency")
            {
                txtPurpose.Enabled = false;
                cboRemarks.Enabled = false;
            }

            if (selectedType == "Barangay Business")
            {
                // Disable txtPurpose and cboRemarks
                txtPurpose.Enabled = false;
                cboRemarks.Enabled = false;

                // Clear values in case they were filled before selecting "Barangay Business"
                txtPurpose.Clear();
                cboRemarks.SelectedIndex = -1;
            }
        }
    }
}
    
