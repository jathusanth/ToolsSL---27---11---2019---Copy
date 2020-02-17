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
    public partial class frmBrands : Form
    {
        long _PKID = 0;
        DataTable _BrandTable = null;
        bool _IsNew = false;
        public frmBrands()
        {
            InitializeComponent();
            _IsNew = true;
            InitiazeDataTable();
        }

        public frmBrands(long PKID)
        {
            InitializeComponent();
            _PKID = PKID;
            InitiazeDataTable();
        }
        private void InitiazeDataTable()
        {

            string sqlQuery = $"SELECT * FROM Brand WHERE BrandId = {_PKID}";
            // get an existing Tool record based on the PKID and it's updateable
            _BrandTable = VR_Controller.Context.GetDataTable(sqlQuery, "Brand");

            if (_IsNew)
            {
                DataRow row = _BrandTable.NewRow();
                _BrandTable.Rows.Add(row);
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Brands_Load(object sender, EventArgs e)
        {
            BindControls();
        }

        private void BindControls()
        {
            // Binding the Textbox txtBrandId with the BrandTable and mapping it to the 
            // databasefiled called 'BrandId' and using the ' Text' property of the Textbox
            // to dispaly the value of the filed.
            txtBrandId.DataBindings.Add("Text", _BrandTable, "BrandId");
            txtBrandName.DataBindings.Add("Text", _BrandTable, "BrandName");

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_BrandTable.Rows.Count > 0)
            {
                // ALWAYS, do the EndEdit of the rqw, otherwise the data will not presist.
                _BrandTable.Rows[0].EndEdit();
            }
            else
            {
                MessageBox.Show("No record exists");
            }

            // call the method in our Context class to save the change to the sql Table
            VR_Controller.Context.saveDatabaseTable(_BrandTable);
            this.Close();
        }
    }
}
