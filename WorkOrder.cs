using DataObjects;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sterling_Lab
{
    public partial class WorkOrder : UserControl
    {
        private int clientID;
        private int projectID;
        private string project_number;
        private int methodID;
        private int sampleID;
        private int locationID;
        private string sample_name;
        private bool sample_saved;
        bool IsDirty = false; // keys pressed
        private DataProcessor dp;

        enum State { Load, Navigate, New, Edit, Save, Cancel, Delete };
        private string form_State;
        private State state { get; set; }

        public WorkOrder()
        {
            InitializeComponent();
            dp = new DataProcessor("DB");
            UpdateState(State.Load);
        }
    

        private void gbxNavControls_Enter(object sender, EventArgs e)
        {

        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            UpdateState(State.Cancel);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            UpdateState(State.Delete);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            UpdateState(State.Edit);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            UpdateState(State.New);
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (state != State.Load)
                UpdateState(State.Save);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            GetSampleItems();
        }

        private void cmbSample_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSampleItems();
        }

        private void ClearControls()
        {
            try
            {
                foreach (Control ctl in this.Controls)
                {
                    if (ctl is ComboBox)
                    {
                        var cmb = (ComboBox)ctl;
                        cmb.SelectedIndex = 0;
                        cmb.Text = "";

                    }
                    if (ctl is TextBox)
                        ctl.Text = "";
                }
                lblDisplay.Text = "Sample";
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message, "Control Clear Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDisplay_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvDisplay.CurrentRow.Selected = true;
            string query = "";
            object val;
            try
            {
                switch (state)
                {
                    case State.Load:
                        val = dgvDisplay.Rows[e.RowIndex].Cells["project_pk"].FormattedValue.ToString();
                        this.projectID = Convert.ToInt32(val);
                        break;
                    case State.Edit:
                        val = dgvDisplay.Rows[e.RowIndex].Cells["project_pk"].FormattedValue.ToString();
                        this.projectID = Convert.ToInt32(val);
                        GetQuery("sample");
                        //string date = dp.GetDataItem("SELECT date_received FROM tbl_sample WHERE sample_pk = " + projectID).ToString();
                        //lblDisplay.Text = "Parameters For Sample " +  " :: " + date;
                        break;
                    case State.New:
                        val = dgvDisplay.Rows[e.RowIndex].Cells["sample_pk"].FormattedValue.ToString();
                        this.projectID = Convert.ToInt32(val);
                        query = "SELECT sample_fk, method_item, value1, u.unit_desc " +
                          "FROM tbl_sample_item i " +
                          "INNER JOIN tbl_sample t ON i.sample_fk = t.sample_pk " +
                          "INNER JOIN tbl_method_item m ON i.method_item_fk = m.method_item_pk " +
                          "INNER JOIN tbl_method_type y ON i.method_type_fk = y.type_pk " +
                          "INNER JOIN tbl_unit u on i.unit_1_fk = u.unit_pk " +
                          "WHERE (i.method_item_fk <> 1 && i.method_type_fk = 1) AND sample_fk = " + this.projectID; // method_item_fk = 1 (blank)
                        break;

                    case State.Save: // get sample items
                        query = "SELECT i.sample_item_pk, m.method_item, i.value1, u.unit_desc " +
                            "FROM tbl_sample_item i " +
                            "INNER JOIN tbl_sample t ON i.sample_fk = t.sample_pk " +
                            "INNER JOIN tbl_method_item m ON i.method_item_fk = m.method_item_pk " +
                            "INNER JOIN tbl_method_type y ON i.method_type_fk = y.type_pk " +
                            "INNER JOIN tbl_unit u on i.unit_1_fk = u.unit_pk " +
                            "WHERE (i.method_item_fk <> 1 && i.method_type_fk = 1) && sample_fk = " + this.projectID; // method_item_fk = 1 (blank)
                        break;
                    case State.Navigate:
                        // populate sample info
                        val = dgvDisplay.Rows[e.RowIndex].Cells["project_pk"].FormattedValue.ToString();
                        this.projectID = Convert.ToInt32(val);
                        query = "SELECT *  FROM tbl_sample WHERE project_fk = " + this.projectID;
                        break;
                    default:
                        break;
                }
                if (!String.IsNullOrEmpty(query))
                    dp.PopulateDataGridView(query, dgvDisplay);
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message + " Error. Try again.", "DataGridView Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                gbxSearch.Text = "Project ID :: " + projectID;
            }
        }

        private void dgvDisplay_KeyUp(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.F2)
            //    btnGetsamples.Focus();
        }

        private void dgvDisplay_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (this.state == State.Edit)
            {
                //dgvDisplay.Rows[e.RowIndex].ErrorText = "";
                DataGridView dgv = (DataGridView)sender;
                DataTable dt = (DataTable)dgv.DataSource;
                string tablename = dt.TableName;
                double d; // Validate after finished editing. 
                if (dgv.Rows[e.RowIndex].IsNewRow) { return; }
                object val = null;
                int itemID = 0;
                string update = "";
                bool IsValue = false;
                switch (tablename)
                {
                    case "tbl_sample_items":
                        if (dgv.Columns[e.ColumnIndex].Name.Contains("k")) // 
                        {
                            IsValue = false;
                        }
                        break;
                    case "tbl_sample":
                        val = dgvDisplay.Rows[e.RowIndex].Cells["sample_pk"].FormattedValue.ToString();
                        IsValue = true;
                        break;
                    default:
                        if (double.TryParse(e.FormattedValue.ToString(), out d))
                        {
                            val = dgvDisplay.Rows[e.RowIndex].Cells["sample_item_pk"].FormattedValue.ToString();
                            IsValue = true;
                        }
                        break;
                }
                if (IsValue)
                {
                    try
                    {
                        itemID = Convert.ToInt32(val);
                        update = "UPDATE " + tablename + " SET `" + dgvDisplay.Columns[e.ColumnIndex].Name + "` = " + e.FormattedValue + " WHERE sample_item_pk = " + itemID;
                        dp.ProcessData("", update, tablename);
                    }
                    catch (Exception ex)
                    {
                        e.Cancel = true;
                        //dgvDisplay.Rows[e.RowIndex].Cells[2].ErrorText = "value must be numeric";
                        MessageBox.Show(ex.Message + "Value must be numeric!", "Data Entry Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void dgvDisplay_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int row_index = e.RowIndex;
            if (row_index++ < dgvDisplay.RowCount)
                dgvDisplay.CurrentCell = dgvDisplay[e.ColumnIndex, row_index]; // auto advance the row
        }


        private void GetQuery(string tablename)
        {
            string query = string.Empty;
            switch (tablename)
            {
                case "batch":
                    query = "SELECT * FROM tbl_sample  WHERE is_open = true AND date_collected BETWEEN '" + dtpBeg.Value + "' AND '" + DateTime.Now + "';";
                    break;
                case "sample":
                    query = "SELECT sample_name FROM tbl_sample WHERE is_open = true AND date_collected BETWEEN '" + dtpBeg.Value + "' AND '" + DateTime.Now + "';";
                    break;
                case "sample_item":
                    query = "SELECT * FROM tbl_sample_item WHERE sample_fk = " + sampleID + ";";
                    break;
                default:
                    break;
            }
            dp.PopulateDataGridView(query, dgvDisplay, tablename);
            if (dgvDisplay.Rows.Count == 0)
                MessageBox.Show("No Samples Available. Please add samples to this project", "Empty Dataset", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void GetSampleItems()
        {
            string query = "";
            DataTable dt = new DataTable();
            try
            {
                query = "SELECT * FROM tbl_sample_items WHERE sample_fk = '" + cmbSample.SelectedValue + "';";
                dt = dp.GetDataTable(query, "sample_items");
            }
            catch(Exception x)
            {
                MessageBox.Show(x.Message, "Sample Item Read", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            if (dt.Rows.Count > 0)
                dgvDisplay.DataSource = dt;
            else
                MessageBox.Show("No Sample Items. Create New.", "S", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void InitializeControls()
        {
            try
            {
                gbxSearch.Text = "Search Sample Items";
                dtpBeg.Value = DateTime.Now.SubtractBusinessDays(30);
                string query = "";
                if (state == State.Load)
                {
                    query = "SELECT sample_name AS display, sample_pk AS value FROM tbl_sample WHERE is_open = true AND date_collected BETWEEN '2020-01-01' AND '" + DateTime.Now + "';"; // ADD + dtpBeg.Value +
                }
                dp.PopulateCombo(query, cmbSample, "display", "value");
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message, "Initialize Controls Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dtpBeg.Value = DateTime.Now.SubtractBusinessDays(30);
            }
        }

        private void NavControlsStatusChanged()
        {
            foreach (Button btn in gbxNavControls.Controls)
            {
                if (btn.Enabled == true)
                {
                    btn.BackColor = Color.Salmon;
                }
                else
                {
                    btn.BackColor = Color.Gray;
                }
            }
        }

        private void PopulateSample()
        {
            //gbxSearch.Text = "Date Collected";
            //lblEnd.Visible = false;
            //dtpEnd.Visible = false;
        }

        //public void StoreFocusedCombo(object sender, EventArgs e)
        //{
        //    if (sender != null)
        //    {
        //        this.last_focused = (ComboBox)sender;
        //    }
        //}

        private bool UpdateSample(State status)
        {
            try
            {
                string query = "";
                if (clientID > 0)
                {
                    ProjectNumberGenerator pn = new ProjectNumberGenerator();
                    this.project_number = pn.Generate(clientID, sampleID, methodID);
                }

                //tbl_project: project_pk, file_name, project_number, client_fk, initiator_fk, project_type_fk, objective_fk, date_initiated,
                //is_reported, priority_requested_fk, billCust, bill_cust_notes, project_notes (13 fields)

                // N.B. in this UC need to supply null values for PROJECT_NUMBER AND FILE_NAME
                // PROJECT_NUMBER CAN BE UPDATATED FROM THE SAMPLE UC
                switch (status)
                {
                    //tbl_sample: sample_pk, batch_fk, project_fk, method_fk, location(site)_fk, sample_name, date_collected, sample_notes, is_in_office, Is_open (10 fields)..
                    case State.New:
                        //query = "SET foreign_key_checks=0;INSERT INTO tbl_sample VALUES (0,null," + this.projectID + "," + methodID + "," + locationID + ",'" + sample_name +
                        //    "','" + dtpBeg.Value + "','" + txtSampleNotes.Text + "'," + cbxSampleInOffice.Checked + "," + cbxSampleIsOpen.Checked + ");SET foreign_key_checks=1;";
                        break;
                    case State.Save:
                        //query = "SET foreign_key_checks=0;INSERT INTO tbl_sample VALUES (0,null," + this.projectID + ",null,null,";
                        //+ this.clientID + "," + agentID + "," + methodID + "," + objectiveID + ",'"
                        //+ dtpInitiated.Value + "'," + false + "," + priorityID + "," + cbxBillClient.Checked + ",'"
                        //+ txtBillingNotes.Text + "','" + txtProjectNotes.Text + "');SET foreign_key_checks=1;";
                        break;
                    case State.Edit:
                        query = "UPDATE tbl_project SET project_pk = " + projectID + ", file_name = null, project_number = '" + project_number + "' WHERE project_pk = " + projectID + ";";
                        dp.ProcessData("", query, "project");
                        //tbl_sample: sample_pk, batch_fk, project_fk, location(site)_fk, sample_name, date_collected, sample_notes, is_in_office, Is_open (9 fields)..
                        //query = "UPDATE tbl_sample SET batch_pk = null, project_fk = " + projectID + ",location_fk = " + locationID + "," +
                        //    ", sample_name = '" + sample_name + "',date_collected = '" + dtpBeg.Value + "', sample_notes = '" + txtSampleNotes.Text +
                        //    "',is_in_office = " + cbxSampleInOffice.Checked + ",is_open = " + cbxSampleIsOpen + " WHERE sample_pk = " + sampleID + ";";
                        break;
                    default:
                        break;
                }
                this.sampleID = dp.ProcessData("", query, "sample"); // ProcessData: select, query, tablename (to identify error source)
                sample_saved = (sampleID > 0);
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message, "Sample Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                sample_saved = false;
            }
            finally
            {
                if (sample_saved)
                {
                    UpdateState(State.Navigate);
                    MessageBox.Show("Sample Saved!", "Form State", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            return this.sample_saved;
        }

        private void UpdateState(State status)
        {
            switch (status)
            {
                case State.Load:
                    form_State = "Load";
                    InitializeControls();
                    UpdateState(State.Navigate);
                    break;
                case State.Navigate:
                    form_State = "Navigate";
                    btnNew.Enabled = true;
                    btnEdit.Enabled = true;
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
                    btnDelete.Enabled = false;
                    break;
                case State.Cancel:
                    form_State = "Cancel";
                    btnNew.Enabled = true;
                    btnEdit.Enabled = true;
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
                    btnDelete.Enabled = false;
                    InitializeControls();
                    UpdateState(State.Navigate);
                    break;
                case State.Edit:
                    form_State = "Edit";
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                    btnDelete.Enabled = true;
                    btnNew.Enabled = false;
                    btnEdit.Enabled = false;
                    this.sample_saved = false; // also fill grid w/ sample items to add data
                    break;
                case State.New:
                    form_State = "New";
                    ClearControls();
                    btnDelete.Enabled = false;
                    btnNew.Enabled = false;
                    btnEdit.Enabled = false;
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                    this.sample_saved = false; // insert sample and populate sample fields
                    UpdateSample(status);
                    break;
                case State.Save:
                    form_State = "Save";
                    UpdateSample(status);
                    break;
                default:
                    break;
            }
            this.state = status;
            lblDisplay.Text = "Work Order :: " + form_State;
            NavControlsStatusChanged();
        }
    }
}
