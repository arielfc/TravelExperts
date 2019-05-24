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
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //dataGridView1.DataSource = DataLayer.PackagesDB.GetPackages();
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

        private void button1_Click(object sender, EventArgs e)
        {
            Package newPackage = new Package();

            newPackage.PkgName = textBox1.Text;
            newPackage.PkgStartDate = dateTimePicker1.Value;
            newPackage.PkgEndDate = dateTimePicker2.Value;
            newPackage.PkgDesc = textBox2.Text;
            newPackage.PkgBasePrice = Convert.ToDecimal(textBox3.Text);
            newPackage.PkgAgencyCommission = Convert.ToDecimal(textBox4.Text);

            //newArticle.Title = txtArticleTitle.Text;
            //newArticle.Body = txtArticleBody.Text;
            //newArticle.PublishDate = DateTime.Now;
            //newArticle.CategoryID = convert.ToInt16(ddlArticleCategories.SelectedValue);

            //Create a new Article Manager that allows you to insert a new article to database
            PackagesDB PackageManager = new PackagesDB();

            int newPackageID = PackageManager.InsertPackage(newPackage);
        }
    }
}
