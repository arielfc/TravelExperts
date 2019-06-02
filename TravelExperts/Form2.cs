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
        // Products listBox1
        List<Product> products = new List<Product>();

        // Suppliers listBox2
        List<Supplier> sList = new List<Supplier>();

        // string listBox3 for showing
        List<string> list3 = new List<string>();

        // Product_Supplier list for DB
        List<Product_Supplier> list3_ToDB = new List<Product_Supplier>();

        public Form2()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            products = ProductsDB.GetProducts();
            listBox1.DataSource = products;
            listBox1.DisplayMember = "ProdName";
            int Box1_initialIndex = 0;
            listBox1.SetSelected(Box1_initialIndex, true);
            LoadSupplierBox(products[Box1_initialIndex].ProductId);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSupplierBox(products[listBox1.SelectedIndex].ProductId);
        }
        private void LoadSupplierBox(int id)
        {
            sList = Products_SuppliersDB.GetSuppliersByProductID(id);

            listBox2.DataSource = null;
            if (sList.Capacity != 0)
            {
                listBox2.DataSource = sList;
                listBox2.DisplayMember = "SupName";
                listBox2.SetSelected(0, true);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string s1 = products[listBox1.SelectedIndex].ProdName.ToString();
            string s2 = sList[listBox2.SelectedIndex].SupName.ToString();
            list3.Add(s1 + " - " + s2);
            listBox3.DataSource = null;
            listBox3.DataSource = list3;
            listBox3.SelectedIndex = listBox3.Items.Count - 1;

            // Prepare for DB
            Product_Supplier nps = new Product_Supplier();
            nps.ProductId = products[listBox1.SelectedIndex].ProductId;
            nps.SupplierId = sList[listBox2.SelectedIndex].SupplierId;
            list3_ToDB.Add(nps);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            list3.RemoveAt(listBox3.SelectedIndex);
            list3_ToDB.RemoveAt(listBox3.SelectedIndex);
            listBox3.DataSource = null;
            listBox3.DataSource = list3;
            listBox1.SelectedIndex = 0;
            listBox2.SelectedIndex = 0;
            listBox3.SelectedIndex = listBox3.Items.Count - 1;
        }

        private void textBox7_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox7.Text))
            {
                e.Cancel = true;
                textBox7.Focus();
                errorProvider1.SetError(textBox7, "Please Enter a Package Name !");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(textBox7, null);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            /*
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                MessageBox.Show(textBox7.Text, "Message", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            */
            int newID = PackagesDB.AddPackageForForm2(textBox7.Text,
                                            dateTimePicker1.Value, dateTimePicker2.Value,
                                            textBox9.Text,
                                            Convert.ToDecimal(textBox11.Text.ToString()),
                                            Convert.ToDecimal(textBox10.Text.ToString()));
            if (newID == -1)
            {
                MessageBox.Show("Error adding Package.");
            }
            else
            {
                foreach (var ps in list3_ToDB)
                {
                    int psid = Products_SuppliersDB.GetPSID_By_P_SID(ps.ProductId, ps.SupplierId);
                    if (!Packages_Products_SuppliersDB.AddPPS(newID, psid))
                    {
                        MessageBox.Show("Error adding Packages_Products_Suppliers.");
                    }
                }
            }

            listBox3.DataSource = null;
            list3.Clear();
            list3_ToDB.Clear();
        }

        private void dateTimePicker2_Validating(object sender, CancelEventArgs e)
        {
            if (dateTimePicker1.Value >= dateTimePicker2.Value)
            {
                e.Cancel = true;
                textBox7.Focus();
                errorProvider2.SetError(dateTimePicker2,
                    "Package End Date must be later than Package Start Date !");
            }
            else
            {
                e.Cancel = false;
                errorProvider2.SetError(dateTimePicker2, null);
            }
        }

        private void textBox9_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox9.Text))
            {
                e.Cancel = true;
                textBox9.Focus();
                errorProvider3.SetError(textBox9, "Please Enter Package Description !");
            }
            else
            {
                e.Cancel = false;
                errorProvider3.SetError(textBox9, null);
            }
        }

        private void textBox10_Validating(object sender, CancelEventArgs e)
        {
            if (Convert.ToInt32(textBox10.Text) > Convert.ToInt32(textBox11.Text))
            {
                e.Cancel = true;
                textBox10.Focus();
                errorProvider4.SetError(textBox10,
                    "Agency Commission cannot be greater than the Base Price !");
            }
            else
            {
                e.Cancel = false;
                errorProvider4.SetError(textBox10, null);
            }
        }

        private void listBox3_Validating(object sender, CancelEventArgs e)
        {/*
            if (string.IsNullOrWhiteSpace(listBox3.ValueMember))
            {
                e.Cancel = true;
                button2.Focus();
                errorProvider5.SetError(listBox3, "Please Enter Package Description !");
            }
            else
            {
                e.Cancel = false;
                errorProvider5.SetError(listBox3, null);
            }*/
        }
    }
}
