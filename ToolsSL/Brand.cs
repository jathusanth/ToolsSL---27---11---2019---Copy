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
    public partial class frmBrand : Form
    {
        public frmBrand()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lnkAdd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmBrands frm = new frmBrands();
            if (frm.ShowDialog() == DialogResult.OK)
                PopulateGrid();
        }

        private void PopulateGrid()
        {
            DataTable dtb = new DataTable();
            dtb = VR_Controller.Context.GetDataTable("Brand");
            dgvBrand.DataSource = dtb;
        }

        private void frmBrand_Load(object sender, EventArgs e)
        {
            PopulateGrid();
        }
        private void frmBrand_DoubleClick_1(object sender, EventArgs e)
        {

            if (dgvBrand.CurrentCell == null) return;
            long PKID = long.Parse(dgvBrand[0, dgvBrand.CurrentCell.RowIndex].Value.ToString());

            frmBrands frm = new frmBrands(PKID);
            if (frm.ShowDialog() == DialogResult.OK)
                PopulateGrid();
        }
        private void frmBrand_Activated(object sender, EventArgs e)
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
                    long PKID = long.Parse(dgvBrand[0, dgvBrand.CurrentCell.RowIndex].Value.ToString());

                    // use the DeleteRecord method of the Context class
                    Context.DeleteRecores("Brand", "BrandId", PKID.ToString());
                    PopulateGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Happy Deleting!");
                }


            }
        }

       
    }
}
