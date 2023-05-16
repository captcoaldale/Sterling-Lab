namespace Sterling_Lab
{
    partial class Analyze
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
            this.pnlWorkOrder = new System.Windows.Forms.Panel();
            this.lblTask = new System.Windows.Forms.Label();
            this.lblDisplay = new System.Windows.Forms.Label();
            this.gbxNavControls = new System.Windows.Forms.GroupBox();
            this.lblPosition = new System.Windows.Forms.Label();
            this.btnEnd = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnBeg = new System.Windows.Forms.Button();
            this.dgvTextBoxColumnFilter1 = new DgvFilterPopup.DgvTextBoxColumnFilter();
            this.gbxDataControls = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.gbxSearch = new System.Windows.Forms.GroupBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dtpBeg_Date = new System.Windows.Forms.DateTimePicker();
            this.cmbClient = new System.Windows.Forms.ComboBox();
            this.dgvDisplay = new System.Windows.Forms.DataGridView();
            this.cbxClient = new System.Windows.Forms.CheckBox();
            this.cbxDate = new System.Windows.Forms.CheckBox();
            this.cbxField = new System.Windows.Forms.CheckBox();
            this.cmbField = new System.Windows.Forms.ComboBox();
            this.dtpEnd_Date = new System.Windows.Forms.DateTimePicker();
            this.gbxDisplay = new System.Windows.Forms.GroupBox();
            this.gbxChart = new System.Windows.Forms.GroupBox();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.pnlWorkOrder.SuspendLayout();
            this.gbxNavControls.SuspendLayout();
            this.gbxDataControls.SuspendLayout();
            this.gbxSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDisplay)).BeginInit();
            this.gbxDisplay.SuspendLayout();
            this.gbxChart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlWorkOrder
            // 
            this.pnlWorkOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.pnlWorkOrder.Controls.Add(this.lblTask);
            this.pnlWorkOrder.Controls.Add(this.lblDisplay);
            this.pnlWorkOrder.Font = new System.Drawing.Font("Lucida Bright", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlWorkOrder.Location = new System.Drawing.Point(0, 0);
            this.pnlWorkOrder.Name = "pnlWorkOrder";
            this.pnlWorkOrder.Size = new System.Drawing.Size(1077, 75);
            this.pnlWorkOrder.TabIndex = 1;
            // 
            // lblTask
            // 
            this.lblTask.AutoSize = true;
            this.lblTask.Font = new System.Drawing.Font("Lucida Bright", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTask.ForeColor = System.Drawing.Color.White;
            this.lblTask.Location = new System.Drawing.Point(665, 33);
            this.lblTask.Name = "lblTask";
            this.lblTask.Size = new System.Drawing.Size(57, 22);
            this.lblTask.TabIndex = 1;
            this.lblTask.Text = "Task";
            // 
            // lblDisplay
            // 
            this.lblDisplay.AutoSize = true;
            this.lblDisplay.ForeColor = System.Drawing.Color.White;
            this.lblDisplay.Location = new System.Drawing.Point(28, 21);
            this.lblDisplay.Name = "lblDisplay";
            this.lblDisplay.Size = new System.Drawing.Size(207, 36);
            this.lblDisplay.TabIndex = 0;
            this.lblDisplay.Text = "Work Order";
            // 
            // gbxNavControls
            // 
            this.gbxNavControls.Controls.Add(this.lblPosition);
            this.gbxNavControls.Controls.Add(this.btnEnd);
            this.gbxNavControls.Controls.Add(this.btnNext);
            this.gbxNavControls.Controls.Add(this.btnPrev);
            this.gbxNavControls.Controls.Add(this.btnBeg);
            this.gbxNavControls.Location = new System.Drawing.Point(614, 330);
            this.gbxNavControls.Name = "gbxNavControls";
            this.gbxNavControls.Size = new System.Drawing.Size(444, 56);
            this.gbxNavControls.TabIndex = 60;
            this.gbxNavControls.TabStop = false;
            this.gbxNavControls.Text = "Scroll";
            // 
            // lblPosition
            // 
            this.lblPosition.AutoSize = true;
            this.lblPosition.Location = new System.Drawing.Point(214, 24);
            this.lblPosition.Name = "lblPosition";
            this.lblPosition.Size = new System.Drawing.Size(14, 13);
            this.lblPosition.TabIndex = 48;
            this.lblPosition.Text = "#";
            // 
            // btnEnd
            // 
            this.btnEnd.BackColor = System.Drawing.Color.Silver;
            this.btnEnd.Location = new System.Drawing.Point(347, 19);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Size = new System.Drawing.Size(77, 25);
            this.btnEnd.TabIndex = 41;
            this.btnEnd.Text = ">>";
            this.btnEnd.UseVisualStyleBackColor = false;
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.Color.Silver;
            this.btnNext.Location = new System.Drawing.Point(264, 19);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(77, 25);
            this.btnNext.TabIndex = 40;
            this.btnNext.Text = ">";
            this.btnNext.UseVisualStyleBackColor = false;
            // 
            // btnPrev
            // 
            this.btnPrev.BackColor = System.Drawing.Color.Silver;
            this.btnPrev.Location = new System.Drawing.Point(103, 20);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(77, 25);
            this.btnPrev.TabIndex = 38;
            this.btnPrev.Text = "<";
            this.btnPrev.UseVisualStyleBackColor = false;
            // 
            // btnBeg
            // 
            this.btnBeg.BackColor = System.Drawing.Color.Silver;
            this.btnBeg.Location = new System.Drawing.Point(20, 19);
            this.btnBeg.Name = "btnBeg";
            this.btnBeg.Size = new System.Drawing.Size(77, 25);
            this.btnBeg.TabIndex = 37;
            this.btnBeg.Text = "<<";
            this.btnBeg.UseVisualStyleBackColor = false;
            // 
            // dgvTextBoxColumnFilter1
            // 
            this.dgvTextBoxColumnFilter1.Active = false;
            this.dgvTextBoxColumnFilter1.BackColor = System.Drawing.Color.Transparent;
            this.dgvTextBoxColumnFilter1.FilterApplySoon = true;
            this.dgvTextBoxColumnFilter1.FilterCaption = null;
            this.dgvTextBoxColumnFilter1.FilterExpression = "";
            this.dgvTextBoxColumnFilter1.HFilterAlignment = DgvFilterPopup.HFilterAlignment.Middle;
            this.dgvTextBoxColumnFilter1.Location = new System.Drawing.Point(1037, 392);
            this.dgvTextBoxColumnFilter1.Name = "dgvTextBoxColumnFilter1";
            this.dgvTextBoxColumnFilter1.Size = new System.Drawing.Size(8, 8);
            this.dgvTextBoxColumnFilter1.TabIndex = 59;
            this.dgvTextBoxColumnFilter1.VFilterAlignment = DgvFilterPopup.VFilterAlignment.Center;
            // 
            // gbxDataControls
            // 
            this.gbxDataControls.Controls.Add(this.btnCancel);
            this.gbxDataControls.Controls.Add(this.btnDelete);
            this.gbxDataControls.Controls.Add(this.btnSave);
            this.gbxDataControls.Controls.Add(this.btnEdit);
            this.gbxDataControls.Controls.Add(this.btnNew);
            this.gbxDataControls.Location = new System.Drawing.Point(614, 400);
            this.gbxDataControls.Name = "gbxDataControls";
            this.gbxDataControls.Size = new System.Drawing.Size(444, 60);
            this.gbxDataControls.TabIndex = 58;
            this.gbxDataControls.TabStop = false;
            this.gbxDataControls.Text = "Data Controls";
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnCancel.Location = new System.Drawing.Point(347, 19);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(77, 25);
            this.btnCancel.TabIndex = 41;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnDelete.Location = new System.Drawing.Point(264, 19);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(77, 25);
            this.btnDelete.TabIndex = 40;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnSave.Location = new System.Drawing.Point(185, 19);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(77, 25);
            this.btnSave.TabIndex = 39;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnEdit.Location = new System.Drawing.Point(103, 20);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(77, 25);
            this.btnEdit.TabIndex = 38;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = false;
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnNew.Location = new System.Drawing.Point(20, 19);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(77, 25);
            this.btnNew.TabIndex = 37;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = false;
            // 
            // gbxSearch
            // 
            this.gbxSearch.Controls.Add(this.dtpEnd_Date);
            this.gbxSearch.Controls.Add(this.cmbField);
            this.gbxSearch.Controls.Add(this.cbxField);
            this.gbxSearch.Controls.Add(this.cbxDate);
            this.gbxSearch.Controls.Add(this.cbxClient);
            this.gbxSearch.Controls.Add(this.btnSearch);
            this.gbxSearch.Controls.Add(this.dtpBeg_Date);
            this.gbxSearch.Controls.Add(this.cmbClient);
            this.gbxSearch.Font = new System.Drawing.Font("Lucida Bright", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxSearch.Location = new System.Drawing.Point(48, 330);
            this.gbxSearch.Name = "gbxSearch";
            this.gbxSearch.Size = new System.Drawing.Size(545, 204);
            this.gbxSearch.TabIndex = 57;
            this.gbxSearch.TabStop = false;
            this.gbxSearch.Text = "Search";
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnSearch.Location = new System.Drawing.Point(407, 130);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(118, 33);
            this.btnSearch.TabIndex = 40;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            // 
            // dtpBeg_Date
            // 
            this.dtpBeg_Date.Location = new System.Drawing.Point(45, 137);
            this.dtpBeg_Date.Name = "dtpBeg_Date";
            this.dtpBeg_Date.Size = new System.Drawing.Size(140, 26);
            this.dtpBeg_Date.TabIndex = 2;
            // 
            // cmbClient
            // 
            this.cmbClient.FormattingEnabled = true;
            this.cmbClient.Location = new System.Drawing.Point(25, 80);
            this.cmbClient.Name = "cmbClient";
            this.cmbClient.Size = new System.Drawing.Size(176, 28);
            this.cmbClient.TabIndex = 1;
            // 
            // dgvDisplay
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.dgvDisplay.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDisplay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDisplay.Location = new System.Drawing.Point(18, 30);
            this.dgvDisplay.Name = "dgvDisplay";
            this.dgvDisplay.Size = new System.Drawing.Size(634, 207);
            this.dgvDisplay.TabIndex = 61;
            // 
            // cbxClient
            // 
            this.cbxClient.AutoSize = true;
            this.cbxClient.Location = new System.Drawing.Point(45, 31);
            this.cbxClient.Name = "cbxClient";
            this.cbxClient.Size = new System.Drawing.Size(96, 24);
            this.cbxClient.TabIndex = 41;
            this.cbxClient.Text = "By Client";
            this.cbxClient.UseVisualStyleBackColor = true;
            // 
            // cbxDate
            // 
            this.cbxDate.AutoSize = true;
            this.cbxDate.Location = new System.Drawing.Point(226, 28);
            this.cbxDate.Name = "cbxDate";
            this.cbxDate.Size = new System.Drawing.Size(88, 24);
            this.cbxDate.TabIndex = 42;
            this.cbxDate.Text = "By Date";
            this.cbxDate.UseVisualStyleBackColor = true;
            // 
            // cbxField
            // 
            this.cbxField.AutoSize = true;
            this.cbxField.Location = new System.Drawing.Point(389, 28);
            this.cbxField.Name = "cbxField";
            this.cbxField.Size = new System.Drawing.Size(90, 24);
            this.cbxField.TabIndex = 43;
            this.cbxField.Text = "By Field";
            this.cbxField.UseVisualStyleBackColor = true;
            // 
            // cmbField
            // 
            this.cmbField.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.cmbField.FormattingEnabled = true;
            this.cmbField.Location = new System.Drawing.Point(273, 80);
            this.cmbField.Name = "cmbField";
            this.cmbField.Size = new System.Drawing.Size(176, 28);
            this.cmbField.TabIndex = 44;
            // 
            // dtpEnd_Date
            // 
            this.dtpEnd_Date.Location = new System.Drawing.Point(237, 137);
            this.dtpEnd_Date.Name = "dtpEnd_Date";
            this.dtpEnd_Date.Size = new System.Drawing.Size(140, 26);
            this.dtpEnd_Date.TabIndex = 45;
            // 
            // gbxDisplay
            // 
            this.gbxDisplay.Controls.Add(this.dgvDisplay);
            this.gbxDisplay.Location = new System.Drawing.Point(406, 81);
            this.gbxDisplay.Name = "gbxDisplay";
            this.gbxDisplay.Size = new System.Drawing.Size(668, 243);
            this.gbxDisplay.TabIndex = 62;
            this.gbxDisplay.TabStop = false;
            this.gbxDisplay.Text = "Display";
            // 
            // gbxChart
            // 
            this.gbxChart.Controls.Add(this.chart1);
            this.gbxChart.Location = new System.Drawing.Point(13, 92);
            this.gbxChart.Name = "gbxChart";
            this.gbxChart.Size = new System.Drawing.Size(336, 226);
            this.gbxChart.TabIndex = 63;
            this.gbxChart.TabStop = false;
            this.gbxChart.Text = "Chart";
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(21, 19);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(366, 190);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // Analyze
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbxChart);
            this.Controls.Add(this.gbxDisplay);
            this.Controls.Add(this.gbxNavControls);
            this.Controls.Add(this.dgvTextBoxColumnFilter1);
            this.Controls.Add(this.gbxDataControls);
            this.Controls.Add(this.gbxSearch);
            this.Controls.Add(this.pnlWorkOrder);
            this.Name = "Analyze";
            this.Size = new System.Drawing.Size(1077, 540);
            this.pnlWorkOrder.ResumeLayout(false);
            this.pnlWorkOrder.PerformLayout();
            this.gbxNavControls.ResumeLayout(false);
            this.gbxNavControls.PerformLayout();
            this.gbxDataControls.ResumeLayout(false);
            this.gbxSearch.ResumeLayout(false);
            this.gbxSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDisplay)).EndInit();
            this.gbxDisplay.ResumeLayout(false);
            this.gbxChart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlWorkOrder;
        private System.Windows.Forms.Label lblTask;
        private System.Windows.Forms.Label lblDisplay;
        private System.Windows.Forms.GroupBox gbxNavControls;
        private System.Windows.Forms.Label lblPosition;
        private System.Windows.Forms.Button btnEnd;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnBeg;
        private DgvFilterPopup.DgvTextBoxColumnFilter dgvTextBoxColumnFilter1;
        private System.Windows.Forms.GroupBox gbxDataControls;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.GroupBox gbxSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DateTimePicker dtpBeg_Date;
        private System.Windows.Forms.ComboBox cmbClient;
        private System.Windows.Forms.DataGridView dgvDisplay;
        private System.Windows.Forms.CheckBox cbxClient;
        private System.Windows.Forms.DateTimePicker dtpEnd_Date;
        private System.Windows.Forms.ComboBox cmbField;
        private System.Windows.Forms.CheckBox cbxField;
        private System.Windows.Forms.CheckBox cbxDate;
        private System.Windows.Forms.GroupBox gbxDisplay;
        private System.Windows.Forms.GroupBox gbxChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
    }
}
