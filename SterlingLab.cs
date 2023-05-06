using DataObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sterling_Lab
{
    public partial class SterlingLab : Form
    {
        public bool IsDirty = false;

        public SterlingLab()
        {
            InitializeComponent();
            this.KeyPreview = true;
        }

        private void btnClient_Click(object sender, EventArgs e)
        {
            AddUserControls(new Client());
        }
        private void btnMigrate_Click(object sender, EventArgs e)
        {
            AddUserControls(new Migrate());
        }
        private void btnICP_Click(object sender, EventArgs e)
        {
            AddUserControls(new ICP());
        }

        private void btnBatch_Click(object sender, EventArgs e)
        {
            AddUserControls(new WorkOrder());
        }

        private void btnProject_Click(object sender, EventArgs e)
        {
            AddUserControls(new Project());
        }

        private void btnSample_Click(object sender, EventArgs e)
        {
            AddUserControls(new Sample());
        }

        private void AddUserControls(UserControl userControl) 
        {
            if (userControl != null) 
            {
                pnlMain.Controls.Clear();
                userControl.Dock = DockStyle.Fill;
                pnlMain.Controls.Add(userControl);
                userControl.BringToFront();
            }
        }
    }
}
