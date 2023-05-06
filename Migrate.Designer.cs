namespace Sterling_Lab
{
    partial class Migrate
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
            this.lblMigrateDisplay = new System.Windows.Forms.Label();
            this.gbxHeader = new System.Windows.Forms.GroupBox();
            this.lblTech = new System.Windows.Forms.Label();
            this.lblAgent = new System.Windows.Forms.Label();
            this.lblOffice = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.lblReported = new System.Windows.Forms.Label();
            this.lblReceived = new System.Windows.Forms.Label();
            this.lblDateSampled = new System.Windows.Forms.Label();
            this.lblField = new System.Windows.Forms.Label();
            this.lblDLS = new System.Windows.Forms.Label();
            this.lblZone = new System.Windows.Forms.Label();
            this.lblSite = new System.Windows.Forms.Label();
            this.dgvParameter = new System.Windows.Forms.DataGridView();
            this.lblDisplay = new System.Windows.Forms.Label();
            this.pnlDisplay = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.gbxOperations = new System.Windows.Forms.GroupBox();
            this.pbrSaving = new System.Windows.Forms.ProgressBar();
            this.gbxResult = new System.Windows.Forms.GroupBox();
            this.dgvCorrosion = new System.Windows.Forms.DataGridView();
            this.dgvTotal = new System.Windows.Forms.DataGridView();
            this.dgvSoluable = new System.Windows.Forms.DataGridView();
            this.gbxSaturation = new System.Windows.Forms.GroupBox();
            this.dgvSaturation = new System.Windows.Forms.DataGridView();
            this.gbxDataControls = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.lblProgress = new System.Windows.Forms.Label();
            this.gbxHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvParameter)).BeginInit();
            this.pnlDisplay.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gbxResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCorrosion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSoluable)).BeginInit();
            this.gbxSaturation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSaturation)).BeginInit();
            this.gbxDataControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblMigrateDisplay
            // 
            this.lblMigrateDisplay.AutoSize = true;
            this.lblMigrateDisplay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblMigrateDisplay.Font = new System.Drawing.Font("Lucida Bright", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMigrateDisplay.ForeColor = System.Drawing.Color.White;
            this.lblMigrateDisplay.Location = new System.Drawing.Point(41, 24);
            this.lblMigrateDisplay.Name = "lblMigrateDisplay";
            this.lblMigrateDisplay.Size = new System.Drawing.Size(123, 33);
            this.lblMigrateDisplay.TabIndex = 0;
            this.lblMigrateDisplay.Text = "Migrate";
            // 
            // gbxHeader
            // 
            this.gbxHeader.Controls.Add(this.lblTech);
            this.gbxHeader.Controls.Add(this.lblAgent);
            this.gbxHeader.Controls.Add(this.lblOffice);
            this.gbxHeader.Controls.Add(this.lblPrice);
            this.gbxHeader.Controls.Add(this.lblReported);
            this.gbxHeader.Controls.Add(this.lblReceived);
            this.gbxHeader.Controls.Add(this.lblDateSampled);
            this.gbxHeader.Controls.Add(this.lblField);
            this.gbxHeader.Controls.Add(this.lblDLS);
            this.gbxHeader.Controls.Add(this.lblZone);
            this.gbxHeader.Controls.Add(this.lblSite);
            this.gbxHeader.Location = new System.Drawing.Point(3, 82);
            this.gbxHeader.Name = "gbxHeader";
            this.gbxHeader.Size = new System.Drawing.Size(666, 112);
            this.gbxHeader.TabIndex = 35;
            this.gbxHeader.TabStop = false;
            this.gbxHeader.Text = "Header";
            // 
            // lblTech
            // 
            this.lblTech.AutoSize = true;
            this.lblTech.Location = new System.Drawing.Point(356, 60);
            this.lblTech.Name = "lblTech";
            this.lblTech.Size = new System.Drawing.Size(32, 13);
            this.lblTech.TabIndex = 30;
            this.lblTech.Text = "Tech";
            // 
            // lblAgent
            // 
            this.lblAgent.AutoSize = true;
            this.lblAgent.Location = new System.Drawing.Point(15, 60);
            this.lblAgent.Name = "lblAgent";
            this.lblAgent.Size = new System.Drawing.Size(29, 13);
            this.lblAgent.TabIndex = 29;
            this.lblAgent.Text = "Acct";
            // 
            // lblOffice
            // 
            this.lblOffice.AutoSize = true;
            this.lblOffice.Location = new System.Drawing.Point(189, 60);
            this.lblOffice.Name = "lblOffice";
            this.lblOffice.Size = new System.Drawing.Size(35, 13);
            this.lblOffice.TabIndex = 28;
            this.lblOffice.Text = "Office";
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Location = new System.Drawing.Point(527, 91);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(13, 13);
            this.lblPrice.TabIndex = 27;
            this.lblPrice.Text = "$";
            // 
            // lblReported
            // 
            this.lblReported.AutoSize = true;
            this.lblReported.Location = new System.Drawing.Point(356, 91);
            this.lblReported.Name = "lblReported";
            this.lblReported.Size = new System.Drawing.Size(51, 13);
            this.lblReported.TabIndex = 26;
            this.lblReported.Text = "Reported";
            // 
            // lblReceived
            // 
            this.lblReceived.AutoSize = true;
            this.lblReceived.Location = new System.Drawing.Point(189, 91);
            this.lblReceived.Name = "lblReceived";
            this.lblReceived.Size = new System.Drawing.Size(56, 13);
            this.lblReceived.TabIndex = 25;
            this.lblReceived.Text = " Received";
            // 
            // lblDateSampled
            // 
            this.lblDateSampled.AutoSize = true;
            this.lblDateSampled.Location = new System.Drawing.Point(15, 91);
            this.lblDateSampled.Name = "lblDateSampled";
            this.lblDateSampled.Size = new System.Drawing.Size(48, 13);
            this.lblDateSampled.TabIndex = 24;
            this.lblDateSampled.Text = "Sampled";
            // 
            // lblField
            // 
            this.lblField.AutoSize = true;
            this.lblField.Location = new System.Drawing.Point(17, 24);
            this.lblField.Name = "lblField";
            this.lblField.Size = new System.Drawing.Size(29, 13);
            this.lblField.TabIndex = 11;
            this.lblField.Text = "Field";
            // 
            // lblDLS
            // 
            this.lblDLS.AutoSize = true;
            this.lblDLS.Location = new System.Drawing.Point(196, 24);
            this.lblDLS.Name = "lblDLS";
            this.lblDLS.Size = new System.Drawing.Size(28, 13);
            this.lblDLS.TabIndex = 9;
            this.lblDLS.Text = "DLS";
            // 
            // lblZone
            // 
            this.lblZone.AutoSize = true;
            this.lblZone.Location = new System.Drawing.Point(356, 24);
            this.lblZone.Name = "lblZone";
            this.lblZone.Size = new System.Drawing.Size(32, 13);
            this.lblZone.TabIndex = 7;
            this.lblZone.Text = "Zone";
            // 
            // lblSite
            // 
            this.lblSite.AutoSize = true;
            this.lblSite.Location = new System.Drawing.Point(515, 24);
            this.lblSite.Name = "lblSite";
            this.lblSite.Size = new System.Drawing.Size(25, 13);
            this.lblSite.TabIndex = 5;
            this.lblSite.Text = "Site";
            // 
            // dgvParameter
            // 
            this.dgvParameter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvParameter.Location = new System.Drawing.Point(6, 19);
            this.dgvParameter.Name = "dgvParameter";
            this.dgvParameter.Size = new System.Drawing.Size(201, 222);
            this.dgvParameter.TabIndex = 31;
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
            // pnlDisplay
            // 
            this.pnlDisplay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.pnlDisplay.Controls.Add(this.lblDisplay);
            this.pnlDisplay.Location = new System.Drawing.Point(102, -231);
            this.pnlDisplay.Name = "pnlDisplay";
            this.pnlDisplay.Size = new System.Drawing.Size(563, 76);
            this.pnlDisplay.TabIndex = 34;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.panel1.Controls.Add(this.lblMigrateDisplay);
            this.panel1.ForeColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1150, 76);
            this.panel1.TabIndex = 36;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1150, 24);
            this.menuStrip1.TabIndex = 37;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // gbxOperations
            // 
            this.gbxOperations.Location = new System.Drawing.Point(518, 48);
            this.gbxOperations.Name = "gbxOperations";
            this.gbxOperations.Size = new System.Drawing.Size(467, 93);
            this.gbxOperations.TabIndex = 38;
            this.gbxOperations.TabStop = false;
            this.gbxOperations.Text = "Operations";
            // 
            // pbrSaving
            // 
            this.pbrSaving.Location = new System.Drawing.Point(741, 173);
            this.pbrSaving.Name = "pbrSaving";
            this.pbrSaving.Size = new System.Drawing.Size(343, 21);
            this.pbrSaving.TabIndex = 1;
            // 
            // gbxResult
            // 
            this.gbxResult.Controls.Add(this.dgvCorrosion);
            this.gbxResult.Controls.Add(this.dgvTotal);
            this.gbxResult.Controls.Add(this.dgvSoluable);
            this.gbxResult.Controls.Add(this.dgvParameter);
            this.gbxResult.Location = new System.Drawing.Point(15, 200);
            this.gbxResult.Name = "gbxResult";
            this.gbxResult.Size = new System.Drawing.Size(1103, 254);
            this.gbxResult.TabIndex = 39;
            this.gbxResult.TabStop = false;
            this.gbxResult.Text = "Lab Results";
            // 
            // dgvCorrosion
            // 
            this.dgvCorrosion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCorrosion.Location = new System.Drawing.Point(627, 173);
            this.dgvCorrosion.Name = "dgvCorrosion";
            this.dgvCorrosion.Size = new System.Drawing.Size(460, 68);
            this.dgvCorrosion.TabIndex = 34;
            // 
            // dgvTotal
            // 
            this.dgvTotal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTotal.Location = new System.Drawing.Point(627, 19);
            this.dgvTotal.Name = "dgvTotal";
            this.dgvTotal.Size = new System.Drawing.Size(287, 99);
            this.dgvTotal.TabIndex = 33;
            // 
            // dgvSoluable
            // 
            this.dgvSoluable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSoluable.Location = new System.Drawing.Point(213, 19);
            this.dgvSoluable.Name = "dgvSoluable";
            this.dgvSoluable.Size = new System.Drawing.Size(408, 222);
            this.dgvSoluable.TabIndex = 32;
            // 
            // gbxSaturation
            // 
            this.gbxSaturation.Controls.Add(this.dgvSaturation);
            this.gbxSaturation.Controls.Add(this.gbxOperations);
            this.gbxSaturation.Location = new System.Drawing.Point(15, 460);
            this.gbxSaturation.Name = "gbxSaturation";
            this.gbxSaturation.Size = new System.Drawing.Size(1103, 188);
            this.gbxSaturation.TabIndex = 40;
            this.gbxSaturation.TabStop = false;
            this.gbxSaturation.Text = "Saturation Index";
            // 
            // dgvSaturation
            // 
            this.dgvSaturation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSaturation.Location = new System.Drawing.Point(10, 19);
            this.dgvSaturation.Name = "dgvSaturation";
            this.dgvSaturation.Size = new System.Drawing.Size(1077, 163);
            this.dgvSaturation.TabIndex = 35;
            // 
            // gbxDataControls
            // 
            this.gbxDataControls.Controls.Add(this.btnCancel);
            this.gbxDataControls.Controls.Add(this.btnDelete);
            this.gbxDataControls.Controls.Add(this.btnSave);
            this.gbxDataControls.Controls.Add(this.btnEdit);
            this.gbxDataControls.Controls.Add(this.btnNew);
            this.gbxDataControls.Location = new System.Drawing.Point(685, 95);
            this.gbxDataControls.Name = "gbxDataControls";
            this.gbxDataControls.Size = new System.Drawing.Size(433, 60);
            this.gbxDataControls.TabIndex = 43;
            this.gbxDataControls.TabStop = false;
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
            this.btnNew.Text = "Load";
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(683, 181);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(48, 13);
            this.lblProgress.TabIndex = 44;
            this.lblProgress.Text = "Progress";
            // 
            // Migrate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.pbrSaving);
            this.Controls.Add(this.gbxDataControls);
            this.Controls.Add(this.gbxSaturation);
            this.Controls.Add(this.gbxResult);
            this.Controls.Add(this.gbxHeader);
            this.Controls.Add(this.pnlDisplay);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Migrate";
            this.Size = new System.Drawing.Size(1150, 800);
            this.gbxHeader.ResumeLayout(false);
            this.gbxHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvParameter)).EndInit();
            this.pnlDisplay.ResumeLayout(false);
            this.pnlDisplay.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.gbxResult.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCorrosion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSoluable)).EndInit();
            this.gbxSaturation.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSaturation)).EndInit();
            this.gbxDataControls.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMigrateDisplay;
        private System.Windows.Forms.GroupBox gbxHeader;
        private System.Windows.Forms.DataGridView dgvParameter;
        private System.Windows.Forms.Label lblField;
        private System.Windows.Forms.Label lblDLS;
        private System.Windows.Forms.Label lblZone;
        private System.Windows.Forms.Label lblSite;
        private System.Windows.Forms.Label lblDisplay;
        private System.Windows.Forms.Panel pnlDisplay;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.GroupBox gbxOperations;
        private System.Windows.Forms.ProgressBar pbrSaving;
        private System.Windows.Forms.Label lblTech;
        private System.Windows.Forms.Label lblAgent;
        private System.Windows.Forms.Label lblOffice;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label lblReported;
        private System.Windows.Forms.Label lblReceived;
        private System.Windows.Forms.Label lblDateSampled;
        private System.Windows.Forms.GroupBox gbxResult;
        private System.Windows.Forms.DataGridView dgvCorrosion;
        private System.Windows.Forms.DataGridView dgvTotal;
        private System.Windows.Forms.DataGridView dgvSoluable;
        private System.Windows.Forms.GroupBox gbxSaturation;
        private System.Windows.Forms.DataGridView dgvSaturation;
        private System.Windows.Forms.GroupBox gbxDataControls;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Label lblProgress;
    }
}
