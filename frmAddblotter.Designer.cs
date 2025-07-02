
namespace barangay_default
{
    partial class frmAddblotter
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddblotter));
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.cboRemarks = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.label29 = new System.Windows.Forms.Label();
            this.dtIncident = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.dtreporteddate = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.txtIncident = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtInvoledstatement = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtInvolved = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRespondent = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtStatement = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtcomplainant = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(57)))), ((int)(((byte)(63)))));
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.cboRemarks);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.cboStatus);
            this.panel1.Controls.Add(this.label29);
            this.panel1.Controls.Add(this.dtIncident);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.dtreporteddate);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.txtIncident);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txtLocation);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtInvoledstatement);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtInvolved);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtRespondent);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtStatement);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtcomplainant);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1455, 784);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(185)))), ((int)(((byte)(255)))));
            this.button2.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(1172, 733);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(223, 39);
            this.button2.TabIndex = 68;
            this.button2.Text = "      NEW RECORD";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Black;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(999, 733);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(154, 39);
            this.button1.TabIndex = 67;
            this.button1.Text = "      CLOSED";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cboRemarks
            // 
            this.cboRemarks.BackColor = System.Drawing.Color.White;
            this.cboRemarks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRemarks.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboRemarks.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboRemarks.ForeColor = System.Drawing.Color.White;
            this.cboRemarks.FormattingEnabled = true;
            this.cboRemarks.Items.AddRange(new object[] {
            "OPEN",
            "CLOSED"});
            this.cboRemarks.Location = new System.Drawing.Point(1213, 327);
            this.cboRemarks.Name = "cboRemarks";
            this.cboRemarks.Size = new System.Drawing.Size(217, 29);
            this.cboRemarks.TabIndex = 65;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(1153, 283);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(104, 32);
            this.label10.TabIndex = 66;
            this.label10.Text = "Remarks";
            // 
            // cboStatus
            // 
            this.cboStatus.BackColor = System.Drawing.Color.White;
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboStatus.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboStatus.ForeColor = System.Drawing.Color.White;
            this.cboStatus.FormattingEnabled = true;
            this.cboStatus.Items.AddRange(new object[] {
            "NEW",
            "ONGOING"});
            this.cboStatus.Location = new System.Drawing.Point(1213, 191);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(217, 29);
            this.cboStatus.TabIndex = 63;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.label29.ForeColor = System.Drawing.Color.White;
            this.label29.Location = new System.Drawing.Point(1153, 147);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(79, 32);
            this.label29.TabIndex = 64;
            this.label29.Text = "Status";
            // 
            // dtIncident
            // 
            this.dtIncident.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(58)))), ((int)(((byte)(64)))));
            this.dtIncident.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.dtIncident.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtIncident.Location = new System.Drawing.Point(1199, 68);
            this.dtIncident.Name = "dtIncident";
            this.dtIncident.Size = new System.Drawing.Size(231, 29);
            this.dtIncident.TabIndex = 40;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(1139, 24);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(187, 32);
            this.label9.TabIndex = 39;
            this.label9.Text = "Date of Incident";
            // 
            // dtreporteddate
            // 
            this.dtreporteddate.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(58)))), ((int)(((byte)(64)))));
            this.dtreporteddate.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.dtreporteddate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtreporteddate.Location = new System.Drawing.Point(817, 327);
            this.dtreporteddate.Name = "dtreporteddate";
            this.dtreporteddate.Size = new System.Drawing.Size(231, 29);
            this.dtreporteddate.TabIndex = 38;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(757, 283);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(169, 32);
            this.label7.TabIndex = 37;
            this.label7.Text = "Date Reported";
            // 
            // txtIncident
            // 
            this.txtIncident.BackColor = System.Drawing.Color.White;
            this.txtIncident.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtIncident.ForeColor = System.Drawing.Color.Black;
            this.txtIncident.Location = new System.Drawing.Point(817, 191);
            this.txtIncident.Multiline = true;
            this.txtIncident.Name = "txtIncident";
            this.txtIncident.Size = new System.Drawing.Size(231, 45);
            this.txtIncident.TabIndex = 35;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(755, 144);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 32);
            this.label6.TabIndex = 36;
            this.label6.Text = "Incident";
            // 
            // txtLocation
            // 
            this.txtLocation.BackColor = System.Drawing.Color.White;
            this.txtLocation.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtLocation.ForeColor = System.Drawing.Color.Black;
            this.txtLocation.Location = new System.Drawing.Point(817, 68);
            this.txtLocation.Multiline = true;
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(231, 45);
            this.txtLocation.TabIndex = 33;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(755, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(227, 32);
            this.label5.TabIndex = 34;
            this.label5.Text = "Location of Incident";
            // 
            // txtInvoledstatement
            // 
            this.txtInvoledstatement.BackColor = System.Drawing.Color.White;
            this.txtInvoledstatement.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtInvoledstatement.ForeColor = System.Drawing.Color.Black;
            this.txtInvoledstatement.Location = new System.Drawing.Point(115, 546);
            this.txtInvoledstatement.Multiline = true;
            this.txtInvoledstatement.Name = "txtInvoledstatement";
            this.txtInvoledstatement.Size = new System.Drawing.Size(537, 45);
            this.txtInvoledstatement.TabIndex = 31;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(53, 499);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(300, 32);
            this.label4.TabIndex = 32;
            this.label4.Text = "Person Involved Statement";
            // 
            // txtInvolved
            // 
            this.txtInvolved.BackColor = System.Drawing.Color.White;
            this.txtInvolved.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtInvolved.ForeColor = System.Drawing.Color.Black;
            this.txtInvolved.Location = new System.Drawing.Point(115, 429);
            this.txtInvolved.Multiline = true;
            this.txtInvolved.Name = "txtInvolved";
            this.txtInvolved.Size = new System.Drawing.Size(537, 45);
            this.txtInvolved.TabIndex = 29;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(53, 382);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(184, 32);
            this.label3.TabIndex = 30;
            this.label3.Text = "Person Involved";
            // 
            // txtRespondent
            // 
            this.txtRespondent.BackColor = System.Drawing.Color.White;
            this.txtRespondent.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtRespondent.ForeColor = System.Drawing.Color.Black;
            this.txtRespondent.Location = new System.Drawing.Point(115, 311);
            this.txtRespondent.Multiline = true;
            this.txtRespondent.Name = "txtRespondent";
            this.txtRespondent.Size = new System.Drawing.Size(537, 45);
            this.txtRespondent.TabIndex = 27;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(53, 264);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 32);
            this.label2.TabIndex = 28;
            this.label2.Text = "Respondent";
            // 
            // txtStatement
            // 
            this.txtStatement.BackColor = System.Drawing.Color.White;
            this.txtStatement.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtStatement.ForeColor = System.Drawing.Color.Black;
            this.txtStatement.Location = new System.Drawing.Point(115, 191);
            this.txtStatement.Multiline = true;
            this.txtStatement.Name = "txtStatement";
            this.txtStatement.Size = new System.Drawing.Size(537, 70);
            this.txtStatement.TabIndex = 25;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(53, 144);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(267, 32);
            this.label1.TabIndex = 26;
            this.label1.Text = "Complainant Statement";
            // 
            // txtcomplainant
            // 
            this.txtcomplainant.AllowDrop = true;
            this.txtcomplainant.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtcomplainant.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtcomplainant.BackColor = System.Drawing.Color.White;
            this.txtcomplainant.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtcomplainant.ForeColor = System.Drawing.Color.Black;
            this.txtcomplainant.Location = new System.Drawing.Point(115, 68);
            this.txtcomplainant.Name = "txtcomplainant";
            this.txtcomplainant.Size = new System.Drawing.Size(537, 29);
            this.txtcomplainant.TabIndex = 23;
            this.txtcomplainant.TextChanged += new System.EventHandler(this.txtcomplainant_TextChanged);
            this.txtcomplainant.Enter += new System.EventHandler(this.txtcomplainant_Enter);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(53, 21);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(151, 32);
            this.label8.TabIndex = 24;
            this.label8.Text = "Complainant";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // frmAddblotter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(77)))), ((int)(((byte)(85)))));
            this.ClientSize = new System.Drawing.Size(1455, 784);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmAddblotter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmAddblotter_Load_1);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.TextBox txtIncident;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox txtInvoledstatement;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox txtInvolved;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txtRespondent;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtStatement;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtcomplainant;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtIncident;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker dtreporteddate;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.ComboBox cboRemarks;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Timer timer1;
    }
}