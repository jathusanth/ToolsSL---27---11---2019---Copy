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
    public partial class frmClient : Form
    {
        long _PKID = 0;
        DataTable _ClientTable = null;
        bool _IsNew = false;

        public frmClient()
        {
            InitializeComponent();
            _IsNew = true;
            InitiazeDataTable();
        }
        public frmClient(long PKID)
        {
            InitializeComponent();
            _PKID = PKID;
            InitiazeDataTable();
        }
        #region Helper Method

       
        private void InitiazeDataTable()
        {
            string sqlQuery = $"SELECT * FROM Clients WHERE ClientId = {_PKID}";
            // get an existing client record based on the PKID and it's updateable
            _ClientTable = VR_Controller.Context.GetDataTable(sqlQuery, "Clients");

            if (_IsNew)
            {
                DataRow row = _ClientTable.NewRow();
                _ClientTable.Rows.Add(row);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmClient_Load(object sender, EventArgs e)
        {
            // read the new Property settings of the ColorTheme
            this.BackColor = Properties.Settings.Default.ColorTheme;
            BindControls();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (_ClientTable.Rows.Count > 0)
            {

                //not a bad idea to consider try & catch this
                //ALWAYS, do the EndEdit of the row otherwise the data will not persist.
                _ClientTable.Rows[0].EndEdit();
            }
            else
            {
                MessageBox.Show("No record exists.");
            }
            //call the method in our Context class to save the changes to the SQL Table
            VR_Controller.Context.saveDatabaseTable(_ClientTable);
        }
        public void BindControls()
        {
            //Binding the textbox txtToolId with the movieTable and mapping it to the 
            //database field called 'ToolId' and using the 'Text' property of the textbox
            //to display the value of the field
            txtClientId.DataBindings.Add("Text", _ClientTable, "ClientId");
            txtClientName.DataBindings.Add("Text", _ClientTable, "ClientName");
            txtNumber.DataBindings.Add("Text", _ClientTable, "Number");
        }
    }
   
}
 #endregion