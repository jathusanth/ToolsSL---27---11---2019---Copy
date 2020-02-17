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
    public partial class frmTool : Form
    {
        long _PKID = 0;
        DataTable _ToolTable = null;
        bool _IsNew = false;

        public frmTool()
        {
            InitializeComponent();
            _IsNew = true;
            InitiazeDataTable();
        }

        public frmTool(long PKID)
        {
            InitializeComponent();
            _PKID = PKID;
            InitiazeDataTable();
        }
        #region Helper Method

        
        private void InitiazeDataTable()
        {
            
                string sqlQuery = $"SELECT * FROM Tool WHERE ToolId = {_PKID}";
                // get an existing Tool record based on the PKID and it's updateable
                _ToolTable = VR_Controller.Context.GetDataTable(sqlQuery, "Tool");

            if (_IsNew)
                {
                    DataRow row = _ToolTable.NewRow();
                    _ToolTable.Rows.Add(row);
                }
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmTool_Activated(object sender, EventArgs e)
        {
            // read the new Property settings of the ColorTheme
            this.BackColor = Properties.Settings.Default.ColorTheme;
        }

        private void frmTool_Load(object sender, EventArgs e)
        {
            PopulateComboBox();
            BindControls();
        }

        private void BindControls()
        {
            // Binding the Textbox txtToolId with the toolTable and mapping it to the 
         // databasefiled called 'ToolId' and using the ' Text' property of the Textbox
         // to dispaly the value of the filed.
            txtToolId.DataBindings.Add("Text", _ToolTable, "ToolId");
            txtToolName.DataBindings.Add("Text", _ToolTable, "ToolName");
            cboBrandName.DataBindings.Add("SelectedValue", _ToolTable, "BrandId");
            chkActive.DataBindings.Add("Checked", _ToolTable, "Active", true);
          
        }
        private void PopulateComboBox()
        {
            // Create a DataTable that will become the data source of the combo box
            DataTable dtb = new DataTable();
            dtb = Context.GetDataTable("Brand");

            // set the ValueMemeber.
            // The ValueMember is the object that stores the primary key of the 
            // source table. whem a user selects a row from the combo box, the 
            // value of the valumember will be saved in the databse.
            cboBrandName.ValueMember = "BrandId";

            // set the DisplayMember.
            // The DisplayMember is the store the column or field from
            // the source table and it's the colum that we want to display in the 
            // combo box.
            cboBrandName.DisplayMember = "BrandName";

            // set the data source of the combo box by using the data table we've
            // created above.
            cboBrandName.DataSource = dtb;
        }

        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_ToolTable.Rows.Count > 0)
            {
                // ALWAYS, do the EndEdit of the rqw, otherwise the data will not presist.
                _ToolTable.Rows[0].EndEdit();
            }
            else
            {
                MessageBox.Show("No record exists");
            }

            // call the method in our Context class to save the change to the sql Table
            VR_Controller.Context.saveDatabaseTable(_ToolTable);
            this.Close();
        }
    }
    
}
