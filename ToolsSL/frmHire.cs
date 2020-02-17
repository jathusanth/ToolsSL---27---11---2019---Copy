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
    public partial class frmHire : Form
    {
        private long _PKID = 0;
        DataTable _dtbHires = null, _dtbHireItems = null;
        bool _IsNew = false;
       // private object cboClient;

        /// <summary>
        /// Constructor to create new Rental 
        /// </summary>
        public frmHire()
        {
            InitializeComponent();
            _IsNew = true;
            InitializeDataTable();
        }

        /// <summary>
        /// Constructor to open and update existing Rental  based on _PKID.       
        /// </summary>
        /// <param name="pKID"></param>
        public frmHire(long pKID)
        {
            InitializeComponent();
            this._PKID = pKID;
            InitializeDataTable();
        }

        /// <summary>
        /// This method will Retrieve the record based on the primary _PKID
        /// If _IsNEW is set to true 
        /// give us the primary 
        /// </summary>
        private void InitializeDataTable()
        {
            _dtbHires = Context.GetDataTable($"SELECT * FROM Hire WHERE HireId={_PKID}", "Hire"); //hireid

            PopulateGrid();


            // TODO: PopulateGrid

            // call the BeginEdit method to indicate that we are starting the edit operation
            // in out DataTAble _dbgRentals and to make the binded controls on edit mode.
            if (_dtbHires.Rows.Count > 0)
                _dtbHires.Rows[0].BeginEdit();
        }

        private void PopulateGrid()
        {
            string sql = "SELECT HireItemId, ToolName, HireId " +
                         "FROM HireItem INNER JOIN Tool " +
                         "ON HireItem.ToolId = Tool.ToolId " +
                         $"WHERE HireId = {_PKID} " +
                         "ORDER BY HireItemId";
            _dtbHireItems = Context.GetDataTable(sql, "HireItem");
            dgvHireItems.DataSource = _dtbHireItems;
        }

        /// <summary>
        /// This method will Retrieve the record based on the primary _PKID
        /// if _ISNew is set to True, it will create a new row and add the new row to the
        /// DataTable _dgbRental
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmHire_Activated(object sender, EventArgs e)
        {
            // read the new Property settings of the ColorTheme
            this.BackColor = Properties.Settings.Default.ColorTheme;
           
        }

        private void frmHire_Load(object sender, EventArgs e)
        {
            PopulateComboBox();
            BindControls();
           
        }

        private void BindControls()
        {
            // to bind a control to the DataTable, you need to select the properties 
            // you want to bind e.g. Text property, the what DataTable to use in 
            // bindings and what column or field name to use 
            txtHireId.DataBindings.Add("Text", _dtbHires, "HireId");
            dtpDateRented.DataBindings.Add("Text", _dtbHires, "DateRented");
            dtpDateReturned.DataBindings.Add("Text", _dtbHires, "DateReturned");
            cboClient1.DataBindings.Add("SelectedValue", _dtbHires, "ClientId");

            // make the DateReturned DateTimerPicker empty fro new record or when DateReturned 
            // is null

            if (_IsNew || string.IsNullOrEmpty(_dtbHires.Rows[0]["DateReturned"].ToString()))
            {
                dtpDateReturned.Format = DateTimePickerFormat.Custom;
                dtpDateReturned.CustomFormat = " ";

            }

            // make the combo box to select nothing when creating new record or rental
            if (_IsNew)
              cboClient1.SelectedIndex = -1;
        }
        private void PopulateComboBox()
        {
            // Create a DataTable that will become the data source of the combo box
            DataTable dtb = new DataTable();
            dtb = Context.GetDataTable("Clients");

            // set the ValueMemeber.
            // The ValueMember is the object that stores the primary key of the 
            // source table. whem a user selects a row from the combo box, the 
            // value of the valumember will be saved in the databse.
            cboClient1.ValueMember = "ClientId";

            // set the DisplayMember.
            // The DisplayMember is the store the column or field from
            // the source table and it's the colum that we want to display in the 
            // combo box.
             cboClient1.DisplayMember = "ClientName";

            // set the data source of the combo box by using the data table we've
            // created above.
            cboClient1.DataSource = dtb;
        }

        private void dtpDateReturned_ValueChanged(object sender, EventArgs e)
        {
            // change the DateReturned format so it doesn't return empty when user change or selected a date 
            dtpDateReturned.CustomFormat = "dd-MMM-yyyy";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {            
            _dtbHires.Rows[0]["DateRented"] = dtpDateRented.Value.ToString("yyyy-MM-dd");

            // always do an EndEdit befor saving the data will not persist in the database 
            _dtbHires.Rows[0].EndEdit();
            Context.saveDatabaseTable(_dtbHires);
        }

        private void dgvHireItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvHireItems.CurrentCell == null) return;

            // Get the value in the first colum of the grid, which in this case is the RentalItem 
            long PKID = long.Parse(dgvHireItems[0, dgvHireItems.CurrentCell.RowIndex].Value.ToString());

            frmHireItems frm = new frmHireItems(PKID);
            if (frm.ShowDialog() == DialogResult.OK);
            PopulateGrid();
        }

        private void cboclient1_SelectedIndexChanged(object sender, EventArgs e)
        {
           btnInsert.Enabled = (cboClient1.SelectedIndex > -1);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure you want to delete the item?", "Tools", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    long PKID = long.Parse(dgvHireItems[0, dgvHireItems.CurrentCell.RowIndex].Value.ToString());

                    // use the DeleteRecord method of the Context class
                    Context.DeleteRecores("HireItem", "HireItemId", PKID.ToString());
                    PopulateGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Happy Deleting!");
                }


            }
        }

        private void dgvHireItems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvHireItems.CurrentCell == null) return;

            long PKID = long.Parse(dgvHireItems[0, dgvHireItems.CurrentCell.RowIndex].Value.ToString());

            frmHireItems frm = new frmHireItems(PKID);
            if (frm.ShowDialog() == DialogResult.OK)
                PopulateGrid();
        }


        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (_IsNew && _PKID <= 0)
            {
                string ColumnNames = "ClientId, DateRented, DateReturned";
                long ClientId = long.Parse(cboClient1.SelectedValue.ToString());
                string DateHired = dtpDateRented.Value.ToString("yyyy-MM-dd");
                string ColumnValues = $"{ClientId}, {DateHired}, null";
                txtHireId.Text = Context.InsertParentTable("Hire", ColumnNames, ColumnValues).ToString();
                _PKID = long.Parse(txtHireId.Text);
                InitializeDataTable();
            }

            frmHireItems frm = new frmHireItems(txtHireId.Text);
            if (frm.ShowDialog() == DialogResult.OK)
                PopulateGrid();
        }
    }
        
   
}
