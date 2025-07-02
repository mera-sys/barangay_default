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
    public partial class frmReports : Form
    {
        public frmReports()
        {
            InitializeComponent();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            string reportType = cboReportType.SelectedItem.ToString();
            DateTime startDate = dtpDateStarted.Value;
            DateTime endDate = dtpDateEnd.Value;

            if (reportType == "Residents")
            {
                string category = cboResidentCategories.SelectedItem.ToString();
                GenerateResidentsReport(startDate, endDate, category);
            }
            else if (reportType == "COLLECTION AND DISBURSEMENT")
            {
                GenerateCollectionAndDisbursementReport(startDate, endDate);
            }
        }
        private void GenerateCollectionAndDisbursementReport(DateTime startDate, DateTime endDate)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(dbconString.connection))
                {
                    conn.Open();

                    // Query for tblbudget (collection part)
                    string queryBudget = @"
                SELECT 
                    ID, Source, Category, Amount, Date, ReferenceNo, Description 
                FROM 
                    tblbudget 
                WHERE 
                    Date BETWEEN @StartDate AND @EndDate";

                    MySqlCommand cmdBudget = new MySqlCommand(queryBudget, conn);
                    cmdBudget.Parameters.AddWithValue("@StartDate", startDate);
                    cmdBudget.Parameters.AddWithValue("@EndDate", endDate);

                    MySqlDataAdapter adapterBudget = new MySqlDataAdapter(cmdBudget);
                    DataTable dtBudget = new DataTable();
                    adapterBudget.Fill(dtBudget);

                    // Query for tbltransactions (disbursement part)
                    string queryTransactions = @"
                SELECT 
                    id, category, amount, date, payee_vendor, reference, purpose 
                FROM 
                    tbltransactions 
                WHERE 
                    date BETWEEN @StartDate AND @EndDate";

                    MySqlCommand cmdTransactions = new MySqlCommand(queryTransactions, conn);
                    cmdTransactions.Parameters.AddWithValue("@StartDate", startDate);
                    cmdTransactions.Parameters.AddWithValue("@EndDate", endDate);

                    MySqlDataAdapter adapterTransactions = new MySqlDataAdapter(cmdTransactions);
                    DataTable dtTransactions = new DataTable();
                    adapterTransactions.Fill(dtTransactions);

                    if (dtBudget.Rows.Count == 0 && dtTransactions.Rows.Count == 0)
                    {
                        MessageBox.Show("No data found for the selected dates.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    // Now pass both tables to the ReportViewer
                    ReportDataSource rds1 = new ReportDataSource("DataSet1", dtBudget);
                    ReportDataSource rds2 = new ReportDataSource("DataSet2", dtTransactions);

                    reportViewer1.LocalReport.DataSources.Clear();
                    reportViewer1.LocalReport.DataSources.Add(rds1);
                    reportViewer1.LocalReport.DataSources.Add(rds2);
                    reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Report\Report11.rdlc";

                    reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                    reportViewer1.ZoomMode = ZoomMode.PageWidth;
                    reportViewer1.RefreshReport();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load Collection and Disbursement report: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void GenerateResidentsReport(DateTime startDate, DateTime endDate, string category)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(dbconString.connection))
                {
                    conn.Open();

                    // Base query with 1=1 condition for safety
                    string query = @"
SELECT 
    ResidentID, LastName, FirstName, MiddleName, Ext, 
    PlaceOfBirth, DateOfBirth, Age, Sex, CivilStatus, 
    Citizenship, Occupation, Religion, LengthOfResidency, 
    FamilyHead, FamilyIncome, IndividualIncome, Indigent, 
    TypeOfID, IDNumber, EmailAddress, ZipCode, Barangay, 
    Purok, HouseNumber, Street, Municipality, Voters, Senior, 
    Nationality, PWD, SingleParent, Contact, Image, Status, 
    Remarks, DateAdded
FROM 
    Residents
WHERE 1=1"; // Always true for appending other conditions

                    // Add extra filter depending on category
                    if (category == "Senior Citizen")
                    {
                        query += " AND Senior = 'YES'"; // Filter for Senior = 'YES'
                    }
                    else if (category == "PWD")
                    {
                        query += " AND PWD = 'Yes'";
                    }
                    else if (category == "Household")
                    {
                        query += " AND FamilyHead = 'Yes'";
                    }
                    else if (category == "Single Parent")
                    {
                        query += " AND SingleParent = 'Yes'";
                    }
                    else if (category == "Indigent")
                    {
                        query += " AND Indigent = 'Yes'";
                    }

                    // Add the date range filter
                    query += " AND DateAdded BETWEEN @StartDate AND @EndDate"; // Add date range filter here

                    // Add Resident Status filter
                    string residentStatus = cboResidentStatus.SelectedItem.ToString();
                    if (residentStatus == "Active")
                    {
                        query += " AND Status = 'Active'"; // Filter for Active residents
                    }
                    else if (residentStatus == "Inactive")
                    {
                        query += " AND Status = 'Inactive'"; // Filter for Inactive residents
                    }
                    // No filter is applied for "All" since it includes both Active and Inactive

                    // Final ORDER BY clause
                    query += " ORDER BY DateAdded DESC";

                    // Debugging output to check the generated query
                    Console.WriteLine(query);

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@StartDate", startDate);
                    cmd.Parameters.AddWithValue("@EndDate", endDate);

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds, "Residents");

                    // If no data is returned, check if the dataset is empty
                    if (ds.Tables["Residents"].Rows.Count == 0)
                    {
                        MessageBox.Show("No data found for the selected filters.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    ReportDataSource rds = new ReportDataSource("DataSet1", ds.Tables["Residents"]);

                    reportViewer1.LocalReport.DataSources.Clear();
                    reportViewer1.LocalReport.DataSources.Add(rds);
                    reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Report\Report5.rdlc";

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
        private void cboReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboReportType.SelectedItem.ToString() == "Residents")
            {
                cboResidentCategories.Enabled = true;
                cboResidentCategories.Items.Clear();
                cboResidentCategories.Items.AddRange(new string[] {
            "All",
            "Senior Citizen",
            "PWD",
            "Household",
            "Single Parent",
            "Indigent"
        });
                cboResidentCategories.SelectedIndex = 0; // Default select "All"

                cboResidentStatus.Enabled = true; // Enable the status combo box
                cboResidentStatus.Items.Clear();
                cboResidentStatus.Items.AddRange(new string[] {
            "All",
            "Active",
            "Inactive"
        });
                cboResidentStatus.SelectedIndex = 0; // Default select "All"
            }
            else
            {
                cboResidentCategories.Enabled = false;
                cboResidentCategories.Items.Clear();

                cboResidentStatus.Enabled = false; // Disable the status combo box
                cboResidentStatus.Items.Clear();
            }
        }
    }
}