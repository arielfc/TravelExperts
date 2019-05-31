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

            comboBox1.DataSource = packageManager.GetPackage();
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

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            this.panel14.SendToBack();
            //this.panel14.Visible = false;
            this.panel15.SendToBack();
            //this.panel15.Visible = false;
            //Show panel13
            this.panel13.BringToFront();
            this.panel13.Visible = true;

            comboBox2.DataSource = ProductsDB.GetProducts();
            comboBox2.DisplayMember = "ProdName";
            comboBox2.ValueMember = "ProdName";


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

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
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
            pkg = packageManager.GetPackageById(comboBox1.SelectedIndex + 1);

            txtName.Text = pkg.PkgName;
            //txtStartDate.Text = pkg.PkgStartDate.ToString();
            //txtEndDate.Text = pkg.PkgEndDate.ToString();
            dateTimePicker1.Value = pkg.PkgStartDate;
            dateTimePicker2.Value = pkg.PkgEndDate;
            txtDesc.Text = pkg.PkgDesc;
            txtPrice.Text = pkg.PkgBasePrice.ToString();
            txtCommission.Text = pkg.PkgAgencyCommission.ToString();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
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

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

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
        
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
                return;

            
            Supplier supplier = new Supplier();
            supplier = supplierManager.GetSupplierByID(comboBox1.SelectedIndex + 1);

            //txtName.Text = supplier.SupplierId;
            //txtStartDate.Text = supplier.SupName;
        }

        private void panel15_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == -1)
                return;

            Product pdt = new Product();
            pdt = ProductsDB.GetProductByID(comboBox2.SelectedIndex + 1);

            txtUptProduct.Text = pdt.ProdName;
        }

        private void btnUptPdt_Click(object sender, EventArgs e)
        {
            string message;
            string title = "Product Update";
            MessageBoxButtons button = MessageBoxButtons.OK;
            MessageBoxIcon icon = MessageBoxIcon.Warning;

            int cunt = comboBox2.Items.Count;
            for (int i = 0; i < cunt; i++)
            {
                if (comboBox2.GetItemText(comboBox2.Items[i]) == txtUptProduct.Text)
                {
                    if (comboBox2.SelectedIndex == i)
                    {
                        message = "Warning! Product name is not changed.\nPlease input new product name.";
                    }
                    else
                    {
                        message = "Warning! The updated product is duplicated in list.\nPlease input new product name.";
                    }
                    MessageBox.Show(message, title, button, icon);
                    return;
                }
            }
            int id = ProductsDB.GetProductId(comboBox2.Text);
            bool flag = ProductsDB.UpdateProduct(id, txtUptProduct.Text);
            if (flag == true)
            {
                int index = comboBox2.SelectedIndex;
                comboBox2.DataSource = ProductsDB.GetProducts();
                comboBox2.DisplayMember = "ProdName";
                comboBox2.ValueMember = "ProdName";
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

        private void btnAddPdt_Click(object sender, EventArgs e)
        {
            string message;
            string title = "New Product Insert";
            MessageBoxButtons button = MessageBoxButtons.OK;
            MessageBoxIcon icon;
            int cunt = comboBox2.Items.Count;
            for (int i = 0; i < cunt; i++)
            {
                if (comboBox2.GetItemText(comboBox2.Items[i]) == txtAddProduct.Text)
                {
                    message = "Warning! The product's name has been in list.\nPlease input new product name.";
                    icon = MessageBoxIcon.Warning;
                    MessageBox.Show(message, title, button, icon);
                    return;
                }
            }
            bool flag = ProductsDB.AddProduct(txtAddProduct.Text);
            if (flag == true)
            {
                comboBox2.DataSource = ProductsDB.GetProducts();
                comboBox2.DisplayMember = "ProdName";
                comboBox2.ValueMember = "ProdName";
                comboBox2.SelectedIndex = comboBox2.Items.Count - 1;
                txtAddProduct.Text = "";
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtUptProduct.Text = comboBox2.Text;
            txtAddProduct.Text = "";
        }
    }
}
