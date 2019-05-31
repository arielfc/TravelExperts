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
    public partial class Form2 : Form
    {
        List<Product> products = ProductsDB.GetProducts();

        public Form2()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {/*
            listBox1.DataSource = products;
            listBox1.DisplayMember = "ProdName";
            listBox1.SetSelected(0, true);

            List<Product_Supplier> tempList = new List<Product_Supplier>();
            tempList = Products_SuppliersDB.GetPSByProductID(products[0].ProductId);
            List <Supplier> sList = new List<Supplier>();
            
            
            sList= SuppliersDB.GetSupplierListByID(l.SupplierId);
            
            listBox2.DataSource = sList;
            listBox1.DisplayMember = "SupName";
            listBox1.SetSelected(0, true);
            */
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            List<Supplier> suppliers =
                GetSupplierByID(products[listBox1.SelectedIndex + 1].);

            listBox2.DataSource = ;*/
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            ProductsDB.AddProduct(name);
            Product newproduct = ProductsDB.GetProductByName(name);
            products.Add(newproduct);
            listBox1.DataSource = null;
            listBox1.DataSource = products;
            listBox1.DisplayMember = "ProdName";
            this.listBox1.SelectedIndex = this.listBox1.Items.Count - 1;
        }
    }
}
