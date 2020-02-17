using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToolsSL
{
    public partial class frmMenu : Form
    {
        public frmMenu()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            frmSettings frm = new frmSettings();
            frm.ShowDialog();
        }

        private void frmMenu_Load(object sender, EventArgs e)
        {
            // read the new Property settings of the ColorTheme
            this.BackColor = Properties.Settings.Default.ColorTheme;
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            frmHistory frm = new frmHistory();
            frm.ShowDialog();
        }

        private void btnClient_Click(object sender, EventArgs e)
        {
            frmClients frm = new frmClients();
            frm.ShowDialog();
        }

        private void btnTools_Click(object sender, EventArgs e)
        {
            frmTools frm = new frmTools();
            frm.ShowDialog();
        }

        private void btnHire_Click(object sender, EventArgs e)
        {
            frmHires frm = new frmHires();
            frm.ShowDialog();
            
        }

        private void frmMenu_Activated(object sender, EventArgs e)
        {
            // read the new Property settings of the ColorTheme
            this.BackColor = Properties.Settings.Default.ColorTheme;
        }

        private void btnBrand_Click(object sender, EventArgs e)
        {
            frmBrand frm = new frmBrand();
            frm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmReport frm = new frmReport();
            frm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmRetiredReport frm = new frmRetiredReport();
            frm.ShowDialog();
        }
    }
}
