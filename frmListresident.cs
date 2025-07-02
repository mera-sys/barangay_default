using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using barangay_management_system;
using ClosedXML.Excel;
using ExcelDataReader;
using MySql.Data.MySqlClient;
using OfficeOpenXml;



namespace barangay_default
{
    public partial class frmListresident : Form
    {
        public frmListresident()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmListresident_Load);
            guna2DataGridView1.Rows.Add("Item 1");
            guna2DataGridView1.Rows[0].Cells["colStatus"].Value = "ACTIVE";
            guna2DataGridView1.Rows[0].Cells["colStatus"].Style.BackColor = Color.LightGreen;

        }

        public void LoadResidents()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(dbconString.connection))
                {
                    conn.Open();
                    string query = @"SELECT Image, FirstName, MiddleName, LastName, Ext, DateOfBirth, Age, PWD, SingleParent, Voters, Status, ResidentID FROM Residents";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            guna2DataGridView1.Rows.Clear(); 

                            while (reader.Read())
                            {
                                Image img = null;
                                if (reader["Image"] != DBNull.Value)
                                {
                                    byte[] imgBytes = (byte[])reader["Image"];
                                    using (MemoryStream ms = new MemoryStream(imgBytes))
                                    {
                                     
                                        Image originalImg = Image.FromStream(ms);
                                      
                                        img = new Bitmap(originalImg, new Size(50, 50));
                                    }
                                }
                                else
                                {
                                    img = null;
                                }

                                string fullName = $"{reader["FirstName"]} {reader["MiddleName"]} {reader["LastName"]} {reader["Ext"]}";
                                guna2DataGridView1.Rows.Add(
                                    img,
                                    fullName,
                                    Convert.ToDateTime(reader["DateOfBirth"]).ToShortDateString(),
                                    reader["Age"].ToString(),
                                    reader["PWD"].ToString(),
                                    reader["SingleParent"].ToString(),
                                    reader["Voters"].ToString(),
                                    reader["Status"].ToString(), 
                                    reader["ResidentID"].ToString() 
                                );
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading residents: " + ex.Message);
            }
        }

        private void frmListresident_Load(object sender, EventArgs e)
        {
            LoadResidents();
            
        }
        private void dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

            MessageBox.Show($"Error in row {e.RowIndex}, column {e.ColumnIndex}: {e.Exception.Message}");
            e.ThrowException = false;
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmListresident_Load_1(object sender, EventArgs e)
        {


        }
        

        private void guna2DataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
          
        }
       

        private void guna2DataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            
            if (e.RowIndex >= 0 && e.RowIndex < guna2DataGridView1.Rows.Count)
            {
                
                if (e.ColumnIndex == guna2DataGridView1.Columns["colStatus"].Index)
                {
                    var row = guna2DataGridView1.Rows[e.RowIndex];
                    var cell = row.Cells["colStatus"];
                    string currentStatus = cell.Value?.ToString();
                    string newStatus = currentStatus == "ACTIVE" ? "INACTIVE" : "ACTIVE";
                    string residentID = row.Cells["colID"].Value?.ToString();

                    
                    cell.Value = newStatus;
                    cell.Style.BackColor = newStatus == "ACTIVE" ? Color.Green : Color.Red;

                  
                    if (e.RowIndex >= 0 && e.RowIndex < guna2DataGridView1.Rows.Count)
                    {
                        guna2DataGridView1.InvalidateCell(e.ColumnIndex, e.RowIndex);
                    }

                    // Update the status in the database
                    try
                    {
                        using (MySqlConnection conn = new MySqlConnection(dbconString.connection))
                        {
                            conn.Open();
                            string updateQuery = "UPDATE Residents SET Status = @status WHERE ResidentID = @id";

                            using (MySqlCommand cmd = new MySqlCommand(updateQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@status", newStatus);
                                cmd.Parameters.AddWithValue("@id", residentID);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Database update failed: " + ex.Message);
                    }
                }
                // Check if the clicked column is the "btnView" button column (assuming the column name is "btnView")
                else if (e.ColumnIndex == guna2DataGridView1.Columns["btnView"].Index)
                {
                    var row = guna2DataGridView1.Rows[e.RowIndex];
                    string residentID = row.Cells["colID"].Value?.ToString();

                    if (!string.IsNullOrEmpty(residentID))
                    {
                       
                        frmUpdateResidence frm = new frmUpdateResidence(residentID);
                        frm.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Resident ID is missing.");
                    }
                }
            }
            }
        // sdsa jjj sdsdsjjj lllad ;; ldadklll;;; jjjASQ JSSD KKKWL ASDW JJJN J SSDKKJKJJJK  JJJNDWWQWW  
        private void guna2DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

        }

        private void guna2DataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void guna2DataGridView1_CellPainting_1(object sender, DataGridViewCellPaintingEventArgs e)
        {
           
            if (e.RowIndex >= 0 && e.ColumnIndex == guna2DataGridView1.Columns["colStatus"].Index)
            {
                var cell = guna2DataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                string status = cell.Value?.ToString();

                Color backgroundColor;
                if (status == "ACTIVE")
                {
                    backgroundColor = Color.LightGreen; 
                }
                else if (status == "INACTIVE")
                {
                    backgroundColor = Color.LightCoral; 
                }
                else
                {
                    backgroundColor = Color.White; 
                }

               
                int borderRadius = 5; 
                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddArc(e.CellBounds.X, e.CellBounds.Y, borderRadius, borderRadius, 180, 90);
                    path.AddArc(e.CellBounds.X + e.CellBounds.Width - borderRadius, e.CellBounds.Y, borderRadius, borderRadius, 270, 90);
                    path.AddArc(e.CellBounds.X + e.CellBounds.Width - borderRadius, e.CellBounds.Y + e.CellBounds.Height - borderRadius, borderRadius, borderRadius, 0, 90);
                    path.AddArc(e.CellBounds.X, e.CellBounds.Y + e.CellBounds.Height - borderRadius, borderRadius, borderRadius, 90, 90);
                    path.CloseFigure();

                   
                    using (SolidBrush brush = new SolidBrush(backgroundColor))
                    {
                        e.Graphics.FillPath(brush, path);
                    }

                   
                    TextRenderer.DrawText(e.Graphics, status, e.CellStyle.Font, e.CellBounds, Color.Black, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                }

                
                e.Handled = true;
            }
        }
       

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                guna2DataGridView1.Rows.Clear();

                string query = @"
            SELECT Image, FirstName, MiddleName, LastName, DateOfBirth, Age, PWD, SingleParent, Voters, Status, ResidentID 
            FROM Residents 
            WHERE FirstName LIKE @search OR MiddleName LIKE @search OR LastName LIKE @search";

                using (MySqlConnection conn = new MySqlConnection(dbconString.connection))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@search", "%" + txtSearch.Text.Trim() + "%");

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Image img = null;
                                if (reader["Image"] != DBNull.Value)
                                {
                                    byte[] imgBytes = (byte[])reader["Image"];
                                    using (MemoryStream ms = new MemoryStream(imgBytes))
                                    {
                                        Image originalImg = Image.FromStream(ms);
                                        img = new Bitmap(originalImg, new Size(50, 50));
                                    }
                                }

                                string fullName = $"{reader["FirstName"]} {reader["MiddleName"]} {reader["LastName"]}";
                                guna2DataGridView1.Rows.Add(
                                    img,
                                    fullName,
                                    Convert.ToDateTime(reader["DateOfBirth"]).ToShortDateString(),
                                    reader["Age"].ToString(),
                                    reader["PWD"].ToString(),
                                    reader["SingleParent"].ToString(),
                                    reader["Voters"].ToString(),
                                    reader["Status"].ToString(),
                                    reader["ResidentID"].ToString()
                                );
                            }
                        }
                    }
                }

                guna2DataGridView1.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Search error: " + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                try
                {
                    
                    using (var workbook = new XLWorkbook(filePath))
                    {
                        var worksheet = workbook.Worksheet(1); 
                        var rows = worksheet.RowsUsed();

                        foreach (var row in rows)
                        {
                            
                            string lastName = row.Cell(1).Value.ToString();
                            string firstName = row.Cell(2).Value.ToString();
                            string middleName = row.Cell(3).Value.ToString();
                            string ext = row.Cell(4).Value.ToString();
                            string placeOfBirth = row.Cell(5).Value.ToString();
                            DateTime dateOfBirth = GetValidDate(row.Cell(6).Value);  
                            int age = GetValidInteger(row.Cell(7).Value.ToString()); 
                            string sex = row.Cell(8).Value.ToString();
                            string civilStatus = row.Cell(9).Value.ToString();
                            string citizenship = row.Cell(10).Value.ToString();
                            string pwd = row.Cell(11).Value.ToString();
                            string singleParent = row.Cell(12).Value.ToString();
                            string voters = row.Cell(13).Value.ToString();

                            
                            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || dateOfBirth == DateTime.MinValue)
                            {
                                continue;
                            }

                           
                            if (IsResidentExists(firstName, lastName))
                            {
                                continue; 
                            }

                            
                            InsertResident(firstName, middleName, lastName, ext, placeOfBirth, dateOfBirth, age, sex, civilStatus, citizenship, pwd, singleParent, voters);
                            
                            string username = GenerateUsername(firstName, lastName);
                            string password = GeneratePassword();
                            InsertAccount(username, password);
                        }

                        MessageBox.Show("Excel data imported successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error importing Excel file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
     

        
        private DateTime GetValidDate(object dateCellValue)
        {
            DateTime validDate;
            if (dateCellValue is DateTime)
            {
                validDate = (DateTime)dateCellValue; 
            }
            else if (DateTime.TryParse(dateCellValue.ToString(), out validDate))
            {
                
            }
            else
            {
               
                validDate = DateTime.MinValue; 
            }
            return validDate;
        }

        
        private int GetValidInteger(string input)
        {
            int result;
            if (int.TryParse(input, out result))
            {
                return result;
            }
            return 0; 
        }

        private bool IsResidentExists(string firstName, string lastName)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(dbconString.connection))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM Residents WHERE FirstName = @firstName AND LastName = @lastName";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@firstName", firstName);
                        cmd.Parameters.AddWithValue("@lastName", lastName);
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error checking if resident exists: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void InsertResident(string firstName, string middleName, string lastName, string ext, string placeOfBirth, DateTime dateOfBirth, int age, string sex, string civilStatus, string citizenship, string pwd, string singleParent, string voters)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(dbconString.connection))
                {
                    conn.Open();
                    string query = "INSERT INTO Residents (FirstName, MiddleName, LastName, Ext, PlaceOfBirth, DateOfBirth, Age, Sex, CivilStatus, Citizenship, PWD, SingleParent, Voters, Status) " +
                                   "VALUES (@firstName, @middleName, @lastName, @ext, @placeOfBirth, @dateOfBirth, @age, @sex, @civilStatus, @citizenship, @pwd, @singleParent, @voters, 'ACTIVE')";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@firstName", firstName);
                        cmd.Parameters.AddWithValue("@middleName", middleName);
                        cmd.Parameters.AddWithValue("@lastName", lastName);
                        cmd.Parameters.AddWithValue("@ext", ext);
                        cmd.Parameters.AddWithValue("@placeOfBirth", placeOfBirth);
                        cmd.Parameters.AddWithValue("@dateOfBirth", dateOfBirth);
                        cmd.Parameters.AddWithValue("@age", age);
                        cmd.Parameters.AddWithValue("@sex", sex);
                        cmd.Parameters.AddWithValue("@civilStatus", civilStatus);
                        cmd.Parameters.AddWithValue("@citizenship", citizenship);
                        cmd.Parameters.AddWithValue("@pwd", pwd);
                        cmd.Parameters.AddWithValue("@singleParent", singleParent);
                        cmd.Parameters.AddWithValue("@voters", voters);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting resident: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Generate a unique username for the resident
        private string GenerateUsername(string firstName, string lastName)
        {
            return firstName.Substring(0, 3) + lastName.Substring(0, 3); // Example: "JohDoe"
        }

        // Generate a random password (you can implement a more complex logic for secure passwords)
        private string GeneratePassword()
        {
            Random rand = new Random();
            return rand.Next(100000, 999999).ToString(); // Example: generate a 6-digit password
        }

        // Insert the new account into the Accounts table
        private void InsertAccount(string username, string password)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(dbconString.connection))
                {
                    conn.Open();
                    string query = "INSERT INTO Accounts (Username, Password, FullName, ResidentID) " +
                                   "VALUES (@username, @password, @fullName, LAST_INSERT_ID())";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);
                        cmd.Parameters.AddWithValue("@fullName", username); // You can change this to the full name of the resident
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting account: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                try
                {

                    using (var workbook = new XLWorkbook(filePath))
                    {
                        var worksheet = workbook.Worksheet(1);
                        var rows = worksheet.RowsUsed();

                        foreach (var row in rows)
                        {

                            string lastName = row.Cell(1).Value.ToString();
                            string firstName = row.Cell(2).Value.ToString();
                            string middleName = row.Cell(3).Value.ToString();
                            string ext = row.Cell(4).Value.ToString();
                            string placeOfBirth = row.Cell(5).Value.ToString();
                            DateTime dateOfBirth = GetValidDate(row.Cell(6).Value);
                            int age = GetValidInteger(row.Cell(7).Value.ToString());
                            string sex = row.Cell(8).Value.ToString();
                            string civilStatus = row.Cell(9).Value.ToString();
                            string citizenship = row.Cell(10).Value.ToString();
                            string pwd = row.Cell(11).Value.ToString();
                            string singleParent = row.Cell(12).Value.ToString();
                            string voters = row.Cell(13).Value.ToString();


                            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || dateOfBirth == DateTime.MinValue)
                            {
                                continue;
                            }


                            if (IsResidentExists(firstName, lastName))
                            {
                                continue;
                            }


                            InsertResident(firstName, middleName, lastName, ext, placeOfBirth, dateOfBirth, age, sex, civilStatus, citizenship, pwd, singleParent, voters);

                            string username = GenerateUsername(firstName, lastName);
                            string password = GeneratePassword();
                            InsertAccount(username, password);
                        }

                        MessageBox.Show("Excel data imported successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error importing Excel file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}





