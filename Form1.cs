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
using System.Windows.Forms.DataVisualization.Charting;

namespace barangay_default
{
    public partial class Form1 : Form
    {
        private string loggedInUsername;
        private string loggedInRole;
        frmaddResidence frmAdd;
        frmListresident frmList; // 1. Add declaration

        public Form1(string username, string role)
        {
        InitializeComponent();
        this.loggedInUsername = username;
        this.loggedInRole = role;

        // Optionally, display the user role somewhere
        //lblUser.Text = $"Logged in as: {role} ({username})";
    }
        private void LoadExpenditureData()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(dbconString.connection))
                {
                    conn.Open();
                    string query = @"SELECT Category, SUM(Amount) AS TotalAmount
                             FROM tbltransactions
                             WHERE Category IN ('OFFICE SUPPLIES', 'REPAIR AND MAINTENANCE', 'HEALTH SERVICES', 
                                                'BARANGAY FIESTA', 'ELECTRICITY', 'GARBAGE COLLECTION', 
                                                'SECURITY SERVICES', 'WATER SUPPLY', 'PUBLIC WORKS', 
                                                'FOOD AND BEVERAGE', 'OTHERS')
                             GROUP BY Category";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        chart2.Series.Clear();
                        chart2.ChartAreas.Clear();
                        chart2.ChartAreas.Add(new ChartArea("MainArea"));

                        Series series = new Series("Expenditure")
                        {
                            ChartType = SeriesChartType.Pie,
                            IsValueShownAsLabel = true,
                            LabelFormat = "{0:C}"
                        };

                        while (reader.Read())
                        {
                            string category = reader["Category"].ToString();
                            decimal totalAmount = Convert.ToDecimal(reader["TotalAmount"]);
                            series.Points.AddXY(category, totalAmount);
                        }

                        chart2.Series.Add(series);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading expenditure data: " + ex.Message);
            }
        }
        private void Form1_LocationChanged(object sender, EventArgs e)
        {
          
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadRecords();
            LoadPurokChart(); // Call the new method
            LoadExpenditureData();
            // Display logged-in user information in label10
            label10.Text = $"Welcome, {loggedInUsername}!";

            if (loggedInRole == "secretary")
            {
                button15.Enabled = false; // Budget
                button17.Enabled = false; // Expenditures
            }
            else if (loggedInRole == "treasurer")
            {
                btnNewOf.Enabled = false;  // Add officials
                btnListOff.Enabled = false; // List officials
                button4.Enabled = false;
                button6.Enabled = false;
                button2.Enabled = false;
                button9.Enabled = false;
                button3.Enabled = false;
                button10.Enabled = false;
            }
        }
        public void LoadPurokChart()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(dbconString.connection))
                {
                    conn.Open();
                    string query = "SELECT purok, COUNT(*) AS population FROM residents GROUP BY purok";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        chart1.Series.Clear();
                        chart1.ChartAreas.Clear();
                        chart1.ChartAreas.Add(new ChartArea("MainArea"));

                        Series series = new Series("Population")
                        {
                            ChartType = SeriesChartType.Column,
                            IsValueShownAsLabel = true
                        };
                        chart1.Series.Add(series);

                        Dictionary<int, int> purokData = Enumerable.Range(1, 10).ToDictionary(i => i, i => 0);

                        while (reader.Read())
                        {
                            if (int.TryParse(reader["purok"].ToString(), out int purok) &&
                                int.TryParse(reader["population"].ToString(), out int population))
                            {
                                if (purokData.ContainsKey(purok))
                                {
                                    purokData[purok] = population;
                                }
                            }
                        }

                        Color[] flatColors = new Color[]
                        {
                    Color.FromArgb(51, 57, 63), Color.FromArgb(46, 204, 113), Color.FromArgb(231, 76, 60),
                    Color.FromArgb(155, 89, 182), Color.FromArgb(241, 196, 15), Color.FromArgb(26, 188, 156),
                    Color.FromArgb(230, 126, 34), Color.FromArgb(52, 73, 94), Color.FromArgb(243, 156, 18),
                    Color.FromArgb(149, 165, 166)
                        };

                        for (int i = 1; i <= 10; i++)
                        {
                            string purokName = $"Purok {i}";
                            int population = purokData[i];
                            int index = series.Points.AddXY(purokName, population);
                            series.Points[index].Color = flatColors[i - 1];
                        }

                        chart1.ChartAreas[0].AxisX.Title = "Purok";
                        chart1.ChartAreas[0].AxisY.Title = "Population";
                        chart1.ChartAreas[0].AxisX.Interval = 1;
                        chart1.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
                        chart1.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

                        chart1.Legends.Clear();
                        Legend legend = new Legend
                        {
                            Title = "Legend",
                            Docking = Docking.Right
                        };
                        chart1.Legends.Add(legend);

                        for (int i = 1; i <= 10; i++)
                        {
                            LegendItem item = new LegendItem
                            {
                                Name = $"Purok {i}",
                                Color = flatColors[i - 1]
                            };
                            chart1.Legends[0].CustomItems.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading purok chart: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void LoadRecords()
        {
            try
            {
                // Total population count
                string populationSql = "SELECT COUNT(*) FROM residents";
                Tpopulation.Text = CountRecords(populationSql);

                
                // Count of unique households
                string householdSql = "SELECT COUNT(DISTINCT HouseNumber) FROM residents";
                Thousehold.Text = CountRecords(householdSql);

                string SingleParentsSql = "SELECT COUNT(DISTINCT SingleParent) FROM residents";
                lblSingleParents.Text = CountRecords(SingleParentsSql);

                // Count of FamilyHeads marked 'Yes'
                string fheadSql = "SELECT COUNT(*) FROM residents WHERE FamilyHead = 'Yes'";
                Tfhead.Text = CountRecords(fheadSql);

                // Count of Seniors marked 'Yes'
                string seniorSql = "SELECT COUNT(*) FROM residents WHERE Senior = 'Yes'";
                Tsenior.Text = CountRecords(seniorSql);

                // Count of Voters marked 'Yes'
                string rvoterSql = "SELECT COUNT(*) FROM residents WHERE Voters = 'Yes'";
                Tvoters.Text = CountRecords(rvoterSql);

                // Count of Indigents marked 'Yes'
                string indigentSql = "SELECT COUNT(*) FROM residents WHERE Indigent = 'Yes'";
                Tindigent.Text = CountRecords(indigentSql);

                // Count of PWDs marked 'Yes'
                string pwdSql = "SELECT COUNT(*) FROM residents WHERE PWD = 'Yes'";
                Tpwd.Text = CountRecords(pwdSql);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private string CountRecords(string query)
        {
            using (MySqlConnection conn = new MySqlConnection(dbconString.connection))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    object result = cmd.ExecuteScalar();
                    return result != null ? result.ToString() : "0";
                }
            }
        }
        private void ResetAllButtonColors(Control parent)
        {
            // Custom reset color: RGB(46, 117, 182)
            Color resetColor = Color.FromArgb(51, 57, 63);

            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.BackColor = resetColor; // Reset to the custom color
                }

                // If control contains other controls (like nested panels), check them too
                if (ctrl.HasChildren)
                {
                    ResetAllButtonColors(ctrl);
                }
            }
        }

        private void HighlightButton(Button clickedButton)
        {
            // Start from the form to check all nested panels
            ResetAllButtonColors(this);

            // Highlight the clicked one
            clickedButton.BackColor = Color.Orchid;
        }


        private void sidebarTransition_Tick(object sender, EventArgs e)
        {
            
        }
        private void btnHam_Click(object sender, EventArgs e)
        {
           
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
           
        }
        
        private void guna2Separator1_Click(object sender, EventArgs e)
        {

        }
        bool residenceExpanded = false;

        private void ResidenceTransistion_Tick(object sender, EventArgs e)
        {
            int step = 10; // Adjust step size for smooth transition

            if (!residenceExpanded)
            {
                residenceContainer.Height += step;
                if (residenceContainer.Height >= 192)
                {
                    residenceContainer.Height = 192;
                    ResidenceTransition.Stop();
                    residenceExpanded = true;
                }
            }
            else
            {
                residenceContainer.Height -= step;
                if (residenceContainer.Height <= 62)
                {
                    residenceContainer.Height = 62;
                    ResidenceTransition.Stop();
                    residenceExpanded = false;
                }
            }

            
            panel9.Top = residenceContainer.Bottom + 1;
        }
       
       
       

       
       
        private void btnResidence_Click(object sender, EventArgs e)
        {
            HighlightButton((Button)sender);

            ResidenceTransition.Interval = 10; // Slower interval for smoother effect
            ResidenceTransition.Start();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        HighlightButton((Button)sender);

            frmaddResidence frm = new frmaddResidence();  
            frm.TopLevel = false;                        
           
            panel3.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();                                  


        }

        private void button6_Click(object sender, EventArgs e)
        {
            HighlightButton((Button)sender);
            frmListresident frm = new frmListresident();
            frm.TopLevel = false;
            panel3.Controls.Add(frm); 
            frm.BringToFront();
            frm.LoadResidents();
            frm.Show();
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            HighlightButton((Button)sender);
            frmCertificate frm = new frmCertificate();
            frm.TopLevel = false;
            panel3.Controls.Add(frm);
            frm.BringToFront();          
            frm.Show();
        }

        private void btnNewOf_Click(object sender, EventArgs e)
        {
            HighlightButton((Button)sender);
            frmOfficials f = new frmOfficials();
            f.TopLevel = false;
            panel3.Controls.Add(f);
            f.BringToFront();
            f.Show();
        }

        private void btnListOff_Click(object sender, EventArgs e)
        {
            HighlightButton((Button)sender);
            frmListofOfficial f = new frmListofOfficial();
            f.TopLevel = false;
            panel3.Controls.Add(f);
            f.BringToFront();
            f.Show();

        }

        private void btnBarrangayOfficial_Click(object sender, EventArgs e)
        {
            HighlightButton((Button)sender);
            OfficialTransition.Interval = 10; 
            OfficialTransition.Start();

        }

        private void BtnUser_Click(object sender, EventArgs e)
        {
            HighlightButton((Button)sender);
            UserTransistion.Interval = 10;
            UserTransistion.Start();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            HighlightButton((Button)sender);
            frmUserAccount f = new frmUserAccount();
            f.TopLevel = false;
            panel3.Controls.Add(f);
            f.BringToFront();
            f.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            HighlightButton((Button)sender);
            frmAdmin f = new frmAdmin();
            f.TopLevel = false;
            panel3.Controls.Add(f);
            f.BringToFront();
            f.Show();

        }
        bool OfficialExpanded = false;

        private void OfficialTransition_Tick_1(object sender, EventArgs e)
        {
            int step = 10; // Adjust step size for smooth transition

            if (!OfficialExpanded)
            {
                OfficialContainer.Height += step;
                if (OfficialContainer.Height >= 192)
                {
                    OfficialContainer.Height = 192;
                    OfficialTransition.Stop();
                    OfficialExpanded = true;
                }
            }
            else
            {
                OfficialContainer.Height -= step;
                if (OfficialContainer.Height <= 62)
                {
                    OfficialContainer.Height = 62;
                    OfficialTransition.Stop();
                    OfficialExpanded = false;
                }
            }


            panel9.Top = OfficialContainer.Bottom + 1;
        }
        bool UserExpanded = false;

        private void UserTransistion_Tick_1(object sender, EventArgs e)
        {
            int step = 10; // Adjust step size for smooth transition

            if (!UserExpanded)
            {
                UserContainer.Height += step;
                if (UserContainer.Height >= 192)
                {
                    UserContainer.Height = 192;
                    UserTransistion.Stop();
                    UserExpanded = true;
                }
            }
            else
            {
                UserContainer.Height -= step;
                if (UserContainer.Height <= 62)
                {
                    UserContainer.Height = 62;
                    UserTransistion.Stop();
                    UserExpanded = false;
                }
            }


            panel9.Top = UserContainer.Bottom + 1;
        }

        private void sidebar_Paint(object sender, PaintEventArgs e)
        {

        }
         bool financeExpanded = false;

        private void FinanceTransistion_Tick(object sender, EventArgs e)
        {
            int step = 10; // Adjust step size for smooth transition

            if (!financeExpanded)
            {
                financeContainer.Height += step;
                if (financeContainer.Height >= 196)
                {
                    financeContainer.Height = 196;
                    FinanceTransistion.Stop();
                    financeExpanded = true;
                }
            }
            else
            {
                financeContainer.Height -= step;
                if (financeContainer.Height <= 62)
                {
                    financeContainer.Height = 62;
                    FinanceTransistion.Stop();
                    financeExpanded = false;
                }
            }


            panel9.Top = financeContainer.Bottom + 1;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            HighlightButton((Button)sender);
            FinanceTransistion.Interval = 10;
            FinanceTransistion.Start();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            HighlightButton((Button)sender);
            frmBudget f = new frmBudget();
            f.TopLevel = false;

            panel3.Controls.Add(f);
            f.BringToFront();
            f.Show();

            f.RefreshDashboard(); // Call the centralized method to load all data
        }

        private void button17_Click(object sender, EventArgs e)
        {
            HighlightButton((Button)sender);
            frmExpenditures f = new frmExpenditures();
            f.TopLevel = false;
            panel3.Controls.Add(f);
            f.BringToFront();
            f.Loadrecords();
            f.Show();
        }

        private void button16_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            HighlightButton((Button)sender);
            frmblotterList f = new frmblotterList();
            f.TopLevel = false;
            panel3.Controls.Add(f);
            f.BringToFront();
            f.Show();


        }

        private void button11_Click(object sender, EventArgs e)
        {
            HighlightButton((Button)sender);
            frmReports frm = new frmReports();
            frm.TopLevel = false;
            panel3.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HighlightButton((Button)sender);
            // Remove only added forms (UserControls or Forms)
            foreach (Control ctrl in panel3.Controls.OfType<Form>().ToList())
            {
                ctrl.Dispose();
            }

            
            LoadRecords();

            
            panel3.Visible = true;
            panel3.BringToFront();

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close(); // Closes Form1

            frmLogin loginForm = new frmLogin();
            loginForm.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            HighlightButton((Button)sender);
            frmLogs frm = new frmLogs();
            frm.TopLevel = false;
            panel3.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();
        }
    }
}
