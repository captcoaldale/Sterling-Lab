using DataObjects;
using Microsoft.AnalysisServices.AdomdClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static System.Net.WebRequestMethods;

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

        enum Task { Project, Sample, Date, Item };
        private string form_task = "";
        private Task current_task;

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
    
        // Data
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
            UpdateSample(State.Edit);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            UpdateSample(State.New);
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (current_state != State.Load)
                UpdateSample(State.Save);
        }

        // Search
        private void btnSearch_Click(object sender, EventArgs e)
        {
            GetSampleSelectQuery(current_task);
        }

        //
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
            //string query = "";
            //object val;
            try
            {
                switch (current_state)
                {
                    //case State.Load:
                    //    break;
                    //case State.Edit:
                    //    if (current_task == Task.Project)
                    //    {
                    //        val = dgvDisplay.Rows[e.RowIndex].Cells["project_pk"].FormattedValue.ToString();
                    //        this.projectID = Convert.ToInt32(val);
                    //    }
                    //    break;
                    //case State.Save: // get sample items
                    //    if (current_task == Task.Project)
                    //    {
                    //        //tbl_sample: sample_pk, batch_fk, project_fk, method_fk, location_fk, sample_type_fk, sample_number, date_collected, sample_notes, is_in_office, is_open (11 fields)
                    //        val = dgvDisplay.Rows[e.RowIndex].Cells["project_pk"].FormattedValue.ToString();
                    //        this.projectID = Convert.ToInt32(val);
                    //        query = "INSERT INTO tbl_sample VALUES (0,null," + projectID + "," + methodID + "," + locationID + ",;" + sample_number + "','" + dtpCollected.Value +
                    //            "','" + txtSampleNotes.Text + "'," + cbxSampleInOffice.Checked + "," + cbxSampleIsOpen.Checked + ");";
                    //    }
                    //    else if (current_task == Task.Sample)
                    //    {
                    //        // tbl_sample_items: sample_item_pk, sample_fk, Tem0(oC), pH, S.G, Silica, Conductivity, TDSm, TDSc, Ohms, T-Hard, T-Alk, T-Acid, T-solids, Oil, Resid, Dissolved (17 fields)
                    //        val = dgvDisplay.Rows[e.RowIndex].Cells["sample_pk"].FormattedValue.ToString();
                    //        this.sampleID = Convert.ToInt32(val);
                    //        query = "iNSERT INTO tbl_sample_items VALUES (0," + sampleID + "null,null,null,null,null,null,null,null,null,null,null,null,null,null,null;)";
                    //    }
                    //    break;                      
                    //case State.Navigate:
                    //    if (current_task == Task.Project)
                    //    {
                    //        // crucial values ...
                    //        val = dgvDisplay.Rows[e.RowIndex].Cells["project_pk"].FormattedValue.ToString();
                    //        this.projectID = Convert.ToInt32(val);
                    //        val = dgvDisplay.Rows[e.RowIndex].Cells["client_fk"].FormattedValue.ToString();
                    //        this.clientID = Convert.ToInt32(val);
                    //        string date = dp.GetDataItem("SELECT date_initiated FROM tbl_project WHERE project_pk = " + projectID).ToString();
                    //        // sample
                    //        GetSampleSelectQuery(current_task);

                    //    }
                    //    else if (current_task == Task.Sample)
                    //    {
                    //        val = dgvDisplay.Rows[e.RowIndex].Cells["sample_pk"].FormattedValue.ToString();
                    //        this.sampleID = Convert.ToInt32(val);
                    //        if (current_state == State.Navigate)
                    //            UpdateSample(current_state);
 
                    //        // sample_items
                    //        current_task = task.;
                    //        UpdateSample(current_state);
                    //    }
                    //    else
                    //    {
                    //        GetSampleSelectQuery(current_task);
                    //        UpdateSample(Task.Item);
                    //    }
                    //    break;
                    default:
                        break;
                }
                UpdateSample(current_state);
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
                        query = "SELECT * FROM tbl_project WHERE date_initiated BETWEEN '" + dtpBeg.Value + "' AND '" + dtpEnd.Value + "'";
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
                    query = "SELECT * FROM tbl_sample WHERE date_collected = BETWEEN '" + dtpBeg.Value +  "' AND '" + dtpEnd.Value + "';";
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
        }

        private void InitializeControls()
        {
            try
            {
                current_state = State.Load;
                current_task = Task.Project;
                cbxProject.Checked = true;
                InitializeCombos();

                dtpBeg.Value = DateTime.Now.SubtractBusinessDays(30);
                dtpEnd.Value = DateTime.Now;

                gbxSample.Visible = false;

            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message, "Initialize Controls Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                UpdateSample(current_state);
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
            try
            {
                if (current_state == State.Load || bs == null)
                {
                    return;
                }
                IsPopulating = true;
                if (dgvDisplay.Rows.Count == 0 || query.Length > 0)
                {
                    query = GetSampleSelectQuery(current_task);
                    dp.PopulateDataGridView(query, dgvDisplay, "project"); // crucial to make a new object
                    this.bs.DataSource = dgvDisplay.DataSource;
                    bs.MoveFirst();
                }

                // datagridview 
                this.current_row = ut.SetRange(bs.Position, 0, dgvDisplay.Rows.Count);
                this.IsPopulating = true;
                DataGridViewRow row = dgvDisplay.Rows[current_row];
                dgvDisplay.ClearSelection();
                row.Selected = true;

                this.projectID = Convert.ToInt32(row.Cells["project_pk"].Value.ToString());
                this.clientID = Convert.ToInt32(row.Cells["client_fk"].Value.ToString());
                string company_name = dp.GetStringFromDB("SELECT company_name FROM tbl_company WHERE company_pk = " + clientID);

                switch (current_task)
                {
                    case Task.Project:
                        DateTime dateVal = Convert.ToDateTime(row.Cells["date_initiated"].Value.ToString());
                        gbxDisplay.Text = " Report for Client :: " + company_name + " :: Opened: " + dateVal;
                        gbxSample.Text = "Sample Info for project #" + projectID;
                        string project_number = row.Cells["project_number"].Value.ToString();
                        //if (project_number.Length < 1)
                        //    UpdateSample(State.Edit);
                        break;
                    case Task.Sample:
                        dateVal = Convert.ToDateTime(row.Cells["date_collected"].Value.ToString());
                        gbxSearch.Text = "Sample: " + row.Cells["sample_number"].ToString() + " :: Client: " + company_name + " :: Date Collected: " + dateVal;
                        gbxDisplay.Text = row.Cells["sample_number"].Value.ToString();
                        cbxSampleInOffice.Checked = Convert.ToBoolean(row.Cells["is_in_office"].Value);
                        cbxSampleIsOpen.Checked = Convert.ToBoolean(row.Cells["is_open"].Value);
                        cmbMethod.SelectedValue = Convert.ToInt32(row.Cells["method_fk"].Value.ToString());
                        cmbField.SelectedValue = Convert.ToInt32(row.Cells["field_fk"].Value.ToString());
                        cmbZone.SelectedValue = Convert.ToInt32(row.Cells["zone_fk"].Value.ToString());
                        cmbSite.SelectedValue = Convert.ToInt32(row.Cells["site_fk"].Value.ToString());
                        mskDLS.Text = row.Cells["land_desc"].ToString();
                        dtpCollected.Value = dateVal;
                        txtSampleNotes.Text = row.Cells["sample_notes"].ToString();
                        break;
                    case Task.Item:
                        break;
                    default:
                        break;
                }
                
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
                UpdateState(current_state);
            }
        }

        public void StoreFocusedCombo(object sender, EventArgs e)
        {
            if (sender != null)
            {
                this.last_focused = (ComboBox)sender;
            }
        }

        private void AutoUpdateProjectNumbers()
        {
            // N.B. UPDATE tbl_project w/ actual PROJECT_NUMBER
            ProjectNumberGenerator pn = new ProjectNumberGenerator();
            this.project_number = pn.Generate(clientID, locationID, methodID);
            string query = "UDATE " + tablename + " SET project_number = '" + this.project_number + "' WHERE project_pk = " + projectID + ";";
        }

        private bool UpdateSample(State status, string query = "")
        {
            try
            {
                string query_beg = "SET FOREIGN_KEY_CHECKS=0;";
                string query_end = "SET FOREIGN_KEY_CHECKS = 1;";
                string tablename = ConvertTaskToString(current_task,true);
                switch (status)
                {
                    case State.Load:
                        query = GetSampleSelectQuery(current_task);
                        current_state = State.Navigate;
                        break;
                    case State.New:                        
                    case State.Cancel:
                        InitializeControls(); 
                        break;
                    case State.Edit:
                        break;

                        case State.Delete:
                            break;
                    case State.Save: 
                        if (current_task == Task.Project)
                        {
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
                                throw new Exception("Choose a method!");
                            
                            // N.B. UPDATE tbl_project w/ actual PROJECT_NUMBER
                            ProjectNumberGenerator pn = new ProjectNumberGenerator(); 
                            this.project_number = pn.Generate(clientID, locationID, methodID);
                            query = "UDATE " + tablename + " SET project_number = '" + this.project_number + "' WHERE project_pk = " + projectID + ";";
                        }
                        //tbl_sample: sample_pk, batch_fk, project_fk, method_fk, location(site)_fk, sample_number, date_collected, sample_notes, is_in_office, Is_open (10 fields)..
                        if (current_task == Task.Sample)
                        {
                            SampleNumberGenerator sn = new SampleNumberGenerator();
                            this.sample_number = sn.Generate();
                            query = "INSERT INTO " + tablename + " VALUES (0,null," + this.projectID + "," + methodID + "," + locationID + ",'" + sample_number +
                                "','" + dtpBeg.Value + "','" + txtSampleNotes.Text + "'," + cbxSampleInOffice.Checked + "," + cbxSampleIsOpen.Checked + ");";
                        }
                        else if (current_task == Task.Item)
                        {
                            query = "INSERT INTO " + tablename + " VALUES (0," + sampleID + "null,null,null,null,null,null,null,null,null,null,null,null,null,null,null);"; // 17 cols

                        }
                        break;
   
                    default:
                        break;
                }
                int id = dp.ProcessData("", query_beg + query + query_end, tablename);
                sample_saved = (id > 0);
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
                    MessageBox.Show("Saved!", "Form Sample", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                    btnNew.Enabled = true;
                    btnEdit.Enabled = false;
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
                    btnDelete.Enabled = false;
                    gbxSample.Enabled = false;
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
                    gbxSample.Enabled  = true;
                    break;
                case State.Save:
                    form_State = "Save";
                    break;
                default:
                    break;
            }
            this.current_state = status;
            lblDisplay.Text = "Sample :: " + form_State;
            lblTask.Text = "Task :: " + ConvertTaskToString(current_task);
            DataControlsStatusChanged();
        }
    }
}