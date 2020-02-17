using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VR_Controller;

namespace ToolsSL
{
    public partial class frmTools : Form
    {
        

        public frmTools()
        {
            InitializeComponent();
        }
        public frmTools(long PKID)
        {
            InitializeComponent();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lnkAdd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmTool frm = new frmTool();
            if (frm.ShowDialog() == DialogResult.OK)
                populateGrid();
        }

        private void frmTools_Load(object sender, EventArgs e)
        {
            populateGrid();
        }

        private void populateGrid()
        {
            string sql = "SELECT Tool.ToolId, Tool.ToolName, Brant.BrantId Tool.Active " +
                        "FROM Tool LEFT JOIN BRAND " +
                        "ON Tool.BrandId = Brand.BrandId";

            DataTable dtb = new DataTable();
            dtb = VR_Controller.Context.GetDataTable("Tool");
            dgvTools.DataSource = dtb;

        }

        private void dgvTools_DoubleClick(object sender, EventArgs e)
        {
            if (dgvTools.CurrentCell == null) return;
            long PKID = long.Parse(dgvTools[0, dgvTools.CurrentCell.RowIndex].Value.ToString());

            frmTool frm = new frmTool(PKID);
            if (frm.ShowDialog() == DialogResult.OK)
                populateGrid();
        }

        private void frmTools_Activated(object sender, EventArgs e)
        {
            // read the new Property settings of the ColorTheme
            this.BackColor = Properties.Settings.Default.ColorTheme; 
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete the item?", "Tools", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    long PKID = long.Parse(dgvTools[0, dgvTools.CurrentCell.RowIndex].Value.ToString());

                    // use the DeleteRecord method of the Context class
                    Context.DeleteRecores("Tool", "ToolId", PKID.ToString());
                    populateGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Happy Deleting!");
                }


            }
        }
    }
}
