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
using barangay_management_system;

namespace barangay_default
{
    public partial class frmExpenditures : Form
    {
        private MySqlConnection cn;
        private MySqlCommand cm;
        private MySqlDataReader dr;
        public string _id;
        public frmExpenditures()
        {
            InitializeComponent();
            cn = new MySqlConnection(dbconString.connection);
        }
        public void Loadrecords(string category = "", string search = "")
        {
            try
            {
                guna2DataGridView1.Rows.Clear();
                cn.Open();

                // Base query
                string query = "SELECT * FROM tblTransactions WHERE 1=1";

                if (!string.IsNullOrEmpty(category))
                {
                    query += " AND category = @category";
                }

                if (!string.IsNullOrEmpty(search))
                {
                    query += @" AND (
                reference LIKE @search OR 
                payee_vendor LIKE @search OR 
                purpose LIKE @search
            )";
                }

                cm = new MySqlCommand(query, cn);

                if (!string.IsNullOrEmpty(category))
                {
                    cm.Parameters.AddWithValue("@category", category);
                }

                if (!string.IsNullOrEmpty(search))
                {
                    cm.Parameters.AddWithValue("@search", "%" + search + "%");
                }

                dr = cm.ExecuteReader();

                while (dr.Read())
                {
                    string formattedAmount = dr["amount"] != DBNull.Value
                        ? "₱" + Convert.ToDecimal(dr["amount"]).ToString("#,##0.00")
                        : "₱0.00";

                    string formattedDate = DateTime.TryParse(dr["date"]?.ToString(), out DateTime dateValue)
                        ? dateValue.ToString("MM-dd-yyyy")
                        : "Invalid Date";

                    guna2DataGridView1.Rows.Add(
                        dr["id"]?.ToString() ?? "",
                        dr["category"]?.ToString() ?? "",
                        formattedAmount,
                        formattedDate,
                        dr["reference"]?.ToString() ?? "",
                        dr["payee_vendor"]?.ToString() ?? "",
                        dr["purpose"]?.ToString() ?? ""
                    );
                }

                dr.Close();
                cn.Close();
                guna2DataGridView1.ClearSelection();

                // Recalculate totals
                UpdateTotalExpenses();
                UpdateOfficeSuppliesTotal();
                UpdateRepairAndMaintenanceTotal();
                UpdateHealthAndServicesTotal();
                UpdateWaterSupplyTotal();
                UpdateFoodAndBeverageTotal();
                UpdateBarangayFiestaTotal();
                UpdateElectricityTotal();
                UpdateGarbageCollectionTotal();
                UpdateSecurityServicesTotal();
                UpdatePublicWorksTotal();
                UpdateOthersTotal();
            }
            catch (Exception ex)
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                MessageBox.Show(ex.Message, "Budget Management System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void UpdateOfficeSuppliesTotal()
        {
            try
            {
                cn.Open();
                cm = new MySqlCommand("SELECT SUM(amount) AS TotalAmount FROM tblTransactions WHERE category = @category", cn);
                cm.Parameters.AddWithValue("@category", "Office Supplies");

                object result = cm.ExecuteScalar();

                if (result != DBNull.Value)
                {
                    decimal totalAmount = Convert.ToDecimal(result);
                    lblOfficesupplies.Text = "₱" + totalAmount.ToString("#,##0.00");
                }
                else
                {
                    lblOfficesupplies.Text = "₱0.00"; // Default if there are no records for this category
                }

                cn.Close();
            }
            catch (Exception ex)
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                MessageBox.Show(ex.Message, "Budget Management System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void UpdateTotalExpenses()
        {
            try
            {
                cn.Open();
                // SQL query to calculate the total sum of the Amount column
                cm = new MySqlCommand("SELECT SUM(Amount) AS TotalExpenses FROM tblTransactions", cn);
                object result = cm.ExecuteScalar();

                // Check if the result is not null and format it with the peso sign
                if (result != DBNull.Value)
                {
                    decimal totalAmount = Convert.ToDecimal(result);
                    lblTotalexpenses.Text = "₱" + totalAmount.ToString("#,##0.00");
                }
                else
                {
                    lblTotalexpenses.Text = "₱0.00"; // Default value if there are no records
                }

                cn.Close();
            }
            catch (Exception ex)
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                MessageBox.Show(ex.Message, "Budget Management System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void frmExpenditures_Load(object sender, EventArgs e)
        {
            try
            {
                // Ensure that the records are loaded before updating totals
                Loadrecords();

                // Update the labels for all categories
                UpdateRepairAndMaintenanceTotal(); // Automatically updates Repair and Maintenance total
                UpdateTotalExpenses();

                // Refresh other category totals
                UpdateOfficeSuppliesTotal();
                UpdateHealthAndServicesTotal();
                UpdateWaterSupplyTotal();
                UpdateFoodAndBeverageTotal();
                UpdateBarangayFiestaTotal();
                UpdateElectricityTotal();
                UpdateGarbageCollectionTotal();
                UpdateSecurityServicesTotal();
                UpdatePublicWorksTotal();
                UpdateOthersTotal();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Budget Management System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            {
                try
                {
                    if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                    {
                        string colName = guna2DataGridView1.Columns[e.ColumnIndex].Name;

                        if (colName == "btnEdit") // Edit button logic
                        {
                            frmAddexpenditures f = new frmAddexpenditures(this);
                            f.btnSave.Enabled = false;
                            f.btnUpdate.Enabled = true;

                            // Get the selected row
                            DataGridViewRow selectedRow = guna2DataGridView1.Rows[e.RowIndex];

                            // Assign the values from the selected row to the form fields
                            f._id = selectedRow.Cells[0].Value?.ToString(); // ID
                            f.cboCategory.Text = selectedRow.Cells[1].Value?.ToString(); // Category
                            f.txtAmount.Text = selectedRow.Cells[2].Value?.ToString(); // Amount
                            string dateValue = selectedRow.Cells[3].Value?.ToString();

                            // Handle date conversion
                            if (DateTime.TryParse(dateValue, out DateTime parsedDate))
                            {
                                f.dtDate.Value = parsedDate;
                            }
                            else
                            {
                                MessageBox.Show("Invalid date format.", "Date Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            f.txtPayee.Text = selectedRow.Cells[4].Value?.ToString(); // Payee
                            f.txtReference.Text = selectedRow.Cells[5].Value?.ToString(); // Reference
                            f.txtPurpose.Text = selectedRow.Cells[6].Value?.ToString(); // Purpose

                            // Show the form for editing
                            f.ShowDialog();
                        }
                        else if (colName == "btnView") // View button logic (existing)
                        {
                            // Existing code for View button (no changes needed)
                            frmAddexpenditures f = new frmAddexpenditures(this);
                            f.btnSave.Enabled = false;
                            f.btnUpdate.Enabled = false;
                            DataGridViewRow selectedRow = guna2DataGridView1.Rows[e.RowIndex];
                            f.cboCategory.Text = selectedRow.Cells[1].Value?.ToString();
                            f.txtAmount.Text = selectedRow.Cells[2].Value?.ToString();
                            string dateValue = selectedRow.Cells[3].Value?.ToString();
                            if (DateTime.TryParse(dateValue, out DateTime parsedDate))
                            {
                                f.dtDate.Value = parsedDate;
                            }
                            else
                            {
                                MessageBox.Show("Invalid date format.", "Date Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            f.txtPayee.Text = selectedRow.Cells[4].Value?.ToString();
                            f.txtReference.Text = selectedRow.Cells[5].Value?.ToString();
                            f.txtPurpose.Text = selectedRow.Cells[6].Value?.ToString();
                        }
                        else if (colName == "btnDelete") // Delete button logic
                        {
                            string recordId = guna2DataGridView1.Rows[e.RowIndex].Cells[0].Value?.ToString();

                            if (MessageBox.Show("Are you sure you want to delete this record?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                            {
                                DeleteRecord(recordId);
                                Loadrecords(); // Reload the records after deletion
                                UpdateTotalExpenses(); // Update the total amount after deletion
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        private void DeleteRecord(string id)
        {
            try
            {
                cn.Open();
                cm = new MySqlCommand("DELETE FROM tblTransactions WHERE id = @id", cn);
                cm.Parameters.AddWithValue("@id", id);
                cm.ExecuteNonQuery();
                cn.Close();

                Loadrecords();  // Refresh the records
                UpdateTotalExpenses();  // Update the total amount
            }
            catch (Exception ex)
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnHealth_Click(object sender, EventArgs e)
        {
            Loadrecords("Health and Services");

            // Update the lblGovernmentGrants to show the total for "Government Grants"
            UpdateHealthAndServicesTotal();
        }
        private void UpdateHealthAndServicesTotal()
        {
            try
            {
                cn.Open();
                cm = new MySqlCommand("SELECT SUM(Amount) AS TotalAmount FROM tblTransactions WHERE category = @category", cn);
                cm.Parameters.AddWithValue("@category", "Health and Services");

                object result = cm.ExecuteScalar();

                if (result != DBNull.Value)
                {
                    decimal totalAmount = Convert.ToDecimal(result);
                    lblHealth.Text = "₱" + totalAmount.ToString("#,##0.00");
                }
                else
                {
                    lblHealth.Text = "₱0.00"; // Default if there are no records for this category
                }

                cn.Close();
            }
            catch (Exception ex)
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                MessageBox.Show(ex.Message, "Budget Management System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnWater_Click(object sender, EventArgs e)
        {
            Loadrecords("Water Supply");

            // Update the lblGovernmentGrants to show the total for "Government Grants"
            UpdateWaterSupplyTotal();
        }
        private void UpdateWaterSupplyTotal()
        {
            try
            {
                cn.Open();
                cm = new MySqlCommand("SELECT SUM(Amount) AS TotalAmount FROM tblTransactions WHERE category = @category", cn);
                cm.Parameters.AddWithValue("@category", "Water Supply");

                object result = cm.ExecuteScalar();

                if (result != DBNull.Value)
                {
                    decimal totalAmount = Convert.ToDecimal(result);
                    lblWater.Text = "₱" + totalAmount.ToString("#,##0.00");
                }
                else
                {
                    lblWater.Text = "₱0.00"; // Default if there are no records for this category
                }

                cn.Close();
            }
            catch (Exception ex)
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                MessageBox.Show(ex.Message, "Budget Management System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnFood_Click(object sender, EventArgs e)
        {
            Loadrecords("Food and Beverage");

            // Update the lblGovernmentGrants to show the total for "Government Grants"
            UpdateFoodAndBeverageTotal();
        }
        private void UpdateFoodAndBeverageTotal()
        {
            try
            {
                cn.Open();
                cm = new MySqlCommand("SELECT SUM(Amount) AS TotalAmount FROM tblTransactions WHERE category = @category", cn);
                cm.Parameters.AddWithValue("@category", "Food and Beverage");

                object result = cm.ExecuteScalar();

                if (result != DBNull.Value)
                {
                    decimal totalAmount = Convert.ToDecimal(result);
                    lblFood.Text = "₱" + totalAmount.ToString("#,##0.00");
                }
                else
                {
                    lblFood.Text = "₱0.00"; // Default if there are no records for this category
                }

                cn.Close();
            }
            catch (Exception ex)
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                MessageBox.Show(ex.Message, "Budget Management System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnFiesta_Click(object sender, EventArgs e)
        {
            Loadrecords("Barangay Fiesta");

            UpdateBarangayFiestaTotal();
        }
        private void UpdateBarangayFiestaTotal()
        {
            try
            {
                cn.Open();
                cm = new MySqlCommand("SELECT SUM(Amount) AS TotalAmount FROM tblTransactions WHERE category = @category", cn);
                cm.Parameters.AddWithValue("@category", "Barangay Fiesta");

                object result = cm.ExecuteScalar();

                if (result != DBNull.Value)
                {
                    decimal totalAmount = Convert.ToDecimal(result);
                    lblFiesta.Text = "₱" + totalAmount.ToString("#,##0.00");
                }
                else
                {
                    lblFiesta.Text = "₱0.00"; // Default if there are no records for this category
                }

                cn.Close();
            }
            catch (Exception ex)
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                MessageBox.Show(ex.Message, "Budget Management System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnElectricity_Click(object sender, EventArgs e)
        {
            Loadrecords("Electricity");

            UpdateElectricityTotal();
        }
        private void UpdateElectricityTotal()
        {
            try
            {
                cn.Open();
                cm = new MySqlCommand("SELECT SUM(Amount) AS TotalAmount FROM tblTransactions WHERE category = @category", cn);
                cm.Parameters.AddWithValue("@category", "Electricity");

                object result = cm.ExecuteScalar();

                if (result != DBNull.Value)
                {
                    decimal totalAmount = Convert.ToDecimal(result);
                    lblElectricity.Text = "₱" + totalAmount.ToString("#,##0.00");
                }
                else
                {
                    lblElectricity.Text = "₱0.00"; // Default if there are no records for this category
                }

                cn.Close();
            }
            catch (Exception ex)
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                MessageBox.Show(ex.Message, "Budget Management System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnGarbage_Click(object sender, EventArgs e)
        {
            Loadrecords("Garbage Collection");

            UpdateGarbageCollectionTotal();
        }
        private void UpdateGarbageCollectionTotal()
        {
            try
            {
                cn.Open();
                cm = new MySqlCommand("SELECT SUM(Amount) AS TotalAmount FROM tblTransactions WHERE category = @category", cn);
                cm.Parameters.AddWithValue("@category", "Garbage Collection");

                object result = cm.ExecuteScalar();

                if (result != DBNull.Value)
                {
                    decimal totalAmount = Convert.ToDecimal(result);
                    lblGarbage.Text = "₱" + totalAmount.ToString("#,##0.00");
                }
                else
                {
                    lblGarbage.Text = "₱0.00"; // Default if there are no records for this category
                }

                cn.Close();
            }
            catch (Exception ex)
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                MessageBox.Show(ex.Message, "Budget Management System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnSecurity_Click(object sender, EventArgs e)
        {
            Loadrecords("Security Services");

            UpdateSecurityServicesTotal();
        }
        private void UpdateSecurityServicesTotal()
        {
            try
            {
                cn.Open();
                cm = new MySqlCommand("SELECT SUM(Amount) AS TotalAmount FROM tblTransactions WHERE category = @category", cn);
                cm.Parameters.AddWithValue("@category", "Security Services");

                object result = cm.ExecuteScalar();

                if (result != DBNull.Value)
                {
                    decimal totalAmount = Convert.ToDecimal(result);
                    lblSecurity.Text = "₱" + totalAmount.ToString("#,##0.00");
                }
                else
                {
                    lblSecurity.Text = "₱0.00"; // Default if there are no records for this category
                }

                cn.Close();
            }
            catch (Exception ex)
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                MessageBox.Show(ex.Message, "Budget Management System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnPublic_Click(object sender, EventArgs e)
        {
            Loadrecords("Public Works");

            UpdatePublicWorksTotal();
        }
        private void UpdatePublicWorksTotal()
        {
            try
            {
                cn.Open();
                cm = new MySqlCommand("SELECT SUM(Amount) AS TotalAmount FROM tblTransactions WHERE category = @category", cn);
                cm.Parameters.AddWithValue("@category", "Public Works");

                object result = cm.ExecuteScalar();

                if (result != DBNull.Value)
                {
                    decimal totalAmount = Convert.ToDecimal(result);
                    lblPublic.Text = "₱" + totalAmount.ToString("#,##0.00");
                }
                else
                {
                    lblPublic.Text = "₱0.00"; // Default if there are no records for this category
                }

                cn.Close();
            }
            catch (Exception ex)
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                MessageBox.Show(ex.Message, "Budget Management System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Loadrecords("Others");

            UpdateOthersTotal();
        }
        private void UpdateOthersTotal()
        {
            try
            {
                cn.Open();
                cm = new MySqlCommand("SELECT SUM(Amount) AS TotalAmount FROM tblTransactions WHERE category = @category", cn);
                cm.Parameters.AddWithValue("@category", "Others");

                object result = cm.ExecuteScalar();

                if (result != DBNull.Value)
                {
                    decimal totalAmount = Convert.ToDecimal(result);
                    lblOthers.Text = "₱" + totalAmount.ToString("#,##0.00");
                }
                else
                {
                    lblOthers.Text = "₱0.00";
                }

                cn.Close();
            }
            catch (Exception ex)
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                MessageBox.Show(ex.Message, "Budget Management System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnAddresident_Click(object sender, EventArgs e)
        {
            frmAddexpenditures f = new frmAddexpenditures(this);
            f.btnUpdate.Enabled = false;
            f.Clear();
            f.ShowDialog();
            Loadrecords();
            UpdateTotalExpenses();

            // Call the update methods to refresh the totals for each category
            UpdateOfficeSuppliesTotal();
            UpdateRepairAndMaintenanceTotal();
            UpdateHealthAndServicesTotal();
            UpdateWaterSupplyTotal();
            UpdateFoodAndBeverageTotal();
            UpdateBarangayFiestaTotal(); // Update the method call to the new name
            UpdateElectricityTotal();
            UpdateGarbageCollectionTotal();
            UpdateSecurityServicesTotal();
            UpdatePublicWorksTotal();
            UpdateOthersTotal();// Add this line to update Government Grants
        }

        private void btnOfficesupplies_Click(object sender, EventArgs e)
        {
            Loadrecords("Office Supplies");

            UpdateOfficeSuppliesTotal();
        }

        private void btnRepair_Click(object sender, EventArgs e)
        {
            Loadrecords("Repair and Maintenance");

            // Update the lblGovernmentGrants to show the total for "Government Grants"
            UpdateRepairAndMaintenanceTotal();
        }
        private void UpdateRepairAndMaintenanceTotal()
        {
            try
            {
                // Open database connection
                if (cn.State != ConnectionState.Open)
                {
                    cn.Open();
                }

                // Command to calculate the sum of the "Repair and Maintenance" category
                cm = new MySqlCommand("SELECT SUM(Amount) AS TotalAmount FROM tblTransactions WHERE category = @category", cn);
                cm.Parameters.AddWithValue("@category", "Repair and Maintenance");

                // Execute query
                object result = cm.ExecuteScalar();

                // Check the result and set the label text
                if (result != DBNull.Value)
                {
                    decimal totalAmount = Convert.ToDecimal(result);
                    lblRepair.Text = "₱" + totalAmount.ToString("#,##0.00"); // Set formatted amount to label
                }
                else
                {
                    lblRepair.Text = "₱0.00"; // Default value if no records found
                }

                // Close connection
                cn.Close();
            }
            catch (Exception ex)
            {
                // Handle exception and close the connection
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }

                MessageBox.Show(ex.Message, "Budget Management System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            Loadrecords("", txtSearch.Text);
        }
    }
}
