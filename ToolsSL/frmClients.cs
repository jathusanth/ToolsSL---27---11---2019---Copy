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
    public partial class frmClients : Form
    {
        public frmClients()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmClients_Load(object sender, EventArgs e)
        {
            // read the new Property settings of the ColorTheme
            this.BackColor = Properties.Settings.Default.ColorTheme;
            PopulateGrid();
        }

        private void PopulateGrid()
        {
            DataTable dtb = new DataTable();
            dtb = VR_Controller.Context.GetDatable("Clients");
            dgvClients.DataSource = dtb;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmClient frm = new frmClient();
            if (frm.ShowDialog() == DialogResult.OK)
            PopulateGrid();

        }

        private void dgvClients_DoubleClick(object sender, EventArgs e)
        {
            if (dgvClients.CurrentCell == null) return;

            long PKID = long.Parse(dgvClients[0, dgvClients.CurrentCell.RowIndex].Value.ToString());

            frmClient frm = new frmClient(PKID);
            if (frm.ShowDialog() == DialogResult.OK)
                PopulateGrid();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure you want to delete the item?", "Tools", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    long PKID = long.Parse(dgvClients[0, dgvClients.CurrentCell.RowIndex].Value.ToString());

                    // use the DeleteRecord method of the Context class
                    Context.DeleteRecores("Clients", "ClientId", PKID.ToString());
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
