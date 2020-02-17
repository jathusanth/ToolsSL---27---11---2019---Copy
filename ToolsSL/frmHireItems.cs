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
    public partial class frmHireItems : Form
    {
        long _PKID = 0, _HireId = 0;
        DataTable _dtbHireItems = null;
        bool _IsNew = false;
       

        /// <summary>
        /// Constructor to Create new Hire Item
        /// </summary>
        public frmHireItems(string HireId)
        {
            _IsNew = true;
            _HireId = long.Parse(HireId);
            InitializeComponent();
            InitializeDataTable();
        }
        /// <summary>
        /// Constructor to open and update existing Rental Item 
        /// </summary>
        /// <param name="pKID"<>/parama>
        public frmHireItems(long pKID)
        {
            InitializeComponent();
            _PKID = pKID;
            InitializeDataTable();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmHireItems_Load(object sender, EventArgs e)
        {
            PopulateComboBox();
            BindControls();
            if (_IsNew)
                txtHireId.Text = _HireId.ToString();
        }

        private void BindControls()
        {
            txtHireId.DataBindings.Add("Text", _dtbHireItems, "HireId");
            cboTool.DataBindings.Add("SelectedValue", _dtbHireItems, "ToolId");
        }

        private void PopulateComboBox()
        {
            DataTable dtb = new DataTable();
            dtb = Context.GetDataTable("Tool");
            cboTool.ValueMember = "ToolId";
            cboTool.DisplayMember = "ToolName";
            cboTool.DataSource = dtb;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_IsNew)
            {
                // this block of code is to make sure that the validate event of the textbox 
                // txtRentalId will trigger and subsewuently will store the value of 
                // txtRental in the DataTable _dtbRentalItems
                txtHireId.Focus();
                txtHireId.Text = _HireId.ToString();
                btnSave.Focus();

            }
            _dtbHireItems.Rows[0].EndEdit();
            Context.saveDatabaseTable(_dtbHireItems);
        }

        private void frmHireItems_Activated(object sender, EventArgs e)
        {
            // read the new Property settings of the ColorTheme
            this.BackColor = Properties.Settings.Default.ColorTheme;
        }

        private void InitializeDataTable()
        {
            string sql = $"SELECT * FROM HireItem WHERE HireItemId = {_PKID}";
            _dtbHireItems = Context.GetDataTable(sql, "HireItem");

            if (_IsNew)
            {
                DataRow row = _dtbHireItems.NewRow();
                _dtbHireItems.Rows.Add(row);
            }
        


        }
    }
}
