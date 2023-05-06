using System;
using System.ComponentModel;
using System.Data;
using DataObjects;
using System.Windows.Forms;
using System.Management;
using System.Drawing;

namespace Sterling_Lab
{
    public partial class Client : UserControl
    {
        DataProcessor dp = new DataProcessor("DB");
        enum State { Load, Navigate, New, Edit, Save, Delete, Cancel };
        private string form_State;
        private State state { get; set; }
        private ComboBox last_focused; 
        bool IsDirty = false; // keys pressed
        public int clientID = -1;
        
        public Client()
        {
            InitializeComponent();
            this.state = State.Load;
            InitializeControls();
            UpdateState(State.Navigate);
        }

        private void ClearControls()
        {
            foreach (Control ctl in this.Controls)
            {
                if (ctl is ComboBox)
                {
                    var cmb = (ComboBox)ctl;
                    cmb.SelectedIndex = -1;
                }
            }
            this.Text = "Client";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            OnStateChanged(State.Cancel);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.state = (this.state == State.Edit) ? State.Edit : State.Save ;
            Combo_Validating(last_focused, new CancelEventArgs());
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            OnStateChanged(State.Edit);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            OnStateChanged(State.New);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnStateChanged(State.Delete);
        }

        private void Combo_KeyUp(object sender, KeyEventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            if (e.KeyCode == Keys.Enter)
            {
                if(IsDirty == true)
                {
                    int index = cmb.SelectedIndex;
                    this.state = (state == State.New) ? State.Save : State.Edit;
                    Combo_Validating(sender, new CancelEventArgs());
                }
            }
            else
            {
                IsDirty = true;
                //OnStateChanged(state);             
            }
        }

        private bool ComboHasDuplicateItem(object sender, CancelEventArgs e)
        {
            bool HasDuplicates = false;
            if (IsDirty && ((state == State.Edit || state == State.New)))
            {
                string tablename = string.Empty;
                try
                {
                    ComboBox cmb = sender as ComboBox;
                    BindingSource bs = (BindingSource)cmb.DataSource;

                    string item = ""; ;
                    foreach (DataRowView row in bs)
                    {
                        item = row[0].ToString();
                        HasDuplicates = (item.Contains(cmb.Text)) ? true : false;
                    }
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.Message, "Review Combo For Duplicates Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return HasDuplicates;
        }
        // store the ID number in the cmb
        private void Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.state != State.Load)
            {
                StoreFocusedCombo(sender, e);
                ComboBox comboBox = (ComboBox)sender;
                {
                    this.clientID = Convert.ToInt32(comboBox.SelectedValue);
                }
            }
        }

        private void Combo_Validating(object sender, CancelEventArgs e)
        {
            if (IsDirty && ((state == State.Edit || state == State.Save)))
            {
                string query = string.Empty;
                string tablename = string.Empty;
                try
                {
                    ComboBox cmb = sender as ComboBox;
                    tablename = dp.GetTableNameFromCombo(cmb.Name);
                    bool HasDuplicates = ComboHasDuplicateItem(sender, e);
                    if (HasDuplicates) { throw new ArgumentException("Item already contained in this Combo!"); }
                    switch (state)
                    {
                        case State.Edit:
                            query = dp.GetQueryForCombo(cmb.Name, cmb.Text, clientID, false);
                            break;
                        case State.Save:
                            query = dp.GetQueryForCombo(cmb.Name,cmb.Text, 0, true);
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.Message, "Combo Insert Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (IsDirty == true) // keys have been pressed!
                {
                    dp.ProcessData("", query, tablename);
                    IsDirty = false;
                    ClearControls();
                    UpdateState(State.Navigate);
                }
            }
        }

        private void DeactivateClient(int client_id)
        {
            string query = "UPDATE tbl_company SET active = false WHERE company_pk = " + client_id + ";";
            dp.ProcessData("", query, "company");
            MessageBox.Show("Client no longer active.", "Delete Client", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void InitializeControls()
        {
            foreach (Control control in this.Controls)
            {
                if (control is ComboBox)
                {
                    string query = "";
                    switch (control.Name)
                    {
                        case "cmbClient":
                            query = "SELECT company_name AS display, company_pk AS value FROM tbl_company WHERE is_client = true ORDER BY company_name ASC";
                            break;
                        case "cmbOffice":
                            query = "SELECT office_desc AS display, office_pk AS value FROM tbl_office ORDER BY office_desc ASC";
                            break;
                        case "cmbAgent":
                            query = "SELECT CONCAT(first_name, ' ', last_name) AS display, personnel_pk AS value FROM tbl_personnel WHERE function_fk = 5 ORDER BY last_name ASC";
                            break;
                    }
                    dp.PopulateCombo(query, control, "display", "value");
                }
            }
        }


        private void DataControlsStatusChanged()
        {
            foreach (Button btn in gbxDataControls.Controls)
            {
                if (btn.Enabled == true)
                {
                    btn.BackColor = Color.Lavender;
                }
                else
                {
                    btn.BackColor = Color.Gray;
                }
            }
        }

        private bool OnStateChanged(State newStatus)
        {
            bool HasChanged = !this.state.Equals(newStatus);
            {
                if (HasChanged == true)
                {
                    UpdateState(newStatus);
                    return HasChanged;
                }
            }
            return HasChanged;
        }

        public void StoreFocusedCombo(object sender, EventArgs e)
        {
            if (sender != null)
            {
                this.last_focused = (ComboBox)sender;
            }
        }

        // Allows searchable items, but not add or edit.
        //private void Combo_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    ComboBox cmb = (ComboBox)sender;
        //    cmb.DroppedDown = true;
        //    if (char.IsControl(e.KeyChar))
        //    {
        //        return;
        //    }
        //    string str = cmb.Text.Substring(0, cmb.SelectionStart) + e.KeyChar;
        //    Int32 index = cmb.FindStringExact(str);
        //    if (index == -1)
        //    {
        //        index =cmb.FindString(str);
        //    }
        //    cmb.SelectedIndex = index;
        //    cmb.SelectionStart = str.Length;
        //    cmb.SelectionLength = cmb.Text.Length - cmb.SelectionStart;
        //    e.Handled = true;
        //}
        private void UpdateState(State MyState)
        {
            switch (MyState)
            {
                case State.Load:
                    form_State = "Load";
                    btnEdit.Enabled = false;
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
                    btnNew.Enabled = false;
                    btnDelete.Enabled = false;
                    InitializeControls();
                    break;
                case State.Navigate:
                    form_State = "Navigate";
                    cmbClient.Enabled = true;
                    cmbOffice.Enabled = true;
                    cmbAgent.Enabled = true;
                    btnCancel.Enabled = false;
                    btnNew.Enabled = true;
                    btnEdit.Enabled = true;
                    btnSave.Enabled = false;
                    btnDelete.Enabled = false;
                    break;
                case State.Cancel:
                    form_State = "Cancel";
                    InitializeControls();
                    UpdateState(State.Navigate);
                    break;
                case State.Delete:
                    form_State = "Delete";
                    DeactivateClient(this.clientID);
                    UpdateState(State.Navigate);
                    break;
                case State.Edit:
                    form_State = "Edit";
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                    btnDelete.Enabled=true;
                    btnNew.Enabled = false;
                    btnEdit.Enabled = false;
                    break;
                case State.New:
                    form_State = "New";
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                    btnNew.Enabled = false;
                    btnEdit.Enabled = false;
                    ClearControls();
                    break;
                case State.Save:
                    if (state != State.Edit)
                        form_State = "Saving";
                    else
                        state = State.Save;
                    break;
                default:
                    break;
            }
            this.state = MyState;
            lblDisplay.Text = "Client :: " + form_State;
            DataControlsStatusChanged();
            //    Combo_Validating(last_focused, new CancelEventArgs());
        }
    }    
}
