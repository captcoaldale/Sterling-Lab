﻿namespace Sterling_Lab
{
    partial class Sample
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
            this.lblDisplay = new System.Windows.Forms.Label();
            this.gbxDataControls = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.cbxSampleIsOpen = new System.Windows.Forms.CheckBox();
            this.pnlDisplay = new System.Windows.Forms.Panel();
            this.lblTask = new System.Windows.Forms.Label();
            this.cbxSampleInOffice = new System.Windows.Forms.CheckBox();
            this.mskDLS = new System.Windows.Forms.MaskedTextBox();
            this.txtSampleNotes = new System.Windows.Forms.TextBox();
            this.lblSampleNotes = new System.Windows.Forms.Label();
            this.gbxSample = new System.Windows.Forms.GroupBox();
            this.lblType = new System.Windows.Forms.Label();
            this.cmbSample_Type = new System.Windows.Forms.ComboBox();
            this.lblPrice = new System.Windows.Forms.Label();
            this.cmbPrice = new System.Windows.Forms.ComboBox();
            this.btnGenerateProjectNumbers = new System.Windows.Forms.Button();
            this.dtpCollected = new System.Windows.Forms.DateTimePicker();
            this.lblCollected = new System.Windows.Forms.Label();
            this.lblTest = new System.Windows.Forms.Label();
            this.cmbMethod = new System.Windows.Forms.ComboBox();
            this.lblField = new System.Windows.Forms.Label();
            this.cmbField = new System.Windows.Forms.ComboBox();
            this.lblDLS = new System.Windows.Forms.Label();
            this.lblZone = new System.Windows.Forms.Label();
            this.cmbZone = new System.Windows.Forms.ComboBox();
            this.lblSite = new System.Windows.Forms.Label();
            this.cmbSite = new System.Windows.Forms.ComboBox();
            this.gbxSearch = new System.Windows.Forms.GroupBox();
            this.cbxItem = new System.Windows.Forms.CheckBox();
            this.cbxDate = new System.Windows.Forms.CheckBox();
            this.cbxSample = new System.Windows.Forms.CheckBox();
            this.cbxProject = new System.Windows.Forms.CheckBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblEnd = new System.Windows.Forms.Label();
            this.dtpEnd_Date = new System.Windows.Forms.DateTimePicker();
            this.lblBeg = new System.Windows.Forms.Label();
            this.dtpBeg_Date = new System.Windows.Forms.DateTimePicker();
            this.gbxNavControls = new System.Windows.Forms.GroupBox();
            this.lblPosition = new System.Windows.Forms.Label();
            this.btnEnd = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnBeg = new System.Windows.Forms.Button();
            this.gbxDisplay = new System.Windows.Forms.GroupBox();
            this.dgvDisplay = new System.Windows.Forms.DataGridView();
            this.dgvTextBoxColumnFilter1 = new DgvFilterPopup.DgvTextBoxColumnFilter();
            this.gbxDataControls.SuspendLayout();
            this.pnlDisplay.SuspendLayout();
            this.gbxSample.SuspendLayout();
            this.gbxSearch.SuspendLayout();
            this.gbxNavControls.SuspendLayout();
            this.gbxDisplay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDisplay)).BeginInit();
            this.SuspendLayout();
            // 
            // lblDisplay
            // 
            this.lblDisplay.AutoSize = true;
            this.lblDisplay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lblDisplay.Font = new System.Drawing.Font("Lucida Bright", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDisplay.ForeColor = System.Drawing.Color.Black;
            this.lblDisplay.Location = new System.Drawing.Point(41, 24);
            this.lblDisplay.Name = "lblDisplay";
            this.lblDisplay.Size = new System.Drawing.Size(118, 33);
            this.lblDisplay.TabIndex = 0;
            this.lblDisplay.Text = "Sample";
            // 
            // gbxDataControls
            // 
            this.gbxDataControls.Controls.Add(this.btnCancel);
            this.gbxDataControls.Controls.Add(this.btnDelete);
            this.gbxDataControls.Controls.Add(this.btnSave);
            this.gbxDataControls.Controls.Add(this.btnEdit);
            this.gbxDataControls.Controls.Add(this.btnNew);
            this.gbxDataControls.Location = new System.Drawing.Point(608, 579);
            this.gbxDataControls.Name = "gbxDataControls";
            this.gbxDataControls.Size = new System.Drawing.Size(444, 60);
            this.gbxDataControls.TabIndex = 42;
            this.gbxDataControls.TabStop = false;
            this.gbxDataControls.Text = "Data Controls";
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Silver;
            this.btnCancel.Location = new System.Drawing.Point(347, 19);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(77, 25);
            this.btnCancel.TabIndex = 41;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.Silver;
            this.btnDelete.Location = new System.Drawing.Point(264, 19);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(77, 25);
            this.btnDelete.TabIndex = 40;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Silver;
            this.btnSave.Location = new System.Drawing.Point(185, 19);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(77, 25);
            this.btnSave.TabIndex = 39;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.Silver;
            this.btnEdit.Location = new System.Drawing.Point(103, 20);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(77, 25);
            this.btnEdit.TabIndex = 38;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.Color.Silver;
            this.btnNew.Location = new System.Drawing.Point(20, 19);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(77, 25);
            this.btnNew.TabIndex = 37;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // cbxSampleIsOpen
            // 
            this.cbxSampleIsOpen.AutoSize = true;
            this.cbxSampleIsOpen.Location = new System.Drawing.Point(237, 42);
            this.cbxSampleIsOpen.Name = "cbxSampleIsOpen";
            this.cbxSampleIsOpen.Size = new System.Drawing.Size(153, 21);
            this.cbxSampleIsOpen.TabIndex = 30;
            this.cbxSampleIsOpen.Text = "Sample Is Open ?";
            this.cbxSampleIsOpen.UseVisualStyleBackColor = true;
            this.cbxSampleIsOpen.Click += new System.EventHandler(this.CheckBox_Click);
            // 
            // pnlDisplay
            // 
            this.pnlDisplay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pnlDisplay.Controls.Add(this.lblTask);
            this.pnlDisplay.Controls.Add(this.lblDisplay);
            this.pnlDisplay.Location = new System.Drawing.Point(0, 0);
            this.pnlDisplay.Name = "pnlDisplay";
            this.pnlDisplay.Size = new System.Drawing.Size(1150, 76);
            this.pnlDisplay.TabIndex = 27;
            // 
            // lblTask
            // 
            this.lblTask.AutoSize = true;
            this.lblTask.Font = new System.Drawing.Font("Lucida Bright", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTask.Location = new System.Drawing.Point(712, 24);
            this.lblTask.Name = "lblTask";
            this.lblTask.Size = new System.Drawing.Size(57, 22);
            this.lblTask.TabIndex = 1;
            this.lblTask.Text = "Task";
            // 
            // cbxSampleInOffice
            // 
            this.cbxSampleInOffice.AutoSize = true;
            this.cbxSampleInOffice.Location = new System.Drawing.Point(18, 42);
            this.cbxSampleInOffice.Name = "cbxSampleInOffice";
            this.cbxSampleInOffice.Size = new System.Drawing.Size(161, 21);
            this.cbxSampleInOffice.TabIndex = 21;
            this.cbxSampleInOffice.Text = "Sample In Office ?";
            this.cbxSampleInOffice.UseVisualStyleBackColor = true;
            this.cbxSampleInOffice.Click += new System.EventHandler(this.CheckBox_Click);
            // 
            // mskDLS
            // 
            this.mskDLS.Location = new System.Drawing.Point(270, 100);
            this.mskDLS.Mask = ">LL 00-00-00-\\W0";
            this.mskDLS.Name = "mskDLS";
            this.mskDLS.Size = new System.Drawing.Size(120, 25);
            this.mskDLS.TabIndex = 20;
            this.mskDLS.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals;
            this.mskDLS.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Sample_KeyPress);
            this.mskDLS.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Sample_KeyUp);
            // 
            // txtSampleNotes
            // 
            this.txtSampleNotes.Location = new System.Drawing.Point(72, 188);
            this.txtSampleNotes.Name = "txtSampleNotes";
            this.txtSampleNotes.Size = new System.Drawing.Size(407, 25);
            this.txtSampleNotes.TabIndex = 22;
            this.txtSampleNotes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Sample_KeyPress);
            this.txtSampleNotes.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Sample_KeyUp);
            // 
            // lblSampleNotes
            // 
            this.lblSampleNotes.AutoSize = true;
            this.lblSampleNotes.Location = new System.Drawing.Point(13, 196);
            this.lblSampleNotes.Name = "lblSampleNotes";
            this.lblSampleNotes.Size = new System.Drawing.Size(53, 17);
            this.lblSampleNotes.TabIndex = 22;
            this.lblSampleNotes.Text = "Notes";
            // 
            // gbxSample
            // 
            this.gbxSample.Controls.Add(this.lblType);
            this.gbxSample.Controls.Add(this.cmbSample_Type);
            this.gbxSample.Controls.Add(this.lblPrice);
            this.gbxSample.Controls.Add(this.cmbPrice);
            this.gbxSample.Controls.Add(this.btnGenerateProjectNumbers);
            this.gbxSample.Controls.Add(this.dtpCollected);
            this.gbxSample.Controls.Add(this.lblCollected);
            this.gbxSample.Controls.Add(this.lblTest);
            this.gbxSample.Controls.Add(this.cmbMethod);
            this.gbxSample.Controls.Add(this.cbxSampleIsOpen);
            this.gbxSample.Controls.Add(this.cbxSampleInOffice);
            this.gbxSample.Controls.Add(this.mskDLS);
            this.gbxSample.Controls.Add(this.txtSampleNotes);
            this.gbxSample.Controls.Add(this.lblSampleNotes);
            this.gbxSample.Controls.Add(this.lblField);
            this.gbxSample.Controls.Add(this.cmbField);
            this.gbxSample.Controls.Add(this.lblDLS);
            this.gbxSample.Controls.Add(this.lblZone);
            this.gbxSample.Controls.Add(this.cmbZone);
            this.gbxSample.Controls.Add(this.lblSite);
            this.gbxSample.Controls.Add(this.cmbSite);
            this.gbxSample.Font = new System.Drawing.Font("Lucida Bright", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxSample.Location = new System.Drawing.Point(3, 312);
            this.gbxSample.Name = "gbxSample";
            this.gbxSample.Size = new System.Drawing.Size(494, 319);
            this.gbxSample.TabIndex = 26;
            this.gbxSample.TabStop = false;
            this.gbxSample.Text = "Sample";
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(251, 241);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(44, 17);
            this.lblType.TabIndex = 60;
            this.lblType.Text = "Type";
            // 
            // cmbSample_Type
            // 
            this.cmbSample_Type.FormattingEnabled = true;
            this.cmbSample_Type.ItemHeight = 17;
            this.cmbSample_Type.Location = new System.Drawing.Point(308, 236);
            this.cmbSample_Type.Name = "cmbSample_Type";
            this.cmbSample_Type.Size = new System.Drawing.Size(126, 25);
            this.cmbSample_Type.TabIndex = 61;
            this.cmbSample_Type.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Sample_KeyPress);
            this.cmbSample_Type.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Sample_KeyUp);
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Location = new System.Drawing.Point(23, 244);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(17, 17);
            this.lblPrice.TabIndex = 58;
            this.lblPrice.Text = "$";
            // 
            // cmbPrice
            // 
            this.cmbPrice.FormattingEnabled = true;
            this.cmbPrice.ItemHeight = 17;
            this.cmbPrice.Location = new System.Drawing.Point(80, 239);
            this.cmbPrice.Name = "cmbPrice";
            this.cmbPrice.Size = new System.Drawing.Size(126, 25);
            this.cmbPrice.TabIndex = 59;
            this.cmbPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Sample_KeyPress);
            this.cmbPrice.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Sample_KeyUp);
            // 
            // btnGenerateProjectNumbers
            // 
            this.btnGenerateProjectNumbers.Location = new System.Drawing.Point(121, 279);
            this.btnGenerateProjectNumbers.Name = "btnGenerateProjectNumbers";
            this.btnGenerateProjectNumbers.Size = new System.Drawing.Size(240, 23);
            this.btnGenerateProjectNumbers.TabIndex = 55;
            this.btnGenerateProjectNumbers.Text = "Generate Project Number";
            this.btnGenerateProjectNumbers.UseVisualStyleBackColor = true;
            this.btnGenerateProjectNumbers.Click += new System.EventHandler(this.btnGenerateProjectNumbers_Click);
            // 
            // dtpCollected
            // 
            this.dtpCollected.Location = new System.Drawing.Point(336, 144);
            this.dtpCollected.Name = "dtpCollected";
            this.dtpCollected.Size = new System.Drawing.Size(140, 25);
            this.dtpCollected.TabIndex = 48;
            // 
            // lblCollected
            // 
            this.lblCollected.AutoSize = true;
            this.lblCollected.Location = new System.Drawing.Point(249, 152);
            this.lblCollected.Name = "lblCollected";
            this.lblCollected.Size = new System.Drawing.Size(81, 17);
            this.lblCollected.TabIndex = 47;
            this.lblCollected.Text = "Collected";
            // 
            // lblTest
            // 
            this.lblTest.AutoSize = true;
            this.lblTest.Location = new System.Drawing.Point(7, 155);
            this.lblTest.Name = "lblTest";
            this.lblTest.Size = new System.Drawing.Size(66, 17);
            this.lblTest.TabIndex = 46;
            this.lblTest.Text = "Method";
            // 
            // cmbMethod
            // 
            this.cmbMethod.FormattingEnabled = true;
            this.cmbMethod.Location = new System.Drawing.Point(77, 147);
            this.cmbMethod.Name = "cmbMethod";
            this.cmbMethod.Size = new System.Drawing.Size(164, 25);
            this.cmbMethod.TabIndex = 45;
            this.cmbMethod.SelectedIndexChanged += new System.EventHandler(this.Combo_SelectedItemChanged);
            this.cmbMethod.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Sample_KeyPress);
            this.cmbMethod.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Sample_KeyUp);
            // 
            // lblField
            // 
            this.lblField.AutoSize = true;
            this.lblField.Location = new System.Drawing.Point(17, 77);
            this.lblField.Name = "lblField";
            this.lblField.Size = new System.Drawing.Size(45, 17);
            this.lblField.TabIndex = 11;
            this.lblField.Text = "Field";
            // 
            // cmbField
            // 
            this.cmbField.FormattingEnabled = true;
            this.cmbField.Location = new System.Drawing.Point(68, 69);
            this.cmbField.Name = "cmbField";
            this.cmbField.Size = new System.Drawing.Size(126, 25);
            this.cmbField.TabIndex = 17;
            this.cmbField.SelectedIndexChanged += new System.EventHandler(this.Combo_SelectedItemChanged);
            this.cmbField.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Sample_KeyPress);
            this.cmbField.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Sample_KeyUp);
            // 
            // lblDLS
            // 
            this.lblDLS.AutoSize = true;
            this.lblDLS.Location = new System.Drawing.Point(205, 102);
            this.lblDLS.Name = "lblDLS";
            this.lblDLS.Size = new System.Drawing.Size(36, 17);
            this.lblDLS.TabIndex = 9;
            this.lblDLS.Text = "DLS";
            // 
            // lblZone
            // 
            this.lblZone.AutoSize = true;
            this.lblZone.Location = new System.Drawing.Point(214, 75);
            this.lblZone.Name = "lblZone";
            this.lblZone.Size = new System.Drawing.Size(46, 17);
            this.lblZone.TabIndex = 7;
            this.lblZone.Text = "Zone";
            // 
            // cmbZone
            // 
            this.cmbZone.FormattingEnabled = true;
            this.cmbZone.ItemHeight = 17;
            this.cmbZone.Location = new System.Drawing.Point(273, 69);
            this.cmbZone.Name = "cmbZone";
            this.cmbZone.Size = new System.Drawing.Size(120, 25);
            this.cmbZone.TabIndex = 18;
            this.cmbZone.SelectedIndexChanged += new System.EventHandler(this.Combo_SelectedItemChanged);
            this.cmbZone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Sample_KeyPress);
            this.cmbZone.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Sample_KeyUp);
            // 
            // lblSite
            // 
            this.lblSite.AutoSize = true;
            this.lblSite.Location = new System.Drawing.Point(8, 108);
            this.lblSite.Name = "lblSite";
            this.lblSite.Size = new System.Drawing.Size(36, 17);
            this.lblSite.TabIndex = 5;
            this.lblSite.Text = "Site";
            // 
            // cmbSite
            // 
            this.cmbSite.FormattingEnabled = true;
            this.cmbSite.ItemHeight = 17;
            this.cmbSite.Location = new System.Drawing.Point(65, 103);
            this.cmbSite.Name = "cmbSite";
            this.cmbSite.Size = new System.Drawing.Size(126, 25);
            this.cmbSite.TabIndex = 19;
            this.cmbSite.SelectedIndexChanged += new System.EventHandler(this.Combo_SelectedItemChanged);
            this.cmbSite.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Sample_KeyPress);
            this.cmbSite.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Sample_KeyUp);
            // 
            // gbxSearch
            // 
            this.gbxSearch.Controls.Add(this.cbxItem);
            this.gbxSearch.Controls.Add(this.cbxDate);
            this.gbxSearch.Controls.Add(this.cbxSample);
            this.gbxSearch.Controls.Add(this.cbxProject);
            this.gbxSearch.Controls.Add(this.btnSearch);
            this.gbxSearch.Controls.Add(this.lblEnd);
            this.gbxSearch.Controls.Add(this.dtpEnd_Date);
            this.gbxSearch.Controls.Add(this.lblBeg);
            this.gbxSearch.Controls.Add(this.dtpBeg_Date);
            this.gbxSearch.Font = new System.Drawing.Font("Lucida Bright", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxSearch.Location = new System.Drawing.Point(3, 82);
            this.gbxSearch.Name = "gbxSearch";
            this.gbxSearch.Size = new System.Drawing.Size(491, 224);
            this.gbxSearch.TabIndex = 45;
            this.gbxSearch.TabStop = false;
            this.gbxSearch.Text = "Search";
            // 
            // cbxItem
            // 
            this.cbxItem.AutoSize = true;
            this.cbxItem.Location = new System.Drawing.Point(270, 72);
            this.cbxItem.Name = "cbxItem";
            this.cbxItem.Size = new System.Drawing.Size(140, 21);
            this.cbxItem.TabIndex = 53;
            this.cbxItem.Text = "By Sample Item";
            this.cbxItem.UseVisualStyleBackColor = true;
            this.cbxItem.Click += new System.EventHandler(this.CheckBox_Click);
            // 
            // cbxDate
            // 
            this.cbxDate.AutoSize = true;
            this.cbxDate.Location = new System.Drawing.Point(273, 24);
            this.cbxDate.Name = "cbxDate";
            this.cbxDate.Size = new System.Drawing.Size(143, 21);
            this.cbxDate.TabIndex = 52;
            this.cbxDate.Text = "By Project Date";
            this.cbxDate.UseVisualStyleBackColor = true;
            this.cbxDate.Click += new System.EventHandler(this.CheckBox_Click);
            // 
            // cbxSample
            // 
            this.cbxSample.AutoSize = true;
            this.cbxSample.Location = new System.Drawing.Point(33, 72);
            this.cbxSample.Name = "cbxSample";
            this.cbxSample.Size = new System.Drawing.Size(146, 21);
            this.cbxSample.TabIndex = 51;
            this.cbxSample.Text = "By Open Sample";
            this.cbxSample.UseVisualStyleBackColor = true;
            this.cbxSample.Click += new System.EventHandler(this.CheckBox_Click);
            // 
            // cbxProject
            // 
            this.cbxProject.AutoSize = true;
            this.cbxProject.Location = new System.Drawing.Point(33, 23);
            this.cbxProject.Name = "cbxProject";
            this.cbxProject.Size = new System.Drawing.Size(147, 21);
            this.cbxProject.TabIndex = 50;
            this.cbxProject.Text = "By Open Project";
            this.cbxProject.UseVisualStyleBackColor = true;
            this.cbxProject.Click += new System.EventHandler(this.CheckBox_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnSearch.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSearch.Location = new System.Drawing.Point(179, 175);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(112, 28);
            this.btnSearch.TabIndex = 49;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lblEnd
            // 
            this.lblEnd.AutoSize = true;
            this.lblEnd.Location = new System.Drawing.Point(237, 119);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(36, 17);
            this.lblEnd.TabIndex = 48;
            this.lblEnd.Text = "End";
            // 
            // dtpEnd_Date
            // 
            this.dtpEnd_Date.CustomFormat = "yyyy-mm-dd hh:mm:ss";
            this.dtpEnd_Date.Location = new System.Drawing.Point(279, 111);
            this.dtpEnd_Date.Name = "dtpEnd_Date";
            this.dtpEnd_Date.Size = new System.Drawing.Size(137, 25);
            this.dtpEnd_Date.TabIndex = 47;
            // 
            // lblBeg
            // 
            this.lblBeg.AutoSize = true;
            this.lblBeg.Location = new System.Drawing.Point(36, 119);
            this.lblBeg.Name = "lblBeg";
            this.lblBeg.Size = new System.Drawing.Size(50, 17);
            this.lblBeg.TabIndex = 46;
            this.lblBeg.Text = "Begin";
            // 
            // dtpBeg_Date
            // 
            this.dtpBeg_Date.CustomFormat = "yyyy-mm-dd hh:mm:ss";
            this.dtpBeg_Date.Location = new System.Drawing.Point(95, 113);
            this.dtpBeg_Date.Name = "dtpBeg_Date";
            this.dtpBeg_Date.Size = new System.Drawing.Size(129, 25);
            this.dtpBeg_Date.TabIndex = 45;
            // 
            // gbxNavControls
            // 
            this.gbxNavControls.Controls.Add(this.lblPosition);
            this.gbxNavControls.Controls.Add(this.btnEnd);
            this.gbxNavControls.Controls.Add(this.btnNext);
            this.gbxNavControls.Controls.Add(this.btnPrev);
            this.gbxNavControls.Controls.Add(this.btnBeg);
            this.gbxNavControls.Location = new System.Drawing.Point(608, 509);
            this.gbxNavControls.Name = "gbxNavControls";
            this.gbxNavControls.Size = new System.Drawing.Size(444, 56);
            this.gbxNavControls.TabIndex = 53;
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
            this.btnEnd.Click += new System.EventHandler(this.btnEnd_Click);
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
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
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
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
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
            this.btnBeg.Click += new System.EventHandler(this.btnBeg_Click);
            // 
            // gbxDisplay
            // 
            this.gbxDisplay.Controls.Add(this.dgvDisplay);
            this.gbxDisplay.Location = new System.Drawing.Point(503, 82);
            this.gbxDisplay.Name = "gbxDisplay";
            this.gbxDisplay.Size = new System.Drawing.Size(635, 421);
            this.gbxDisplay.TabIndex = 46;
            this.gbxDisplay.TabStop = false;
            this.gbxDisplay.Text = "Results";
            // 
            // dgvDisplay
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dgvDisplay.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDisplay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDisplay.Location = new System.Drawing.Point(18, 19);
            this.dgvDisplay.Name = "dgvDisplay";
            this.dgvDisplay.Size = new System.Drawing.Size(611, 392);
            this.dgvDisplay.TabIndex = 0;
            this.dgvDisplay.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvDisplay_CellBeginEdit);
            this.dgvDisplay.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDisplay_CellContentClick);
            this.dgvDisplay.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDisplay_CellEndEdit);
            this.dgvDisplay.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDisplay_CellValidated);
            this.dgvDisplay.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDisplay_CellValueChanged);
            this.dgvDisplay.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.dgvDisplay_CellValueNeeded);
            // 
            // dgvTextBoxColumnFilter1
            // 
            this.dgvTextBoxColumnFilter1.Active = false;
            this.dgvTextBoxColumnFilter1.BackColor = System.Drawing.Color.Transparent;
            this.dgvTextBoxColumnFilter1.FilterApplySoon = true;
            this.dgvTextBoxColumnFilter1.FilterCaption = null;
            this.dgvTextBoxColumnFilter1.FilterExpression = "";
            this.dgvTextBoxColumnFilter1.HFilterAlignment = DgvFilterPopup.HFilterAlignment.Middle;
            this.dgvTextBoxColumnFilter1.Location = new System.Drawing.Point(1031, 538);
            this.dgvTextBoxColumnFilter1.Name = "dgvTextBoxColumnFilter1";
            this.dgvTextBoxColumnFilter1.Size = new System.Drawing.Size(8, 8);
            this.dgvTextBoxColumnFilter1.TabIndex = 47;
            this.dgvTextBoxColumnFilter1.VFilterAlignment = DgvFilterPopup.VFilterAlignment.Center;
            // 
            // Sample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbxNavControls);
            this.Controls.Add(this.dgvTextBoxColumnFilter1);
            this.Controls.Add(this.gbxDisplay);
            this.Controls.Add(this.gbxDataControls);
            this.Controls.Add(this.gbxSearch);
            this.Controls.Add(this.pnlDisplay);
            this.Controls.Add(this.gbxSample);
            this.Name = "Sample";
            this.Size = new System.Drawing.Size(1150, 800);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgvDisplay_KeyUp);
            this.gbxDataControls.ResumeLayout(false);
            this.pnlDisplay.ResumeLayout(false);
            this.pnlDisplay.PerformLayout();
            this.gbxSample.ResumeLayout(false);
            this.gbxSample.PerformLayout();
            this.gbxSearch.ResumeLayout(false);
            this.gbxSearch.PerformLayout();
            this.gbxNavControls.ResumeLayout(false);
            this.gbxNavControls.PerformLayout();
            this.gbxDisplay.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDisplay)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblDisplay;
        private System.Windows.Forms.GroupBox gbxDataControls;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnNew;

        private System.Windows.Forms.CheckBox cbxSampleIsOpen;
        private System.Windows.Forms.Panel pnlDisplay;
        private System.Windows.Forms.CheckBox cbxSampleInOffice;
        private System.Windows.Forms.MaskedTextBox mskDLS;
        private System.Windows.Forms.TextBox txtSampleNotes;
        private System.Windows.Forms.Label lblSampleNotes;
        private System.Windows.Forms.GroupBox gbxSample;
        private System.Windows.Forms.Label lblField;
        private System.Windows.Forms.ComboBox cmbField;
        private System.Windows.Forms.Label lblDLS;
        private System.Windows.Forms.Label lblZone;
        private System.Windows.Forms.ComboBox cmbZone;
        private System.Windows.Forms.Label lblSite;
        private System.Windows.Forms.ComboBox cmbSite;
        private System.Windows.Forms.GroupBox gbxSearch;
        private System.Windows.Forms.Label lblTest;
        private System.Windows.Forms.ComboBox cmbMethod;
        private System.Windows.Forms.Label lblEnd;
        private System.Windows.Forms.DateTimePicker dtpEnd_Date;
        private System.Windows.Forms.Label lblBeg;
        private System.Windows.Forms.DateTimePicker dtpBeg_Date;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.GroupBox gbxDisplay;
        private System.Windows.Forms.Label lblTask;
        private System.Windows.Forms.CheckBox cbxDate;
        private System.Windows.Forms.CheckBox cbxSample;
        private System.Windows.Forms.CheckBox cbxProject;
        private DgvFilterPopup.DgvTextBoxColumnFilter dgvTextBoxColumnFilter1;
        private System.Windows.Forms.DataGridView dgvDisplay;
        private System.Windows.Forms.GroupBox gbxNavControls;
        private System.Windows.Forms.Label lblPosition;
        private System.Windows.Forms.Button btnEnd;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnBeg;
        private System.Windows.Forms.DateTimePicker dtpCollected;
        private System.Windows.Forms.Label lblCollected;
        private System.Windows.Forms.CheckBox cbxItem;
        private System.Windows.Forms.Button btnGenerateProjectNumbers;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.ComboBox cmbSample_Type;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.ComboBox cmbPrice;
    }
}
