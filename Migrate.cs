using DataObjects;
using System;
using System.IO;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Sterling_Lab
{
    public partial class Migrate : UserControl
    {
        bool bln_abort; // skip project methods if duplicate ...
        bool bln_saved;
        bool bln_loaded;

        private string office;
        private int office_id;

        private string acct_manager;
        private int acct_manager_id;

        private string price;
        private int price_id;

        private string project_number;
        private int project_id;

        private string tech_assigned;
        private int tech_assigned_id;

        private string client;
        private int client_id;
        private int _objective_id = 3; // "Routine Analysis" (default for our migrator)...
        private int project_type_id = 1; // Maintenance (default) ...
        private int sample_type_id = 3; // water
        private int method_id = 1; // FWA

        private string area; // renamed from field nomenclature w/ DGV
        private int field_id;

        private string zone;
        private int zone_id;

        private string site;
        private int site_id;

        private int location_id;
        private string dls;

        private string date_initiated;
        private string date_sampled;
        private string date_received;
        private string date_reported;

        private DataProcessor dp;
        private Utilties kp;
        private SetDecimal sd;
        private ProjectNumberGenerator png;
        private SampleNumberGenerator sng;
        private StringBuilder sb;
        private string path;
        private string file_name;

        private TableBuilder tblParameter;
        private TableBuilder tblSoluable;
        private TableBuilder tblTotal;
        private TableBuilder tblSaturation;
        private TableBuilder tblCorrosion;

        delegate void stopDouble(int count);
        private enum State { Load, Edit, Save, Navigate, Delete, Cancel }
        public string form_state = "";
        private State state;

        public Migrate()
        {
            InitializeComponent();
            InitializeControls();
            UpdateState(State.Load);
        }

        public void InitializeControls()
        {
            this.dp = new DataProcessor("DB");
            this.sb = new StringBuilder();
            this.kp = new Utilties();
            this.sd = new SetDecimal();
            this.png = new ProjectNumberGenerator();
            this.sng = new SampleNumberGenerator(); 
            bln_loaded = false;
            bln_saved = false;
            bln_abort = false;
        }

        private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bln_abort = !bln_abort;
            if (bln_abort) // prevent rebound...
                Application.Exit();
            // Determine if text has changed in the textbox by comparing to original text.           
            if (btnSave.Enabled && !bln_saved)
            {
                // Display a MsgBox asking the user to save changes or abort.
                if (MessageBox.Show("You haven't saved to the database. Do really want to exit?", "My Application",
                    MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    // Cancel the Closing event from closing the form.
                    e.Cancel = true;
                }
                else
                    Application.Exit();
                // return to save ...
            }
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            this.path = @"D:\Sterling-MySQL Excel Models"; // @ verbatim string ... TODO (Modify for production version

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Browse FWA Files";

            // openFileDialog1.Filter = "Excel Files (.xls, xlsx, xlxm) | *.xls, *.xlxs. *.xlsm";
            ofd.FilterIndex = 1;
            ofd.InitialDirectory = path;
            /*
             openFileDialog1.CheckFileExists = true;
             openFileDialog1.CheckPathExists = true;
             */
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                this.path = ofd.FileName;
                try
                {
                    this.file_name = Path.GetFileName(this.path);

                    if (!this.path.Contains("FWA"))
                    {
                        MessageBox.Show("Not a full water anaylysis. Choose another file.", "Wrong File Type", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (this.bln_abort = dp.IsDuplicate("select count(file_name) from tbl_project WHERE file_name='" + this.file_name + "'"))
                    {
                        MessageBox.Show("File already processed! Select a new file.", "Duplicate File", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                        ReadFWA(this.path);
                }
                catch (Exception x)
                {
                    MessageBox.Show("File read error: " + x.ToString());
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            UpdateState(State.Save);
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

        private void ReadFWA(string path)
        {
            {
                state = State.Load;
                //ResetControls("Reading File", "Loading Excel Data");
                dp.InitializeExcel(path);

                // populate vital variables
                this.project_number = dp.CleanCell("J4");
                this.project_id = dp.GetID("select project_pk from tbl_project WHERE project_number = '" + this.project_number + "';");
                this.client = dp.CleanCell("J6");
                this.client_id = dp.GetID("select company_pk from tbl_company WHERE company_name = '" + this.client + "';");

                gbxHeader.Text = dp.CleanCell("G2") + ":" + this.client + " -- " + this.project_number;

                // empty values cause crashes ... // excel numbers are doubles, but dp.CleanCell() converts to string (Vital to use before populating) ...     
                // populate label and variable w/ viable entries ... 
                this.area = dp.CleanCell("O6");
                lblField.Text = "Field: " + this.area;
                this.zone = dp.CleanCell("O7");
                lblZone.Text = "Zone: " + this.zone;
                this.dls = dp.CleanCell("T6");
                lblDLS.Text = "DLS: " + this.dls;
                this.site = dp.CleanCell("T8");
                lblSite.Text = "Sample Site: " + this.site;

                this.date_sampled = dp.CleanCell("J8");
                this.date_sampled = dp.GetDateString(this.date_sampled, 2);
                lblDateSampled.Text = "Sampled: " + this.date_sampled;

                this.date_received = dp.CleanCell("J9");
                this.date_received = dp.GetDateString(this.date_received, 2);
                lblReceived.Text = "Rec'd: " + this.date_received;

                this.date_reported = dp.CleanCell("J10");
                this.date_reported = dp.GetDateString(this.date_reported, 2);
                lblReported.Text = "Rep'd: " + this.date_reported;

                this. acct_manager = dp.CleanCell("T9");
                lblAgent.Text = "Acct: " + acct_manager;
                this.office = dp.CleanCell("T10");
                lblOffice.Text = "Dist: " + office;
                this.tech_assigned = dp.CleanCell("O8");
                lblTech.Text = "Tech: " + tech_assigned;
                this.price = dp.CleanCell("T4");
                lblPrice.Text = "$: " + this.price; // this must be a string value else breaks the program ...
                dp.UpdateProgressBar(pbrSaving, Color.White, 10);

                // Grids -- Labratory Values (Parms, Soluables, Totals) ...
                tblParameter = new TableBuilder(dp.wks, "F18:G33");
                dgvParameter.DataSource = tblParameter.BuildTwoColumn();
                dp.UpdateProgressBar(pbrSaving, Color.White, 20);

                tblSoluable = new TableBuilder(dp.wks);
                dgvSoluable.DataSource = tblSoluable.BuildSoluable();
                dp.UpdateProgressBar(pbrSaving, Color.White, 20);

                tblTotal = new TableBuilder(dp.wks, "V20:W29");
                dgvTotal.DataSource = tblTotal.BuildTwoColumn();
                dp.UpdateProgressBar(pbrSaving, Color.White, 20);

                tblSaturation = new TableBuilder(dp.wks);
                dgvSaturation.DataSource = tblSaturation.BuildSaturation();
                dp.UpdateProgressBar(pbrSaving, Color.White, 20);

                tblCorrosion = new TableBuilder(dp.wks, "C65:V67");
                dgvCorrosion.DataSource = tblCorrosion.BuildCorrosion();
                dp.UpdateProgressBar(pbrSaving, Color.White, 20);
                bln_loaded = true;
                //dp.CloseExcel(path);
                UpdateState(State.Navigate);
                //ResetControls("File Loaded", "Ready to Save");
            }
        }

        private void RefreshControls()
        {
            foreach (Control ctl in gbxHeader.Controls)
            {
                if (ctl is Label)
                {
                    ctl.Visible = true;
                }
            }
        }

        private void SaveFWA()
        {
            //ResetControls("Saving", "Save Progress");
            // tbl_company: company_pk, company_name, active, is_supplier, is_client, notes (6 cols)
            string tablename = "company";
            string select = "SELECT COUNT(company_pk) FROM tbl_company WHERE company_name LIKE " + '"' + '%' + this.client.Substring(0, 12) + "%" + '"' + ";"; //  NB use double quotes!  LIKE selectes Silverleaf + (Co, Inc, Resources, Ltd ...)
            sb.Clear();
            sb.Append("INSERT INTO tbl_company VALUES ");
            sb.Append("(" + 0 + ",'" + this.client + "', true, false, true, false);");
            string insert = sb.ToString();
            this.client_id = dp.ProcessData(select, insert, tablename);

            // init necessary fk's
            this.price_id = (this.price_id > 0) ? this.price_id : 1; // TODO: complete price table

            // tbl_office = office_pk, office_desc  (2 cols) ...
            select = "SELECT COUNT(office_pk) from tbl_office WHERE office_desc LIKE " + '"' + '%' + this.office + "%" + '"' + ";";
            tablename = "office";
            insert = "INSERT INTO tbl_office VALUES (0,'" + this.office + "')";
            this.office_id = dp.ProcessData(select, insert, tablename);

            // tbl_field: field_pk, field_desc (2cols)
            select = "SELECT COUNT(field_pk) from tbl_field WHERE field_desc = '" + this.area + "';";
            tablename = "field";
            insert = "INSERT INTO tbl_field VALUES (0,'" + this.area + "');";
            this.field_id = dp.ProcessData(select, insert, tablename);

            dp.UpdateProgressBar(pbrSaving, Color.Green, 20);

            // tbl_zone: zone_pk, zone_desc 2 fields
            select = "SELECT COUNT(zone_pk) from tbl_zone WHERE zone_desc = '" + this.zone + "';";
            tablename = "zone";
            insert = "INSERT INTO tbl_zone VALUES(0,'" + this.zone + "');";
            this.zone_id = dp.ProcessData(select, insert, tablename);

            // check tbl_site
            select = "SELECT COUNT(site_pk) from tbl_site WHERE site_desc = '" + this.site + "';";
            tablename = "site";
            insert = "INSERT INTO tbl_site VALUES (0, '" + this.site + "')";
            this.site_id = dp.ProcessData(select, insert, tablename);

            // POPULATE tbl_location ... columns: location_pk, client_fk, field_fk, zone_fk, site_fk, land_desc, location_notes
            // check for dup locations ...
            sb.Clear();
            select = "SELECT COUNT(location_pk) from tbl_location WHERE client_fk = " + this.client_id + ";";
            tablename = "location";
            sb.Append("INSERT INTO tbl_location VALUES ");
            sb.Append("(" + 0 + "," + this.client_id + ", " + this.field_id + ", " + this.zone_id + "," + this.site_id + ",'" + this.dls + "', null);");
            insert = sb.ToString();
            this.location_id = dp.ProcessData(select, insert, tablename);

            // TODO: fk inserts fail w/out set foreign_key_checks = 0; (why can't we do this w/ the keys in place??
           //  POPULATE tbl_project
            int priority_fk = 1; // "Standard"
            if (this.acct_manager.Contains(" "))
            {
                string[] names = this.acct_manager.Split(' '); // string array of first & last names ...
                select = "SELECT COUNT(personnel_pk) from tbl_personnel WHERE first_name = "
                    + "'" + names[0] + "' and last_name = '" + names[1] + "'";
            }
            else
            {
                select = "SELECT personnel_pk from tbl_personnel WHERE first_name = '" + this.acct_manager + "';";
            }
            this.acct_manager_id = dp.GetID(select);

            sb.Clear();
            tablename = "project";
            int max_cols = 0;
            this.date_initiated = dp.GetDateString(this.date_sampled, 1); // SQL format (use date_sampled for missing date initiated)
            this.date_sampled = this.date_initiated;
            this.date_reported = dp.GetDateString(this.date_reported, 1); // date_report_expected in DB

            // Generate a standard project number (clientID, siteID, methodID, dateinitiated)
            ProjectNumberGenerator pn = new ProjectNumberGenerator();
            project_number = pn.Generate(client_id, location_id, method_id, date_initiated);


            //  POPULATE tbl_project: project_pk, file_name, project_number, client_fk, agent_fk, project_type_fk, _objective_fk, priority_requested_fk, 
            //  date_initiated, date_report_expected, is_reported,  bill_cust, bill_cust_notes, project_notes (14 fields)
            select = "";
            sb.Append("INSERT INTO tbl_project VALUES (" + 0 + ",'" + this.file_name + "','" + this.project_number + "', " + this.client_id + ", " + this.acct_manager_id + "," 
                + this.project_type_id + "," + this._objective_id + "," + priority_fk + ",'"
                + this.date_initiated + "','" + date_reported + "'," + true + "," + priority_fk + ", null, null);");
            insert = sb.ToString();
            int project_fk = dp.ProcessData(select, insert, tablename);
            dp.UpdateProgressBar(pbrSaving, Color.Green, 20);

            // tbl_sample: sample_pk, batch_fk, project_fk, method_fk, location_fk, sample_type_fk, sample_number, date_collected, sample_notes, is_in_office, is_open (11 fields)...
            sb.Clear();
            tablename = "sample";
            select = ""; // 0?
            string sample_name = sng.Generate();
            sb.Append("INSERT INTO tbl_sample VALUES (" + 0 + ",null," + project_fk + "," + method_id + "," + location_id + "," + sample_type_id +
                ",'" + sample_name + "','" + date_sampled + "','Full Water Analysis',true,true);");
            insert = sb.ToString();
            int sample_fk = dp.ProcessData(select, insert, tablename);

            // populate tbl_sample_item (from dgvParameter) ...
            // (sample_item_pk, sample_fk,) temp(C), pH, S.G., Silica, Conductivity, TDS meas, TDS calc, ohms,Tot Hard, Tot Alk, Tot Acid, Tot Susp, Oil&G, Resid, Disolved (17 columns)
            tablename = "tbl_sample_items";
            string extension = ",";
            string value = "";
            max_cols = 14; // 15 actual cols but zero-based
            int col_count = 0; //  column count
            sb.Clear();
            sb.Append("INSERT INTO tbl_sample_items VALUES (0," + sample_fk + ",");
            foreach (DataRow row in tblParameter.tbl.Rows)
            {
                foreach (DataColumn col in tblParameter.tbl.Columns)
                {
                    value = row[col.ColumnName].ToString();
                    if (value.ContainsAny("S", "H", "C", "T", "R", "D", "O", "-")) // strip out display (use values only)
                    {
                        continue;
                    }
                    else if (double.TryParse(value, out double d))
                    {
                        value = d.ToString();
                        sd.SetDecimalPoint(value, 3);
                    }
                    if (col_count < max_cols)
                    {
                        sb.Append(value + extension);
                    }
                    else
                        sb.Append(value);
                    col_count++;
                }
            }
            sb.Append(");");
            insert = sb.ToString();
            int sample_item_pk = dp.ProcessData(select, insert, tablename);

            // tbl_personnel = personnel_pk, first_name, initials, last_name, function_fk, active, personnel_notes, office_fk (8 cols) ...
            tablename = "personnel";
            int tech_id = 0;
            int alt_id = 0;
            string tech;
            string alt;
            if (this.tech_assigned.Contains("/"))
            {
                string[] names = this.tech_assigned.Split('/'); // string array of first & last names ...
                tech = "select personnel_pk from tbl_personnel WHERE first_name = '" + names[0] + "';";
                tech_id = dp.GetID(tech);
                alt = "select personnel_pk from tbl_personnel WHERE first_name = '" + names[1] + "';";
                alt_id = dp.GetID(alt);
            }
            else if (this.tech_assigned.Contains(" "))
            {
                string[] names = this.tech_assigned.Split(' '); // string array of first & last names ...
                tech = "select count(personnel_pk) from tbl_personnel WHERE first_name = "
                    + "'" + names[0] + "' and last_name = '" + names[1] + "'";
                tech_id = dp.GetID(tech);
            }

            // POPUlATE tbl_test
            // tbl_test: test_pk, sample_fk, meth_fk, tech_fk, alt_fk, prior_fk, date_received, date reported, test_notes, photo_path (10 fields)...
            sb.Clear();
            tablename = "test";
            int method_fk = 1; // FWA
            select = ""; //
            sb.Append("INSERT INTO tbl_test VALUES ");
            sb.Append("(0," + sample_fk + "," + method_fk + "," + tech_id + "," + alt_id + "," + priority_fk + ",'"
                + this.date_received + "','" + this.date_reported + "', 'FWA', 'N/A');");
            insert = sb.ToString();
            int test_fk = dp.ProcessData(select, insert, tablename);

            // POPULATE tbl_test_item (from tblSoluable)
            int unit_1_fk = 2; //"mg/L"         
            int unit_2_fk = 19;// "meq/L"
            int method_item_fk = 1; // Blank
            int method_type_fk = 1; // Soluble
            int test_item_pk = 0;
            int element_fk = 0;
            tablename = "test_item";
            // test_item_pk, test_fk, method_item_fk, method_type_fk, run_date, ELEMENT_FK, unit_1_fk, value1, unit_2_fk, value2, unit_3_fk, value3(12 columns)
            foreach (DataRow row in tblSoluable.tbl.Rows)
            {
                sb.Clear();
                string element = row[0].ToString();
                element_fk = dp.GetID("SELECT element_pk FROM tbl_element WHERE acronym = '" + element + "';");
                sb = new StringBuilder("INSERT INTO tbl_test_item VALUES ");
                sb.Append("(0," + test_fk + "," + method_item_fk + "," + method_type_fk + ",'" + date_received + "'," + element_fk + ","
                     + row[1] + "," + unit_1_fk + "," + row[2] + "," + unit_2_fk + ",null,null);"); // NULLS for test_item's three values
            }
            insert = sb.ToString();
            test_item_pk = dp.ProcessData("", insert, tablename);
            dp.UpdateProgressBar(this.pbrSaving, Color.Green, 20);

            // populate tbl_test_item (from dgvTotals) ...
            method_type_fk = 2; // total soluble
            foreach (DataRow row in tblTotal.tbl.Rows)
            {
                element_fk = dp.GetID("select element_pk from tbl_element WHERE acronym= '" + row[0] + "'");
                sb.Clear();
                sb = new StringBuilder("INSERT INTO tbl_test_item VALUES (0,");
                sb.Append(test_fk + ", " + method_item_fk + ", " + method_type_fk + ",'" + date_received + "'," + element_fk + ", "
                    + row[1] + "," + unit_1_fk + ", null, null,null,null);");
            }
            insert = sb.ToString();
            test_item_pk = dp.ProcessData(select, insert, tablename);

            // populate tbl_saturation (from dgvSaturation) 24 cols (added index_pk && test_fk)
            insert = "";
            tablename = "tbl_saturation";
            int saturation_pk;
            max_cols = tblSaturation.tbl.Columns.Count - 2;
            int max_rows = tblSaturation.tbl.Rows.Count - 1;
            foreach (DataRow row in tblSaturation.tbl.Rows)
            {
                sb.Clear();
                sb.Append("INSERT INTO tbl_saturation VALUES (0," + test_fk + ",'" + date_received + "',");
                int colCount = 0; //  added above 
                foreach (DataColumn col in tblSaturation.tbl.Columns)
                {
                    value = row[col.ColumnName].ToString();
                    // replace blanks to avoid crash ...
                    if (String.IsNullOrEmpty(value))
                        row[col.ColumnName] = -0.0;
                    if (colCount < max_cols)
                        sb.Append(row[col.ColumnName] + extension);
                    else
                        sb.Append(row[col.ColumnName]);
                    colCount++;
                }
                sb.Append(");");
                insert = sb.ToString();
                saturation_pk = dp.ProcessData(select, insert, tablename);
            }

            dp.UpdateProgressBar(this.pbrSaving, Color.Green, 20);

            // Corrosion ... populate tbl_corrosion (from tblCorrosion) ...
            // columns: test_item_pk, test_fk, run_date, P(bar), T(C), CO2 Brine, CO2 gas, H2S Brine, H2S gas, Corr-mm, Cor-mpy, Corr CO2%, Corr H2S%,Corr H2O%,Corr pH% (15 columns)
            tablename = "tbl_corrosion";
            method_type_fk = 4; // corrosion
            int corrosion_pk;
            max_cols = 11;
            foreach (DataRow row in tblCorrosion.tbl.Rows)
            {
                sb.Clear();
                sb.Append("INSERT INTO tbl_corrosion VALUES (0," + test_fk + ",'" + date_received + "',");
                int colCount = 0; //  added above 
                foreach (DataColumn col in tblCorrosion.tbl.Columns)
                {
                    value = row[col.ColumnName].ToString();
                    // replace blanks to avoid crash ...
                    if (String.IsNullOrEmpty(value))
                        row[col.ColumnName] = -0.0;
                    if (colCount < max_cols)
                        sb.Append(row[col.ColumnName] + extension);
                    else
                        sb.Append(row[col.ColumnName]);
                    colCount++;
                }
                sb.Append(");");
                insert = sb.ToString();
                corrosion_pk = dp.ProcessData(select, insert, tablename);
            }
            // reinitialize form ...
            dp.UpdateProgressBar(pbrSaving, Color.Green, 20);
            bln_saved = true;
            bln_loaded = false;
            MessageBox.Show("Project Saved!", "Project Status", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            // ResetControls("Load Another File?", "Project Saved!");
            dp.CloseExcel(path);
            UpdateState(State.Navigate);
        }

        private void UpdateState(State status)
        {
            switch (status)
            {
                case State.Load:
                    form_state = "Load";
                    foreach (Control ctl in gbxHeader.Controls)
                    {
                        if (ctl is Label)
                        {
                            {
                                // Labels original text is "client: -- so trim the added text "
                                string[] prompt = ctl.Text.Split(':');
                                ctl.Text = prompt[0] + ':';
                                ctl.ForeColor = Color.Black;
                            }
                        }
                    }
                    state = State.Navigate;
                    if (bln_saved)
                    {
                        dgvParameter.DataSource = null;
                        dgvSoluable.DataSource = null;
                        dgvSaturation.DataSource = null;
                        dgvTotal.DataSource = null;
                        dgvCorrosion.DataSource = null;
                    }
                    UpdateState(State.Navigate);
                    break;
                case State.Navigate:
                    form_state = "Navigate";
                    pbrSaving.Value = 0;
                    btnSave.Enabled = bln_loaded;
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;
                    btnCancel.Enabled = bln_loaded;
                    btnNew.Enabled = !btnSave.Enabled;
                    break;
                case State.Cancel:
                    form_state = "Cancel"; 
                    InitializeControls();
                    UpdateState(State.Navigate);
                    break;
                case State.Edit:
                    form_state = "Edit";
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                    btnDelete.Enabled = true;
                    btnNew.Enabled = false;
                    btnEdit.Enabled = false;
                    this.bln_saved = false; // also fill grid w/ sample items to add data
                    break;
                case State.Save:
                    form_state = "Save";
                    btnSave.Enabled = false;
                    btnCancel.Enabled = true;
                    btnDelete.Enabled = false;
                    btnNew.Enabled = false;
                    btnEdit.Enabled = false;
                    SaveFWA();
                    break;
                default:
                    break;
            }
            this.state = status;
            lblMigrateDisplay.Text = "Migrate :: " + form_state;
            DataControlsStatusChanged();
        }
    }
}
