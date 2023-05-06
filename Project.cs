using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using DataObjects;
using System.Windows.Forms;
using System.Reflection;

namespace Sterling_Lab
{
    public partial class Project : UserControl
    {
        private int agentID;
        private int clientID;
        private string company_name ="";
        private int objectiveID;
        private int priceID = 5; // standard 190.00
        private int priorityID;
        private int projectID;
        private bool project_saved;
        private int projectTypeID;
        private ComboBox last_focused;
        private bool IsDirty = false; // keys pressed
        private bool IsPopulating = false;

        private int currentRow;

        private DataProcessor dp;
        private Utilties ut;
        private BindingSource bs;
        private DataTable dt;

        enum State { Load, Navigate, New, Edit, Save, Cancel, Delete };
        private string form_State;
        private State state { get; set; }

        enum Task { Client, Date, Project, Open }
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
            UpdateProject(State.Edit); // don't change anything until Save!
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsDirty)
            {
                current_task = Task.Project;
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

        private void cbxBillClient_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void ClearControls()
        {
            if(state == State.Load) {return;}
            try
            {
                foreach (GroupBox gbxctl in new GroupBox[] { gbxSearch, gbxProject, gbxBill_Client })
                {
                    foreach (Control ctl in gbxctl.Controls)
                    {
                        if (ctl is ComboBox || ctl is TextBox)
                        {
                            if (ctl.Text.Length > 0)
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

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (IsPopulating)
                {
                    //cbxByClient.Checked = cbxByDate.Checked = cbxByOpenProject.Checked = false;
                    return;
                }

                CheckBox checkBox = (CheckBox)sender;
                switch (checkBox.Name)
                {
                    case "cbxByClient":
                        if (cbxByClient.Checked)
                        {
                            this.clientID = dp.GetSelectedValue(cmbClient); // set the global
                            lblBeg.Visible = false;
                            lblEnd.Visible = false;
                            dtpBeg.Visible = false;
                            dtpEnd.Visible = false;
                            this.current_task = Task.Client;
                            this.previous_task = Task.Open;
                            cbxByDate.Checked = cbxByOpenProject.Checked = !checkBox.Checked;
                        }
                        break;
                    case "cbxByDate":
                        if (cbxByDate.Checked)
                        {
                            dtpBeg.Visible = true;
                            dtpEnd.Visible = true;
                            lblBeg.Visible = true;
                            lblEnd.Visible = true;
                            cbxByClient.Checked = cbxByOpenProject.Checked = !checkBox.Checked;
                            this.current_task = Task.Date;
                            this.previous_task = Task.Open;
                            break;
                        }
                        else
                        {
                            dtpBeg.Visible = false;
                            dtpEnd.Visible = false;
                            lblBeg.Visible = false;
                            lblEnd.Visible = false;
                        }
                        break;
                    case "cbxByOpenProject":
                        if (cbxByOpenProject.Checked)
                        {
                            cbxByDate.Checked = cbxByClient.Checked = !checkBox.Checked;
                            this.current_task = Task.Open;
                            this.previous_task = Task.Client;
                        }
                        break;
                        // for project cbxIs_Reported & cbxBill_Cust ...
                    default:
                        if(state == State.Edit)
                        {
                            IsDirty = true;
                        }
                        break;
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message, "Project Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                this.IsPopulating = false;
            }

            if (state == State.Navigate)
            {
                UpdateProject(state, GetProjectSelectQuery(current_task));
            }
            else if (IsDirty) // set buttons (don't update project) yet
            {
                UpdateProject(State.Edit);
            }
            else  // set buttons (don't update project) yet
            {
                UpdateProject(State.Navigate);
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
                        cbxByClient.Checked = true;
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
                current_task = Task.Client;
                state = State.Navigate;
                UpdateProject(state, GetProjectSelectQuery(current_task));
            }
            else if(state == State.Edit) // changes to other combos need saving 
            {
                current_task = Task.Project;
                IsDirty = true;
                UpdateState(state);
            }
        }

        // ensure valid IDs
        private void Combo_Validating(object sender, CancelEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            string name = comboBox.Name.Remove(0, 3).ToLower();
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
        }

        private void dgvDisplay_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            bs.Position = e.RowIndex; // sync binding source to gridrow
            if (projectID < 1)
                this.projectID = Convert.ToInt32(dgvDisplay.Rows[e.RowIndex].Cells["project_pk"].FormattedValue.ToString());
            current_task = Task.Project; // projects in dgv
            previous_task = Task.Client;
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

        private void dtpBeg_ValueChanged(object sender, EventArgs e)
        {
            if (state == State.Load)
                return;
            else if (state == State.Edit)
            {
                this.IsDirty = true;
            }
            UpdateProject(state);
        }

        private void dtpEnd_ValueChanged(object sender, EventArgs e)
        {
            if (state == State.Load)
                return;
            else if(state == State.Edit)
            {
                this.IsDirty = true;
            }
            UpdateProject(state);
        }

        private void dtpProjectInitiated_ValueChanged(object sender, EventArgs e)
        {
            if (state == State.Load)
                return;
            else if (state == State.Edit)
            {
                this.IsDirty = true;
            }
            UpdateProject(state);
        }

        private void dtpReportExpected_ValueChanged(object sender, EventArgs e)
        {
            if (state == State.Load)
                return;
            else if(state == State.Edit)
            {
                this.IsDirty = true;
            }
            UpdateProject(state);
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

        private string GetProjectSelectQuery(Task current_task)
        {
            //if (state == State.Load)
            //    return null;
            string query = "SELECT p.project_pk, p.client_fk, c.company_name AS client_desc, p. agent_fk, CONCAT(l.first_name,' ',l.last_name) AS agent_desc, " +
                "p.project_type_fk, t.project_type_desc, p.objective_fk, o.objective_desc, p.priority_requested_fk, r.priority_desc AS priority_requested_desc, " +
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
                    query += "date_initiated BETWEEN '" + dtpBeg.Value + "' AND '" + dtpEnd.Value + "'";
                    break;
                case Task.Open:
                    query += "is_reported = false;";
                    break;
                case Task.Project:
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
                //default search 
                this.IsPopulating = true; // shut down control change events
                current_task = Task.Open;
                cbxByOpenProject.Checked = true;
                
                // time
                dtpBeg.Value = DateTime.Now.SubtractBusinessDays(30);
                dtpEnd.Value = DateTime.Now;

                // controls
                InitializeCombos(gbxSearch);
                InitializeCombos(gbxProject);

                UpdateProject(State.Navigate);
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
        private void PopulateForm()
        {
            if(state == State.Load || bs == null) 
            { 
                return; 
            }
            IsPopulating = true;
            try
            {
                 if(dgvDisplay.Rows.Count == 0)
                 {
                    string query = GetProjectSelectQuery(current_task);
                    dp.PopulateDataGridView(query, dgvDisplay,"project"); // crucial to make a new object
                    this.bs.DataSource = dgvDisplay.DataSource;
                    bs.MoveFirst();
                 }

                // datagridview 
                dgvDisplay.ClearSelection();
                this.currentRow = ut.SetRange(bs.Position,0,dgvDisplay.Rows.Count);
                
                //populate form controls
                DataGridViewRow row = dgvDisplay.Rows[currentRow];
                row.Selected = true;
                
                this.projectID = Convert.ToInt32(row.Cells["project_pk"].Value.ToString());
                this.clientID = Convert.ToInt32(row.Cells["client_fk"].Value.ToString());
                this.clientID = Convert.ToInt32(row.Cells["client_fk"].Value.ToString());
                this.agentID = Convert.ToInt32(row.Cells["agent_fk"].Value.ToString());
                this.clientID = Convert.ToInt32(row.Cells["client_fk"].Value.ToString());
                this.clientID = Convert.ToInt32(row.Cells["client_fk"].Value.ToString());
                this.objectiveID = Convert.ToInt32(row.Cells["objective_fk"].Value.ToString());
                this.priorityID = Convert.ToInt32(row.Cells["priority_requested_fk"].Value.ToString());
                this.projectTypeID = Convert.ToInt32(row.Cells["project_type_fk"].Value.ToString());
                string date_initiated = row.Cells["date_initiated"].Value.ToString();

                cmbClient.SelectedValue = clientID;
                cmbAgent.SelectedValue = agentID;
                cmbObjective.SelectedValue = objectiveID;
                cmbProject_Type.SelectedValue = projectTypeID;
                cmbPriority_Requested.SelectedValue = priorityID;

                txtBill_Cust.Text = row.Cells["bill_cust_notes"].Value.ToString();
                txtProject.Text = row.Cells["project_notes"].Value.ToString();

                cbxBill_Cust.Checked = Convert.ToBoolean(row.Cells["bill_cust"].Value);
                cbxIs_Reported.Checked = Convert.ToBoolean(row.Cells["is_reported"].Value);

                object company = dp.GetDataItem("SELECT company_name FROM tbl_company WHERE company_pk = " + this.clientID + ";");
                if (company != null)
                {
                    this.company_name = company.ToString();
                }
                gbxDisplay.Text = "Project #" + projectID + " :: Client :: " + company_name + " :: Project Opened: " + date_initiated;
                gbxProject.Text = "Details for Project #" + projectID;
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message, "Data Read Error -- Populate Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                IsPopulating = false;
                UpdateState(state);
            }
        }

        private void Project_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetterOrDigit(e.KeyChar))
            {
                if (!IsDirty)
                {
                    IsDirty = true;
                    UpdateState(State.Edit);
                }
            }
            else if(e.KeyChar == (char)Keys.Return) // enter key
            {
                current_task = Task.Project;
                UpdateProject(State.Save);
            }
        }

        private void Project_KeyUp(object sender, KeyEventArgs e)
        {
           switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (IsDirty)
                    {
                        current_task = Task.Project;
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

        // tbl_project: project_pk, file_name, project_number, client_fk, agent_fk, project_type_fk, objective_fk, date_initiated,date_report_expected,
        // is_reported, priority_requested_fk, billCust, bill_cust_notes, project_notes (14 fields)

        // N.B. in this UC we supply null values for PROJECT_NUMBER AND FILE_NAME -- these values will be added in the SAMPLE UserControl where we have access to them.
        private void UpdateProject(State status, string query = "")
        {
            if (status == State.Load)
                return;
            int rows_affected = 0;
            try
            {
                switch (status)
                {
                    case State.Load:
                        state = State.Navigate;
                        break;
                    case State.Edit:
                        query = GetProjectSelectQuery(current_task);
                        break;
                    case State.Navigate:
                        query = GetProjectSelectQuery(current_task);
                        break;
                    case State.Cancel:
                        ClearControls();
                        status = State.Navigate;
                        break;
                    case State.New:
                        ClearControls();
                        query = "SET foreign_key_checks=0;INSERT INTO tbl_project VALUES (0,null,null,"
                           + this.clientID + "," + agentID + "," + cmbProject_Type.SelectedValue + "," + cmbObjective.SelectedValue + "," + cmbPriority_Requested.SelectedValue + ",'"
                           + dtpDate_Initiated.Value + "','" + dtpDate_Report_Expected.Value.AddBusinessDays(5) + "'," + cbxIs_Reported.Checked + "," + cbxBill_Cust.Checked + ",'"
                           + txtBill_Cust.Text + "','" + txtProject.Text + "');SET foreign_key_checks=1;";
                        break;
                    case State.Save:
                        query = "SET foreign_key_checks=0;UPDATE tbl_project SET file_name = null, project_number = null, client_fk = " + clientID + ", agent_fK = " + agentID +
                            ", project_type_fk = " + projectTypeID + ", objective_fk = " + objectiveID + ", date_initiated = '" + dtpDate_Initiated.Value + "', date_report_expected = '" +
                            dtpDate_Report_Expected.Value + "', is_reported = " + cbxIs_Reported.Checked + ", priority_requested_fk = " + priorityID + ", bill_cust = " + cbxBill_Cust.Checked +
                            ", bill_cust_notes = '" + txtBill_Cust.Text + "', project_notes = '" + txtProject.Text + "' WHERE ";
                        switch (current_task)
                        {
                            case Task.Project:
                                query += "project_pk = " + projectID;
                                break;
                            case Task.Client:
                                query += "client_fk = " + clientID;
                                break;
                        }
                        query += "; SET foreign_key_checks = 1;";
                        break;
                    case State.Delete:
                        query = "DELETE FROM tbl_project WHERE project_pk = " + projectID + ";";
                        break;
                    default:
                        break;
                }
                // only process new, update, delete
                switch (status)
                {
                    case State.New:
                    case State.Save:
                    case State.Delete:
                        if ((projectID > 0 || clientID > 0) && (query.Length > 0)) // Update Project
                        {
                            this.projectID = dp.ProcessData("", query, "project"); // parms: select, query, tablename (to identify error source)
                            rows_affected = dp.RowsAffected;
                            if (project_saved = (projectID > 0))
                            {
                                MessageBox.Show("Project Saved!", "Form State", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                IsDirty = false;
                                cbxByClient.Checked = cbxByDate.Checked = cbxByOpenProject.Checked = false;
                                status = State.Navigate;
                                current_task = Task.Open;
                                dgvDisplay.DataSource = null;
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
                    InitializeControls();                  
                    break;
                case State.Navigate:
                    form_State = "Navigate";
                    btnNew.Enabled = true;
                    btnEdit.Enabled = bs.DataSource != null;
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
                    btnDelete.Enabled = false;
                    gbxBill_Client.Enabled = false;
                    gbxProject.Enabled = false;
                    gbxDataControls.Enabled = true;
                    break;
                case State.Cancel:
                    form_State = "Cancel";
                    btnNew.Enabled = true;
                    btnEdit.Enabled = true;
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
                    gbxProject.Enabled = true;
                    gbxBill_Client.Enabled = true;
                    gbxDataControls.Enabled = true;
                    this.project_saved = false;
                    break;
                case State.New:
                    form_State = "New";
                    btnNew.Enabled = false;
                    btnEdit.Enabled = false;
                    btnCancel.Enabled = true;
                    btnDelete.Enabled = false;
                    btnSave.Enabled = false;
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
