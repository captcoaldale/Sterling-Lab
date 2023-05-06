namespace Sterling_Lab
{
    partial class ICP
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.pnlDisplay = new System.Windows.Forms.Panel();
            this.lblDisplay = new System.Windows.Forms.Label();
            this.dgvDisplay = new System.Windows.Forms.DataGridView();
            this.dtpBeg = new System.Windows.Forms.DateTimePicker();
            this.gbxFile = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.pbrProgress = new System.Windows.Forms.ProgressBar();
            this.btnQuery = new System.Windows.Forms.Button();
            this.cmbFile = new System.Windows.Forms.ComboBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.lblEnd = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.lblBeg = new System.Windows.Forms.Label();
            this.lblICPDisplay = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chtDisplay = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.gbxSearch = new System.Windows.Forms.GroupBox();
            this.lblSample2 = new System.Windows.Forms.Label();
            this.cmbSample2 = new System.Windows.Forms.ComboBox();
            this.lblClass2 = new System.Windows.Forms.Label();
            this.cmbClass2 = new System.Windows.Forms.ComboBox();
            this.lblSample1 = new System.Windows.Forms.Label();
            this.cmbSample1 = new System.Windows.Forms.ComboBox();
            this.lblClass1 = new System.Windows.Forms.Label();
            this.cmbClass1 = new System.Windows.Forms.ComboBox();
            this.lblElement = new System.Windows.Forms.Label();
            this.cmbElement = new System.Windows.Forms.ComboBox();
            this.pnlDisplay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDisplay)).BeginInit();
            this.gbxFile.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chtDisplay)).BeginInit();
            this.gbxSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlDisplay
            // 
            this.pnlDisplay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.pnlDisplay.Controls.Add(this.lblDisplay);
            this.pnlDisplay.Location = new System.Drawing.Point(68, -240);
            this.pnlDisplay.Name = "pnlDisplay";
            this.pnlDisplay.Size = new System.Drawing.Size(563, 76);
            this.pnlDisplay.TabIndex = 31;
            // 
            // lblDisplay
            // 
            this.lblDisplay.AutoSize = true;
            this.lblDisplay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblDisplay.Font = new System.Drawing.Font("Lucida Bright", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDisplay.ForeColor = System.Drawing.Color.White;
            this.lblDisplay.Location = new System.Drawing.Point(41, 24);
            this.lblDisplay.Name = "lblDisplay";
            this.lblDisplay.Size = new System.Drawing.Size(185, 33);
            this.lblDisplay.TabIndex = 0;
            this.lblDisplay.Text = "Work Order";
            // 
            // dgvDisplay
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dgvDisplay.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDisplay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDisplay.Location = new System.Drawing.Point(328, 208);
            this.dgvDisplay.Name = "dgvDisplay";
            this.dgvDisplay.ReadOnly = true;
            this.dgvDisplay.Size = new System.Drawing.Size(796, 421);
            this.dgvDisplay.TabIndex = 31;
            this.dgvDisplay.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDisplay_CellContentClick);
            // 
            // dtpBeg
            // 
            this.dtpBeg.CustomFormat = "yyyy-mm-dd hh:mm:ss";
            this.dtpBeg.Location = new System.Drawing.Point(56, 20);
            this.dtpBeg.Name = "dtpBeg";
            this.dtpBeg.Size = new System.Drawing.Size(107, 20);
            this.dtpBeg.TabIndex = 29;
            // 
            // gbxFile
            // 
            this.gbxFile.Controls.Add(this.btnSave);
            this.gbxFile.Controls.Add(this.pbrProgress);
            this.gbxFile.Controls.Add(this.btnQuery);
            this.gbxFile.Controls.Add(this.cmbFile);
            this.gbxFile.Controls.Add(this.btnOpen);
            this.gbxFile.Controls.Add(this.lblEnd);
            this.gbxFile.Controls.Add(this.dtpEnd);
            this.gbxFile.Controls.Add(this.lblBeg);
            this.gbxFile.Controls.Add(this.dtpBeg);
            this.gbxFile.Location = new System.Drawing.Point(3, 85);
            this.gbxFile.Name = "gbxFile";
            this.gbxFile.Size = new System.Drawing.Size(462, 117);
            this.gbxFile.TabIndex = 32;
            this.gbxFile.TabStop = false;
            this.gbxFile.Text = "File";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(357, 52);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(89, 23);
            this.btnSave.TabIndex = 38;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // pbrProgress
            // 
            this.pbrProgress.Location = new System.Drawing.Point(19, 81);
            this.pbrProgress.Name = "pbrProgress";
            this.pbrProgress.Size = new System.Drawing.Size(428, 22);
            this.pbrProgress.TabIndex = 37;
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(357, 20);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(89, 23);
            this.btnQuery.TabIndex = 36;
            this.btnQuery.Text = "Daily Results";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // cmbFile
            // 
            this.cmbFile.FormattingEnabled = true;
            this.cmbFile.Location = new System.Drawing.Point(128, 54);
            this.cmbFile.Name = "cmbFile";
            this.cmbFile.Size = new System.Drawing.Size(199, 21);
            this.cmbFile.TabIndex = 35;
            this.cmbFile.SelectedIndexChanged += new System.EventHandler(this.cmbFile_SelectedIndexChanged);
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(19, 52);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(89, 23);
            this.btnOpen.TabIndex = 34;
            this.btnOpen.Text = "Open File";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // lblEnd
            // 
            this.lblEnd.AutoSize = true;
            this.lblEnd.Location = new System.Drawing.Point(180, 25);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(26, 13);
            this.lblEnd.TabIndex = 33;
            this.lblEnd.Text = "End";
            // 
            // dtpEnd
            // 
            this.dtpEnd.CustomFormat = "yyyy-mm-dd hh:mm:ss";
            this.dtpEnd.Location = new System.Drawing.Point(221, 18);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(106, 20);
            this.dtpEnd.TabIndex = 32;
            // 
            // lblBeg
            // 
            this.lblBeg.AutoSize = true;
            this.lblBeg.Location = new System.Drawing.Point(16, 26);
            this.lblBeg.Name = "lblBeg";
            this.lblBeg.Size = new System.Drawing.Size(34, 13);
            this.lblBeg.TabIndex = 31;
            this.lblBeg.Text = "Begin";
            // 
            // lblICPDisplay
            // 
            this.lblICPDisplay.AutoSize = true;
            this.lblICPDisplay.BackColor = System.Drawing.Color.Blue;
            this.lblICPDisplay.Font = new System.Drawing.Font("Lucida Bright", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblICPDisplay.ForeColor = System.Drawing.Color.White;
            this.lblICPDisplay.Location = new System.Drawing.Point(41, 24);
            this.lblICPDisplay.Name = "lblICPDisplay";
            this.lblICPDisplay.Size = new System.Drawing.Size(63, 33);
            this.lblICPDisplay.TabIndex = 0;
            this.lblICPDisplay.Text = "ICP";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Blue;
            this.panel1.Controls.Add(this.lblICPDisplay);
            this.panel1.ForeColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1150, 79);
            this.panel1.TabIndex = 33;
            // 
            // chtDisplay
            // 
            chartArea1.Name = "ChartArea1";
            this.chtDisplay.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chtDisplay.Legends.Add(legend1);
            this.chtDisplay.Location = new System.Drawing.Point(22, 223);
            this.chtDisplay.Name = "chtDisplay";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chtDisplay.Series.Add(series1);
            this.chtDisplay.Size = new System.Drawing.Size(300, 300);
            this.chtDisplay.TabIndex = 32;
            this.chtDisplay.Text = "chart1";
            // 
            // gbxSearch
            // 
            this.gbxSearch.Controls.Add(this.lblSample2);
            this.gbxSearch.Controls.Add(this.cmbSample2);
            this.gbxSearch.Controls.Add(this.lblClass2);
            this.gbxSearch.Controls.Add(this.cmbClass2);
            this.gbxSearch.Controls.Add(this.lblSample1);
            this.gbxSearch.Controls.Add(this.cmbSample1);
            this.gbxSearch.Controls.Add(this.lblClass1);
            this.gbxSearch.Controls.Add(this.cmbClass1);
            this.gbxSearch.Controls.Add(this.lblElement);
            this.gbxSearch.Controls.Add(this.cmbElement);
            this.gbxSearch.Location = new System.Drawing.Point(471, 93);
            this.gbxSearch.Name = "gbxSearch";
            this.gbxSearch.Size = new System.Drawing.Size(661, 109);
            this.gbxSearch.TabIndex = 34;
            this.gbxSearch.TabStop = false;
            this.gbxSearch.Text = "Search";
            // 
            // lblSample2
            // 
            this.lblSample2.AutoSize = true;
            this.lblSample2.Location = new System.Drawing.Point(408, 63);
            this.lblSample2.Name = "lblSample2";
            this.lblSample2.Size = new System.Drawing.Size(51, 13);
            this.lblSample2.TabIndex = 45;
            this.lblSample2.Text = "Sample 2";
            // 
            // cmbSample2
            // 
            this.cmbSample2.FormattingEnabled = true;
            this.cmbSample2.Location = new System.Drawing.Point(465, 55);
            this.cmbSample2.Name = "cmbSample2";
            this.cmbSample2.Size = new System.Drawing.Size(121, 21);
            this.cmbSample2.TabIndex = 44;
            this.cmbSample2.SelectedIndexChanged += new System.EventHandler(this.cmbSample2_SelectedIndexChanged);
            // 
            // lblClass2
            // 
            this.lblClass2.AutoSize = true;
            this.lblClass2.Location = new System.Drawing.Point(201, 60);
            this.lblClass2.Name = "lblClass2";
            this.lblClass2.Size = new System.Drawing.Size(41, 13);
            this.lblClass2.TabIndex = 43;
            this.lblClass2.Text = "Class 2";
            // 
            // cmbClass2
            // 
            this.cmbClass2.FormattingEnabled = true;
            this.cmbClass2.Location = new System.Drawing.Point(248, 57);
            this.cmbClass2.Name = "cmbClass2";
            this.cmbClass2.Size = new System.Drawing.Size(121, 21);
            this.cmbClass2.TabIndex = 42;
            this.cmbClass2.SelectedIndexChanged += new System.EventHandler(this.cmbClass2_SelectedIndexChanged);
            // 
            // lblSample1
            // 
            this.lblSample1.AutoSize = true;
            this.lblSample1.Location = new System.Drawing.Point(408, 25);
            this.lblSample1.Name = "lblSample1";
            this.lblSample1.Size = new System.Drawing.Size(51, 13);
            this.lblSample1.TabIndex = 41;
            this.lblSample1.Text = "Sample 1";
            // 
            // cmbSample1
            // 
            this.cmbSample1.FormattingEnabled = true;
            this.cmbSample1.Location = new System.Drawing.Point(465, 17);
            this.cmbSample1.Name = "cmbSample1";
            this.cmbSample1.Size = new System.Drawing.Size(121, 21);
            this.cmbSample1.TabIndex = 40;
            this.cmbSample1.SelectedIndexChanged += new System.EventHandler(this.cmbSample1_SelectedIndexChanged);
            // 
            // lblClass1
            // 
            this.lblClass1.AutoSize = true;
            this.lblClass1.Location = new System.Drawing.Point(201, 22);
            this.lblClass1.Name = "lblClass1";
            this.lblClass1.Size = new System.Drawing.Size(41, 13);
            this.lblClass1.TabIndex = 39;
            this.lblClass1.Text = "Class 1";
            // 
            // cmbClass1
            // 
            this.cmbClass1.FormattingEnabled = true;
            this.cmbClass1.Location = new System.Drawing.Point(248, 19);
            this.cmbClass1.Name = "cmbClass1";
            this.cmbClass1.Size = new System.Drawing.Size(121, 21);
            this.cmbClass1.TabIndex = 38;
            this.cmbClass1.SelectedIndexChanged += new System.EventHandler(this.cmbClass1_SelectedIndexChanged);
            // 
            // lblElement
            // 
            this.lblElement.AutoSize = true;
            this.lblElement.Location = new System.Drawing.Point(6, 26);
            this.lblElement.Name = "lblElement";
            this.lblElement.Size = new System.Drawing.Size(45, 13);
            this.lblElement.TabIndex = 37;
            this.lblElement.Text = "Element";
            // 
            // cmbElement
            // 
            this.cmbElement.FormattingEnabled = true;
            this.cmbElement.Location = new System.Drawing.Point(57, 19);
            this.cmbElement.Name = "cmbElement";
            this.cmbElement.Size = new System.Drawing.Size(121, 21);
            this.cmbElement.TabIndex = 36;
            this.cmbElement.SelectedIndexChanged += new System.EventHandler(this.cmbElement_SelectedIndexChanged);
            // 
            // ICP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbxSearch);
            this.Controls.Add(this.chtDisplay);
            this.Controls.Add(this.gbxFile);
            this.Controls.Add(this.dgvDisplay);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlDisplay);
            this.Name = "ICP";
            this.Size = new System.Drawing.Size(1150, 800);
            this.pnlDisplay.ResumeLayout(false);
            this.pnlDisplay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDisplay)).EndInit();
            this.gbxFile.ResumeLayout(false);
            this.gbxFile.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chtDisplay)).EndInit();
            this.gbxSearch.ResumeLayout(false);
            this.gbxSearch.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnlDisplay;
        private System.Windows.Forms.Label lblDisplay;
        private System.Windows.Forms.DataGridView dgvDisplay;
        private System.Windows.Forms.DateTimePicker dtpBeg;
        private System.Windows.Forms.GroupBox gbxFile;
        private System.Windows.Forms.Label lblBeg;
        private System.Windows.Forms.Label lblICPDisplay;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chtDisplay;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Label lblEnd;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.ComboBox cmbFile;
        private System.Windows.Forms.GroupBox gbxSearch;
        private System.Windows.Forms.Label lblSample2;
        private System.Windows.Forms.ComboBox cmbSample2;
        private System.Windows.Forms.Label lblClass2;
        private System.Windows.Forms.ComboBox cmbClass2;
        private System.Windows.Forms.Label lblSample1;
        private System.Windows.Forms.ComboBox cmbSample1;
        private System.Windows.Forms.Label lblClass1;
        private System.Windows.Forms.ComboBox cmbClass1;
        private System.Windows.Forms.Label lblElement;
        private System.Windows.Forms.ComboBox cmbElement;
        private System.Windows.Forms.ProgressBar pbrProgress;
        private System.Windows.Forms.Button btnSave;
    }
}
