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
    public partial class frmReport : Form
    {
        DataView _dvgReport = null;

        public frmReport()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmReport_Activated(object sender, EventArgs e)
        {
            // read the new Property settings of the ColorTheme
            this.BackColor = Properties.Settings.Default.ColorTheme;
        }

        private void frmReport_Load(object sender, EventArgs e)
        {
            PoppulateGrid();
        }

        private void PoppulateGrid()
        {

            string sqlQuery = "SELECT Clients.ClientName, Tool.ToolName, Hire.DateRented, Hire.DateReturned, Tool.Active " +
                           "FROM Clients INNER JOIN " +
                           "Hire ON Clients.ClientId = Hire.ClientId INNER JOIN " +
                           "HireItem ON Hire.HireId = HireItem.HireId INNER JOIN " +
                           "Tool ON HireItem.ToolId = Tool.ToolId " +
                           "Order by DateRented DESC";

            DataTable dtb = Context.GetDataTable(sqlQuery, "ToolReport", true);

            _dvgReport = new DataView(dtb);

           dvgReport.DataSource = _dvgReport;
            
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            _dvgReport.RowFilter = $"ClientName LIKE '%{txtSearch.Text}%'" + $"OR ToolName LIKE'%{txtSearch.Text}%'";
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ExportUsingBuilder(); 
        }

        private void ExportUsingBuilder()
        {
            StringBuilder csv = new StringBuilder();

            foreach (DataRowView drv in _dvgReport)
            {

                csv.AppendLine($"{drv["DateRented"].ToString()}, " +
                      $"{drv["ClientName"].ToString()}, " +
                      $"{drv["ToolName"].ToString()}, " +
                      $"{drv["DateReturned"].ToString()}");


            }

            File.WriteAllText(Application.StartupPath + @"\ToolReport.csv", csv.ToString());
            MessageBox.Show("Export Completed", "Tool Report");
        }
    }
}
