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
    public partial class frmRetiredReport : Form
    {
        DataView _dgvRetiredReport = null;

        public frmRetiredReport()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmRetiredReport_Activated(object sender, EventArgs e)
        {
            // read the new Property settings of the ColorTheme
            this.BackColor = Properties.Settings.Default.ColorTheme;
        }

        private void frmRetiredReport_Load(object sender, EventArgs e)
        {
            PoppulateGrid();
        }

        private void PoppulateGrid()
        {
            string sqlQuery = "SELECT Tool.ToolName, Brand.BrandName, Tool.Active " +
                              "FROM Brand INNER JOIN " +
                             "Tool ON Brand.BrandId = Tool.BrandId " +
                             "WHERE(Tool.Active = 0)";


            DataTable dtb = Context.GetDataTable(sqlQuery, "RetiredReport", true);

            _dgvRetiredReport = new DataView(dtb);

            dgvRetiredReport.DataSource = _dgvRetiredReport;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            _dgvRetiredReport.RowFilter = $"BrandName LIKE '%{txtSearch.Text}%'" + $"OR ToolName LIKE'%{txtSearch.Text}%'";
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ExportUsingBuilder();
        }

        private void ExportUsingBuilder()
        {
            StringBuilder csv = new StringBuilder();

            foreach (DataRowView drv in _dgvRetiredReport)
            {

                csv.AppendLine($"{drv["BrandName"].ToString()}, " +
                      $"{drv["ToolName"].ToString()}");


            }

            File.WriteAllText(Application.StartupPath + @"\ToolRetiededReport.csv", csv.ToString());
            MessageBox.Show("Export Completed", "Tool Retieded Report");
        }
    }
    
}
