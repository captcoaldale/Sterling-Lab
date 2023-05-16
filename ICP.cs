using System;
using DataObjects;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;
using System.Data;
using Microsoft.VisualBasic.FileIO;
using System.Collections.Generic;
//using DgvFilterPopup;

namespace Sterling_Lab
{
    public partial class ICP : UserControl
    {
        private string path;
        private string file; // filename
        private int file_ID = 0; // from tbl_file_name_pk
        private string form_state;

        private List<string> filters = new List<string>();
        private string element1;
        private string class1;
        private string class2;
        private string sample1;
        private string sample2;
        private string dgv_filter;

        private DataProcessor dp;
        private BindingSource bs;
        private SetDecimal sd;
        private bool blnFileSaved = false;
        private bool blnAbort = false;
        private StringBuilder sb;
        private enum State { Load, Navigate, Save, Cancel, Delete, New, Edit, EditOnly };
        private State MyState;

        public ICP()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }
        public void Form1_Load(object sender, EventArgs e)
        {
            UpdateState(State.Load);
            //this.FormClosing += new FormClosingEventHandler(Form1_Closing);

            this.dp = new DataProcessor("ICP");
            this.sb = new StringBuilder();
            this.bs = new BindingSource();
            this.sd= new SetDecimal();
            dtpEnd.Value = DateTime.Now;
            dtpBeg.Value = DateTime.Now.SubtractBusinessDays(30);
            //DgvFilterManager dgv = new DgvFilterManager(dgvDisplay);


            this.file_ID = dp.GetID("SELECT MAX(file_name_pk) FROM tbl_file_name;");
            dp.PopulateCombo("SELECT file_name_desc AS display, file_name_pk as value FROM tbl_file_name ORDER BY display",cmbFile, "display", "value");

            dp.PopulateCombo("SELECT concat(acronym, ' -- ', element_desc)  AS display, element_pk as value FROM tbl_element;", cmbElement, "display", "value");
            dp.PopulateCombo("SELECT class_desc AS display, class_pk AS value FROM tbl_class;", cmbClass1, "display", "value");
            dp.PopulateCombo("SELECT sample_name_desc AS display, sample_name_pk as value FROM tbl_sample_name;", cmbSample1, "display", "value");

            dp.PopulateCombo("SELECT sample_name_desc AS display, sample_name_pk as value FROM tbl_sample_name;", cmbSample2, "display", "value");
            dp.PopulateCombo("SELECT class_desc AS display, class_pk AS value FROM tbl_class;", cmbClass2, "display", "value");

            UpdateState(State.Navigate);
            PopulateChart();
        }

        private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            blnAbort = !blnAbort;
            if (blnAbort) // prevent rebound...

           // Determine if text has changed in the textbox by comparing to original text.
            if (btnSave.Enabled && !blnFileSaved)
            {
                 if (MessageBox.Show("You haven't saved to the database. Do really want to exit?", "My Application",
                    MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    // Cancel the Closing event from closing the form.
                    e.Cancel = true;
                }
                //else
                // dp.FreeMemory();
                // return to save ...
            }
        }

        public void Abort(string message)
        {
            // Clear the form and restart ...                     
            ResetControls(message);
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            ResetControls();

            this.path = @"D:\Sterling-MySQL Excel Models\Raw Data"; // @ verbatim string ... TODO (Modify for production version

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Browse ICP Files";

            ofd.Filter = "Excel CSV Files (.csv) | *.csv";
            ofd.FilterIndex = 1;
            ofd.InitialDirectory = path;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                this.path = ofd.FileName;
                try
                {
                    this.file = Path.GetFileName(this.path);
                    string tablename = "tbl_file_name";
                    if ((!(this.path.Contains("ICP"))))
                        MessageBox.Show("Not an ICP file.", "Wrong File Type", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else if (this.blnAbort = dp.IsDuplicate("select count(file_name_desc) from tbl_file_name WHERE file_name_desc='" + this.file + "'"))
                    {
                        this.file_ID = dp.GetID("SELECT file_name_pk FROM tbl_file_name WHERE file_name_desc = '" + file + "';");
                        MyState = State.EditOnly;
                    }
                    else if (this.file_ID < 1)  // new file
                    {
                        string query = "INSERT INTO " + tablename + " VALUES (0,'" + this.file + "');";
                        this.file_ID = dp.ProcessData("", query, tablename);
                        MyState = State.Edit;
                    }
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.Message + " Cannot process this file (:", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                finally
                {
                    UpdateState(MyState);
                    if (this.file_ID > 0)
                        ReadICP(this.path, "tbl_reading");
                }
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            string[] element_desc = cmbElement.Text.Split(' '); // display is concatenated (Li -- Lithium) so take the first part of the split
            bs.DataSource = null;
            string query = "";
            if (this.file_ID == 0)
                this.file_ID = dp.GetID(query, "file_name", "file_name_desc",cmbFile.Text);

            query = "  SELECT reading_pk AS item_pk, element, class, sample_name, wavelength, `intensity[Avg]` AS `intensity` FROM tbl_reading WHERE `date-time` BETWEEN '" + dtpBeg.Value + "' AND '" + dtpEnd.Value + "' ORDER BY sample_name, `intensity`;";
            if (this.file_ID > 0)
                GetElementFromReading(this.file_ID, query); // add dummy field so GetElement doesn't change the query
            else
                MessageBox.Show("No File Number!", "File Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveICP(State.Save);
        }

        private void cmbClass1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MyState != State.Load)
            {
                try
                {
                    if (!string.IsNullOrEmpty(cmbClass1.Text))
                    {
                        this.class1 = cmbClass1.Text;
                    }
                }
                catch (Exception x)
                {

                    MessageBox.Show(x.Message, "Class 1 Read Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                finally
                {

                }
            }
        }

        private void cmbClass2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MyState != State.Load)
            {
                {
                    try
                    {
                        this.class2 = cmbClass2.Text;
                        if (!string.IsNullOrEmpty(this.class1) && !string.IsNullOrEmpty(this.class2))
                        {
                            filters.Add(string.Format($"([class] >= '" + class1 + "' AND [class] <= '" + class2 + "')"));
                        }
                    }
                    catch (Exception x)
                    {
                        MessageBox.Show(x.Message, "Class 2 read Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    finally
                    {
                        bs = (BindingSource)dgvDisplay.DataSource;
                        bs.Filter = (string.Format($"([element] = '" + this.element1 + "' AND [class] >= '" + class1 + "' AND [class] <= '" + class2 + "')"));
                    }
                }
            }
        }

        private void CombineFilters()
        {
            // Now combine them
            //var combinedFilter = string.Join(" AND ", filters);
            //BindingSource bs = (BindingSource)dgvDisplay.DataSource;
            //bs.Filter = combinedFilter;
            //dgvDisplay.DataSource = bs;
            //combinedFilter = null;
            PopulateChart();
        }

        private void cmbElement_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MyState != State.Load)
            {
                try
                {
                    if (dgvDisplay.DataSource == null)
                        throw new Exception("Please select a Daily Result!");
                    string[] element_desc = this.cmbElement.Text.Split(' '); // combo display is concatenated (Li -- Lithium) so take part before the space
                    this.element1 = element_desc[0];
                    if (!string.IsNullOrEmpty(element1))
                    {
                        filters.Add(string.Format($"([element] = '" + element1 + "')"));
                    }
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.Message, "Element Read Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                finally
                {
                    bs = (BindingSource)dgvDisplay.DataSource;
                    bs.Filter = string.Format($"([element] = '" + element1 + "')");
                }
            }
        }

        private void cmbFile_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MyState != State.Load)
            {
                this.file_ID = dp.GetSelectedValue(cmbFile);
                if (file_ID > -1)
                {
                    string query = "SELECT reading_pk AS item_pk, element, class, sample_name, wavelength, `intensity[Avg]` AS `intensity` FROM tbl_reading WHERE file_fk = " + this.file_ID + " ORDER BY sample_name, `intensity`;";
                    GetElementFromReading(file_ID, query);
                }
                else
                    MessageBox.Show("No File ID.", "Data Read Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }
        }

        private void cmbSample1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MyState != State.Load)
            {
                try
                {
                    this.sample1 = cmbSample1.Text;
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.Message, "Sample 1 Read Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                finally
                {

                }
            }
        }

        private void cmbSample2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MyState != State.Load)
            {
                this.sample2 = cmbSample2.Text;
                try
                {
                    if (!string.IsNullOrEmpty(this.sample1) && !string.IsNullOrEmpty(this.sample2))
                    {
                        filters.Add(string.Format($"([sample_name] >= '" + sample1 + "' AND [sample_name] <= '" + sample2 + "')"));
                    }
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.Message, "sample 2 read Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                finally
                {
                    this.bs = (BindingSource)dgvDisplay.DataSource;
                    bs.Filter = string.Format($"([element] ='" + this.element1 + "' AND [class] >= '" + this.class1 + "' AND [class] <= '" + this.cmbClass1 + "' AND [sample_name] >= '" + sample1 + "' AND [sample_name] <= '" + sample2 + "')");
                }
            }
        }

        private void dgvDisplay_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (MyState != State.Load)
            {
                try
                {
                    switch (e.ColumnIndex)
                    {
                        case 1:
                            dgv_filter = string.Format($"([element] = '" + dgvDisplay.Rows[e.RowIndex].Cells[1].Value.ToString() + "')");
                            break;
                        case 2:
                            dgv_filter += string.Format(" AND ([class] = '" + dgvDisplay.Rows[e.RowIndex].Cells[2].Value.ToString() + "')");
                            break;
                        case 3:
                            dgv_filter += string.Format(" AND ([sample_name] = '" + dgvDisplay.Rows[e.RowIndex].Cells[3].Value.ToString() + "')");
                            break;
                        case 4:
                            dgv_filter += string.Format($" AND ([wavelength] = '" + dgvDisplay.Rows[e.RowIndex].Cells[4].Value.ToString());
                            break;
                    }
                    this.bs = (BindingSource)dgvDisplay.DataSource;
                    bs.Filter = dgv_filter;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Grid Read Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                }
                finally
                {
                    if (dgv_filter.Contains("wavelength"))
                        PopulateChart();
                }
            }
        }

        public DataTable GetDataTableFromCSVFile(string csv_file_path, string tablename = "")
        {
            System.Data.DataTable csvData = new System.Data.DataTable();
            csvData.TableName = tablename;
            StringBuilder sb = new StringBuilder();
            try
            {
                if (File.Exists(csv_file_path))
                {
                    sb.Append("INSERT INTO tbl_reader VALUES 0,");
                    using (TextFieldParser csvReader = new TextFieldParser(csv_file_path))
                    {
                        csvReader.SetDelimiters(",");
                        csvReader.HasFieldsEnclosedInQuotes = true;
                        string[] colFields = csvReader.ReadFields();
                        foreach (string column in colFields)
                        {
                            DataColumn datecolumn = new DataColumn(column);
                            datecolumn.AllowDBNull = true;
                            csvData.Columns.Add(datecolumn);
                        }

                        while (!csvReader.EndOfData)
                        {
                            string[] fieldData = csvReader.ReadFields();
                            //Making empty value as null
                            for (int i = 0; i < fieldData.Length; i++)
                            {
                                sd.SetDecimalPoint(fieldData[i], 2);
                                //if (fieldData[i].ContainsAny("<LOD", "< 0.005a"))
                                //{
                                //    fieldData[i] = "-0.001"; //fieldData[i] = null
                                //}
                                //if (fieldData[i] == string.Empty)
                                //{
                                //    fieldData[i] = string.Empty; //fieldData[i] = null
                                //}
                                //Skip rows that have any csv header information or blank rows in them
                                if (fieldData[0].Contains("Disclaimer") || string.IsNullOrEmpty(fieldData[0]))
                                {
                                    continue;
                                }
                            }
                            csvData.Rows.Add(fieldData);
                            gbxFile.Text = "Loading";
                            pbrProgress.Increment(1);
                        }
                    }
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message, "Error Reading File");
                return null;
            }
            finally
            {
                //UpdateState(State.Edit);
            }
            return csvData;
        }

        private void GetElementFromReading(int file_ID, string query)

        {
            string tablename = dp.GetTableNameFromQuery(query);
            dp.PopulateDataGridView(query, dgvDisplay, bs, tablename);
            //PopulateChart(query);
            UpdateState(State.Edit);
        }


        public void PopulateChart(string query = "", string tablename = "")
        {
            try
            {
                if (this.bs.DataSource != DBNull.Value)
                    chtDisplay.DataSource = this.bs;
                chtDisplay.Series.Clear();
                chtDisplay.Series.Add("MySeries");
                chtDisplay.Series["MySeries"].ChartType = SeriesChartType.Line;
                chtDisplay.Series["MySeries"].XValueMember = "sample_name";
                chtDisplay.Series["MySeries"].YValueMembers = "intensity";
                chtDisplay.Series["MySeries"].Name = "Intensity";
                // chtDisplay.Series["MySeries"].BorderWidth = 5;
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message, "Chart Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool RangeSelector(ComboBox cmb, string column, int lowerbound, int upperbound)
        {
            try
            {
                // loop the cmb items to find range of values

                string cell = "";
                string item = "";

                foreach (System.Windows.Forms.DataGridViewRow r in dgvDisplay.SelectedRows)
                {
                    cell += r.Cells[column].Value.ToString();

                    item = cmb.Items[0].ToString();
                    if (cell == item)
                    {
                        dgvDisplay.Rows[r.Index].Selected = dgvDisplay.Rows[r.Index].Visible = true;
                    }
                    else
                    {
                        dgvDisplay.CurrentCell = null;
                        dgvDisplay.Rows[r.Index].Visible = false;
                    }
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message, "Error Reading " + column);
                return false;
            }
            return true;
        }

        private void ReadICP(string path, string tablename)
        {
            dgvDisplay.DataSource = GetDataTableFromCSVFile(this.path, tablename);
            UpdateState(State.Edit);
        }

        private void RefreshControls()
        {
            foreach (Control ctl in this.Controls)
            {
                if (ctl is System.Windows.Forms.Label)
                {
                    ctl.Visible = true;
                }
            }
        }

        public void ResetControls(string message = "")
        {
            // clear the grids ...
            dgvDisplay.DataSource = null;
            btnSave.ForeColor = Color.Black;

            foreach (Control ctl in this.Controls)
            {
                if (ctl is System.Windows.Forms.Label)
                {
                    {
                        // Labels original text is "client: "
                        string[] prompt = ctl.Text.Split(':');
                        ctl.Text = prompt[0] + ':';
                        ctl.ForeColor = Color.Black;
                    }
                }
            }

            //gbxHeader.Text = message;
            this.blnFileSaved = false; //reset
            btnSave.Enabled = false;
            pbrProgress.Value = 0;
            //this.project_number = this.blnProjectSaved ? "" : this.project_number;
        }

        private void SaveICP(State state)
        {
            try
            {
                System.Data.DataTable dt = dgvDisplay.DataSource as System.Data.DataTable;
                string target = dt.TableName;
                //int maxCols = dgvDisplay.Columns.Count - 2;
                StringBuilder sb = new StringBuilder();

                foreach (DataRow row in dt.Rows)
                {
                    string insert;
                    sb.Append("INSERT INTO " + target + " VALUES (0," + this.file_ID + ",");
                    foreach (DataColumn col in dt.Columns)
                    {
                        object item = row[col.ColumnName];
                        double d = 0;
                        if (col.ColumnName.Contains("Date"))
                            item = "'" + DateTime.Parse(row[col.ColumnName].ToString()) + "'";
                        else if (double.TryParse(row[col.ColumnName].ToString(), out d)) // add the first value
                            item = d;
                        else
                        {
                            item = "'" + row[col.ColumnName] + "'";  // strings in quotes for insert statement
                            if (item.ToString().ContainsAny("CAL8", "CAL9"))
                            {
                                var temp = item.ToString().Split('L');
                                item = temp[0] + "L0" + temp[1];
                            }
                            else if (item.ToString().Contains("CAL1") && item.ToString().Length <= 6)
                            {
                                var temp = item.ToString().Split('L');
                                item = temp[0] + "L0" + temp[1];
                            }
                        }
                        if (String.IsNullOrEmpty(row[col.ColumnName].ToString()))
                        {
                            item = "null";
                        }
                        // add tested values to insert string
                        if (col.Ordinal < dt.Columns.Count - 1)
                        {
                            //if(!(item == null))
                            sb.Append(item + ",");
                        }
                        else
                            sb.Append(item + ");");
                    }
                    insert = sb.ToString();
                    dp.ProcessData("", insert, target);
                    sb.Clear();
                    //dp.UpdateProgressBar(1, (ProgressBar)pbrProgress, Color.AliceBlue);
                    gbxFile.Text = "Saving";
                    //pbrProgress.Increment(1);
                    dp.UpdateProgressBar(pbrProgress, Color.Red);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                UpdateState(State.Navigate);
                gbxFile.Text = "File Saved!";

            }
        }

        private void UpdateState(State st)
        {
            switch (st)
            {
                case State.Load:
                    form_state = "Loading";
                    gbxFile.Enabled = false;
                    break;
                case State.Navigate:
                    form_state = "Navigate";
                    gbxFile.Enabled = true;
                    btnOpen.Enabled = true;
                    btnSave.Enabled = false;
                    pbrProgress.Value = 0;
                    break;
                case State.Cancel:
                    form_state = "Cancel";

                    //ClearControls(gbxProject);
                    //updateStatus(State.Navigation);
                    break;
                case State.Edit:
                    //form_state = "Edit Project";
                    form_state = "Edit";
                    gbxFile.Enabled = true;
                    btnOpen.Enabled = false;
                    btnSave.Enabled = true;
                    pbrProgress.Value = 0;
                    break;
                case State.EditOnly:
                    form_state = "Edit Only";
                    gbxFile.Enabled = true;
                    gbxFile.Enabled = false;
                    btnSave.Enabled = false;
                    btnOpen.Enabled = true;
                    btnSave.Enabled = true;
                    pbrProgress.Value = 0;
                    break;
                case State.New:
                    form_state = "New";
                    //ClearControls(gbxProject);
                    //updateStatus(State.Navigation);
                    break;

                case State.Save:
                    this.form_state = "Save";
                    btnSave.ForeColor = Color.Green;
                    btnOpen.Enabled = false;
                    btnSave.Enabled = false;
                    break;
                default:
                    break;

            }
            lblICPDisplay.Text = "ICP :: " + form_state;
            this.MyState = st;
            //gbxFile.Text = dp.GetDateString(file, 2);
        }
    }
}
