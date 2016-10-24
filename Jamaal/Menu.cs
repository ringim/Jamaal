using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace Jamaal
{
    public partial class Menu : Telerik.WinControls.UI.RadForm
    {
        //private JamaalDatabaseEntities jdb = new JamaalDatabaseEntities();
        public Menu()
        {
            InitializeComponent();            
        }

        private void Menu_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ViewProducts vp = new ViewProducts();
            vp.Show();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Add_Product ap = new Add_Product();
            ap.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            JamaalPOS pos = new JamaalPOS();
            pos.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            viewSales VS = new viewSales();
            VS.ShowDialog();

        }
    }
}
