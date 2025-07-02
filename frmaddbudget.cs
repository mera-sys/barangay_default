using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;           // For MemoryStream if needed
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using barangay_management_system;

namespace barangay_default
{
    public partial class frmaddbudget : Form
    {

        private frmBudget f;
        public string _id;


        public frmaddbudget(frmBudget parentForm)
        {
            InitializeComponent();
            f = parentForm;
        }
        public void SetButtonStates(bool button1Enabled, bool button2Enabled)
        {
            button1.Enabled = button1Enabled;
            button2.Enabled = button2Enabled;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection cn = new MySqlConnection(dbconString.connection))
                {
                    cn.Open();
                    DateTime dateValue = dtDate.Value.Date;

                    // Check for duplicates
                    using (MySqlCommand cm = new MySqlCommand("SELECT COUNT(*) FROM tblBudget WHERE ReferenceNo = @ReferenceNo AND Amount = @Amount AND Date = @Date", cn))
                    {
                        cm.Parameters.AddWithValue("@ReferenceNo", txtReference.Text);
                        cm.Parameters.AddWithValue("@Amount", txtAmount.Text);
                        cm.Parameters.AddWithValue("@Date", dateValue);

                        int count = Convert.ToInt32(cm.ExecuteScalar());
                        if (count > 0)
                        {
                            MessageBox.Show("This entry already exists.", "Barangay Management System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // Insert into tblBudget
                    int lastID;
                    using (MySqlCommand cm = new MySqlCommand("INSERT INTO tblBudget (Source, Category, Amount, Date, ReferenceNo, Description) VALUES (@Source, @Category, @Amount, @Date, @ReferenceNo, @Description)", cn))
                    {
                        cm.Parameters.AddWithValue("@Source", txtSource.Text);
                        cm.Parameters.AddWithValue("@Category", cboCategory.Text);
                        cm.Parameters.AddWithValue("@Amount", txtAmount.Text);
                        cm.Parameters.AddWithValue("@Date", dateValue);
                        cm.Parameters.AddWithValue("@ReferenceNo", txtReference.Text);
                        cm.Parameters.AddWithValue("@Description", txtDiscription.Text);
                        cm.ExecuteNonQuery();
                        lastID = (int)cm.LastInsertedId;
                    }

                    

                    MessageBox.Show("Record saved successfully", "Barangay Management System", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                    f.Loadrecords();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Barangay Management System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(_id))
                {
                    MessageBox.Show("ID is missing. Unable to update.", "Barangay Management System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (MySqlConnection cn = new MySqlConnection(dbconString.connection))
                {
                    cn.Open();

                    // Old Data
                    string oldData = "";
                    using (MySqlCommand cm = new MySqlCommand("SELECT * FROM tblBudget WHERE ID = @ID", cn))
                    {
                        cm.Parameters.AddWithValue("@ID", _id);
                        using (MySqlDataReader dr = cm.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                oldData = $"Source={dr["Source"]}, Amount={dr["Amount"]}, Date={dr["Date"]}, ReferenceNo={dr["ReferenceNo"]}";
                            }
                        }
                    }

                    // Update Budget
                    using (MySqlCommand cm = new MySqlCommand("UPDATE tblBudget SET Source=@Source, Category=@Category, Amount=@Amount, Date=@Date, ReferenceNo=@ReferenceNo, Description=@Description WHERE ID=@ID", cn))
                    {
                        cm.Parameters.AddWithValue("@Source", txtSource.Text);
                        cm.Parameters.AddWithValue("@Category", cboCategory.Text);
                        cm.Parameters.AddWithValue("@Amount", txtAmount.Text);
                        cm.Parameters.AddWithValue("@Date", dtDate.Value.Date);
                        cm.Parameters.AddWithValue("@ReferenceNo", txtReference.Text);
                        cm.Parameters.AddWithValue("@Description", txtDiscription.Text);
                        cm.Parameters.AddWithValue("@ID", _id);
                        cm.ExecuteNonQuery();
                    }

                    

                    MessageBox.Show("Record updated successfully", "Barangay Management System", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                    f.Loadrecords();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Barangay Management System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        public void Clear()
        {
            txtSource.Clear();
            cboCategory.SelectedIndex = -1;
            txtAmount.Clear();
            txtReference.Clear();
            txtDiscription.Clear();
            dtDate.Value = DateTime.Now;
        }
        public void LoadDataForEdit(string id)
        {
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
                                _id = dr["ID"].ToString();
                                txtSource.Text = dr["Source"].ToString();
                                cboCategory.Text = dr["Category"].ToString();
                                txtAmount.Text = dr["Amount"].ToString();
                                dtDate.Value = Convert.ToDateTime(dr["Date"]);
                                txtReference.Text = dr["ReferenceNo"].ToString();
                                txtDiscription.Text = dr["Description"].ToString();
                                button2.Enabled = false;
                                button1.Enabled = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Barangay Management System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void LoadDataForView(string id)
        {
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
                                txtSource.Text = dr["Source"].ToString();
                                cboCategory.Text = dr["Category"].ToString();
                                txtAmount.Text = dr["Amount"].ToString();
                                if (DateTime.TryParse(dr["Date"].ToString(), out DateTime parsedDate))
                                {
                                    dtDate.Value = parsedDate;
                                }
                                else
                                {
                                    dtDate.Value = DateTime.Now;
                                }
                                txtReference.Text = dr["ReferenceNo"].ToString();
                                txtDiscription.Text = dr["Description"].ToString();
                                button2.Enabled = false;
                                button1.Enabled = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Barangay Management System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}
