namespace Sterling_Lab
{
    partial class Project
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
            this.gbxProject = new System.Windows.Forms.GroupBox();
            this.cbxIs_Reported = new System.Windows.Forms.CheckBox();
            this.gbxBill_Client = new System.Windows.Forms.GroupBox();
            this.txtBill_Cust = new System.Windows.Forms.TextBox();
            this.lblBillCustNotes = new System.Windows.Forms.Label();
            this.cbxBill_Cust = new System.Windows.Forms.CheckBox();
            this.lblDateReportExpected = new System.Windows.Forms.Label();
            this.dtpDate_Report_Expected = new System.Windows.Forms.DateTimePicker();
            this.lblProjectType = new System.Windows.Forms.Label();
            this.cmbPriority_Requested = new System.Windows.Forms.ComboBox();
            this.lblPriorityRequested = new System.Windows.Forms.Label();
            this.cmbObjective = new System.Windows.Forms.ComboBox();
            this.lblObjective = new System.Windows.Forms.Label();
            this.lblDateInitiated = new System.Windows.Forms.Label();
            this.dtpDate_Initiated = new System.Windows.Forms.DateTimePicker();
            this.lblProjectNotes = new System.Windows.Forms.Label();
            this.txtProject = new System.Windows.Forms.TextBox();
            this.cmbProject_Type = new System.Windows.Forms.ComboBox();
            this.pnlProject = new System.Windows.Forms.Panel();
            this.lblDisplay = new System.Windows.Forms.Label();
            this.gbxSearch = new System.Windows.Forms.GroupBox();
            this.txtProject_Number = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.cbxNumber = new System.Windows.Forms.CheckBox();
            this.cmbAgent = new System.Windows.Forms.ComboBox();
            this.cbxOpen = new System.Windows.Forms.CheckBox();
            this.cbxDate = new System.Windows.Forms.CheckBox();
            this.cbxClient = new System.Windows.Forms.CheckBox();
            this.lblAgent = new System.Windows.Forms.Label();
            this.lblClient = new System.Windows.Forms.Label();
            this.cmbClient = new System.Windows.Forms.ComboBox();
            this.lblEnd = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblBeg = new System.Windows.Forms.Label();
            this.dtpBegDate = new System.Windows.Forms.DateTimePicker();
            this.dgvDisplay = new System.Windows.Forms.DataGridView();
            this.gbxDisplay = new System.Windows.Forms.GroupBox();
            this.gbxDataControls = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.gbxNavControls = new System.Windows.Forms.GroupBox();
            this.lblPosition = new System.Windows.Forms.Label();
            this.btnEnd = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnBeg = new System.Windows.Forms.Button();
            this.gbxProject.SuspendLayout();
            this.gbxBill_Client.SuspendLayout();
            this.pnlProject.SuspendLayout();
            this.gbxSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDisplay)).BeginInit();
            this.gbxDisplay.SuspendLayout();
            this.gbxDataControls.SuspendLayout();
            this.gbxNavControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxProject
            // 
            this.gbxProject.Controls.Add(this.cbxIs_Reported);
            this.gbxProject.Controls.Add(this.gbxBill_Client);
            this.gbxProject.Controls.Add(this.lblDateReportExpected);
            this.gbxProject.Controls.Add(this.dtpDate_Report_Expected);
            this.gbxProject.Controls.Add(this.lblProjectType);
            this.gbxProject.Controls.Add(this.cmbPriority_Requested);
            this.gbxProject.Controls.Add(this.lblPriorityRequested);
            this.gbxProject.Controls.Add(this.cmbObjective);
            this.gbxProject.Controls.Add(this.lblObjective);
            this.gbxProject.Controls.Add(this.lblDateInitiated);
            this.gbxProject.Controls.Add(this.dtpDate_Initiated);
            this.gbxProject.Controls.Add(this.lblProjectNotes);
            this.gbxProject.Controls.Add(this.txtProject);
            this.gbxProject.Controls.Add(this.cmbProject_Type);
            this.gbxProject.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxProject.Location = new System.Drawing.Point(9, 316);
            this.gbxProject.Name = "gbxProject";
            this.gbxProject.Size = new System.Drawing.Size(604, 305);
            this.gbxProject.TabIndex = 1;
            this.gbxProject.TabStop = false;
            this.gbxProject.Text = "Project Details";
            // 
            // cbxIs_Reported
            // 
            this.cbxIs_Reported.AutoSize = true;
            this.cbxIs_Reported.Location = new System.Drawing.Point(20, 184);
            this.cbxIs_Reported.Name = "cbxIs_Reported";
            this.cbxIs_Reported.Size = new System.Drawing.Size(125, 24);
            this.cbxIs_Reported.TabIndex = 10;
            this.cbxIs_Reported.Text = "Is Reported ?";
            this.cbxIs_Reported.UseVisualStyleBackColor = true;
            // 
            // gbxBill_Client
            // 
            this.gbxBill_Client.Controls.Add(this.txtBill_Cust);
            this.gbxBill_Client.Controls.Add(this.lblBillCustNotes);
            this.gbxBill_Client.Controls.Add(this.cbxBill_Cust);
            this.gbxBill_Client.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxBill_Client.Location = new System.Drawing.Point(13, 226);
            this.gbxBill_Client.Name = "gbxBill_Client";
            this.gbxBill_Client.Size = new System.Drawing.Size(572, 73);
            this.gbxBill_Client.TabIndex = 13;
            this.gbxBill_Client.TabStop = false;
            this.gbxBill_Client.Text = "Billing";
            // 
            // txtBill_Cust
            // 
            this.txtBill_Cust.Location = new System.Drawing.Point(235, 29);
            this.txtBill_Cust.Name = "txtBill_Cust";
            this.txtBill_Cust.Size = new System.Drawing.Size(312, 26);
            this.txtBill_Cust.TabIndex = 2;
            this.txtBill_Cust.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Project_KeyPress);
            this.txtBill_Cust.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Project_KeyUp);
            // 
            // lblBillCustNotes
            // 
            this.lblBillCustNotes.AutoSize = true;
            this.lblBillCustNotes.Location = new System.Drawing.Point(178, 35);
            this.lblBillCustNotes.Name = "lblBillCustNotes";
            this.lblBillCustNotes.Size = new System.Drawing.Size(51, 20);
            this.lblBillCustNotes.TabIndex = 0;
            this.lblBillCustNotes.Text = "Notes";
            // 
            // cbxBill_Cust
            // 
            this.cbxBill_Cust.AutoSize = true;
            this.cbxBill_Cust.Location = new System.Drawing.Point(21, 29);
            this.cbxBill_Cust.Name = "cbxBill_Cust";
            this.cbxBill_Cust.Size = new System.Drawing.Size(105, 24);
            this.cbxBill_Cust.TabIndex = 1;
            this.cbxBill_Cust.Text = "Bill Client ?";
            this.cbxBill_Cust.UseVisualStyleBackColor = true;
            // 
            // lblDateReportExpected
            // 
            this.lblDateReportExpected.AutoSize = true;
            this.lblDateReportExpected.Location = new System.Drawing.Point(28, 146);
            this.lblDateReportExpected.Name = "lblDateReportExpected";
            this.lblDateReportExpected.Size = new System.Drawing.Size(88, 20);
            this.lblDateReportExpected.TabIndex = 6;
            this.lblDateReportExpected.Text = "ReportDue";
            // 
            // dtpDate_Report_Expected
            // 
            this.dtpDate_Report_Expected.CustomFormat = "yyyy-MMM-dd HH:mm";
            this.dtpDate_Report_Expected.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate_Report_Expected.Location = new System.Drawing.Point(136, 140);
            this.dtpDate_Report_Expected.Name = "dtpDate_Report_Expected";
            this.dtpDate_Report_Expected.Size = new System.Drawing.Size(175, 26);
            this.dtpDate_Report_Expected.TabIndex = 7;
            this.dtpDate_Report_Expected.ValueChanged += new System.EventHandler(this.DatePicker_ValueChanged);
            // 
            // lblProjectType
            // 
            this.lblProjectType.AutoSize = true;
            this.lblProjectType.Location = new System.Drawing.Point(337, 90);
            this.lblProjectType.Name = "lblProjectType";
            this.lblProjectType.Size = new System.Drawing.Size(43, 20);
            this.lblProjectType.TabIndex = 4;
            this.lblProjectType.Text = "Type";
            // 
            // cmbPriority_Requested
            // 
            this.cmbPriority_Requested.FormattingEnabled = true;
            this.cmbPriority_Requested.Location = new System.Drawing.Point(392, 137);
            this.cmbPriority_Requested.Name = "cmbPriority_Requested";
            this.cmbPriority_Requested.Size = new System.Drawing.Size(170, 28);
            this.cmbPriority_Requested.TabIndex = 9;
            this.cmbPriority_Requested.SelectedIndexChanged += new System.EventHandler(this.Combo_SelectedIndexChanged);
            this.cmbPriority_Requested.Validating += new System.ComponentModel.CancelEventHandler(this.Combo_Validating);
            // 
            // lblPriorityRequested
            // 
            this.lblPriorityRequested.AutoSize = true;
            this.lblPriorityRequested.Location = new System.Drawing.Point(330, 145);
            this.lblPriorityRequested.Name = "lblPriorityRequested";
            this.lblPriorityRequested.Size = new System.Drawing.Size(56, 20);
            this.lblPriorityRequested.TabIndex = 8;
            this.lblPriorityRequested.Text = "Priority";
            // 
            // cmbObjective
            // 
            this.cmbObjective.FormattingEnabled = true;
            this.cmbObjective.Location = new System.Drawing.Point(93, 84);
            this.cmbObjective.Name = "cmbObjective";
            this.cmbObjective.Size = new System.Drawing.Size(229, 28);
            this.cmbObjective.TabIndex = 3;
            this.cmbObjective.SelectedIndexChanged += new System.EventHandler(this.Combo_SelectedIndexChanged);
            this.cmbObjective.Validating += new System.ComponentModel.CancelEventHandler(this.Combo_Validating);
            // 
            // lblObjective
            // 
            this.lblObjective.AutoSize = true;
            this.lblObjective.Location = new System.Drawing.Point(13, 92);
            this.lblObjective.Name = "lblObjective";
            this.lblObjective.Size = new System.Drawing.Size(74, 20);
            this.lblObjective.TabIndex = 2;
            this.lblObjective.Text = "Objective";
            // 
            // lblDateInitiated
            // 
            this.lblDateInitiated.AutoSize = true;
            this.lblDateInitiated.Location = new System.Drawing.Point(9, 38);
            this.lblDateInitiated.Name = "lblDateInitiated";
            this.lblDateInitiated.Size = new System.Drawing.Size(105, 20);
            this.lblDateInitiated.TabIndex = 0;
            this.lblDateInitiated.Text = "Date Initiated";
            // 
            // dtpDate_Initiated
            // 
            this.dtpDate_Initiated.CustomFormat = "yyyy-MMM-dd HH:mm";
            this.dtpDate_Initiated.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate_Initiated.Location = new System.Drawing.Point(136, 33);
            this.dtpDate_Initiated.Name = "dtpDate_Initiated";
            this.dtpDate_Initiated.Size = new System.Drawing.Size(186, 26);
            this.dtpDate_Initiated.TabIndex = 1;
            this.dtpDate_Initiated.ValueChanged += new System.EventHandler(this.DatePicker_ValueChanged);
            // 
            // lblProjectNotes
            // 
            this.lblProjectNotes.AutoSize = true;
            this.lblProjectNotes.Location = new System.Drawing.Point(153, 188);
            this.lblProjectNotes.Name = "lblProjectNotes";
            this.lblProjectNotes.Size = new System.Drawing.Size(51, 20);
            this.lblProjectNotes.TabIndex = 11;
            this.lblProjectNotes.Text = "Notes";
            // 
            // txtProject
            // 
            this.txtProject.Location = new System.Drawing.Point(214, 182);
            this.txtProject.Name = "txtProject";
            this.txtProject.Size = new System.Drawing.Size(344, 26);
            this.txtProject.TabIndex = 12;
            this.txtProject.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Project_KeyPress);
            this.txtProject.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Project_KeyUp);
            // 
            // cmbProject_Type
            // 
            this.cmbProject_Type.FormattingEnabled = true;
            this.cmbProject_Type.Location = new System.Drawing.Point(389, 84);
            this.cmbProject_Type.Name = "cmbProject_Type";
            this.cmbProject_Type.Size = new System.Drawing.Size(173, 28);
            this.cmbProject_Type.TabIndex = 5;
            this.cmbProject_Type.SelectedIndexChanged += new System.EventHandler(this.Combo_SelectedIndexChanged);
            this.cmbProject_Type.Validating += new System.ComponentModel.CancelEventHandler(this.Combo_Validating);
            // 
            // pnlProject
            // 
            this.pnlProject.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.pnlProject.Controls.Add(this.lblDisplay);
            this.pnlProject.Location = new System.Drawing.Point(0, 0);
            this.pnlProject.Name = "pnlProject";
            this.pnlProject.Size = new System.Drawing.Size(1133, 79);
            this.pnlProject.TabIndex = 0;
            // 
            // lblDisplay
            // 
            this.lblDisplay.AutoSize = true;
            this.lblDisplay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lblDisplay.Font = new System.Drawing.Font("Lucida Bright", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDisplay.ForeColor = System.Drawing.Color.Black;
            this.lblDisplay.Location = new System.Drawing.Point(38, 31);
            this.lblDisplay.Name = "lblDisplay";
            this.lblDisplay.Size = new System.Drawing.Size(116, 33);
            this.lblDisplay.TabIndex = 0;
            this.lblDisplay.Text = "Project";
            // 
            // gbxSearch
            // 
            this.gbxSearch.Controls.Add(this.txtProject_Number);
            this.gbxSearch.Controls.Add(this.btnSearch);
            this.gbxSearch.Controls.Add(this.cbxNumber);
            this.gbxSearch.Controls.Add(this.cmbAgent);
            this.gbxSearch.Controls.Add(this.cbxOpen);
            this.gbxSearch.Controls.Add(this.cbxDate);
            this.gbxSearch.Controls.Add(this.cbxClient);
            this.gbxSearch.Controls.Add(this.lblAgent);
            this.gbxSearch.Controls.Add(this.lblClient);
            this.gbxSearch.Controls.Add(this.cmbClient);
            this.gbxSearch.Controls.Add(this.lblEnd);
            this.gbxSearch.Controls.Add(this.dtpEndDate);
            this.gbxSearch.Controls.Add(this.lblBeg);
            this.gbxSearch.Controls.Add(this.dtpBegDate);
            this.gbxSearch.Font = new System.Drawing.Font("Lucida Bright", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxSearch.Location = new System.Drawing.Point(3, 85);
            this.gbxSearch.Name = "gbxSearch";
            this.gbxSearch.Size = new System.Drawing.Size(601, 207);
            this.gbxSearch.TabIndex = 0;
            this.gbxSearch.TabStop = false;
            this.gbxSearch.Text = "Search";
            // 
            // txtProject_Number
            // 
            this.txtProject_Number.Location = new System.Drawing.Point(66, 161);
            this.txtProject_Number.Name = "txtProject_Number";
            this.txtProject_Number.Size = new System.Drawing.Size(205, 26);
            this.txtProject_Number.TabIndex = 43;
            this.txtProject_Number.Enter += new System.EventHandler(this.txtProject_Number_Enter);
            this.txtProject_Number.Leave += new System.EventHandler(this.txtProject_Number_Leave);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnSearch.Location = new System.Drawing.Point(391, 161);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(200, 27);
            this.btnSearch.TabIndex = 42;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cbxNumber
            // 
            this.cbxNumber.AutoSize = true;
            this.cbxNumber.Location = new System.Drawing.Point(466, 29);
            this.cbxNumber.Name = "cbxNumber";
            this.cbxNumber.Size = new System.Drawing.Size(111, 24);
            this.cbxNumber.TabIndex = 41;
            this.cbxNumber.Text = "By Number";
            this.cbxNumber.UseVisualStyleBackColor = true;
            this.cbxNumber.CheckStateChanged += new System.EventHandler(this.CheckBox_Click);
            // 
            // cmbAgent
            // 
            this.cmbAgent.FormattingEnabled = true;
            this.cmbAgent.Location = new System.Drawing.Point(350, 70);
            this.cmbAgent.Name = "cmbAgent";
            this.cmbAgent.Size = new System.Drawing.Size(241, 28);
            this.cmbAgent.TabIndex = 5;
            this.cmbAgent.SelectedIndexChanged += new System.EventHandler(this.Combo_SelectedIndexChanged);
            // 
            // cbxOpen
            // 
            this.cbxOpen.AutoSize = true;
            this.cbxOpen.Location = new System.Drawing.Point(294, 29);
            this.cbxOpen.Name = "cbxOpen";
            this.cbxOpen.Size = new System.Drawing.Size(152, 24);
            this.cbxOpen.TabIndex = 4;
            this.cbxOpen.Text = "By Open Project";
            this.cbxOpen.UseVisualStyleBackColor = true;
            this.cbxOpen.CheckStateChanged += new System.EventHandler(this.CheckBox_Click);
            // 
            // cbxDate
            // 
            this.cbxDate.AutoSize = true;
            this.cbxDate.Location = new System.Drawing.Point(163, 29);
            this.cbxDate.Name = "cbxDate";
            this.cbxDate.Size = new System.Drawing.Size(88, 24);
            this.cbxDate.TabIndex = 2;
            this.cbxDate.Text = "By Date";
            this.cbxDate.UseVisualStyleBackColor = true;
            this.cbxDate.CheckStateChanged += new System.EventHandler(this.CheckBox_Click);
            // 
            // cbxClient
            // 
            this.cbxClient.AutoSize = true;
            this.cbxClient.Location = new System.Drawing.Point(38, 29);
            this.cbxClient.Name = "cbxClient";
            this.cbxClient.Size = new System.Drawing.Size(96, 24);
            this.cbxClient.TabIndex = 2;
            this.cbxClient.Text = "By Client";
            this.cbxClient.UseVisualStyleBackColor = true;
            this.cbxClient.CheckStateChanged += new System.EventHandler(this.CheckBox_Click);
            // 
            // lblAgent
            // 
            this.lblAgent.AutoSize = true;
            this.lblAgent.Location = new System.Drawing.Point(290, 73);
            this.lblAgent.Name = "lblAgent";
            this.lblAgent.Size = new System.Drawing.Size(54, 20);
            this.lblAgent.TabIndex = 40;
            this.lblAgent.Text = "Agent";
            // 
            // lblClient
            // 
            this.lblClient.AutoSize = true;
            this.lblClient.Location = new System.Drawing.Point(6, 78);
            this.lblClient.Name = "lblClient";
            this.lblClient.Size = new System.Drawing.Size(54, 20);
            this.lblClient.TabIndex = 36;
            this.lblClient.Text = "Client";
            // 
            // cmbClient
            // 
            this.cmbClient.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbClient.FormattingEnabled = true;
            this.cmbClient.Location = new System.Drawing.Point(66, 70);
            this.cmbClient.Name = "cmbClient";
            this.cmbClient.Size = new System.Drawing.Size(205, 28);
            this.cmbClient.TabIndex = 0;
            this.cmbClient.SelectedIndexChanged += new System.EventHandler(this.Combo_SelectedIndexChanged);
            // 
            // lblEnd
            // 
            this.lblEnd.AutoSize = true;
            this.lblEnd.Location = new System.Drawing.Point(354, 116);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(38, 20);
            this.lblEnd.TabIndex = 34;
            this.lblEnd.Text = "End";
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.CustomFormat = "yyyy-mm-dd hh:mm:ss";
            this.dtpEndDate.Location = new System.Drawing.Point(409, 111);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(182, 26);
            this.dtpEndDate.TabIndex = 7;
            this.dtpEndDate.ValueChanged += new System.EventHandler(this.DatePicker_ValueChanged);
            // 
            // lblBeg
            // 
            this.lblBeg.AutoSize = true;
            this.lblBeg.Location = new System.Drawing.Point(15, 115);
            this.lblBeg.Name = "lblBeg";
            this.lblBeg.Size = new System.Drawing.Size(51, 20);
            this.lblBeg.TabIndex = 32;
            this.lblBeg.Text = "Begin";
            // 
            // dtpBegDate
            // 
            this.dtpBegDate.CustomFormat = "yyyy-mm-dd hh:mm:ss";
            this.dtpBegDate.Location = new System.Drawing.Point(72, 111);
            this.dtpBegDate.Name = "dtpBegDate";
            this.dtpBegDate.Size = new System.Drawing.Size(199, 26);
            this.dtpBegDate.TabIndex = 6;
            this.dtpBegDate.ValueChanged += new System.EventHandler(this.DatePicker_ValueChanged);
            // 
            // dgvDisplay
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dgvDisplay.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDisplay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDisplay.Location = new System.Drawing.Point(6, 19);
            this.dgvDisplay.Name = "dgvDisplay";
            this.dgvDisplay.Size = new System.Drawing.Size(494, 231);
            this.dgvDisplay.TabIndex = 26;
            this.dgvDisplay.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDisplay_CellContentClick);
            this.dgvDisplay.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDisplay_RowEnter);
            // 
            // gbxDisplay
            // 
            this.gbxDisplay.Controls.Add(this.dgvDisplay);
            this.gbxDisplay.Location = new System.Drawing.Point(613, 95);
            this.gbxDisplay.Name = "gbxDisplay";
            this.gbxDisplay.Size = new System.Drawing.Size(506, 277);
            this.gbxDisplay.TabIndex = 4;
            this.gbxDisplay.TabStop = false;
            this.gbxDisplay.Text = "Results";
            // 
            // gbxDataControls
            // 
            this.gbxDataControls.Controls.Add(this.btnCancel);
            this.gbxDataControls.Controls.Add(this.btnDelete);
            this.gbxDataControls.Controls.Add(this.btnSave);
            this.gbxDataControls.Controls.Add(this.btnEdit);
            this.gbxDataControls.Controls.Add(this.btnNew);
            this.gbxDataControls.Font = new System.Drawing.Font("Lucida Bright", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxDataControls.Location = new System.Drawing.Point(631, 469);
            this.gbxDataControls.Name = "gbxDataControls";
            this.gbxDataControls.Size = new System.Drawing.Size(452, 65);
            this.gbxDataControls.TabIndex = 3;
            this.gbxDataControls.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Silver;
            this.btnCancel.Location = new System.Drawing.Point(345, 22);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 30);
            this.btnCancel.TabIndex = 25;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.Silver;
            this.btnDelete.Location = new System.Drawing.Point(267, 22);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 30);
            this.btnDelete.TabIndex = 24;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Silver;
            this.btnSave.Location = new System.Drawing.Point(188, 22);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 30);
            this.btnSave.TabIndex = 23;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.Silver;
            this.btnEdit.Location = new System.Drawing.Point(106, 23);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(80, 30);
            this.btnEdit.TabIndex = 22;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.Color.Silver;
            this.btnNew.Location = new System.Drawing.Point(23, 22);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(80, 30);
            this.btnNew.TabIndex = 21;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // gbxNavControls
            // 
            this.gbxNavControls.Controls.Add(this.lblPosition);
            this.gbxNavControls.Controls.Add(this.btnEnd);
            this.gbxNavControls.Controls.Add(this.btnNext);
            this.gbxNavControls.Controls.Add(this.btnPrev);
            this.gbxNavControls.Controls.Add(this.btnBeg);
            this.gbxNavControls.Location = new System.Drawing.Point(631, 390);
            this.gbxNavControls.Name = "gbxNavControls";
            this.gbxNavControls.Size = new System.Drawing.Size(452, 56);
            this.gbxNavControls.TabIndex = 2;
            this.gbxNavControls.TabStop = false;
            this.gbxNavControls.Text = "Scroll";
            // 
            // lblPosition
            // 
            this.lblPosition.AutoSize = true;
            this.lblPosition.Location = new System.Drawing.Point(196, 25);
            this.lblPosition.Name = "lblPosition";
            this.lblPosition.Size = new System.Drawing.Size(14, 13);
            this.lblPosition.TabIndex = 21;
            this.lblPosition.Text = "#";
            // 
            // btnEnd
            // 
            this.btnEnd.BackColor = System.Drawing.Color.Silver;
            this.btnEnd.Location = new System.Drawing.Point(347, 19);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Size = new System.Drawing.Size(77, 25);
            this.btnEnd.TabIndex = 19;
            this.btnEnd.Text = ">>";
            this.btnEnd.UseVisualStyleBackColor = false;
            this.btnEnd.Click += new System.EventHandler(this.btnEnd_Click);
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.Color.Silver;
            this.btnNext.Location = new System.Drawing.Point(264, 19);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(77, 25);
            this.btnNext.TabIndex = 18;
            this.btnNext.Text = ">";
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.BackColor = System.Drawing.Color.Silver;
            this.btnPrev.Location = new System.Drawing.Point(103, 20);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(77, 25);
            this.btnPrev.TabIndex = 17;
            this.btnPrev.Text = "<";
            this.btnPrev.UseVisualStyleBackColor = false;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnBeg
            // 
            this.btnBeg.BackColor = System.Drawing.Color.Silver;
            this.btnBeg.Location = new System.Drawing.Point(20, 19);
            this.btnBeg.Name = "btnBeg";
            this.btnBeg.Size = new System.Drawing.Size(77, 25);
            this.btnBeg.TabIndex = 16;
            this.btnBeg.Text = "<<";
            this.btnBeg.UseVisualStyleBackColor = false;
            this.btnBeg.Click += new System.EventHandler(this.btnBeg_Click);
            // 
            // Project
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbxNavControls);
            this.Controls.Add(this.gbxDataControls);
            this.Controls.Add(this.gbxSearch);
            this.Controls.Add(this.gbxDisplay);
            this.Controls.Add(this.pnlProject);
            this.Controls.Add(this.gbxProject);
            this.Name = "Project";
            this.Size = new System.Drawing.Size(1133, 639);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Project_KeyPress);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Project_KeyUp);
            this.gbxProject.ResumeLayout(false);
            this.gbxProject.PerformLayout();
            this.gbxBill_Client.ResumeLayout(false);
            this.gbxBill_Client.PerformLayout();
            this.pnlProject.ResumeLayout(false);
            this.pnlProject.PerformLayout();
            this.gbxSearch.ResumeLayout(false);
            this.gbxSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDisplay)).EndInit();
            this.gbxDisplay.ResumeLayout(false);
            this.gbxDataControls.ResumeLayout(false);
            this.gbxNavControls.ResumeLayout(false);
            this.gbxNavControls.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxProject;
        private System.Windows.Forms.Label lblDateReportExpected;
        private System.Windows.Forms.DateTimePicker dtpDate_Report_Expected;
        private System.Windows.Forms.Label lblProjectType;
        private System.Windows.Forms.ComboBox cmbPriority_Requested;
        private System.Windows.Forms.Label lblPriorityRequested;
        private System.Windows.Forms.ComboBox cmbObjective;
        private System.Windows.Forms.Label lblObjective;
        private System.Windows.Forms.Label lblDateInitiated;
        private System.Windows.Forms.DateTimePicker dtpDate_Initiated;
        private System.Windows.Forms.Label lblProjectNotes;
        private System.Windows.Forms.TextBox txtProject;
        private System.Windows.Forms.ComboBox cmbProject_Type;
        private System.Windows.Forms.GroupBox gbxBill_Client;
        private System.Windows.Forms.TextBox txtBill_Cust;
        private System.Windows.Forms.Label lblBillCustNotes;
        private System.Windows.Forms.CheckBox cbxBill_Cust;
        private System.Windows.Forms.Panel pnlProject;
        private System.Windows.Forms.Label lblDisplay;
        private System.Windows.Forms.GroupBox gbxSearch;
        private System.Windows.Forms.DateTimePicker dtpBegDate;
        private System.Windows.Forms.Label lblEnd;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label lblBeg;
        private System.Windows.Forms.Label lblClient;
        private System.Windows.Forms.ComboBox cmbClient;
        private System.Windows.Forms.Label lblAgent;

        private System.Windows.Forms.DataGridView dgvDisplay;
        private System.Windows.Forms.GroupBox gbxDisplay;
        private System.Windows.Forms.GroupBox gbxDataControls;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.GroupBox gbxNavControls;
        private System.Windows.Forms.Button btnEnd;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnBeg;
        private System.Windows.Forms.CheckBox cbxOpen;
        private System.Windows.Forms.CheckBox cbxDate;
        private System.Windows.Forms.CheckBox cbxClient;
        private System.Windows.Forms.CheckBox cbxIs_Reported;
        private System.Windows.Forms.ComboBox cmbAgent;
        private System.Windows.Forms.Label lblPosition;
        private System.Windows.Forms.CheckBox cbxNumber;
        private System.Windows.Forms.TextBox txtProject_Number;
        private System.Windows.Forms.Button btnSearch;
    }
}
