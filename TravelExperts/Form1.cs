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
        PackagesDB articlesManager = new PackagesDB();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.panel14.SendToBack();
            this.panel4.BringToFront();
            //dataGridView1.DataSource = DataLayer.PackagesDB.GetPackages();

            comboBox1.DataSource = articlesManager.GetPackage();
            comboBox1.DisplayMember = "PkgName";
            comboBox1.ValueMember = "PkgName";
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
            this.panel4.SendToBack();
            this.panel14.BringToFront();
           
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
            pkg = articlesManager.GetPackageById(comboBox1.SelectedIndex + 1);

            txtName.Text = pkg.PkgName;
            txtStartDate.Text = pkg.PkgStartDate.ToString();
            txtEndDate.Text = pkg.PkgEndDate.ToString();
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
            this.panel14.SendToBack();
            this.panel4.BringToFront();
        }
    }
}
