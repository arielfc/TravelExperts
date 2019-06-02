using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataLayer;
using BusinessLayer;

namespace TravelExperts
{
    public partial class Form1 : Form
    {
        // 20190529 moved here -> global
        PackagesDB packageManager = new PackagesDB();
        SuppliersDB supplierManager = new SuppliersDB();
        // 20190602 make pkges as combox datasource
        List<Package> pkges = new List<Package>();

        //Set Panel status
        enum PanelStatus {Products, Suppliers}
		PanelStatus panelStatus;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.panel14.BringToFront();
            this.panel14.BringToFront();
            this.panel13.BringToFront();
            //dataGridView1.DataSource = DataLayer.PackagesDB.GetPackages();
            pkges = packageManager.GetPackage();
            comboBox1.DataSource = pkges;
            comboBox1.DisplayMember = "PkgName";
            comboBox1.ValueMember = "PkgName";

            // Invisible Panel 13 for products information
            panel13.Visible = false;

        }
        // Allows Dragging Borderless Form
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x84:
                    base.WndProc(ref m);
                    if ((int)m.Result == 0x1)
                        m.Result = (IntPtr)0x2;
                    return;
            }

            base.WndProc(ref m);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            this.panel14.SendToBack();
            this.panel4.BringToFront();
        }

	// Radio-button-Click event on "Products"----Edit/Add product
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

				this.panel14.SendToBack();
				this.panel15.SendToBack();
				//Show panel13
				this.panel13.BringToFront();
				this.panel13.Visible = true;

			comboBox2.DataSource = ProductsDB.GetProducts();
            comboBox2.DisplayMember = "ProdName";
            comboBox2.ValueMember = "ProductId";

			panelStatus = PanelStatus.Products;
			lblItemList.Text = "Products";
			lblUpdateItem.Text = "Update Product:";
			btnUpdate.Text = "Update Product";
			lblAddItem.Text = "Add Product:";
			btnAdd.Text = "Add Product";
			txtAddItem.Clear();

            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                if (rb.Checked)
                {
                    // Only one radio button will be checked
                    //Console.WriteLine("Changed: " + rb.Name);
                    //datagridview2.source = DataLayer.ProductsDB.GetProducts(); 
                }
            }
        }

	// Radio-button-Click event on "Suppliers"----Edit/Add supplier
		private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
				this.panel14.SendToBack();
				this.panel15.SendToBack();
				//Show panel13
				this.panel13.BringToFront();
				this.panel13.Visible = true;

			comboBox2.DataSource = SuppliersDB.GetSuppliers();
			comboBox2.DisplayMember = "SupName";
			comboBox2.ValueMember = "SupplierId";

			panelStatus = PanelStatus.Suppliers;
			lblItemList.Text = "Suppliers";
			lblUpdateItem.Text = "Update Supplier:";
			btnUpdate.Text = "Update Supplier";
			lblAddItem.Text = "Add Supplier:";
			btnAdd.Text = "Add Supplier";
			txtAddItem.Clear();

			RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                if (rb.Checked)
                {
                    // Only one radio button will be checked
                    //Console.WriteLine("Changed: " + rb.Name);
                    //datagridview2.source = DataLayer.SupplierDB.GetSuppliers(); 
                }
            }

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
                return;

            Package pkg = new Package();
            // 20190602 could be a problem when packageIds are not sequenced
            //pkg = packageManager.GetPackageById(comboBox1.SelectedIndex + 1);
            pkg = pkges[comboBox1.SelectedIndex];

            txtName.Text = pkg.PkgName;
            //txtStartDate.Text = pkg.PkgStartDate.ToString();
            //txtEndDate.Text = pkg.PkgEndDate.ToString();
            dateTimePicker1.Value = pkg.PkgStartDate;
            dateTimePicker2.Value = pkg.PkgEndDate;
            txtDesc.Text = pkg.PkgDesc;
            txtPrice.Text = pkg.PkgBasePrice.ToString();
            txtCommission.Text = pkg.PkgAgencyCommission.ToString();
        }

		private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBox2.SelectedIndex == -1)
				return;

			txtUpdateItem.Text = comboBox2.Text;
		}

		//**********I comment following code since I did not find where to use it. ***********
		//**********Please check and delete this part code. From Wei***************
		//private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
		//{
		//	if (comboBox1.SelectedIndex == -1)
		//		return;


		//	Supplier supplier = new Supplier();
		//	supplier = supplierManager.GetSupplierByID(comboBox1.SelectedIndex + 1);

		//	//txtName.Text = supplier.SupplierId;
		//	//txtStartDate.Text = supplier.SupName;
		//}

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            var formPopup = new Form2();
            formPopup.Show(this);
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            this.panel13.SendToBack();
            this.panel14.BringToFront();
            this.panel15.BringToFront();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        
        private void panel15_Paint(object sender, PaintEventArgs e)
        {

        }

	// Button-Click Event for Updating
        private void btnUpdate_Click(object sender, EventArgs e)
        {
			string txtUpdate = txtUpdateItem.Text.Trim();
			if (panelStatus==PanelStatus.Products)
			{
				if (UpdateProductValidation(txtUpdate)==true)
					UpdateProduct(txtUpdate);
			}
			else if(panelStatus==PanelStatus.Suppliers)
			{
				if (UpdateSupplierValidation(txtUpdate) == true)
					UpdateSupplier(txtUpdate);
			}
			else
			{
			}
        }

	// Validate input of product name before updating
		public bool UpdateProductValidation(string txtUpdate)
		{
		// Initialization for messagebox
			string message;
			string title = "Product Update";
			MessageBoxButtons button = MessageBoxButtons.OK;
			MessageBoxIcon icon = MessageBoxIcon.Warning;

		// Check whether input is empty
			if (txtUpdate == "")
			{
				message = "Warning! The updated product is empty.\nPlease input new product name.";
				MessageBox.Show(message, title, button, icon);
				return false;
			}
		
		// Check whether input is duplicate with any item of table contents
			int cunt = comboBox2.Items.Count;
			for (int i = 0; i < cunt; i++)
			{
				if (comboBox2.GetItemText(comboBox2.Items[i]) == txtUpdate)
				{
					if (comboBox2.SelectedIndex == i)
					{
						message = "Warning! Product name is not changed.\nPlease input new product name.";
					}
					else
					{
						message = "Warning! The updated product is duplicated with existed item in database.\nPlease input new product name.";
					}
					MessageBox.Show(message, title, button, icon);
					return false;
				}
			}
			return true;

		}

	// Update product name to database
		public void UpdateProduct(string txtUpdate)
		{
		// Initialization for messagebox
			string message;
			string title = "Product Update";
			MessageBoxButtons button = MessageBoxButtons.OK;
			MessageBoxIcon icon = MessageBoxIcon.Warning;

		// Excute updating
			int id = ProductsDB.GetProductId(comboBox2.Text);
			bool flag = ProductsDB.UpdateProduct(id, txtUpdate);
		
		// Check whether updating is successful, and show message
			if (flag == true)
			{
				int index = comboBox2.SelectedIndex;
				comboBox2.DataSource = ProductsDB.GetProducts();
				comboBox2.DisplayMember = "ProdName";
				comboBox2.ValueMember = "ProductId";
				comboBox2.SelectedIndex = index;
				message = "The product is updated successfully!";
				icon = MessageBoxIcon.None;
			}
			else
			{
				message = "Error: Fail to update product!\n Please try again or contact IT support.";
				icon = MessageBoxIcon.Error;
			}
			MessageBox.Show(message, title, button, icon);
		}

	// Validate input of supplier before updating
		public bool UpdateSupplierValidation(string txtUpdate)
		{
		// Initialization for messagebox
			string message;
			string title = "Supplier Update";
			MessageBoxButtons button = MessageBoxButtons.OK;
			MessageBoxIcon icon = MessageBoxIcon.Warning;

		// Check whether input is empty
			if (txtUpdate == "")
			{
				message = "Warning! The updated supplier is empty.\nPlease input new supplier name.";
				MessageBox.Show(message, title, button, icon);
				return false;
			}

		// Check whether input is duplicate with any item of table contents
			int cunt = comboBox2.Items.Count;
			for (int i = 0; i < cunt; i++)
			{
				if (comboBox2.GetItemText(comboBox2.Items[i]) == txtUpdate)
				{
					if (comboBox2.SelectedIndex == i)
					{
						message = "Warning! supplier name is not changed.\nPlease input new supplier name.";
					}
					else
					{
						message = "Warning! The updated supplier is duplicated with existed item in database.\nPlease input new supplier name.";
					}
					MessageBox.Show(message, title, button, icon);
					return false;
				}
			}
			return true;
		}
		
	// Update suplier's name to database
		public void UpdateSupplier(string txtUpdate)
		{
		// Initialization for messagebox
			string message;
			string title = "Supplier Update";
			MessageBoxButtons button = MessageBoxButtons.OK;
			MessageBoxIcon icon = MessageBoxIcon.Warning;

		// Excute updating
			int id = Convert.ToInt32(comboBox2.SelectedValue);
			bool flag = SuppliersDB.UpdateSupplier(id, txtUpdate);
			
		// Check whether updating is successful, and show message
			if (flag == true)
			{
				int index = comboBox2.SelectedIndex;
				comboBox2.DataSource = SuppliersDB.GetSuppliers();
				comboBox2.DisplayMember = "SupName";
				comboBox2.ValueMember = "SupplierId";
				comboBox2.SelectedIndex = index;
				message = "The supplier is updated successfully!";
				icon = MessageBoxIcon.None;
			}
			else
			{
				message = "Error: Fail to update supplier!\n Please try again or contact IT support.";
				icon = MessageBoxIcon.Error;
			}
			MessageBox.Show(message, title, button, icon);
		}


	// Button-Click Event for Adding
		private void btnAdd_Click(object sender, EventArgs e)
        {
           	string txtAdd = txtAddItem.Text.Trim();
			if(panelStatus==PanelStatus.Products)
			{
				if (AddProductValidation(txtAdd) ==true)
					AddProduct(txtAdd);
			}
		   else if(panelStatus == PanelStatus.Suppliers)
			{
				if (AddSupplierValidation(txtAdd) == true)
					AddSupplier(txtAdd);
			}
		   else
			{

			}
        }
		
	// Validate input of product before insert
		public bool AddProductValidation(string txtAdd)
		{
		// Innitialize for messagebox
			string message;
			string title = "New Product Insert";
			MessageBoxButtons button = MessageBoxButtons.OK;
			MessageBoxIcon icon;

		// Check whether input is empty
			if (txtAdd == "")
			{
				message = "Warning! The product's name is empty.\nPlease type product name.";
				icon = MessageBoxIcon.Warning;
				MessageBox.Show(message, title, button, icon);
				return false;
			}

		// Check whether input is duplicate with any item of table contents
			int cunt = comboBox2.Items.Count;
			for (int i = 0; i < cunt; i++)
			{

				if (comboBox2.GetItemText(comboBox2.Items[i]) == txtAdd)
				{
					message = "Warning! The new product name is duplicated with existed item in database.\nPlease input new product name.";
					icon = MessageBoxIcon.Warning;
					MessageBox.Show(message, title, button, icon);
					return false;
				}
			}
			return true;
		}
		
	// Insert product name to database
		public void AddProduct(string txtAdd)
		{
		// Innitialize for messagebox
			string message;
			string title = "New Product Insert";
			MessageBoxButtons button = MessageBoxButtons.OK;
			MessageBoxIcon icon;

		// Add new item to database
			bool flag = ProductsDB.AddProduct(txtAdd);

		// Check whether adding excution is successful, and show message
			if (flag == true)
			{
				comboBox2.DataSource = ProductsDB.GetProducts();
				comboBox2.DisplayMember = "ProdName";
				comboBox2.ValueMember = "ProductId";
				comboBox2.SelectedIndex = comboBox2.Items.Count - 1;
				txtAddItem.Text = "";
				message = "The product is added successfully!";
				icon = MessageBoxIcon.None;
			}
			else
			{
				message = "Error: Fail to add new product!\n Please try again or contact IT supports.";
				icon = MessageBoxIcon.Error;
			}
			MessageBox.Show(message, title, button, icon);
		}

	// Validate input of supplier before insert
		public bool AddSupplierValidation(string txtAdd)
		{
		// Innitialize for messagebox
			string message;
			string title = "New Product Insert";
			MessageBoxButtons button = MessageBoxButtons.OK;
			MessageBoxIcon icon;

		// Check whether input is empty
			if (txtAdd == "")
			{
				message = "Warning! The Supplier's name is empty.\nPlease type Supplier's name.";
				icon = MessageBoxIcon.Warning;
				MessageBox.Show(message, title, button, icon);
				return false;
			}

		// Check whether input is duplicate with any item of table contents
			int cunt = comboBox2.Items.Count;
			for (int i = 0; i < cunt; i++)
			{

				if (comboBox2.GetItemText(comboBox2.Items[i]) == txtAdd)
				{
					message = "Warning! The new supplier name is duplicated with existed item in database.\nPlease input new supplier name.";
					icon = MessageBoxIcon.Warning;
					MessageBox.Show(message, title, button, icon);
					return false;
				}
			}
			return true;
		}

	// Insert suplier's name to database
		public void AddSupplier(string txtAdd)
		{
		// Innitialize for messagebox
			string message;
			string title = "New Product Insert";
			MessageBoxButtons button = MessageBoxButtons.OK;
			MessageBoxIcon icon;

		// Generate new supplier Id based on max id in database
			int id = SuppliersDB.GetMaxId() + 1;

		// Add new item to database
			bool flag = SuppliersDB.AddSupplier(id, txtAdd);

		// Check whether adding excution is successful, and show message
			if (flag == true)
			{
				comboBox2.DataSource = SuppliersDB.GetSuppliers();
				comboBox2.DisplayMember = "SupName";
				comboBox2.ValueMember = "SupplierId";
				comboBox2.SelectedIndex = comboBox2.Items.Count - 1;
				txtAddItem.Text = "";
				message = "The supplier is added successfully!";
				icon = MessageBoxIcon.None;
			}
			else
			{
				message = "Error: Fail to add new supplier!\n Please try again or contact IT supports.";
				icon = MessageBoxIcon.Error;
			}
			MessageBox.Show(message, title, button, icon);
		}

		// Cancel operation of updating or Adding, reset all textboxes to default values
		private void btnCancel_Click(object sender, EventArgs e)
        {
            txtUpdateItem.Text = comboBox2.Text;
            txtAddItem.Text = "";
        }
    }
}
