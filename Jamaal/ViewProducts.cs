using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using Telerik.WinControls;
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;

namespace Jamaal
{
    public partial class ViewProducts : Telerik.WinControls.UI.RadForm
    {
        private DatabaseEntities jdb = new DatabaseEntities();
        ObjectContext context = new ObjectContext("name=DatabaseEntities");

        public ViewProducts()
        {
            InitializeComponent();

            context.DefaultContainerName = "DatabaseEntities";          
            ObjectSet<tblProductType> query2 = context.CreateObjectSet<tblProductType>();

            var s = from c in jdb.tblProducts
              select c;
            var products = s.ToList();

            dataGridView1.DataSource = products;
            dataGridView1.RowTemplate.Height = 30;
           
                       
            dataGridView1.Columns["ProductType"].Visible = false;
            dataGridView1.Columns["TblProductType"].Visible = false;
            dataGridView1.Columns["TblTransactionItems"].Visible = false;

            comboFilter.DataSource = query2;
            comboFilter.ValueMember = "ProductType";
            comboFilter.DisplayMember = "Description";                   

        }

        private void ViewProducts_Load(object sender, EventArgs e)
        {
            
        }
         
        private void button1_Click(object sender, EventArgs e)
        {
            Menu mn = new Jamaal.Menu();
            mn.Show();          
        }

        private void comboFilter_SelectionChangeCommitted(object sender, EventArgs e)
        {            
            jdb = new DatabaseEntities();
            int co = Convert.ToInt32(comboFilter.SelectedValue);

            var query = from c in jdb.tblProducts
                        where c.ProductType == co
                        select c;
            var products = query.ToList().Select(r => new tblProduct
            {
                ProductName = r.ProductName,
                ProductType = r.ProductType,
                Picture = r.Picture,
                Price = r.Price,
                Description = r.Description
            }).ToList();       
            dataGridView1.DataSource = products;
            dataGridView1.Columns["ProductID"].Visible = false;

        }
       
    }
}
