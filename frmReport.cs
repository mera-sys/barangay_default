using barangay_management_system;
using Microsoft.Reporting.WinForms;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace barangay_default
{
    public partial class frmReport : Form
    {
        public frmReport()
        {
            InitializeComponent();
        }

        private void frmReport_Load(object sender, EventArgs e)
        {

        }
        public void PreviewBlotter(string blotterId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(dbconString.connection))
                {
                    conn.Open();
                    string query = "SELECT * FROM blotter_record WHERE blotter_id = @blotter_id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@blotter_id", blotterId);

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds, "blotter_record"); // The name here must match the table name in RDLC

                    ReportDataSource rds = new ReportDataSource("blotter", ds.Tables["blotter_record"]); // "DataSet1" must match your RDLC DataSource name

                    reportViewer1.LocalReport.DataSources.Clear();
                    reportViewer1.LocalReport.DataSources.Add(rds);
                    reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Report\Report1.rdlc";

                    reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                    reportViewer1.ZoomMode = ZoomMode.PageWidth;
                    reportViewer1.RefreshReport();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load report: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}