using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VR_Controller;

namespace ToolsSL
{
    public partial class frmHistory : Form
    {
        DataView _dvHistory = null;

        public frmHistory()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmHistory_Activated(object sender, EventArgs e)
        {
            // read the new Property settings of the ColorTheme
            this.BackColor = Properties.Settings.Default.ColorTheme;
        }

        private void frmHistory_Load(object sender, EventArgs e)
        {
            PoppulateDrid();
        }

        private void PoppulateDrid()
        {
            string sqlQuery = "SELECT Tool.ToolName, Brand.BrandName, Tool.Active " +
                              "FROM Brand INNER JOIN " +
                             "Tool ON Brand.BrandId = Tool.BrandId " +
                             "WHERE(Tool.Active = 1)";
            DataTable dtb = Context.GetDataTable(sqlQuery, "ToolHistory", true);

            _dvHistory = new DataView(dtb);

            dgvHistory.DataSource = _dvHistory;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            _dvHistory.RowFilter = $"BrandName LIKE '%{txtSearch.Text}%'" + $"OR ToolName LIKE'%{txtSearch.Text}%'";
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            //ExporotUsingString();
            ExportUsingBuilder();
        }

        private void ExportUsingBuilder()
        {
            StringBuilder csv = new StringBuilder();

            foreach (DataRowView drv in _dvHistory)
            {

                csv.AppendLine($"{drv["BrandName"].ToString()}, " +

                      $"{drv["ToolName"].ToString()}");


            }

            File.WriteAllText(Application.StartupPath + @"\ToolActive.csv", csv.ToString());
            MessageBox.Show("Export Completed", "Tool Active");
        }
    }
}
