using DataObjects;
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
        private int rowCount = 0;

        private bool sample_saved;
        private bool UpdatingProject = false; //stop double checkbox clicks
        private bool UpdatingDate = false; //stop double checkbox clicks
        private bool UpdatingSample = false; //stop double checkbox clicks

        private bool IsEditingCell = false;
        private bool IsDirty = false;

        private DataProcessor dp;
        private Utilties utilities;

        enum State { Load, Navigate, New, Edit, Save, Cancel, Delete };
        private string form_State;
        private State state { get; set; }

        enum Task { Project, Sample, Date, Item };
        private string form_task = "";
        private Task current_task;


        public Sample()
        {
            InitializeComponent();
            dp = new DataProcessor("DB");
            utilities = new Utilties();
            UpdateState(State.Load);
        }

        // Scrolll
        private void btnBeg_Click(object sender, EventArgs e)
        {
            rowCount = 0;
            dgvDisplay.ClearSelection();
            dgvDisplay.Rows[rowCount].Selected = true;
            PopulateForm(tablename);
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            rowCount = dgvDisplay.Rows.Count - 2;
            dgvDisplay.ClearSelection();
            dgvDisplay.Rows[rowCount].Selected = true;
            PopulateForm(tablename);
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            rowCount = utilities.SetRange(--rowCount, 0, dgvDisplay.Rows.Count - 2);
            dgvDisplay.ClearSelection();
            dgvDisplay.Rows[rowCount].Selected = true;
            PopulateForm(tablename);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            rowCount = utilities.SetRange(++rowCount, 0, dgvDisplay.Rows.Count - 2);
            dgvDisplay.ClearSelection();
            dgvDisplay.Rows[rowCount].Selected = true;
            PopulateForm(tablename);
        }
    
        // Data
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

        // Search
        private void btnProject_Click(object sender, EventArgs e)
        {
            GetSelectQuery("project");
        }

        private void btnSample_Click(object sender, EventArgs e)
        {
            GetSelectQuery("sample");
        }

        private void btnSampleItem_Click(object sender, EventArgs e)
        {
            GetSelectQuery("sample_item");
        }

        //
        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UpdatingProject = cbxProject.Checked;
            UpdatingDate = cbxDate.Checked;
            UpdatingSample = cbxSample.Checked;

            CheckBox checkBox = (CheckBox)sender;
            switch (checkBox.Name)
            {
                case "cbxProject":
                    if (UpdatingProject)
                    {
                        cbxDate.Checked = cbxSample.Checked = !checkBox.Checked;
                        UpdatingProject = !UpdatingProject;
                    }
                    break;
                case "cbxDate":
                    if (state == State.Load)
                    {
                        cbxDate.Checked = false;
                    }
                    if (cbxDate.Checked)
                    {
                        dtpBeg.Visible = true;
                        dtpEnd.Visible = true;
                        lblBeg.Visible = true;
                        lblEnd.Visible = true;
                    }
                    else
                    {
                        dtpBeg.Visible = false;
                        dtpEnd.Visible = false;
                        lblBeg.Visible = false;
                        lblEnd.Visible = false;
                    }
                    if (UpdatingDate)
                    {
                        cbxProject.Checked = cbxSample.Checked = !checkBox.Checked;
                        UpdatingDate = !UpdatingDate;
                    }
                    break;
                case "cbxSample":
                    if (UpdatingSample)
                    {
                        cbxDate.Checked = cbxProject.Checked = !checkBox.Checked;
                        UpdatingSample = !UpdatingSample;
                    }
                    break;
            }
            //IsUpdating = !IsUpdating;
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

        private void dgvDisplay_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
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
                        if (task == Task.Project)
                        {
                            val = dgvDisplay.Rows[e.RowIndex].Cells["project_pk"].FormattedValue.ToString();
                            this.projectID = Convert.ToInt32(val);
                        }
                        break;
                    case State.New:
                        InitializeControls();
                        break;
                    case State.Save: // get sample items
                        if (task == Task.Project)
                        {
                            //tbl_sample: sample_pk, batch_fk, project_fk, method_fk, location_fk, sample_type_fk, sample_number, date_collected, sample_notes, is_in_office, is_open (11 fields)
                            val = dgvDisplay.Rows[e.RowIndex].Cells["project_pk"].FormattedValue.ToString();
                            this.projectID = Convert.ToInt32(val);
                            query = "INSERT INTO tbl_sample VALUES (0,null," + projectID + "," + methodID + "," + locationID + ",;" + sample_number + "','" + dtpCollected.Value +
                                "','" + txtSampleNotes.Text + "'," + cbxSampleInOffice.Checked + "," + cbxSampleIsOpen.Checked + ");";
                        }
                        else if (task == Task.Sample)
                        {
                            // tbl_sample_items: sample_item_pk, sample_fk, Tem0(oC), pH, S.G, Silica, Conductivity, TDSm, TDSc, Ohms, T-Hard, T-Alk, T-Acid, T-solids, Oil, Resid, Dissolved (17 fields)
                            val = dgvDisplay.Rows[e.RowIndex].Cells["sample_pk"].FormattedValue.ToString();
                            this.sampleID = Convert.ToInt32(val);
                            query = "iNSERT INTO tbl_sample_items VALUES (0," + sampleID + "null,null,null,null,null,null,null,null,null,null,null,null,null,null,null;)";
                        }
                        break;                      
                    case State.Navigate:
                        if (task == Task.Project)
                        {
                            // crucial values ...
                            val = dgvDisplay.Rows[e.RowIndex].Cells["project_pk"].FormattedValue.ToString();
                            this.projectID = Convert.ToInt32(val);
                            val = dgvDisplay.Rows[e.RowIndex].Cells["client_fk"].FormattedValue.ToString();
                            this.clientID = Convert.ToInt32(val);
                            string date = dp.GetDataItem("SELECT date_initiated FROM tbl_project WHERE project_pk = " + projectID).ToString();
                            // sample
                            GetSelectQuery("sample");
                            UpdateTask(Task.Sample);
                        }
                        else if (task == Task.Sample)
                        {
                            val = dgvDisplay.Rows[e.RowIndex].Cells["sample_pk"].FormattedValue.ToString();
                            this.sampleID = Convert.ToInt32(val);
                            if (state == State.Navigate)
                            {
                                GetSelectQuery("sample");
                            }
                            // sample_items
                            GetSelectQuery("sample_items");
                            UpdateTask(Task.Item);
                        }
                        else
                        {
                            GetSelectQuery("sample_items");
                            UpdateTask(Task.Item);
                        }
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
                this.IsDirty = (state == State.Edit);
            }
        }


        private string ConvertTaskToString(Task task)
        {
            this.form_task = "";
            switch (task)
            {
                case Task.Number:
                    form_task = "Project";
                    break;
                case Task.Date:
                    form_task = "Date";
                    break;
                case Task.Client:
                    form_task = "Client";
                    break;
                case Task.Open:
                    form_task = "Open";
                    break;
                default:
                    break;
            }
            return form_task;
        }

        private Task ConvertStringToTask(string task)
        {
            string name = task.Substring(3);
            Task tempTask = Task.Number;
            switch (name)
            {
                case "Project":
                    //alread initialized
                    break;
                case "Date":
                    tempTask = Task.Date;
                    break;
                case "Client":
                    tempTask = Task.Client;
                    break;
                case "Open":
                    tempTask = Task.Open;
                    break;
                default:
                    break;
            }
            return tempTask;
        }

        private void GetSelectQuery(string tablename)
        {
            string query = string.Empty;
            this.tablename = tablename;
            if (state != State.Load)
            {
                try
                {
                    switch (tablename)
                    {
                        case "project":
                            if (cbxDate.Checked)
                                query = "SELECT * FROM tbl_project WHERE date_initiated BETWEEN '" + dtpBeg.Value + "' AND '" + dtpEnd.Value + "'";
                            else if (cbxProject.Checked)
                                query = "SELECT * FROM tbl_project WHERE is_reported = false;";
                            task = Task.Project;
                            break;
                        case "sample":
                            if (cbxSample.Checked)
                                query = "SELECT * FROM tbl_sample WHERE is_open = true;";
                            else if (projectID > 0)
                                query = "SELECT * FROM tbl_sample WHERE project_fk = " + projectID + ";";
                            task = Task.Sample;
                            break;
                        case "sample_item":
                            if (sampleID > 0)
                                query = "SELECT * FROM tbl_sample_items WHERE sample_fk = " + sampleID + ";";
                            else
                                throw (new Exception("No items for this sample. Create new!"));
                            task = Task.Item;
                            break;
                        case "item":
                            break;
                        default:
                            // saved snippet for sample items
                            //query = "SELECT i.sample_item_pk, m.method_item, i.value1, u.unit_desc " +
                            //    "FROM tbl_sample_item i " +
                            //    "INNER JOIN tbl_sample t ON i.sample_fk = t.sample_pk " +
                            //    "INNER JOIN tbl_method_item m ON i.method_item_fk = m.method_item_pk " +
                            //    "INNER JOIN tbl_method_type y ON i.method_type_fk = y.type_pk " +
                            //    "INNER JOIN tbl_unit u on i.unit_1_fk = u.unit_pk " +
                            //    "WHERE (i.method_item_fk <> 1 && i.method_type_fk = 1) && sample_fk = " + this.projectID; // method_item_fk = 1 (blank)
                            break;
                    }
                    PopulateForm(tablename);
                    dp.PopulateDataGridView(query, dgvDisplay, tablename);
                    if (dp.RowsAffected == 0)
                    {
                        switch (task)
                        {
                            case Task.Project:
                                if (cbxDate.Checked)
                                    throw new Exception("No projects for this date range.");
                                else
                                    throw new Exception("No open projects available.");
                            case Task.Sample:
                                throw new Exception("No open samples for this project.");
                            case Task.Item:
                                throw new Exception("No items for this sample. Create New.");
                        }
                    }
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.Message, "Query read error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    UpdateTask(task);
                }
            }
        }

        private void InitializeControls(bool checktheboxes = false)
        {
            try
            {
                string query = "";
                query = "SELECT concat(acronym, ' -- ',method_desc) AS display, method_pk AS value FROM tbl_method";
                dp.PopulateCombo(query, cmbMethod, "display", "value");
                query = "SELECT field_desc AS display, field_pk AS value FROM tbl_field;";
                dp.PopulateCombo(query, cmbField, "display", "value");
                query = "SELECT zone_desc AS display, zone_pk AS value FROM tbl_zone;";
                dp.PopulateCombo(query, cmbZone, "display", "value");
                query = "SELECT site_desc AS display, site_pk AS value FROM tbl_site";
                dp.PopulateCombo(query, cmbSite, "display", "value");
                query = "SELECT amount AS display, price_pk AS value FROM tbl_price";
                dp.PopulateCombo(query, cmbPrice, "display", "value");
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message, "Initialize Controls Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dtpBeg.Value = DateTime.Now.SubtractBusinessDays(30);
                dtpEnd.Value = DateTime.Now;
                CheckBox_CheckedChanged(cbxDate, EventArgs.Empty);
                if(checktheboxes) 
                { 
                    cbxSampleInOffice.Checked = true;
                    cbxSampleIsOpen.Checked = true;
                }
                lblTask.Text = "";
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
        }

        private void PopulateForm(string tablename)
        {
            string query = "SELECT * FROM tbl_";
            try
            {
                switch (tablename)
                {
                    case "project":
                        query += tablename + " INNER JOIN tbl_company ON client_fk = company_pk WHERE ";
                        if (clientID == 0)
                            query += "is_reported = false;";
                        else
                            query += "company_pk = " + clientID + ";";
                        break;
                    case "sample":
                        if (task == Task.Project)
                        {
                            query += tablename + " WHERE project_fk = " + projectID + ";";
                        }
                        else
                        {
                            query += tablename + "c.company_pk, c.company_name, p.project_number, s.method_fk, s.sample_number, s.sample_type_fk, " +
                                "s.date_collected, s.is_in_office, s.is_open, s.sample_notes,location_fk, " +
                                "l.field_fk, l.zone_fk, l.site_fk, l.land_desc FROM " + tablename + 
                                " INNER JOIN tbl_sample_type t ON s.sample_type_fk = t.sample_type_pk " +
                                "INNER JOIN tbl_project p ON s.project_fk = p.project_pk " +
                                "INNER JOIN tbl_location l ON s.location_fk = l.location_pk " +
                                "INNER JOIN tbl_company c ON p.client_fk = c.company_pk WHERE ";
                            if (sampleID > 0)
                                query += "sample_pk = " + sampleID;
                            else
                                query += "is_open = true";
                        }
                        break;
                    case "sample_item":
                        query = "SELECT * FROM tbl_sample_items WHERE sample_pk = " + sampleID;
                        break;
                    default:
                        break;
                }
                if (!String.IsNullOrEmpty(query))
                {
                    DataTable dt = dp.GetDataTable(query, tablename);
                    switch (tablename)
                    {
                        case "project":
                            foreach (DataRow row in dt.Rows)
                            {
                                foreach (DataColumn col in dt.Columns)
                                {
                                    this.clientID = Convert.ToInt32(row["client_fk"]);
                                    this.projectID = Convert.ToInt32(row["project_pk"]);

                                    string dateVal = dp.GetDateString(row["date_initiated"].ToString());
                                    gbxDisplay.Text = " Client :: " + row["company_name"].ToString() + " :: Opened: " + dateVal;
                                    gbxSample.Text = "Sample Info for project #" + projectID;
                                }
                            }
                            break;

                        case "sample":
                            foreach (DataRow row in dt.Rows)
                            {
                                foreach (DataColumn col in dt.Columns)
                                {
                                    this.clientID = Convert.ToInt32(row["client_fk"]);
                                    gbxSearch.Text = "Sample: " + row["sample_number"].ToString() + " :: Client: " + row["company_name"].ToString() + " :: Date Collected: " + row["date_collected"].ToString();
                                    gbxDisplay.Text = row["sample_number"].ToString();
                                    cbxSampleInOffice.Checked = Convert.ToBoolean(row["is_in_office"]);
                                    cbxSampleIsOpen.Checked = Convert.ToBoolean(row["is_open"]);
                                    cmbMethod.SelectedItem = row["method_fk"];
                                    cmbField.SelectedItem = row["field_fk"];
                                    cmbZone.SelectedItem = row["zone_fk"];
                                    cmbSite.SelectedItem = row["site_fk"];
                                    mskDLS.Text = row["land_desc"].ToString();
                                    dtpCollected.Value = Convert.ToDateTime(row["date_collected"]);
                                    txtSampleNotes.Text = row["sample_notes"].ToString();
                                }
                            }
                            break;
                        case "sample_items":
                            break;
                        default:
                            break;
                    }
                }
            }

            catch (Exception x)
            {
                MessageBox.Show(x.Message, "Data Read Error -- Populate Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                switch (tablename)
                {
                    case "project":
                        task = Task.Project; break;
                    case "sample":
                        task = Task.Sample; break;
                    case "sample_item":
                        task = Task.Item;
                        break;
                }
                UpdateTask(task);
            }
        }

        public void StoreFocusedCombo(object sender, EventArgs e)
        {
            if (sender != null)
            {
                this.last_focused = (ComboBox)sender;
            }
        }

        private bool UpdateSample(State status)
        {
            try
            {
                string query = "";
                // N.B. UPDATE tbl_project w/ actual PROJECT_NUMBER
                switch (status)
                {
                    case State.Load:
                        break;
                    case State.Cancel:
                        InitializeControls();                        break;

                    case State.Save:
                        if (task == Task.Project)
                        {
                            if (clientID == 0)
                                throw new Exception("No client ID!");
                            if (locationID == 0)
                            {
                                locationID = dp.GetID("SELECT location_pk FROM tbl_location WHERE client_fk = " + clientID + " AND site_fk = " + cmbSite.SelectedValue + ";");
                                if(locationID == 0)
                                {
                                    query = "SET FOREIGN_KEY_CHECKS=0;INSERT INTO tbl_location VALUES (0," + clientID + "," + cmbField.SelectedValue + "," + cmbZone.SelectedValue + "," + cmbSite.SelectedValue + 
                                      ",'" + mskDLS.Text + "',null);SET FOREIGN_KEY_CHECKS=1";
                                    locationID = dp.ProcessData("", query, "location");
                                }
                            }
                            else if(locationID == 0)
                            {
                                throw new Exception("No location ID!");
                            }
                            if (methodID == 0)
                            {
                                methodID = dp.GetSelectedValue(cmbMethod);
                            }
                            else if(methodID == 0)
                                throw new Exception("No method ID!");
                            tablename = "tbl_project";
                            ProjectNumberGenerator pn = new ProjectNumberGenerator();
                            this.project_number = pn.Generate(clientID, locationID, methodID);
                            query = "UDATE tbl_project SET project_number = '" + this.project_number + "' WHERE project_pk = " + projectID + ";";
                            dp.ProcessData("", query, tablename);
                        }
                        //tbl_sample: sample_pk, batch_fk, project_fk, method_fk, location(site)_fk, sample_number, date_collected, sample_notes, is_in_office, Is_open (10 fields)..
                        if (task == Task.Sample)
                        {
                            tablename = "tbl_sample";
                            SampleNumberGenerator sn = new SampleNumberGenerator();
                            this.sample_number = sn.Generate();
                            query = "SET foreign_key_checks=0;INSERT INTO tbl_sample VALUES (0,null," + this.projectID + "," + methodID + "," + locationID + ",'" + sample_number +
                                "','" + dtpBeg.Value + "','" + txtSampleNotes.Text + "'," + cbxSampleInOffice.Checked + "," + cbxSampleIsOpen.Checked + ");SET foreign_key_checks=1;";
                            this.sampleID = dp.ProcessData("", query, tablename); // ProcessData: select, query, tablename (to identify error source)
                            sample_saved = (sampleID > 0);
                        }
                        else if (task == Task.Item)
                        {
                            tablename = "tbl_sample_items";
                            query = "INSERT INTO tbl_sample_items VALUES (0," + sampleID + "null,null,null,null,null,null,null,null,null,null,null,null,null,null,null);";
                            this.sampleItemID = dp.ProcessData("", query, tablename); // ProcessData: select, query, tablename (to identify error source)
                            sample_saved = (sampleItemID > 0);
                        }
                        break;
                    case State.New:
                        InitializeControls();
                        break;
                    case State.Edit:
                        if (task == Task.Project)
                        {
                            tablename = "tbl_project";
                            query = "UPDATE tbl_project SET project_number = '" + project_number + "' WHERE project_pk = " + projectID + ";";
                        }
                        else if (task == Task.Sample)
                        {
                            tablename = "tbl_sample";
                            //tbl_sample: sample_pk, batch_fk, project_fk, location(site)_fk, sample_number, date_collected, sample_notes, is_in_office, Is_open (9 fields)..
                            query = "UPDATE tbl_sample SET batch_pk = null, project_fk = " + projectID + ",location_fk = " + locationID + "," +
                                ", sample_number = '" + sample_number + "',date_collected = '" + dtpBeg.Value + "', sample_notes = '" + txtSampleNotes.Text +
                                "',is_in_office = " + cbxSampleInOffice.Checked + ",is_open = " + cbxSampleIsOpen + " WHERE sample_pk = " + sampleID + ";";
                        }
                        else
                        {
                            // sample item data updated in the datagrid
                        }
                        dp.ProcessData("", query, tablename);
                        break;
                    default:
                        break;
                }
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
                    MessageBox.Show("Saved!", "Form State", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                    status = State.Navigate;
                    UpdateState(status);
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
            this.state = status;
            lblDisplay.Text = "Sample :: " + form_State;
            UpdateSample(status);
            DataControlsStatusChanged();
            UpdateTask(task);
        }

        private void UpdateTask(Task temp)
        {
            task = temp;
            switch (task)
            {
                case Task.Project:
                    process = "Project";
                    break;
                case Task.Sample:
                    process = "Sample";
                    break;
                case Task.Item:
                    process = "Item";
                    break;
                default: break;
            }
            lblTask.Text = form_task;
            if (task == Task.Project)
                gbxDataControls.Text = "Sample";
            else             
                gbxDataControls.Text = form_task;            
        }
    }
}