﻿namespace Sterling_Lab
{
    partial class WorkOrder
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
            this.pnlWorkOrder = new System.Windows.Forms.Panel();
            this.lblTask = new System.Windows.Forms.Label();
            this.lblDisplay = new System.Windows.Forms.Label();
            this.gbxSearch = new System.Windows.Forms.GroupBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dtpBeg = new System.Windows.Forms.DateTimePicker();
            this.cmbSample = new System.Windows.Forms.ComboBox();
            this.dgvDisplay = new System.Windows.Forms.DataGridView();
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
            this.migrate1 = new Sterling_Lab.Migrate();
            this.pnlWorkOrder.SuspendLayout();
            this.gbxSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDisplay)).BeginInit();
            this.gbxNavControls.SuspendLayout();
            this.gbxDataControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlWorkOrder
            // 
            this.pnlWorkOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.pnlWorkOrder.Controls.Add(this.lblTask);
            this.pnlWorkOrder.Controls.Add(this.lblDisplay);
            this.pnlWorkOrder.Font = new System.Drawing.Font("Lucida Bright", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlWorkOrder.Location = new System.Drawing.Point(0, 0);
            this.pnlWorkOrder.Name = "pnlWorkOrder";
            this.pnlWorkOrder.Size = new System.Drawing.Size(1077, 75);
            this.pnlWorkOrder.TabIndex = 0;
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
            // gbxSearch
            // 
            this.gbxSearch.Controls.Add(this.btnSearch);
            this.gbxSearch.Controls.Add(this.dtpBeg);
            this.gbxSearch.Controls.Add(this.cmbSample);
            this.gbxSearch.Font = new System.Drawing.Font("Lucida Bright", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxSearch.Location = new System.Drawing.Point(21, 389);
            this.gbxSearch.Name = "gbxSearch";
            this.gbxSearch.Size = new System.Drawing.Size(545, 88);
            this.gbxSearch.TabIndex = 1;
            this.gbxSearch.TabStop = false;
            this.gbxSearch.Text = "Search";
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnSearch.Location = new System.Drawing.Point(409, 31);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(113, 33);
            this.btnSearch.TabIndex = 40;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dtpBeg
            // 
            this.dtpBeg.Location = new System.Drawing.Point(236, 31);
            this.dtpBeg.Name = "dtpBeg";
            this.dtpBeg.Size = new System.Drawing.Size(140, 26);
            this.dtpBeg.TabIndex = 2;
            // 
            // cmbSample
            // 
            this.cmbSample.FormattingEnabled = true;
            this.cmbSample.Location = new System.Drawing.Point(22, 29);
            this.cmbSample.Name = "cmbSample";
            this.cmbSample.Size = new System.Drawing.Size(176, 28);
            this.cmbSample.TabIndex = 1;
            this.cmbSample.SelectedIndexChanged += new System.EventHandler(this.cmbSample_SelectedIndexChanged);
            // 
            // dgvDisplay
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.dgvDisplay.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDisplay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDisplay.Location = new System.Drawing.Point(21, 81);
            this.dgvDisplay.Name = "dgvDisplay";
            this.dgvDisplay.Size = new System.Drawing.Size(1040, 289);
            this.dgvDisplay.TabIndex = 2;
            // 
            // gbxNavControls
            // 
            this.gbxNavControls.Controls.Add(this.lblPosition);
            this.gbxNavControls.Controls.Add(this.btnEnd);
            this.gbxNavControls.Controls.Add(this.btnNext);
            this.gbxNavControls.Controls.Add(this.btnPrev);
            this.gbxNavControls.Controls.Add(this.btnBeg);
            this.gbxNavControls.Location = new System.Drawing.Point(601, 389);
            this.gbxNavControls.Name = "gbxNavControls";
            this.gbxNavControls.Size = new System.Drawing.Size(444, 56);
            this.gbxNavControls.TabIndex = 56;
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
            this.dgvTextBoxColumnFilter1.Location = new System.Drawing.Point(1024, 418);
            this.dgvTextBoxColumnFilter1.Name = "dgvTextBoxColumnFilter1";
            this.dgvTextBoxColumnFilter1.Size = new System.Drawing.Size(8, 8);
            this.dgvTextBoxColumnFilter1.TabIndex = 55;
            this.dgvTextBoxColumnFilter1.VFilterAlignment = DgvFilterPopup.VFilterAlignment.Center;
            // 
            // gbxDataControls
            // 
            this.gbxDataControls.Controls.Add(this.btnCancel);
            this.gbxDataControls.Controls.Add(this.btnDelete);
            this.gbxDataControls.Controls.Add(this.btnSave);
            this.gbxDataControls.Controls.Add(this.btnEdit);
            this.gbxDataControls.Controls.Add(this.btnNew);
            this.gbxDataControls.Location = new System.Drawing.Point(601, 459);
            this.gbxDataControls.Name = "gbxDataControls";
            this.gbxDataControls.Size = new System.Drawing.Size(444, 60);
            this.gbxDataControls.TabIndex = 54;
            this.gbxDataControls.TabStop = false;
            this.gbxDataControls.Text = "Data Controls";
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnCancel.Location = new System.Drawing.Point(347, 19);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(77, 25);
            this.btnCancel.TabIndex = 41;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnDelete.Location = new System.Drawing.Point(264, 19);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(77, 25);
            this.btnDelete.TabIndex = 40;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnSave.Location = new System.Drawing.Point(185, 19);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(77, 25);
            this.btnSave.TabIndex = 39;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnEdit.Location = new System.Drawing.Point(103, 20);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(77, 25);
            this.btnEdit.TabIndex = 38;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = false;
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnNew.Location = new System.Drawing.Point(20, 19);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(77, 25);
            this.btnNew.TabIndex = 37;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = false;
            // 

            // migrate1
            // 
            this.migrate1.Location = new System.Drawing.Point(595, 138);
            this.migrate1.Name = "migrate1";
            this.migrate1.Size = new System.Drawing.Size(1150, 800);
            this.migrate1.TabIndex = 57;
            // 
            // WorkOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.migrate1);
            this.Controls.Add(this.gbxNavControls);
            this.Controls.Add(this.dgvTextBoxColumnFilter1);
            this.Controls.Add(this.gbxDataControls);
            this.Controls.Add(this.dgvDisplay);
            this.Controls.Add(this.gbxSearch);
            this.Controls.Add(this.pnlWorkOrder);
            this.Name = "WorkOrder";
            this.Size = new System.Drawing.Size(1077, 540);
            this.pnlWorkOrder.ResumeLayout(false);
            this.pnlWorkOrder.PerformLayout();
            this.gbxSearch.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDisplay)).EndInit();
            this.gbxNavControls.ResumeLayout(false);
            this.gbxNavControls.PerformLayout();
            this.gbxDataControls.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlWorkOrder;
        private System.Windows.Forms.Label lblDisplay;
        private System.Windows.Forms.GroupBox gbxSearch;
        private System.Windows.Forms.DateTimePicker dtpBeg;
        private System.Windows.Forms.ComboBox cmbSample;
        private System.Windows.Forms.DataGridView dgvDisplay;
        private System.Windows.Forms.Button btnSearch;
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
        private System.Windows.Forms.Label lblTask;
        private Migrate migrate1;
    }
}
