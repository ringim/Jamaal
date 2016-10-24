using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using System.IO;
using System.Linq;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Data.Sql;


namespace Jamaal
{
    public partial class Add_Product : Telerik.WinControls.UI.RadForm
    {
        public DatabaseEntities jde = new DatabaseEntities();
        ObjectContext context = new ObjectContext("name=DatabaseEntities");
        public BindingList<tblProduct> po = new BindingList<tblProduct>();

        private byte[] picbyte;

        public Add_Product()
        {
            InitializeComponent();

            context.DefaultContainerName = "DatabaseEntities";
            ObjectSet<tblProductType> query = context.CreateObjectSet<tblProductType>();

            comboBox1.DataSource = query;
            comboBox1.DisplayMember = "Description";
            comboBox1.ValueMember = "ProductType";

        }


        private void radButton1_Click(object sender, EventArgs e)
        {
            MemoryStream stream = new MemoryStream();
            pictureBox1.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            picbyte = stream.ToArray();

            using (var move = new DatabaseEntities())
            {
                var products = new Jamaal.tblProduct()
                {
                    ProductName = textBox1.Text,
                    Description = radTextBox1.Text,
                    Price = decimal.Parse(radTextBox2.Text),
                    ProductType = (int)comboBox1.SelectedValue,
                    Picture = picbyte,
                                        
                };
                
                move.tblProducts.Add(products);
                move.SaveChanges();
                MessageBox.Show("Saved");
            }
        }
            
        private void clearTextBoxes()
        {
            radTextBox1.Clear();
            radTextBox2.Clear();
            comboBox1.Text = null;
            pictureBox1.Image = pictureBox1.BackgroundImage;
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "JPG Files(*.jpg) | *.jpg| PNG Files (*.png) |*.png | ALL Files(*.*)| *.* "; ;
            if (fd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(fd.FileName);
            }
        }
    }
}
