using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using System.Linq;
using System.Data.Entity.Core.Objects;

namespace Jamaal
{
    public partial class viewSales : Telerik.WinControls.UI.RadForm
    {

        private BindingList<tblProduct> proList = new BindingList<tblProduct>();
        private DatabaseEntities jde = new DatabaseEntities();
        ObjectContext context = new ObjectContext("name=DatabaseEntities");

        public viewSales()
        {
            InitializeComponent();

            ObjectSet<tblProductType> query2 = context.CreateObjectSet<tblProductType>();

            var s = from c in jde.tblTransactions
                    select c;
            var products = s.ToList();

            context.DefaultContainerName = "DatabaseEntities";
            dataGridView1.DataSource = products;

            DataGridViewButtonColumn col = new DataGridViewButtonColumn();
            col.UseColumnTextForButtonValue = true;
            col.Text = "ViewProducts";
            col.Name = "Products";
            dataGridView1.Columns.Add(col);
            dataGridView1.Columns["tblTransactionItems"].Visible = false;

        }
            

        private void viewSales_Load(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                MessageBox.Show("hollllla");

               
            }
        }
    }
}
