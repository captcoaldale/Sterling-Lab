using DataObjects;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Sterling_Lab
{
    public partial class Analyze : UserControl
    {
        private int current_row;
        private int clientID;
        private int projectID;
        private string project_number;
        private int methodID;
        private int sampleID;
        private int locationID;
        private string sample_number;
        private bool sample_saved;

        bool IsDirty = false; // keys
        bool IsPopulating = false;

        private Utilties ut;
        private BindingSource bs;
        private DataProcessor dp;

        enum State { Load, Navigate, New, Edit, Save, Cancel, Delete };
        private string form_state;
        private State current_state { get; set; }
        private State previous_state { get; set; }

        enum Task { Batch, Element, Item, Sample }
        private string form_task;
        private Task current_task { get; set; }
        private Task previous_task { get; set; }

        public Analyze()
        {
            InitializeComponent();
            dp = new DataProcessor("DB");
            ut = new Utilties();
            bs = new BindingSource();
            InitializeControls();
        }

        // Scrolll
        private void btnBeg_Click(object sender, EventArgs e)
        {
            bs.MoveFirst();
            PopulateForm();
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            bs.MoveLast();
            PopulateForm();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            bs.MovePrevious();
            PopulateForm();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            bs.MoveNext();
            PopulateForm();
        }

        // Data buttons


        private void btnCancel_Click(object sender, EventArgs e)
        {
            UpdateAnalysis(State.Cancel);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            UpdateAnalysis(State.Delete);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            UpdateState(State.Edit);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            current_task = Task.Sample;
            UpdateAnalysis(State.New);
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (current_state != State.Load)
            {
                current_task = Task.Sample;
                UpdateAnalysis(State.Save);
            }
        }

        // Search
        private void btnSearch_Click(object sender, EventArgs e)
        {
            UpdateAnalysis(State.Load);
        }

        private void cmbSample_SelectedIndexChanged(object sender, EventArgs e)
        {
            current_task = Task.Item;
            UpdateAnalysis(State.Load);
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

        private Task ConvertStringToTask(string str)
        {
            switch (form_task)
            {
                case "Batch":
                    current_task = Task.Batch; break;
                case "Element":
                    current_task = Task.Element; break;
                case "Item":
                    current_task = Task.Item; break;
                case "Sample":
                    current_task = Task.Sample; break;
            }
            return current_task;
        }

        private string ConvertTaskToString(Task task)
        {
            switch (task)
            {
                case Task.Batch:
                    form_task = "Batch"; break;
                case Task.Element:
                    form_task = "Element"; break;
                case Task.Item:
                    form_task = "Item"; break;
                case Task.Sample:
                    form_task = "Sample"; break;
            }
            return form_task;
        }

        private void DataControlsStatusChanged()
        {
            foreach (Button btn in gbxDataControls.Controls)
            {
                if (btn.Enabled == true)
                {
                    btn.BackColor = Color.MistyRose;
                }
                else
                {
                    btn.BackColor = Color.Gray;
                }
            }
            lblPosition.Text = (current_row + 1) + " of " + (dgvDisplay.RowCount - 1);
        }

        private void dgvDisplay_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvDisplay.CurrentRow.Selected = true;
            string query = "";
            object val;
            try
            {
                switch (current_state)
                {
                    case State.Load:
                        val = dgvDisplay.Rows[e.RowIndex].Cells["project_pk"].FormattedValue.ToString();
                        this.projectID = Convert.ToInt32(val);
                        break;
                    case State.Edit:
                        val = dgvDisplay.Rows[e.RowIndex].Cells["project_pk"].FormattedValue.ToString();
                        this.projectID = Convert.ToInt32(val);
                        GetAnalysisSelectQuery(Task.Sample);
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
                    dp.PopulateDataGridView(query, dgvDisplay, bs);
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
            if (this.current_state == State.Edit)
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

        private string GetAnalysisSelectQuery(Task current_task)
        {
            string query = string.Empty;
            switch (current_task)
            {
                case Task.Sample:
                    query = "SELECT * FROM tbl_sample WHERE is_open = true;";
                    break;
                case Task.Item:
                    query = "SELECT * FROM tbl_sample_items WHERE sample_fk = " + cmbClient.SelectedValue + ";";
                    break;
                default:
                    break;
            }
            return query;
        }

        private void InitializeControls()
        {
            try
            {
                gbxSearch.Text = "Search Items";
                dtpBeg_Date.Value = DateTime.Now.SubtractBusinessDays(3);
                dtpEnd_Date.Value = DateTime.Now;
                string query = "";
                current_task = Task.Sample;
                previous_state = State.Navigate;
                if (current_state == State.Load)
                {
                    query = "SELECT company_name AS display, company_pk AS value FROM tbl_company WHERE is_client = true;"; // ADD + dtpBeg.Value +
                    dp.PopulateCombo(query, cmbClient, "display", "value");
                    cmbClient.SelectedIndex = 0;
                    query = "SELECT field_desc AS display, field_pk AS value FROM tbl_field " +
                        "INNER JOIN tbl_location ON field_fk = field_pk" +
                        " WHERE client_fk = " + Convert.ToInt32(dp.GetSelectedValue(cmbClient).ToString()); // ADD + dtpBeg.Value +
                    dp.PopulateCombo(query, cmbClient, "display", "value");
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message, "Initialize Controls Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

            }
            UpdateAnalysis(current_state);
        }

        private void PopulateForm(string query = "")
        {
            string company_name = "";
            form_task = ConvertTaskToString(current_task);
            try
            {
                IsPopulating = true;
                if (query.Length == 0)
                {
                    query = GetAnalysisSelectQuery(current_task);
                }
                if (current_state == State.Load)
                {
                    dp.PopulateDataGridView(query, dgvDisplay, bs, form_task); // crucial to make a new object
                    bs.MoveFirst();
                }
                IsPopulating = true;
                // datagridview 
                this.current_row = ut.SetRange(bs.Position, 0, dgvDisplay.Rows.Count);
                DataGridViewRow row = dgvDisplay.Rows[current_row];
                dgvDisplay.ClearSelection();
                row.Selected = true;
                this.sampleID = Convert.ToInt32(cmbClient.SelectedValue);
                sample_number = dp.GetDataItem("SELECT sample_number FROM tbl_sample WHERE sample_pk = " + sampleID + ";").ToString();
                company_name = dp.GetStringFromDB("SELECT company_name FROM tbl_company WHERE company_pk = " + clientID);
                gbxDisplay.Text = " Report for Client :: " + company_name;
                //project_number = row.Cells["project_number"].Value.ToString();
                //btnGenerateProjectNumbers.Visible = (project_number.Length == 0);

                // populate sample controls w/ sample data
                lblTask.Text = "Sample No " + sample_number;
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message, "Data Read Error -- Populate Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.IsPopulating = false;
                switch (form_task)
                {
                    case "Item":

                        break;
                }
                current_state = State.Navigate;
            }
            UpdateState(current_state);
        }


        private void Analysis_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetterOrDigit(e.KeyChar))
            {
                if (!IsDirty && current_state == State.Edit)
                {
                    IsDirty = true;
                    UpdateState(current_state);
                }
            }
            else if (e.KeyChar == (char)Keys.Return) // enter key
            {
                current_task = Task.Sample;
                ConvertTaskToString(current_task);
                UpdateAnalysis(State.Save);
            }
        }

        private void Analysis_KeyUp(object sender, KeyEventArgs e)
        {
            Control ctl = sender as Control;
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (ctl.Name == "txtSample_Number")
                        UpdateAnalysis(State.Navigate);
                    else if (IsDirty)
                    {
                        current_task = Task.Sample;
                        ConvertTaskToString(current_task);
                        UpdateAnalysis(State.Save);
                    }
                    break;
                case Keys.F2:
                    UpdateState(State.Edit);
                    break;
            }
        }

        private bool UpdateAnalysis(State status, string query = "")
        {
            try
            {
                string tablename = ConvertTaskToString(current_task);
                previous_state = current_state;
                current_state = status;

                switch (status)
                {
                    case State.Edit:
                    case State.Load:
                    case State.Navigate:
                        break;
                    case State.Cancel:
                        InitializeControls();
                        break;
                    case State.New:
                        //tbl_sample: sample_pk, batch_fk, project_fk, method_fk, location(site)_fk, sample_type_fk, sample_number, date_collected, sample_notes, is_in_office, Is_open (11 fields)..
                        if (current_task == Task.Sample)
                        {
                            DateTime collected = DateTime.Now.SubtractBusinessDays(2);
                            SampleNumberGenerator sn = new SampleNumberGenerator();
                            query = "INSERT INTO " + tablename + " VALUES (0,0," + this.projectID + ",0,0,0,'" + sn.Generate() + "','" + collected + "','',0,1);";
                        }
                        else if (current_task == Task.Item)
                        {
                            query = "INSERT INTO " + tablename + " VALUES (0," + sampleID + "null,null,null,null,null,null,null,null,null,null,null,null,null,null,null);"; // 17 cols
                        }
                        break;
                    case State.Delete:
                        int ID = 0;
                        if (current_task == Task.Item)
                        {
                            ID = projectID;
                            query = "DELETE FROM tbl_project WHERE project_pk = " + ID + "'";
                        }
                        if (current_task == Task.Sample)
                        {
                            ID = sampleID;
                            query = "DELETE FROM tbl_sample WHERE sample_pk = " + ID + "'";
                        }

                        DialogResult result = MessageBox.Show("This will remove " + tablename + " # " + ID + "!. Do you wish to continue?", "Delete " + tablename, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                        {
                            dp.ProcessData("", query, tablename);
                            MessageBox.Show(tablename + " # " + ID + " removed.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            current_state = State.Navigate;
                        }
                        break;

                    case State.Save:
                        switch (current_task)
                        {
                            case Task.Item:
                                if (clientID == 0)
                                    throw new Exception("No client ID!");
                                if (locationID == 0)
                                {
                                    locationID = dp.GetID("SELECT location_pk FROM tbl_location WHERE client_fk = " + clientID + " AND site_fk = " + ";");
                                    if (locationID == 0)
                                    { // add a new location
                                        query = "INSERT INTO tbl_location VALUES (0,";
                                        locationID = dp.ProcessData("", query, "location");
                                    }
                                }
                                else if (locationID == 0)
                                {
                                    throw new Exception("No location ID!");
                                }
                                if (methodID == 0)
                                {
                                    sampleID = dp.GetSelectedValue(cmbClient);
                                }
                                else if (methodID == 0)
                                {
                                    throw new Exception("Choose a method!");
                                }
                                break;
                            case Task.Sample: // TODO: update location data, esp land_desc. 23May116
                                SampleNumberGenerator sn = new SampleNumberGenerator();


                                query = "UPDATE " + tablename + " SET method_fk = ";
                                // save to tbl_sample   
                                this.sampleID = dp.ProcessData("", query, tablename);
                                if (locationID > 0)
                                {
                                    // save to tbl_location (fall through)
                                    query = "UPDATE tbl_location SET field_fk = ";
                                }
                                break;
                        }
                        break;
                    default:
                        break;
                }
                if (current_state == State.Save || current_state == State.New)
                {
                    int id = dp.ProcessData("", query, tablename);
                    sample_saved = (id > 0);
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message, "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                sample_saved = false;
            }
            finally
            {
                if (sample_saved)
                {
                    current_state = State.Navigate;
                    current_task = previous_task;
                    MessageBox.Show("Saved!", "Form Sample", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    sample_saved = false;
                }
            }
            PopulateForm();
            return this.sample_saved;
        }

        private void UpdateState(State status)
        {
            switch (status)
            {
                case State.Load:
                    form_state = "Load";
                    status = State.Navigate;
                    break;
                case State.Navigate:
                    form_state = "Navigate";
                    btnNew.Enabled = current_state == State.Navigate;
                    btnEdit.Enabled = (projectID > 0);
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
                    btnDelete.Enabled = false;
                    btnSearch.Enabled = true;

                    gbxSearch.Enabled = true;
                    break;
                case State.Cancel:
                    form_state = "Cancel";
                    btnNew.Enabled = true;
                    btnEdit.Enabled = true;
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
                    btnDelete.Enabled = false;
                    break;
                case State.Edit:
                    form_state = "Edit";
                    btnSave.Enabled = IsDirty;
                    btnCancel.Enabled = true;
                    btnDelete.Enabled = true;
                    btnNew.Enabled = false;
                    btnEdit.Enabled = false;

                    gbxSearch.Enabled = false;
                    break;
                case State.New:
                    form_state = "New";
                    btnDelete.Enabled = false;
                    btnNew.Enabled = false;
                    btnEdit.Enabled = false;
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                    this.sample_saved = false; // insert sample and populate sample fields

                    gbxSearch.Enabled = false;
                    break;
                case State.Save:
                    form_state = "Save";
                    btnSave.Enabled = false;
                    break;
                default:
                    break;
            }
            this.current_state = status;
            lblDisplay.Text = ConvertTaskToString(current_task) + " :: " + form_state;
            DataControlsStatusChanged();
        }

    }
}
