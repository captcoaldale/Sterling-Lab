using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using DataObjects;
using System.Windows.Forms;
using System.Diagnostics.Eventing.Reader;
using System.Diagnostics;
using System.Linq;
using Spire.Pdf.Exporting.XPS.Schema;
using System.Diagnostics.Contracts;
using System.CodeDom;

namespace Sterling_Lab
{
    public partial class Project : System.Windows.Forms.UserControl
    {
        private int agentID;
        private int clientID;
        private string company_name ="";
        private int objectiveID;
        private int priceID = 5; // standard 190.00
        private int priorityID;
        private int projectID;
        private string file_name;
        private string project_number;

        private int projectTypeID;
        private ComboBox last_focused;
        private bool IsDirty = false; // keys pressed
        private bool IsPopulating = false;
        private bool IsSearching = false;
        private bool project_saved;

        private int currentRow;

        private DataProcessor dp;
        private Utilties ut;
        private BindingSource bs;
        private DataTable dt;

        enum State { Load, Navigate, New, Edit, Save, Cancel, Delete };
        private string form_State;
        private State state { get; set; }

        enum Task { Client, Date, ID, Number, Open }
        private string form_task;
        private Task current_task { get; set; }
        private Task previous_task { get; set; }

        public Project()
        {
            InitializeComponent();
            dp = new DataProcessor("DB");
            ut = new Utilties();
            bs = new BindingSource();
            dt = new DataTable();

            this.state = State.Load;
            InitializeControls();
        }

        // Scroll
        private void btnBeg_Click(object sender, EventArgs e)
        {
            bs.MoveFirst();
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
    
        private void btnEnd_Click(object sender, EventArgs e)
        {
            bs.MoveLast();
            PopulateForm();
        }
        // Data

        private void btnNew_Click(object sender, EventArgs e)
        {
            UpdateProject(State.New);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            UpdateState(State.Edit); // don't change anything until Save!
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsDirty)
            {
                ConvertTaskToString(Task.ID);
                UpdateProject(State.Save);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            UpdateProject(State.Delete);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            UpdateProject(State.Cancel);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            IsSearching = true;
            if (current_task == Task.Number)
                UpdateProject(State.Navigate); // prepare to enter project number
            else
                UpdateProject(State.Load);
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
                                if (current_task == Task.Number)
                                {
                                    ClearControls();
                                }
                                cbx.Checked = true;
                                IsSearching = true;
                                break;
                            default: 
                                break;
                        }
                    }
                    else
                    { // only disable entry controls without parallel names
                        switch (ctl.Name.Substring(0,3))
                        {
                            case "cbx":
                                CheckBox cbx = (CheckBox)ctl;
                                cbx.Checked = false;
                                break;
                            case "txt":
                            case "dtp":
                            case "cmb":
                                //if (current_task == Task.Open)
                                //    ctl.Enabled = true;
                                //else
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
                ConvertTaskToString(current_task);
            }
        }

        private void ClearControls()
        {
            try
            {
                foreach (GroupBox gbxctl in new GroupBox[] { gbxSearch, gbxProject, gbxBill_Client })
                {
                    foreach (Control ctl in gbxctl.Controls)
                    {
                        if (ctl is ComboBox || ctl is TextBox)
                        {
                            if (ctl.Name == "txtProject_Number")
                                ctl.Text = "Type 3-4 chars of Proj Num.";
                                else
                                ctl.Text = "";
                        }
                        if (ctl is DateTimePicker)
                        {
                            DateTimePicker dtp = (DateTimePicker)ctl;

                            if (dtp.Name == "Date_Report_Expected")
                                dtp.Value = DateTime.Now.AddBusinessDays(5);
                            else
                                dtp.Value = DateTime.Now;
                        }
                        if(ctl is CheckBox)
                        {
                            CheckBox checkBox = (CheckBox)ctl;
                            checkBox.Checked = false;
                        }
                    }
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message, "Clear Controls", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Text = "Project ";
            }
        }

        // set global and/or populate form
        private void Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.state == State.Load || IsPopulating)
                return;
            string name = "";
            try
            {
                StoreFocusedCombo(sender, e);
                ComboBox comboBox = (ComboBox)sender;
                name = comboBox.Name.Remove(0, 3).ToLower();
                switch (name)
                {
                    case "client":
                        this.clientID = Convert.ToInt32(comboBox.SelectedValue);
                        cbxClient.Checked = true;
                        break;
                    case "agent":
                        this.agentID = Convert.ToInt32(comboBox.SelectedValue);
                        break;
                    case "objective":
                        this.objectiveID = Convert.ToInt32(comboBox.SelectedValue);
                        break;
                    case "project_type":
                        this.projectTypeID = Convert.ToInt32(comboBox.SelectedValue);
                        break;
                    case "priority_requested":
                        this.priorityID = Convert.ToInt32(comboBox.SelectedValue);
                        break;
                    default:
                        break;
                }
            }            
            catch(Exception x)
            {
                MessageBox.Show(x.Message, "ID Read Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (name == "client")
            {
                cmbAgent.Enabled = (state == State.Edit);
                ConvertTaskToString(Task.Client);                
                UpdateProject(State.Load);
            }
            else if(state == State.Edit) // changes to other combos need saving 
            {
                ConvertTaskToString(Task.Number);
                IsDirty = true;
                UpdateState(state); // Combo Selected Index Changed.
            }
        }

        // ensure valid IDs
        private void Combo_Validating(object sender, CancelEventArgs e)
        {
            if (state != State.Edit)
                return;
            ComboBox comboBox = (ComboBox)sender;
            string name = comboBox.Name.Remove(0, 3).ToLower(); // trim prefix (txt..., cmb...)
            if (IsDirty && ((state == State.Edit || state == State.Save)))
            {                
                try
                {
                    switch (name)
                    {
                        case "client":
                            if(clientID <= 0)
                                throw new Exception("Choose a client!");
                            break;
                        case "agent":
                            if (agentID <= 0)
                                throw new Exception("Choose an agent!");
                            break;
                        case "objective":
                            if (objectiveID <= 0)
                                throw new Exception("Choose an objective!");
                            break;
                        case "project_type":
                            if (projectTypeID <= 0)
                                throw new Exception("Choose a project type!");
                            break;
                        case "priority_requested":
                            if (priorityID <= 0)
                                throw new Exception("Choose a priority!");
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.Message, "Combo Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private string ConvertTaskToString(Task task)
        {
            this.form_task = "";
            previous_task = current_task;
            switch (task)
            {
                case Task.Client:
                    form_task = "Client";
                    break;
                case Task.Date:
                    form_task = "Date";
                    break;
                case Task.ID:
                    form_task = "ID";
                    break;
                case Task.Number:
                    form_task = "Number";
                    break;
                case Task.Open:
                    form_task = "Open";
                    break;
                default:
                    break;
            }
            this.current_task = task;
            return form_task;
        }

        private Task ConvertStringToTask(string task)
        { 
            previous_task = this.current_task;
           
            string name = task.ToString().Substring(3);
            Task tempTask = Task.Number;
            switch (name)
            {
                case "Client":
                    tempTask = Task.Client;
                    break;
                case "Date":
                    tempTask = Task.Date;
                    break;
                case "ID":
                    tempTask = Task.ID;
                    break;
                case "Open":
                    tempTask = Task.Open;
                    break;
                case "Number":
                    //Project (Task.Number) already initialized
                    break;
                default:
                    break;
            }
            this.current_task = tempTask;
            return tempTask;
        }

        private void DataControlsStatusChange()
        {
            foreach (Button btn in gbxDataControls.Controls)
            {
                if (btn.Enabled == true)
                {
                    btn.BackColor = Color.LightCyan;
                }
                else
                {
                    btn.BackColor = Color.Gray;
                }
            }
            lblPosition.Text = (currentRow + 1) + " of " + (dgvDisplay.RowCount - 1);
            if (!IsPopulating)
            {
                //cbxNumber.Checked = true;
                //cbxOpen.Checked = false;
            }
        }

        private void dgvDisplay_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            bs.Position = e.RowIndex; // sync binding source to gridrow
            this.projectID = Convert.ToInt32(dgvDisplay.Rows[e.RowIndex].Cells["project_pk"].FormattedValue.ToString());
            previous_task = current_task;
            current_task = Task.ID; // projects in dgv
            ConvertTaskToString(current_task);    
            UpdateProject(State.Navigate);
        }

        private void dgvDisplay_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //if (IsPopulating)
            //    return;
            //if(projectID < 1)
            //    projectID = Convert.ToInt32(dgvDisplay.Rows[e.RowIndex].Cells["project_pk"].FormattedValue.ToString());
            //UpdateProject(this.state);
        }


        private void DatePicker_ValueChanged(object sender, EventArgs e)
        {
            if (state == State.Load)
                return;
            try
            { 
                DateTimePicker dtp = (DateTimePicker)sender;
                switch(dtp.Name)
                {
                    case "dtpBeg":
                    case "dtpEnd":
                        current_task = Task.Date;
                        ConvertTaskToString(current_task);
                        break;
                    default:
                        if (state == State.Edit)
                            this.IsDirty = true;
                        break;
                }
            }
            catch(Exception x)
            {
                MessageBox.Show(x.Message,"DatePicker Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                UpdateProject(state);
            }
        }

        private void GetAgentForClient(int clientid)
        {
            if (state == State.Navigate)
            {
                string query = "SELECT p.personnel_pk AS value, concat(p.first_name, p.last_name,' -- ',o.office_desc) as display " +
                    "FROM tbl_personnel p " +
                    "INNER JOIN tbl_project r ON r.agent_fk = p.personnel_pk " +
                    "INNER JOIN  tbl_company  c ON r.client_fk = c.company_pk " +
                    "INNER JOIN  tbl_office  o ON p.office_fk = o.office_pk " +
                        "WHERE c.company_pk = " + clientid;
                dp.PopulateCombo(query, cmbAgent, "display", "value");
            }
        }

        private string GetProjectSelectQuery(Task current_task, bool UseLike = false)
        {
            //if (state == State.Load)
            //    return null;
            string query = "SELECT p.project_pk, p.client_fk, c.company_name AS client_desc, p. agent_fk, CONCAT(l.first_name,' ',l.last_name) AS agent_desc, " +
                "p.file_name, p.project_number, p.project_type_fk, t.project_type_desc, p.objective_fk, o.objective_desc, p.priority_requested_fk, r.priority_desc AS priority_requested_desc, " +
                "p.date_initiated, p.date_report_expected, p.is_reported, p.bill_cust, " +
                "p.bill_cust_notes, p.project_notes FROM tbl_project p " +
                "INNER JOIN tbl_company c ON p.client_fk = c.company_pk " +
                "INNER JOIN tbl_personnel l ON p.agent_fk = l.personnel_pk " +
                "INNER JOIN tbl_priority r ON p.priority_requested_fk = r.priority_pk " +
                "INNER JOIN tbl_project_type t ON p.project_type_fk = t.project_type_pk " +
                "INNER JOIN tbl_objective o ON p.objective_fk = o.objective_pk " +
                "WHERE ";

            switch (current_task)
            {
                case Task.Client:
                    query += "client_fk = " + clientID + ";";
                    break;
                case Task.Date:
                    query += "date_initiated BETWEEN '" + dtpBegDate.Value + "' AND '" + dtpEndDate.Value + "';";
                    break;
                case Task.Open:
                    query += "is_reported = false;";
                    break;
                case Task.Number:
                    if (UseLike)
                        query += "project_number LIKE " + '"' + '%' + txtProject_Number.Text + '%' + '"' + ";";
                    break;
                case Task.ID:
                        query += "project_pk = " + projectID + ";";
                    break;
                default:
                    break;
            }                     
            return query;
        }

        private void InitializeCombos(GroupBox gbx)
        {
            foreach (Control ctl in gbx.Controls)
            {
                if (ctl is ComboBox)
                {
                    string prompt = "";
                    string query = "";
                    ComboBox cmb = (ComboBox)ctl;
                    switch (cmb.Name)
                    {
                        case "cmbClient":
                            query = "SELECT company_pk AS value, company_name AS display FROM tbl_company WHERE is_client = true";
                            prompt = "Select Client";
                            break;
                        case "cmbAgent":
                            query = "SELECT p.personnel_pk AS value, concat(p.first_name,' ',p.last_name,' -- ',o.office_desc) " +
                                "AS display " +
                                "FROM tbl_personnel p " +
                                "INNER JOIN tbl_office o " +
                                "ON p.office_fk = o.office_pk " +
                                "WHERE p.function_fk = 5";
                            prompt = "Select Agent";
                            break;
                        case "cmbProject_Type":
                            query = "SELECT project_type_pk AS value, project_type_desc AS display FROM tbl_project_type;";
                            prompt = "Select Project Type";
                            break;
                        case "cmbObjective":
                            query = "SELECT objective_pk AS value, objective_desc AS display FROM tbl_objective;";
                            prompt = "Select Project Objective";
                            break;
                        case "cmbPriority_Requested":
                            query = "SELECT priority_pk  AS value, priority_desc AS display FROM tbl_priority;";
                            prompt = "Select Project Priority";
                            break;
                        default:
                            break;
                    }

                    if (!string.IsNullOrEmpty(query))
                    {
                        dp.PopulateCombo(query, (ComboBox)ctl, "display", "value");
                        // set properties
                        cmb.Text = prompt;
                        //cmb.ForeColor = Color.SteelBlue;
                    }
                }
            }
        }

        private void InitializeControls()
        {
            if(state != State.Load) { return; }
            try
            {
                // time
                dtpBegDate.Value = DateTime.Now.SubtractBusinessDays(30);
                dtpEndDate.Value = DateTime.Now;
                dtpDate_Initiated.Value = DateTime.Now;
                dtpDate_Report_Expected.Value = DateTime.Now.AddBusinessDays(5);

                // combos
                InitializeCombos(gbxSearch);
                InitializeCombos(gbxProject);

                //default search 
                current_task = Task.Open;
                ConvertTaskToString(current_task);
                foreach (Control ctl in gbxSearch.Controls)
                {
                    if (ctl is CheckBox) 
                    {
                        CheckBox checkBox = (CheckBox)ctl;
                        checkBox.Checked = false;
                    }
                    else
                        ctl.Enabled = false;
                }
                cbxOpen.Checked = true;
                IsSearching = true;

                txtProject_Number.Text = "Enter Project Number";

                UpdateProject(state);
            }
            catch (Exception ex) 
            { 
                MessageBox.Show(ex.Message,"Initialize Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }   
            finally
            {
                IsPopulating = false;
            }
        }

        //tbl_project: project_pk, file_name, project_number (generated in UC Sample),client_fk,agent_fk, project_type_fk, objective_fk,priority_requested_fk,
        //date_initiated, date_reported_expected, is_reported,  bill_cust, bill_notes, proj_notes (14 fields)
        private void PopulateForm(string query = "")
        {
            IsPopulating = true;
            try
            {
                if (dgvDisplay.Rows.Count == 0 || state == State.Load)
                {
                    //dgvDisplay.DataSource = null; // start fresh
                    query = GetProjectSelectQuery(current_task);
                    //MessageBox.Show(query, "SQL", MessageBoxButtons.OK,MessageBoxIcon.Information); // Used b/c debug crashed w/ dates
                    // populate the datagridview and sync w/ form controls
                    state = State.Navigate;
                    dp.PopulateDataGridView(query, dgvDisplay,bs, "project"); // crucial to make a new object
                }
                if (bs.Count > 0)
                {
                    // adjust datagridview rows
                    this.currentRow = ut.SetRange(bs.Position, 0, dgvDisplay.Rows.Count);
                    dgvDisplay.ClearSelection();
                    dgvDisplay.Rows[currentRow].Selected = true;

                    //populate form controls
                    this.projectID = Convert.ToInt32(dgvDisplay.Rows[currentRow].Cells["project_pk"].Value.ToString());
                    this.file_name = dgvDisplay.Rows[currentRow].Cells["file_name"].Value.ToString();
                    this.project_number = dgvDisplay.Rows[currentRow].Cells["project_number"].Value.ToString();
                    this.clientID = Convert.ToInt32(dgvDisplay.Rows[currentRow].Cells["client_fk"].Value.ToString());
                    this.agentID = Convert.ToInt32(dgvDisplay.Rows[currentRow].Cells["agent_fk"].Value.ToString());
                    this.objectiveID = Convert.ToInt32(dgvDisplay.Rows[currentRow].Cells["objective_fk"].Value.ToString());
                    this.priorityID = Convert.ToInt32(dgvDisplay.Rows[currentRow].Cells["priority_requested_fk"].Value.ToString());
                    this.projectTypeID = Convert.ToInt32(dgvDisplay.Rows[currentRow].Cells["project_type_fk"].Value.ToString());
                    string date_initiated = dgvDisplay.Rows[currentRow].Cells["date_initiated"].Value.ToString();

                    cmbClient.SelectedValue = clientID;
                    cmbAgent.SelectedValue = agentID;
                    //cmbAgent.Enabled = true;
                    cmbObjective.SelectedValue = objectiveID;
                    cmbProject_Type.SelectedValue = projectTypeID;
                    cmbPriority_Requested.SelectedValue = priorityID;
                    if (current_task != Task.Number)
                        txtProject_Number.Text = dgvDisplay.Rows[currentRow].Cells["project_number"].Value.ToString();
                    txtBill_Cust.Text = dgvDisplay.Rows[currentRow].Cells["bill_cust_notes"].Value.ToString();
                    txtProject_Notes.Text = dgvDisplay.Rows[currentRow].Cells["project_notes"].Value.ToString();

                    cbxBill_Cust.Checked = Convert.ToBoolean(dgvDisplay.Rows[currentRow].Cells["bill_cust"].Value);
                    cbxIs_Reported.Checked = Convert.ToBoolean(dgvDisplay.Rows[currentRow].Cells["is_reported"].Value);

                    object company = dp.GetDataItem("SELECT company_name FROM tbl_company WHERE company_pk = " + this.clientID + ";");
                    if (company != null)
                    {
                        this.company_name = company.ToString();
                    }
                    gbxDisplay.Text = "Project #" + projectID + " :: Client :: " + company_name + " :: Project Opened: " + date_initiated;
                    gbxProject.Text = "Details for Project #" + projectID;
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message, "Data Read Error -- Populate Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                query = "";
                IsPopulating = false;
                IsSearching = false;
                state = State.Navigate;
                UpdateState(state);
            }
        }

        private void Project_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetterOrDigit(e.KeyChar))
            {
                if (!IsDirty && state == State.Edit)
                {
                    IsDirty = true;
                    UpdateState(state);
                }
            }
            else if(e.KeyChar == (char)Keys.Return) // enter key
            {
                //current_task = Task.Number;
                ConvertTaskToString(current_task);
                UpdateProject(State.Save);
            }
        }

        private void Project_KeyUp(object sender, KeyEventArgs e)
        {
            Control ctl = sender as Control;
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (ctl.Name == "txtProject_Number")
                    {
                        current_task = Task.Number;
                        UpdateProject(State.Navigate, true);
                    }
                    else if (IsDirty)
                    {
                        ConvertTaskToString(current_task);
                        UpdateProject(State.Save);
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


        private void txtProject_Number_Enter(object sender, EventArgs e)
        {
            if (txtProject_Number.Text.Length > 0)
            {
                txtProject_Number.Font = new Font("Lucida Bright", 12, FontStyle.Regular);
                txtProject_Number.Text = "";
            }
        }

        private void txtProject_Number_Leave(object sender, EventArgs e)
        {
            if (txtProject_Number.Text.Trim() == "")
            {
                txtProject_Number.Font = new Font("Lucida Bright", 12, FontStyle.Italic);
                txtProject_Number.Text = "Type 3-4 chars of Proj Num";
            }
        }
        // tbl_project: project_pk, file_name, project_number, client_fk, agent_fk, project_type_fk, objective_fk, date_initiated,date_report_expected,
        // is_reported, priority_requested_fk, billCust, bill_cust_notes, project_notes (14 fields)

        // N.B. in this UC we supply null values for PROJECT_NUMBER AND FILE_NAME -- these values will be added in the SAMPLE UserControl where we have access to them.
        private void UpdateProject(State status, bool UseLike = false, string query = "")
        {
            int rows_affected = 0;
            try
            {
                if ((current_task == Task.Client && clientID == 0) || current_task == Task.Number)
                {
                    UseLike = true;
                }
                switch (status)
                {
                    case State.Load:
                        break;
                    case State.Edit:
                    case State.Navigate:
                        if (current_task == Task.Number) // by project_NUMBER
                        {
                            if (IsSearching && !txtProject_Number.Text.ContainsAny("Enter","Type"))
                            {
                                query = GetProjectSelectQuery(current_task, true);
                            }
                            break;
                        }                           
                        else // get by projectID
                            query = GetProjectSelectQuery(Task.ID);
                        break;
                    case State.Cancel:
                        ClearControls();
                        status = State.Navigate;
                        break;
                    case State.New:
                        query = "INSERT INTO tbl_project VALUES (0,null,null,"
                           + this.clientID + "," + agentID + "," + cmbProject_Type.SelectedValue + "," + cmbObjective.SelectedValue + "," + cmbPriority_Requested.SelectedValue + ",'"
                           + dtpDate_Initiated.Value + "','" + dtpDate_Report_Expected.Value.AddBusinessDays(5) + "'," + cbxIs_Reported.Checked + "," + cbxBill_Cust.Checked + ",'"
                           + txtBill_Cust.Text + "','" + txtProject_Notes.Text + "');";
                        current_task = Task.Client;
                        break;
                    case State.Save:
                        query = "UPDATE tbl_project SET file_name = '" + file_name + "', project_number = '" + txtProject_Number.Text + "', client_fk = " + clientID + ", agent_fK = " + agentID +
                            ", project_type_fk = " + projectTypeID + ", objective_fk = " + objectiveID + ", date_initiated = '" + dtpDate_Initiated.Value + "', date_report_expected = '" +
                            dtpDate_Report_Expected.Value + "', is_reported = " + cbxIs_Reported.Checked + ", priority_requested_fk = " + priorityID + ", bill_cust = " + cbxBill_Cust.Checked +
                            ", bill_cust_notes = '" + txtBill_Cust.Text + "', project_notes = '" + txtProject_Notes.Text + "' WHERE ";
                        switch (current_task)
                        {
                            case Task.ID:
                                query += "project_pk = " + projectID;
                                break;
                            case Task.Client:
                                query += "client_fk = " + clientID;
                                break;
                        }
                        query += ";";
                        break;
                    case State.Delete:
                        query = "DELETE FROM tbl_project WHERE project_pk = " + projectID + ";";
                        break;
                    default:
                        break;
                }
                // process new, update, delete
                string tablename = "project";
                switch (status)
                {
                    case State.Navigate: // pass query on to PopulateForm()
                    case State.Edit:
                            break;
                    case State.Delete:
                        dp.ProcessData("",query, tablename);
                        rows_affected = dp.RowsAffected;
                        if (rows_affected > 0)
                            MessageBox.Show("Project #" + projectID + " removed.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        status = State.Load;
                        break;
                    case State.New:
                        if(clientID > 0)
                            this.projectID = dp.ProcessData("", query, tablename);
                        if (projectID > 0)
                        {
                            query = GetProjectSelectQuery(Task.Client);
                            status = State.Load;
                        }
                        break;
                    case State.Save:      
                        if ((projectID > 0 || clientID > 0) && (query.Length > 0)) // Update Project
                        {
                            this.projectID = dp.ProcessData("", query, tablename); // parms: select, query, tablename (to identify error source)
                            rows_affected = dp.RowsAffected;
                            if (project_saved = (projectID > 0))
                            {
                                MessageBox.Show("Project Saved!", "Form State", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            else
                                throw new Exception("Project not saved!");
                            break;
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message, "Project Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally // update form values
            {
                if(project_saved)
                {
                    IsDirty = false;
                    query = "";
                    dgvDisplay.DataSource = null;
                    current_task = previous_task;
                    status = State.Load;
                }
                this.state = status;
            }
            PopulateForm(); // refresh w/ new info
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
                    btnEdit.Enabled = bs.DataSource != null;
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
                    btnDelete.Enabled = (projectID > 0);
                    btnSearch.Enabled = true;
                    gbxBill_Client.Enabled = false;
                    gbxProject.Enabled = false;
                    gbxDataControls.Enabled = true;
                    break;
                case State.Cancel:
                    form_State = "Cancel";
                    btnNew.Enabled = true;
                    btnEdit.Enabled = true;
                    btnSearch.Enabled = true;
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
                    btnDelete.Enabled = false;
                    gbxDataControls.Enabled = true;
                    break;
                case State.Delete: 
                    form_State = "Delete";
                    btnNew.Enabled = false;
                    btnEdit.Enabled = false;
                    btnSave.Enabled = false;
                    btnCancel.Enabled = true;
                    btnDelete.Enabled = true;
                    gbxDataControls.Enabled = false;
                    break;
                case State.Edit:
                    form_State = "Edit";
                    btnSave.Enabled = IsDirty;
                    btnCancel.Enabled = true;
                    btnDelete.Enabled = true;
                    btnNew.Enabled = false;
                    btnEdit.Enabled = false;
                    btnSearch.Enabled=true;
                    gbxProject.Enabled = true;
                    gbxBill_Client.Enabled = true;
                    gbxDataControls.Enabled = true;
                    cmbAgent.Enabled = true; // (state == State.Edit); // && (current_task == Task.Client));
                    this.project_saved = false;
                    break;
                case State.New:
                    form_State = "New";
                    btnNew.Enabled = false;
                    btnEdit.Enabled = false;
                    btnCancel.Enabled = true;
                    btnDelete.Enabled = false;
                    btnSave.Enabled = false;
                    btnSearch.Enabled = false;
                    gbxProject.Enabled = true;
                    gbxBill_Client.Enabled = true;
                    gbxDataControls.Enabled = true;
                    this.project_saved = false;
                    break;
                case State.Save:
                    form_State = "Save";
                    btnNew.Enabled = false;
                    btnEdit.Enabled = false;
                    btnCancel.Enabled = true;
                    btnDelete.Enabled = false;
                    btnSave.Enabled = false;
                    btnSearch.Enabled = false;
                    gbxProject.Enabled = false;
                    gbxBill_Client.Enabled = false;
                    gbxDataControls.Enabled = false;
                    break;
                default:
                    break;
            }
            this.state = status;
            lblDisplay.Text = "Project :: " + form_State;
            gbxNavControls.Enabled = (state == State.Navigate);
            DataControlsStatusChange();                
        }
    }
}
