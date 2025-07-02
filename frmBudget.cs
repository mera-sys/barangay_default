using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient; // Changed from System.Data.SqlClient
using System.IO;
using barangay_management_system;

namespace barangay_default
{
    public partial class frmBudget : Form
    {
        public string _id;
        public frmBudget()
        {
            InitializeComponent();
           
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
        public void Loadrecords(string category = "", string search = "")
        {
            try
            {
                guna2DataGridView1.Rows.Clear();
                using (MySqlConnection cn = new MySqlConnection(dbconString.connection))
                {
                    cn.Open();

                    string query = "SELECT * FROM tblBudget WHERE 1=1";
                    if (!string.IsNullOrEmpty(category))
                        query += " AND Category = @Category";
                    if (!string.IsNullOrEmpty(search))
                        query += " AND (Source LIKE @Search OR Category LIKE @Search OR ReferenceNo LIKE @Search OR Description LIKE @Search)";

                    using (MySqlCommand cm = new MySqlCommand(query, cn))
                    {
                        if (!string.IsNullOrEmpty(category))
                            cm.Parameters.AddWithValue("@Category", category);
                        if (!string.IsNullOrEmpty(search))
                            cm.Parameters.AddWithValue("@Search", "%" + search + "%");

                        using (MySqlDataReader dr = cm.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                string formattedAmount = dr["Amount"] != DBNull.Value
                                    ? "₱" + Convert.ToDecimal(dr["Amount"]).ToString("#,##0.00")
                                    : "₱0.00";

                                string formattedDate = DateTime.TryParse(dr["Date"]?.ToString(), out DateTime dateValue)
                                    ? dateValue.ToString("MM-dd-yyyy")
                                    : "Invalid Date";

                                guna2DataGridView1.Rows.Add(
                                    dr["ID"]?.ToString() ?? "",
                                    dr["Source"]?.ToString() ?? "",
                                    dr["Category"]?.ToString() ?? "",
                                    formattedAmount,
                                    formattedDate,
                                    dr["ReferenceNo"]?.ToString() ?? "",
                                    dr["Description"]?.ToString() ?? ""
                                );
                            }
                        }
                    }
                }

                guna2DataGridView1.ClearSelection();
                UpdateTotalAmount();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Budget Management System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void UpdateTotalAmount()
        {
            try
            {
                using (MySqlConnection cn = new MySqlConnection(dbconString.connection))
                {
                    cn.Open();
                    using (MySqlCommand cm = new MySqlCommand("SELECT SUM(Amount) AS TotalAmount FROM tblBudget", cn))
                    {
                        object result = cm.ExecuteScalar();
                        lblTotal.Text = result != DBNull.Value
                            ? "₱" + Convert.ToDecimal(result).ToString("#,##0.00")
                            : "₱0.00";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Budget Management System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void frmBudget_Load(object sender, EventArgs e)
        {
            Loadrecords();
            UpdateTotalAmount();

            UpdateLocalRevenueTotal();
            UpdateTaxRevenueTotal();
            UpdateDonationsTotal();
            UpdateServiceIncomeTotal();
            UpdateRentalIncomeTotal();
            UpdateGovernmentIRATotal();
            UpdateGovernmentGrantsTotal();
        }
        private void UpdateLocalRevenueTotal()
        {
            try
            {
                using (MySqlConnection cn = new MySqlConnection(dbconString.connection))
                {
                    cn.Open();
                    using (MySqlCommand cm = new MySqlCommand("SELECT SUM(Amount) AS TotalAmount FROM tblBudget WHERE Category = @Category", cn))
                    {
                        cm.Parameters.AddWithValue("@Category", "Local Revenue");
                        object result = cm.ExecuteScalar();

                        if (result != DBNull.Value)
                        {
                            decimal totalAmount = Convert.ToDecimal(result);
                            lblLocal.Text = "₱" + totalAmount.ToString("#,##0.00");
                        }
                        else
                        {
                            lblLocal.Text = "₱0.00";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string colName = guna2DataGridView1.Columns[e.ColumnIndex].Name;

                if (colName == "btnEdit")
                {
                    string recordId = guna2DataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    string oldData = GetBudgetDetails(recordId);

                    // Check if frmaddbudget is already open
                    frmaddbudget f = Application.OpenForms.OfType<frmaddbudget>().FirstOrDefault();
                    if (f == null) // If not open, create a new instance
                    {
                        f = new frmaddbudget(this)
                        {
                            _id = recordId
                        };
                    }

                    f.SetButtonStates(false, true);  // Disable button2, Enable button1

                    // Set the fields in the form
                    DataGridViewRow row = guna2DataGridView1.Rows[e.RowIndex];
                    f.txtSource.Text = row.Cells[1].Value?.ToString();
                    f.cboCategory.Text = row.Cells[2].Value?.ToString();
                    f.txtAmount.Text = row.Cells[3].Value?.ToString();

                    if (DateTime.TryParse(row.Cells[4].Value?.ToString(), out DateTime parsedDate))
                        f.dtDate.Value = parsedDate;

                    f.txtReference.Text = row.Cells[5].Value?.ToString();
                    f.txtDiscription.Text = row.Cells[6].Value?.ToString();

                    f.ShowDialog();  // Show the form as modal
                    
                }
                else if (colName == "btnDelete")
                {
                    string recordId = guna2DataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    string deletedData = GetBudgetDetails(recordId);

                    if (MessageBox.Show("Are you sure you want to delete this record?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        DeleteRecord(recordId);
                        Loadrecords();
                        UpdateTotalAmount();

                       
                    }
                }
            }
        }
        private string GetBudgetDetails(string id)
        {
            string details = "";

            try
            {
                using (MySqlConnection cn = new MySqlConnection(dbconString.connection))
                {
                    cn.Open();
                    using (MySqlCommand cm = new MySqlCommand("SELECT * FROM tblBudget WHERE ID = @ID", cn))
                    {
                        cm.Parameters.AddWithValue("@ID", id);
                        using (MySqlDataReader dr = cm.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                details = $"Source: {dr["Source"]}, Category: {dr["Category"]}, Amount: {dr["Amount"]}, Date: {dr["Date"]}, Description: {dr["Description"]}";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching details: " + ex.Message, "Budget Management System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return details;
        }

        private void LogAction(string actionType, string tableName, string recordID, string changes)
        {
            try
            {
                using (MySqlConnection cn = new MySqlConnection(dbconString.connection))
                {
                    cn.Open();
                    string query = "INSERT INTO tblLogs (Account, ActionType, TableName, RecordID, Changes, Timestamp) VALUES (@Account, @ActionType, @TableName, @RecordID, @Changes, NOW())";
                    using (MySqlCommand cm = new MySqlCommand(query, cn))
                    {
                        cm.Parameters.AddWithValue("@Account", "Admin");
                        cm.Parameters.AddWithValue("@ActionType", actionType);
                        cm.Parameters.AddWithValue("@TableName", tableName);
                        cm.Parameters.AddWithValue("@RecordID", recordID);
                        cm.Parameters.AddWithValue("@Changes", changes);
                        cm.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Logging error: " + ex.Message, "Budget Management System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DeleteRecord(string id)
        {
            try
            {
                using (MySqlConnection cn = new MySqlConnection(dbconString.connection))
                {
                    cn.Open();
                    using (MySqlCommand cm = new MySqlCommand("DELETE FROM tblBudget WHERE ID = @ID", cn))
                    {
                        cm.Parameters.AddWithValue("@ID", id);
                        cm.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Budget Management System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Category-based update methods like:
        private void UpdateGovernmentGrantsTotal() => UpdateCategoryTotal("Government Grants", lblGrants);
        private void UpdateTaxRevenueTotal() => UpdateCategoryTotal("Tax Revenue", lblTax);
        private void UpdateDonationsTotal() => UpdateCategoryTotal("Donations", lblDonation);
        private void UpdateServiceIncomeTotal() => UpdateCategoryTotal("Service Income", lblService);
        private void UpdateRentalIncomeTotal() => UpdateCategoryTotal("Rental Income", lblRental);
        private void UpdateGovernmentIRATotal() => UpdateCategoryTotal("IRA", lblIra); // IRA Label assumed

        private void UpdateCategoryTotal(string category, Label label)
        {
            try
            {
                using (MySqlConnection cn = new MySqlConnection(dbconString.connection))
                {
                    cn.Open();
                    using (MySqlCommand cm = new MySqlCommand("SELECT SUM(Amount) FROM tblBudget WHERE Category = @Category", cn))
                    {
                        cm.Parameters.AddWithValue("@Category", category);
                        object result = cm.ExecuteScalar();
                        label.Text = result != DBNull.Value
                            ? "₱" + Convert.ToDecimal(result).ToString("#,##0.00")
                            : "₱0.00";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Budget Management System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnAddresident_Click(object sender, EventArgs e)
        {
            frmaddbudget f = new frmaddbudget(this);
            
            f.Clear();
            f.ShowDialog();

            // Reload the records and update the total amount
            Loadrecords();
            UpdateTotalAmount();

            // Update all category totals
            UpdateLocalRevenueTotal();
            UpdateTaxRevenueTotal();
            UpdateDonationsTotal();
            UpdateServiceIncomeTotal();
            UpdateRentalIncomeTotal();
            UpdateGovernmentIRATotal(); // Corrected method call
            UpdateGovernmentGrantsTotal();
        }
        public void RefreshDashboard()
        {
            {
                txtSearch.Text = ""; // Clear search box when refreshing
                Loadrecords();
                UpdateTotalAmount();
                UpdateLocalRevenueTotal();
                UpdateTaxRevenueTotal();
                UpdateDonationsTotal();
                UpdateServiceIncomeTotal();
                UpdateRentalIncomeTotal();
                UpdateGovernmentIRATotal();
                UpdateGovernmentGrantsTotal();
            }
        }

        private void btnAddresident_Click_1(object sender, EventArgs e)
        {
            frmaddbudget f = new frmaddbudget(this);

            f.Clear();
            f.ShowDialog();

            // Reload the records and update the total amount
            Loadrecords();
            UpdateTotalAmount();

            // Update all category totals
            UpdateLocalRevenueTotal();
            UpdateTaxRevenueTotal();
            UpdateDonationsTotal();
            UpdateServiceIncomeTotal();
            UpdateRentalIncomeTotal();
            UpdateGovernmentIRATotal(); // Corrected method call
            UpdateGovernmentGrantsTotal();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            {
                Loadrecords("", txtSearch.Text);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
