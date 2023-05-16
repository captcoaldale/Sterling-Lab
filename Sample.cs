using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using DataObjects;

namespace Sterling_Lab
{
    public partial class Sample : UserControl
    {
        private ComboBox last_focused;

        private int clientID;
        private int projectID;
        private string project_number;
        private int methodID;

        private int siteID;
        private int fieldID;
        private int zoneID;

        private int locationID;

        private int sampleID;
        private int sampleItemID;
        private int sampleTypeID;
        private string sample_number;

        private string tablename;
        private string CellChangedValue;
        private int current_row = 0;

        private bool sample_saved;

        private bool IsEditingCell = false;
        private bool IsDirty = false;
        private bool IsPopulating = false;

        enum State { Load, Navigate, New, Edit, Save, Cancel, Delete };
        private string form_State;
        private State current_state { get; set; }
        private State previous_state { get; set; }

        enum Task { Project, Sample, Date, Item };
        private string form_task = "";
        private Task current_task { get; set; }
        private Task previous_task { get; set; }

        private DataProcessor dp;
        private Utilties ut;
        private BindingSource bs;
        private DataTable dt;

        public Sample()
        {
            InitializeComponent();
            dp = new DataProcessor("DB");
            ut= new Utilties();
            bs = new BindingSource();
            dt = new DataTable();
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

        private void btnGenerateProjectNumbers_Click(object sender, EventArgs e)
        {
            AutoGenerateProjectNumber();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            UpdateSample(State.Cancel);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            UpdateSample(State.Delete);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            UpdateState(State.Edit);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            current_task = Task.Sample;
            UpdateSample(State.New);
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (current_state != State.Load)
            {
                current_task = Task.Sample;
                UpdateSample(State.Save);
            }
        }

        // Search
        private void btnSearch_Click(object sender, EventArgs e)
        {
            UpdateSample(State.Load);
        }

        //
        // N.B. UPDATE tbl_project w/ actual PROJECT_NUMBER
        private void AutoGenerateProjectNumber()
        {
            string tablename = "tbl_project";
            if (string.IsNullOrEmpty(this.project_number))
            {
                ProjectNumberGenerator pn = new ProjectNumberGenerator();
                this.project_number = pn.Generate(clientID, locationID, methodID, this.dtpCollected.Value.ToString());
                string query = "UPDATE " + tablename + " SET project_number = '" + this.project_number + "' WHERE project_pk = " + projectID + ";";
                dp.ProcessData("", query, tablename);
                UpdateSample(State.Navigate);
            }
        }

        private void CheckBox_Click(object sender, EventArgs e)
        {
            if (IsPopulating) { return; }
            try
            {
                int count = 0;
                CheckBox sendCheck = (CheckBox)sender;
                foreach (Control ctl in gbxSearch.Controls)
                {
                    if (ctl.Name.Substring(3).Contains(sendCheck.Name.Substring(3)))
                    {
                        ctl.Enabled = true; // enable all ctrls w/ parallel names
                        switch (ctl.Name.Substring(0, 3))
                        {
                            case "cbx":
                                CheckBox cbx = (CheckBox)ctl; // strange but this worsk (cannot declare new chbx after an if ... !
                                current_task = ConvertStringToTask(ctl.Name);
                                cbx.Checked = true;
                                break;
                            default: break;
                        }
                    }
                    else
                    { // only disable entry controls without parallel names
                        switch (ctl.Name.Substring(0, 3))
                        {
                            case "cbx":
                                CheckBox cbx = (CheckBox)ctl;
                                cbx.Checked = false;
                                break;
                            case "txt":
                            case "dtp":
                            case "cmb":
                                ctl.Enabled = false;
                                break;
                            default: //(but not buttons and labels)
                                ctl.Enabled = true;
                                break;
                        }
                    }
                    count++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "CheckBox Click Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // prepare for update
                this.IsDirty = (current_state == State.Edit);
            }
            UpdateState(current_state);
        }

        private void Combo_SelectedItemChanged(object sender, EventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            switch (cmb.Name)
            {
                case "cmbMethod":
                    methodID = dp.GetSelectedValue(cmbMethod);
                    break;
                case "cmbField":
                    fieldID = dp.GetSelectedValue(cmbField);
                    break;
                case "cmbZone":
                    zoneID = dp.GetSelectedValue(cmbZone);
                    break;
                case "cmbSite":
                    siteID = dp.GetSelectedValue(cmbSite);
                    break;
                default:
                    break;
            }
            IsDirty = (this.current_state == State.Edit);
            UpdateState(current_state);
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

                    if (ctl is Label)
                    {
                        string[] text = ctl.Text.Split(':');
                        ctl.Text = text[0];
                    }
                }
                lblDisplay.Text = "Sample";
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message, "Control Clear Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string ConvertTaskToString(Task task, bool IsTable = false)
        {
            this.form_task = "";
            switch (task)
            {
                case Task.Project:
                    form_task = (IsTable) ? "tbl_project" : "Project";
                    break;
                case Task.Date:
                    if (IsTable)
                    {
                        if (current_task == Task.Project)
                            form_task = "tbl_project";
                        else if (current_task == Task.Sample)
                            form_task = "tbl_sample";
                    }
                    else form_task = "Date";
                    break;
                case Task.Sample:
                    form_task = (IsTable) ? "tbl_sample" : "Sample";
                    break;
                case Task.Item:
                    form_task = (IsTable) ? "tbl_sample_items":"Item";
                    break;
                default:
                    break;
            }
            return form_task;
        }

        private Task ConvertStringToTask(string task)
        {
            string name = task.Substring(3);
            Task tempTask = Task.Project;
            switch (name)
            {
                case "Project":
                    //default
                    break;
                case "Date":
                    tempTask = Task.Date;
                    break;
                case "Sample":
                    tempTask = Task.Sample;
                    break;
                case "Item":
                    tempTask = Task.Item;
                    break;
                default:
                    break;
            }
            return tempTask;
        }

        private void dgvDisplay_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            object val;
            try
            {
                if (current_task == Task.Project)
                {
                    // crucial values ...
                    val = dgvDisplay.Rows[e.RowIndex].Cells["project_pk"].FormattedValue.ToString();
                    this.projectID = Convert.ToInt32(val);
                    val = dgvDisplay.Rows[e.RowIndex].Cells["client_fk"].FormattedValue.ToString();
                    this.clientID = Convert.ToInt32(val);
                    string date = dp.GetDataItem("SELECT date_initiated FROM tbl_project WHERE project_pk = " + projectID).ToString();
        
                }
                if (current_task == Task.Sample)
                {
                    val = dgvDisplay.Rows[e.RowIndex].Cells["sample_pk"].FormattedValue.ToString();
                    this.sampleID = Convert.ToInt32(val);
                }
                UpdateSample(State.Edit);
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message + " Error. Try again.", "DataGridView Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDisplay_KeyDown(object sender, KeyEventArgs e)
        {
            DataGridViewCellCancelEventArgs n = new DataGridViewCellCancelEventArgs(0, 0);
            if (e.KeyCode == Keys.F2)
                dgvDisplay_CellBeginEdit(sender, n);
            e.Handled = true;
        }

        private void dgvDisplay_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals(Keys.F2))
                IsEditingCell = true;
        }

        private void dgvDisplay_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue > 47 && e.KeyValue < 58)
            {
                IsDirty = true;
                e.Handled = true;
            }
        }

        public void dgvDisplay_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (IsEditingCell)
            {
                double d; // Validate after finished editing. 
                if (dgvDisplay.Rows[e.RowIndex].IsNewRow) { return; }
                object val = null;
                int itemID = 0;
                bool IsValue = false;

                switch (e.ColumnIndex)
                {
                    case 0:
                        if (dgvDisplay.Columns[e.ColumnIndex].Name.Contains("k")) // 
                        {
                            IsValue = false;
                        }
                        break;
                    case 1:
                        IsValue = true;
                        break;
                    default:
                        if (double.TryParse(e.ToString(), out d))
                        {
                            IsValue = true;
                        }
                        break;
                }
                if (IsValue)
                {
                    try
                    {
                        itemID = Convert.ToInt32(val);
                    }
                    catch (Exception ex)
                    {
                        e.Cancel = true;
                        MessageBox.Show(ex.Message + "Value must be numeric!", "Data Entry Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void dgvDisplay_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            IsEditingCell = true;
        }

        private void dgvDisplay_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            IsEditingCell = false;
        }

        private void dgvDisplay_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDisplay.Rows[e.RowIndex].IsNewRow) { return; }
            if (IsDirty && IsEditingCell && !String.IsNullOrEmpty(CellChangedValue))
            {
                string tablename = "";
                string colname = "";
                double d = 0;  // Validate after finished editing.
                double x;
                object val = null;
                int itemID = 0;
                string update = "";
                bool IsValue = false;
                colname = dgvDisplay.Columns[e.ColumnIndex].Name;
                if (colname.Contains("f")) // 
                    IsValue = false;
                else if (colname == "tbl_sample_pk")
                {
                    tablename = "tbl_sample";
                    val = dgvDisplay.Rows[e.RowIndex].Cells["sample_pk"].FormattedValue.ToString();
                    IsValue = true;
                }
                else
                {
                    if (double.TryParse(CellChangedValue, out x))
                        d = x;
                    tablename = "tbl_sample_items";
                    val = dgvDisplay.Rows[e.RowIndex].Cells["sample_item_pk"].FormattedValue.ToString();
                    IsValue = true;
                }
                if (IsValue)
                {
                    try
                    {
                        itemID = Convert.ToInt32(val);
                        update = "UPDATE " + tablename + " SET `" + colname + "` = " + d + " WHERE sample_item_pk = " + itemID;
                        dp.ProcessData("", update, tablename);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "Value must be numeric!", "Data Entry Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        IsDirty = false;
                    }
                }
            }
        }

        private void dgvDisplay_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            var row = dgvDisplay.Rows[e.RowIndex];
            this.CellChangedValue = (string)row.Cells[e.ColumnIndex].Value.ToString();
            IsDirty = true;
        }

        private void dgvDisplay_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            if (dgvDisplay.IsCurrentCellDirty)
                IsDirty = true;
        }

        private void dgvDisplay_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewTextBoxEditingControl)
            {
                DataGridViewTextBoxEditingControl tb = e.Control as DataGridViewTextBoxEditingControl;
                tb.KeyDown -= dgvDisplay_KeyDown;
                //tb.PreviewKeyDown -= dgvDisplay_PreviewKeyDown;
                tb.KeyDown += dgvDisplay_KeyDown;
                //tb.PreviewKeyDown += dgvDisplay_PreviewKeyDown;
            }
        }

        private string GetSampleSelectQuery(Task current_task)
        {
            string query = string.Empty;
            switch (current_task)
            {
                case Task.Project:
                    if (cbxDate.Checked)
                        query = "SELECT * FROM tbl_project WHERE date_initiated BETWEEN '" + dtpBeg_Date.Value + "' AND '" + dtpEnd_Date.Value + "';";
                    else if (cbxProject.Checked)
                        query = "SELECT * FROM tbl_project WHERE is_reported = false;";
                    break;
                case Task.Sample:
                    if (cbxSample.Checked)
                        query = "SELECT * FROM tbl_sample WHERE is_open = true;";
                    else if (projectID > 0)
                        query = "SELECT * FROM tbl_sample WHERE project_fk = " + projectID + ";";
                    break;
                case Task.Date:
                    query = "SELECT * FROM tbl_sample WHERE date_collected = BETWEEN '" + dtpBeg_Date.Value +  "' AND '" + dtpEnd_Date.Value + "';";
                    break;
                case Task.Item:
                    if (sampleID > 0)
                        query = "SELECT * FROM tbl_sample_items WHERE sample_fk = " + sampleID + ";";
                    break;
                default:
                    break;            
            }
            return query;
        }

        private void InitializeCombos()
        {
            try
            {
                string query = "";
                foreach (Control ctl in gbxSample.Controls)
                {
                    if (ctl is ComboBox)
                    {
                        ComboBox comboBox = (ComboBox)ctl;
                        switch (comboBox.Name)
                        {
                            case "cmbMethod":
                                query = "SELECT concat(acronym, ' -- ',method_desc) AS display, method_pk AS value FROM tbl_method";
                                break;
                            case "cmbField":
                                query = "SELECT field_desc AS display, field_pk AS value FROM tbl_field;";
                                break;
                            case "cmbZone":
                                query = "SELECT zone_desc AS display, zone_pk AS value FROM tbl_zone;";
                                break;
                            case "cmbSite":
                                query = "SELECT site_desc AS display, site_pk AS value FROM tbl_site";
                                break;
                            case "cmbPrice":
                                query = "SELECT amount AS display, price_pk AS value FROM tbl_price";
                                break;
                            case "cmbSample_Type":
                                query = "SELECT sample_type_desc AS display, sample_type_pk AS value FROM tbl_sample_type";
                                break;
                            default: break;
                        }
                        dp.PopulateCombo(query, comboBox, "display", "value");
                    }
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message, "Initialize Combos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            cmbSample_Type.SelectedIndex = 1;
        }

        private void InitializeControls()
        {
            try
            {
                current_task = Task.Project;
                cbxProject.Checked = true;
                InitializeCombos();

                dtpBeg_Date.Value = DateTime.Now.SubtractBusinessDays(30);
                dtpEnd_Date.Value = DateTime.Now;
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message, "Initialize Controls Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

                UpdateSample(State.Load);
            }
        }

        private void DataControlsStatusChanged()
        {
            foreach (Button btn in gbxDataControls.Controls)
            {
                if (btn.Enabled == true)
                {
                    btn.BackColor = Color.LightGreen;
                }
                else
                {
                    btn.BackColor = Color.Gray;
                }
            }
            lblPosition.Text = (current_row + 1) + " of " + (dgvDisplay.RowCount - 1);
        }

        private void PopulateForm(string query = "")
        {
            string company_name = "";
            string project_number = "";
            string message = "";
            string tablename = ConvertTaskToString(current_task);
            try
            {
                if(query.Length ==  0) 
                {
                    query = GetSampleSelectQuery(current_task);
                }
                if (current_state == State.Load)
                {
                    dp.PopulateDataGridView(query, dgvDisplay, bs, tablename); // crucial to make a new object
                    bs.MoveFirst();
                }
                IsPopulating = true;
                // datagridview 
                this.current_row = ut.SetRange(bs.Position, 0, dgvDisplay.Rows.Count);                
                DataGridViewRow row = dgvDisplay.Rows[current_row];
                dgvDisplay.ClearSelection();
                row.Selected = true;

                this.projectID = Convert.ToInt32(row.Cells["project_pk"].Value.ToString());
                this.clientID = Convert.ToInt32(row.Cells["client_fk"].Value.ToString());
                company_name = dp.GetStringFromDB("SELECT company_name FROM tbl_company WHERE company_pk = " + clientID);
                gbxDisplay.Text = " Report for Client :: " + company_name;
                 project_number = row.Cells["project_number"].Value.ToString();
                btnGenerateProjectNumbers.Visible = (project_number.Length == 0);

                // populate sample controls w/ sample data
                sampleID = dp.GetID("SELECT sample_pk FROM tbl_sample WHERE project_fk = " + projectID + ";","sample","project_fk");
                if (sampleID > 0)
                {
                    message = "Samples available for project # " + projectID + ".";
                    lblTask.Text = message;
                }
                else
                {
                    message = "No samples for project " + projectID + ". Add New?";
                    lblTask.Text = message;
                    return;
                }

                DataTable st = new DataTable(); // sample table
                query = "SELECT * FROM tbl_sample WHERE project_fk = " + projectID + ";";
                st = dp.GetDataTable(query, "sample");
                int stCurrentRow = 0;
                int maxStRows = st.Rows.Count;

                DataRow stRow = st.Rows[stCurrentRow]; 

                this.sample_number = stRow["sample_number"].ToString();
                cbxSampleInOffice.Checked = Convert.ToBoolean(stRow["is_in_office"]);
                cbxSampleIsOpen.Checked = Convert.ToBoolean(stRow["is_open"]);
                cmbMethod.SelectedValue = Convert.ToInt32(stRow["method_fk"].ToString());
                // populate location values
                locationID = dp.GetID("SELECT location_pk FROM tbl_location WHERE client_fk = " + clientID + ";","location","client_fk");
                fieldID = dp.GetID("SELECT field_fk FROM tbl_location WHERE location_pk = " + locationID + ";","location","location_pk");
                zoneID = dp.GetID("SELECT zone_fk FROM tbl_location WHERE location_pk = " + locationID + ";","location","location_pk");
                siteID = dp.GetID("SELECT site_fk FROM tbl_location WHERE location_pk = " + locationID + ";", "location", "location_pk");
                string land_desc = dp.GetStringFromDB("SELECT land_desc FROM tbl_location WHERE location_pk = " + locationID + ";", "location_pk");
                sampleTypeID = Convert.ToInt32(stRow["sample_type_fk"].ToString());
                sampleTypeID = (sampleTypeID > -1)  ? sampleTypeID : 3; // default to "oil"
                cmbField.SelectedValue = fieldID;
                cmbZone.SelectedValue = zoneID;
                cmbSite.SelectedValue = siteID;
                cmbSample_Type.SelectedItem = sampleTypeID;
                mskDLS.Text = land_desc;
                txtSampleNotes.Text = stRow["sample_notes"].ToString();

                dtpCollected.Value = Convert.ToDateTime(stRow["date_collected"]);

                gbxDisplay.Text = "Sample Info for project #" + projectID + " :: Client: " + company_name + " :: Date Collected: " + dtpCollected.Value;
                gbxSample.Text = "Sample # " + sample_number;
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message, "Data Read Error -- Populate Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.IsPopulating = false;

                switch (tablename)
                {
                    case "project":
                        current_task = Task.Project; break;
                    case "sample":
                        current_task = Task.Sample; break;
                    case "sample_item":
                        current_task = Task.Item;
                        break;
                }
                current_state = previous_state;
                UpdateState(current_state);
            }
        }


        private void Sample_KeyPress(object sender, KeyPressEventArgs e)
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
                UpdateSample(State.Save);
            }
        }

        private void Sample_KeyUp(object sender, KeyEventArgs e)
        {
            Control ctl = sender as Control;
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (ctl.Name == "txtSample_Number")
                        UpdateSample(State.Navigate);
                    else if (IsDirty)
                    {
                        current_task = Task.Sample;
                        ConvertTaskToString(current_task);
                        UpdateSample(State.Save);
                    }
                    break;
                case Keys.F2:
                    UpdateState(State.Edit);
                    break;
            }
        }

        public void StoreFocusedCombo(object sender, EventArgs e)
        {
            if (sender != null)
            {
                this.last_focused = (ComboBox)sender;
            }
        }

        private bool UpdateSample(State status, string query = "")
        {
            try
            {
                string tablename = ConvertTaskToString(current_task, true);
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
                        if (current_task == Task.Project)
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
                            case Task.Project:
                                if (clientID == 0)
                                    throw new Exception("No client ID!");
                                if (locationID == 0)
                                {
                                    locationID = dp.GetID("SELECT location_pk FROM tbl_location WHERE client_fk = " + clientID + " AND site_fk = " + cmbSite.SelectedValue + ";");
                                    if (locationID == 0)
                                    { // add a new location
                                        query = "INSERT INTO tbl_location VALUES (0," + clientID + "," + cmbField.SelectedValue + "," + cmbZone.SelectedValue + ","
                                            + cmbSite.SelectedValue + ",'" + mskDLS.Text + "',null);";
                                        locationID = dp.ProcessData("", query, "location");
                                    }
                                }
                                else if (locationID == 0)
                                {
                                    throw new Exception("No location ID!");
                                }
                                if (methodID == 0)
                                {
                                    methodID = dp.GetSelectedValue(cmbMethod);
                                }
                                else if (methodID == 0)
                                {
                                    throw new Exception("Choose a method!");
                                }
                                break;
                            case Task.Sample: // TODO: update location data, esp land_desc. 23May116
                                SampleNumberGenerator sn = new SampleNumberGenerator();
                                if (String.IsNullOrEmpty(this.sample_number))
                                    this.sample_number = sn.Generate();

                                query = "UPDATE " + tablename + " SET method_fk = " + this.methodID + ", location_fk = " +
                                    this.locationID + ", sample_number = '" + this.sample_number + "',date_collected = '" + dtpCollected.Value +
                                    "',sample_notes = '" + txtSampleNotes.Text + "', is_in_office = " + cbxSampleInOffice.Checked +
                                    ", is_open = " + cbxSampleIsOpen.Checked + " WHERE sample_pk = " + sampleID + ";";
                                // save to tbl_sample   
                                this.sampleID = dp.ProcessData("", query, tablename);
                                if (locationID > 0)
                                {
                                    // save to tbl_location (fall through)
                                    query = "UPDATE tbl_location SET field_fk = " + cmbField.SelectedValue + ", zone_fk = " + cmbZone.SelectedValue +
                                         ", site_fk = " + cmbSite.SelectedValue + ", land_desc = '" + mskDLS.Text + "' WHERE location_pk = " + locationID + ";";
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
                    form_State = "Load";
                    status = State.Navigate;
                    break;
                case State.Navigate:
                    form_State = "Navigate";
                    btnNew.Enabled = current_state == State.Navigate;
                    btnEdit.Enabled = (projectID > 0);
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
                    btnDelete.Enabled = false;
                    btnSearch.Enabled = true;
                    gbxSample.Enabled = false;
                    gbxSearch.Enabled = true;
                    break;
                case State.Cancel:
                    form_State = "Cancel";
                    btnNew.Enabled = true;
                    btnEdit.Enabled = true;
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
                    btnDelete.Enabled = false;
                    break;
                case State.Edit:
                    form_State = "Edit";
                    btnSave.Enabled = IsDirty;
                    btnCancel.Enabled = true;
                    btnDelete.Enabled = true;
                    btnNew.Enabled = false;
                    btnEdit.Enabled = false;
                    gbxSample.Enabled = true;
                    gbxSearch.Enabled = false;
                    break;
                case State.New:
                    form_State = "New";
                    btnDelete.Enabled = false;
                    btnNew.Enabled = false;
                    btnEdit.Enabled = false;
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                    this.sample_saved = false; // insert sample and populate sample fields
                    gbxSample.Enabled  = true;
                    gbxSearch.Enabled = false;
                    break;
                case State.Save:
                    form_State = "Save";
                    btnSave.Enabled = false;
                    break;
                default:
                    break;
            }
            this.current_state = status;
            lblDisplay.Text = "Sample :: " + form_State;
            //lblTask.Text = "Task :: " + ConvertTaskToString(current_task);
            DataControlsStatusChanged();
        }
    }
}