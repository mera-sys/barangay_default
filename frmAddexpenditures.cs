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
using MySql.Data.MySqlClient; // Use MySQL namespace

namespace barangay_default
{
    public partial class frmAddexpenditures : Form
    {
        private MySqlConnection cn;
        private MySqlCommand cm;
        private frmExpenditures f;
        public string _id;
        public frmAddexpenditures(frmExpenditures parentForm)
        {
            InitializeComponent();
            f = parentForm;
            cn = new MySqlConnection(dbconString.connection);
        }

        private void frmAddexpenditures_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                cn.Open();

                DateTime dateValue = dtDate.Value.Date;
                if (dateValue == DateTime.MinValue || dateValue == DateTime.MaxValue)
                {
                  
                    return;
                }

                // Check for duplicate entries
                string checkQuery = "SELECT COUNT(*) FROM tblTransactions WHERE reference = @reference AND amount = @amount AND date = @date";
                cm = new MySqlCommand(checkQuery, cn);
                cm.Parameters.AddWithValue("@reference", txtReference.Text);
                cm.Parameters.AddWithValue("@amount", txtAmount.Text);
                cm.Parameters.AddWithValue("@date", dateValue);

                int count = Convert.ToInt32(cm.ExecuteScalar());

                if (count > 0)
                {
                    
                    return;
                }

                // Insert transaction record
                cm = new MySqlCommand("INSERT INTO tblTransactions (category, amount, date, payee_vendor, reference, purpose) " +
                                        "VALUES (@category, @amount, @date, @payee_vendor, @reference, @purpose)", cn);
                cm.Parameters.AddWithValue("@category", cboCategory.Text);
                cm.Parameters.AddWithValue("@amount", txtAmount.Text);
                cm.Parameters.AddWithValue("@date", dateValue);
                cm.Parameters.AddWithValue("@reference", txtReference.Text);
                cm.Parameters.AddWithValue("@payee_vendor", txtPayee.Text);
                cm.Parameters.AddWithValue("@purpose", txtPurpose.Text);

                cm.ExecuteNonQuery();

              

              

               
                Clear();
                f.Loadrecords();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Budget Management System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }
        public void Clear()
        {
            cboCategory.SelectedIndex = -1;
            txtAmount.Clear();
            txtReference.Clear();
            txtPurpose.Clear();
            dtDate.Value = DateTime.Now;
        }
        public void LoadDataForEdit(string id)
        {
            try
            {
                cn.Open();
                cm = new MySqlCommand("SELECT * FROM tblTransactions WHERE id = @id", cn);
                cm.Parameters.AddWithValue("@id", id);
                MySqlDataReader dr = cm.ExecuteReader();

                if (dr.Read())
                {
                    // Assign the _id field to the selected record's ID
                    _id = dr["id"].ToString();

                    cboCategory.Text = dr["category"].ToString();
                    txtAmount.Text = dr["amount"].ToString();

                    if (dr["date"] != DBNull.Value)
                    {
                        // Safely parse the date
                        string dateStr = dr["date"].ToString();
                        if (DateTime.TryParseExact(dateStr, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
                        {
                            dtDate.Value = parsedDate;
                        }
                        else
                        {
                            dtDate.Value = DateTime.Now; // Default to current date if parsing fails
                        }
                    }
                    else
                    {
                        dtDate.Value = DateTime.Now; // Default if null
                    }

                    txtPayee.Text = dr["payee_vendor"].ToString();
                    txtReference.Text = dr["reference"].ToString();
                    txtPurpose.Text = dr["purpose"].ToString();

                    // Disable btnSave and enable btnUpdate
                    btnSave.Enabled = false;
                    btnUpdate.Enabled = true;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Budget Management System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public void LoadDataForView(string id)
        {
            try
            {
                cn.Open();
                cm = new MySqlCommand("SELECT * FROM tblTransactions WHERE id = @id", cn);
                cm.Parameters.AddWithValue("@id", id);
                MySqlDataReader dr = cm.ExecuteReader();

                if (dr.Read())
                {
                    // Assign the _id field to the selected record's ID
                    _id = dr["id"].ToString();

                    cboCategory.Text = dr["category"].ToString();
                    txtAmount.Text = dr["amount"].ToString();

                    if (dr["date"] != DBNull.Value)
                    {
                        // Ensure the date is correctly parsed
                        string dateStr = dr["date"].ToString();
                        DateTime parsedDate;
                        if (DateTime.TryParse(dateStr, out parsedDate))
                        {
                            dtDate.Value = parsedDate;
                        }
                        else
                        {
                            dtDate.Value = DateTime.Now; // Default to current date if parsing fails
                        }
                    }
                    else
                    {
                        dtDate.Value = DateTime.Now; // Default if null
                    }

                    txtPayee.Text = dr["payee_vendor"].ToString();
                    txtReference.Text = dr["reference"].ToString();
                    txtPurpose.Text = dr["purpose"].ToString();

                    // Disable btnSave and enable btnUpdate
                    btnSave.Enabled = false;
                    btnUpdate.Enabled = true;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Budget Management System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public void LoadData(string id, bool isEdit = false)
        {
            try
            {
                cn.Open();
                cm = new MySqlCommand("SELECT * FROM tblTransactions WHERE id = @id", cn);
                cm.Parameters.AddWithValue("@id", id);
                MySqlDataReader dr = cm.ExecuteReader();

                if (dr.Read())
                {
                    cboCategory.Text = dr["category"].ToString();
                    txtAmount.Text = dr["amount"].ToString();

                    if (dr["date"] != DBNull.Value && DateTime.TryParse(dr["date"].ToString(), out DateTime parsedDate))
                    {
                        dtDate.Value = parsedDate;
                    }
                    else
                    {
                        dtDate.Value = DateTime.Now;
                    }
                    txtPayee.Text = dr["payee_vendor"].ToString();
                    txtReference.Text = dr["reference"].ToString();
                    txtPurpose.Text = dr["purpose"].ToString();

                    btnSave.Enabled = !isEdit;
                    btnUpdate.Enabled = isEdit;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Budget Management System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(_id))
                {
                    return;
                }

                cn.Open();

                // **Get previous values before updating (for logging)**
                string oldValues = "";
                cm = new MySqlCommand("SELECT * FROM tblTransactions WHERE id = @id", cn);
                cm.Parameters.AddWithValue("@id", _id);
                MySqlDataReader dr = cm.ExecuteReader();
                if (dr.Read())
                {
                    oldValues = $"Category={dr["category"]}, Amount={dr["amount"]}, Date={dr["date"]}, Payee={dr["payee_vendor"]}, Reference={dr["reference"]}, Purpose={dr["purpose"]}";
                }
                dr.Close();

                // **Update record**
                cm = new MySqlCommand("UPDATE tblTransactions SET category = @category, amount = @amount, date = @date, payee_vendor = @payee_vendor, reference = @reference, purpose = @purpose WHERE id = @id", cn);
                cm.Parameters.AddWithValue("@category", cboCategory.Text);
                cm.Parameters.AddWithValue("@amount", txtAmount.Text);
                cm.Parameters.AddWithValue("@date", dtDate.Value.ToString("yyyy-MM-dd")); // Format the date
                cm.Parameters.AddWithValue("@payee_vendor", txtPayee.Text);
                cm.Parameters.AddWithValue("@reference", txtReference.Text);
                cm.Parameters.AddWithValue("@purpose", txtPurpose.Text);
                cm.Parameters.AddWithValue("@id", _id);

                cm.ExecuteNonQuery();

                Clear();
                f.Loadrecords();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Budget Management System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

    }
}
