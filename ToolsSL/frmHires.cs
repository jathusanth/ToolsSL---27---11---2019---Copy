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
    public partial class frmHires : Form
    {
        public frmHires()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmHires_Activated(object sender, EventArgs e)
        {
            // read the new Property settings of the ColorTheme
            this.BackColor = Properties.Settings.Default.ColorTheme;
        }

        private void lnkAdd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmHire frm = new frmHire();
            if (frm.ShowDialog() == DialogResult.OK)
                populateGrid();
        }

        private void frmHires_Load(object sender, EventArgs e)
        {
            populateGrid();
        }

        private void populateGrid()
        {
            string sql = "SELECT Hire.HireId, Clients.ClientName, Hire.DateRented, " +
                      "Hire.DateReturned " +
                      "FROM Hire INNER JOIN Clients " +
                      "ON Hire.ClientId = Clients.ClientId " +
                      "ORDER BY DateRented DESC";

            DataTable dtb = new DataTable();
            dtb = Context.GetDataTable(sql, "Hires");
            dgvHires.DataSource = dtb;
        }

        private void dgvHires_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvHires.CurrentCell == null) return;

            long PKID = long.Parse(dgvHires[0, dgvHires.CurrentCell.RowIndex].Value.ToString());

            frmHire frm = new frmHire(PKID);
            if (frm.ShowDialog() == DialogResult.OK)
                populateGrid();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure you want to delete the item?", "Tools", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    long PKID = long.Parse(dgvHires[0, dgvHires.CurrentCell.RowIndex].Value.ToString());

                    // use the DeleteRecord method of the Context class
                    Context.DeleteRecores("Hire", "HireId", PKID.ToString());
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
