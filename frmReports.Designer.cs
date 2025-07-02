
namespace barangay_default
{
    partial class frmReports
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReports));
            this.btnGenerate = new System.Windows.Forms.Button();
            this.label = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpDateStarted = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.cboReportType = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cboResidentStatus = new System.Windows.Forms.ComboBox();
            this.cboResidentCategories = new System.Windows.Forms.ComboBox();
            this.dtpDateEnd = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGenerate
            // 
            this.btnGenerate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(112)))));
            this.btnGenerate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGenerate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(78)))), ((int)(((byte)(84)))));
            this.btnGenerate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(78)))), ((int)(((byte)(84)))));
            this.btnGenerate.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerate.ForeColor = System.Drawing.Color.White;
            this.btnGenerate.Image = ((System.Drawing.Image)(resources.GetObject("btnGenerate.Image")));
            this.btnGenerate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGenerate.Location = new System.Drawing.Point(179, 677);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.btnGenerate.Size = new System.Drawing.Size(207, 80);
            this.btnGenerate.TabIndex = 61;
            this.btnGenerate.Text = "      Generate";
            this.btnGenerate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGenerate.UseVisualStyleBackColor = false;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.label.ForeColor = System.Drawing.Color.White;
            this.label.Location = new System.Drawing.Point(3, 310);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(231, 32);
            this.label.TabIndex = 44;
            this.label.Text = "Resident Categories:";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(77)))), ((int)(((byte)(85)))));
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(546, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(848, 149);
            this.panel2.TabIndex = 67;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reportViewer1.Location = new System.Drawing.Point(546, 155);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(836, 712);
            this.reportViewer1.TabIndex = 66;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 359);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(182, 32);
            this.label1.TabIndex = 44;
            this.label1.Text = "Resident Status:";
            // 
            // dtpDateStarted
            // 
            this.dtpDateStarted.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(58)))), ((int)(((byte)(64)))));
            this.dtpDateStarted.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.dtpDateStarted.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateStarted.Location = new System.Drawing.Point(245, 155);
            this.dtpDateStarted.Name = "dtpDateStarted";
            this.dtpDateStarted.Size = new System.Drawing.Size(275, 29);
            this.dtpDateStarted.TabIndex = 42;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(3, 152);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 32);
            this.label3.TabIndex = 41;
            this.label3.Text = "Start Date:";
            // 
            // cboReportType
            // 
            this.cboReportType.BackColor = System.Drawing.Color.White;
            this.cboReportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboReportType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboReportType.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboReportType.ForeColor = System.Drawing.Color.Black;
            this.cboReportType.FormattingEnabled = true;
            this.cboReportType.Items.AddRange(new object[] {
            "Residents",
            "COLLECTION AND DISBURSEMENT"});
            this.cboReportType.Location = new System.Drawing.Point(245, 261);
            this.cboReportType.Name = "cboReportType";
            this.cboReportType.Size = new System.Drawing.Size(275, 29);
            this.cboReportType.TabIndex = 39;
            this.cboReportType.SelectedIndexChanged += new System.EventHandler(this.cboReportType_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(3, 255);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(148, 32);
            this.label11.TabIndex = 40;
            this.label11.Text = "Report Type:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(77)))), ((int)(((byte)(85)))));
            this.panel1.Controls.Add(this.cboResidentStatus);
            this.panel1.Controls.Add(this.cboResidentCategories);
            this.panel1.Controls.Add(this.dtpDateEnd);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.btnGenerate);
            this.panel1.Controls.Add(this.label);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dtpDateStarted);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cboReportType);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(546, 879);
            this.panel1.TabIndex = 65;
            // 
            // cboResidentStatus
            // 
            this.cboResidentStatus.BackColor = System.Drawing.Color.White;
            this.cboResidentStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboResidentStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboResidentStatus.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboResidentStatus.ForeColor = System.Drawing.Color.Black;
            this.cboResidentStatus.FormattingEnabled = true;
            this.cboResidentStatus.Items.AddRange(new object[] {
            "All",
            "Active",
            "Inactive"});
            this.cboResidentStatus.Location = new System.Drawing.Point(245, 365);
            this.cboResidentStatus.Name = "cboResidentStatus";
            this.cboResidentStatus.Size = new System.Drawing.Size(275, 29);
            this.cboResidentStatus.TabIndex = 72;
            // 
            // cboResidentCategories
            // 
            this.cboResidentCategories.BackColor = System.Drawing.Color.White;
            this.cboResidentCategories.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboResidentCategories.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboResidentCategories.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboResidentCategories.ForeColor = System.Drawing.Color.Black;
            this.cboResidentCategories.FormattingEnabled = true;
            this.cboResidentCategories.Items.AddRange(new object[] {
            "PWD",
            "Household",
            "Senior Citizen",
            "Family Head",
            "Single Parent",
            "All"});
            this.cboResidentCategories.Location = new System.Drawing.Point(245, 313);
            this.cboResidentCategories.Name = "cboResidentCategories";
            this.cboResidentCategories.Size = new System.Drawing.Size(275, 29);
            this.cboResidentCategories.TabIndex = 71;
            // 
            // dtpDateEnd
            // 
            this.dtpDateEnd.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(58)))), ((int)(((byte)(64)))));
            this.dtpDateEnd.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.dtpDateEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateEnd.Location = new System.Drawing.Point(245, 205);
            this.dtpDateEnd.Name = "dtpDateEnd";
            this.dtpDateEnd.Size = new System.Drawing.Size(275, 29);
            this.dtpDateEnd.TabIndex = 70;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(3, 202);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(117, 32);
            this.label6.TabIndex = 69;
            this.label6.Text = "End Date:";
            // 
            // frmReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(77)))), ((int)(((byte)(85)))));
            this.ClientSize = new System.Drawing.Size(1394, 879);
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmReports";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Panel panel2;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpDateStarted;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.ComboBox cboReportType;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.ComboBox cboResidentCategories;
        private System.Windows.Forms.DateTimePicker dtpDateEnd;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.ComboBox cboResidentStatus;
    }
}