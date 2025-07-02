
namespace barangay_default
{
    partial class frmCertificate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCertificate));
            this.panel1 = new System.Windows.Forms.Panel();
            this.cboRemarks = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtReference = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.txtNameofBusiness = new System.Windows.Forms.TextBox();
            this.txtPurpose = new System.Windows.Forms.TextBox();
            this.label = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpDateIssued = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.cboCertificateType = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtResident = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(77)))), ((int)(((byte)(85)))));
            this.panel1.Controls.Add(this.cboRemarks);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtReference);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtAmount);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnGenerate);
            this.panel1.Controls.Add(this.txtNameofBusiness);
            this.panel1.Controls.Add(this.txtPurpose);
            this.panel1.Controls.Add(this.label);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dtpDateIssued);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cboCertificateType);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.txtResident);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(546, 879);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // cboRemarks
            // 
            this.cboRemarks.BackColor = System.Drawing.Color.White;
            this.cboRemarks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRemarks.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboRemarks.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboRemarks.ForeColor = System.Drawing.Color.Black;
            this.cboRemarks.FormattingEnabled = true;
            this.cboRemarks.Items.AddRange(new object[] {
            "NO RECORDS OF ANY OFFENCE AND THAT THE SAID PERSON IS OF GOOD MORAL CHARACTER AND" +
                " A LAW ABIDING CITIZEN OF THE COMMUNITY. ",
            "THERE IS A PENDING RECORD CURRENTLY SUBJECT FOR VERIFICATION",
            "HAS A RECORD OF AN OFFENCE AND THAT THE SAID PERSON\'S CASE HAS BEEN RECORDED IN T" +
                "HE BARANGAY"});
            this.cboRemarks.Location = new System.Drawing.Point(245, 410);
            this.cboRemarks.Name = "cboRemarks";
            this.cboRemarks.Size = new System.Drawing.Size(275, 29);
            this.cboRemarks.TabIndex = 68;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(12, 407);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 32);
            this.label5.TabIndex = 67;
            this.label5.Text = "Remarks";
            // 
            // txtReference
            // 
            this.txtReference.BackColor = System.Drawing.Color.White;
            this.txtReference.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtReference.ForeColor = System.Drawing.Color.Black;
            this.txtReference.Location = new System.Drawing.Point(245, 460);
            this.txtReference.Name = "txtReference";
            this.txtReference.Size = new System.Drawing.Size(275, 29);
            this.txtReference.TabIndex = 64;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(12, 457);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(216, 32);
            this.label4.TabIndex = 65;
            this.label4.Text = "Reference Number";
            // 
            // txtAmount
            // 
            this.txtAmount.BackColor = System.Drawing.Color.White;
            this.txtAmount.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtAmount.ForeColor = System.Drawing.Color.Black;
            this.txtAmount.Location = new System.Drawing.Point(245, 517);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(90, 29);
            this.txtAmount.TabIndex = 62;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(12, 514);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 32);
            this.label2.TabIndex = 63;
            this.label2.Text = "Amount:";
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
            // txtNameofBusiness
            // 
            this.txtNameofBusiness.BackColor = System.Drawing.Color.White;
            this.txtNameofBusiness.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtNameofBusiness.ForeColor = System.Drawing.Color.Black;
            this.txtNameofBusiness.Location = new System.Drawing.Point(245, 316);
            this.txtNameofBusiness.Name = "txtNameofBusiness";
            this.txtNameofBusiness.Size = new System.Drawing.Size(275, 29);
            this.txtNameofBusiness.TabIndex = 43;
            // 
            // txtPurpose
            // 
            this.txtPurpose.BackColor = System.Drawing.Color.White;
            this.txtPurpose.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtPurpose.ForeColor = System.Drawing.Color.Black;
            this.txtPurpose.Location = new System.Drawing.Point(245, 362);
            this.txtPurpose.Name = "txtPurpose";
            this.txtPurpose.Size = new System.Drawing.Size(275, 29);
            this.txtPurpose.TabIndex = 43;
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.label.ForeColor = System.Drawing.Color.White;
            this.label.Location = new System.Drawing.Point(12, 313);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(211, 32);
            this.label.TabIndex = 44;
            this.label.Text = "Name of Business:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 359);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(227, 32);
            this.label1.TabIndex = 44;
            this.label1.Text = "Purpose of Request:";
            // 
            // dtpDateIssued
            // 
            this.dtpDateIssued.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(58)))), ((int)(((byte)(64)))));
            this.dtpDateIssued.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.dtpDateIssued.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateIssued.Location = new System.Drawing.Point(245, 210);
            this.dtpDateIssued.Name = "dtpDateIssued";
            this.dtpDateIssued.Size = new System.Drawing.Size(275, 29);
            this.dtpDateIssued.TabIndex = 42;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(3, 207);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(165, 32);
            this.label3.TabIndex = 41;
            this.label3.Text = " Date Issued:  ";
            // 
            // cboCertificateType
            // 
            this.cboCertificateType.BackColor = System.Drawing.Color.White;
            this.cboCertificateType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCertificateType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboCertificateType.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCertificateType.ForeColor = System.Drawing.Color.Black;
            this.cboCertificateType.FormattingEnabled = true;
            this.cboCertificateType.Items.AddRange(new object[] {
            "Barangay Certificate",
            "Barangay Indigency",
            "Business Pirmet"});
            this.cboCertificateType.Location = new System.Drawing.Point(245, 267);
            this.cboCertificateType.Name = "cboCertificateType";
            this.cboCertificateType.Size = new System.Drawing.Size(275, 29);
            this.cboCertificateType.TabIndex = 39;
            this.cboCertificateType.SelectedIndexChanged += new System.EventHandler(this.cboCertificateType_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(12, 264);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(193, 32);
            this.label11.TabIndex = 40;
            this.label11.Text = "Certificate Type: ";
            // 
            // txtResident
            // 
            this.txtResident.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtResident.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtResident.BackColor = System.Drawing.Color.White;
            this.txtResident.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtResident.ForeColor = System.Drawing.Color.Black;
            this.txtResident.Location = new System.Drawing.Point(245, 158);
            this.txtResident.Name = "txtResident";
            this.txtResident.Size = new System.Drawing.Size(275, 29);
            this.txtResident.TabIndex = 37;
            this.txtResident.TextChanged += new System.EventHandler(this.txtResident_TextChanged);
            this.txtResident.Enter += new System.EventHandler(this.txtResident_Enter);
            this.txtResident.Leave += new System.EventHandler(this.txtResident_Leave);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(12, 155);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(182, 32);
            this.label8.TabIndex = 38;
            this.label8.Text = "Select Resident:";
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
            this.reportViewer1.TabIndex = 1;
            this.reportViewer1.Load += new System.EventHandler(this.reportViewer1_Load);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(77)))), ((int)(((byte)(85)))));
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(546, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(848, 149);
            this.panel2.TabIndex = 64;
            // 
            // frmCertificate
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
            this.Name = "frmCertificate";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmCertificate_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.TextBox txtPurpose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpDateIssued;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.ComboBox cboCertificateType;
        private System.Windows.Forms.Label label11;
        public System.Windows.Forms.TextBox txtResident;
        private System.Windows.Forms.Label label8;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.Button btnGenerate;
        public System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox txtReference;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.ComboBox cboRemarks;
        public System.Windows.Forms.TextBox txtNameofBusiness;
        private System.Windows.Forms.Label label;
    }
}