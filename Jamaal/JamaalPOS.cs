using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Jamaal
{
    public partial class JamaalPOS : Telerik.WinControls.UI.RadForm
    {
        private BindingList<tblProduct> proList = new BindingList<tblProduct>();
        private DatabaseEntities jde = new DatabaseEntities();
        ObjectContext context = new ObjectContext("name=DatabaseEntities");

        public JamaalPOS()
        {
            InitializeComponent();
                      
            listBox1.DataSource = proList;
            listBox1.Font = new Font(FontFamily.GenericMonospace, listBox1.Font.Size);
            listBox1.DisplayMember = "ProductName";
            createTabbedPanel();           
            AddProductsToTab();
        }

        private decimal transactionTotal;

        public decimal TransactionTotal
        {
            get
            {
                return transactionTotal;
            }
            set
            {                 
                var formatter = new System.Globalization.CultureInfo("HA-LATN-NG");               
                
                transactionTotal = value;
                txtTotal.Text = string.Format(formatter.NumberFormat.CurrencySymbol = "₦", transactionTotal) + transactionTotal;
               
            }
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tblProduct prod = new tblProduct()
            {
                Description = "Product A ",
                Price = 10m
            };
            proList.Add(prod);
        }

        private void FormatList(object sender, ListControlConvertEventArgs e)
        {
            string CurrentDescription = ((tblProduct)e.ListItem).ProductName;
            var formatter = new System.Globalization.CultureInfo("HA-LATN-NG");
            string currentPrice = string.Format(formatter.NumberFormat.CurrencySymbol = "₦", ((tblProduct)e.ListItem).Price) + ((tblProduct)e.ListItem).Price;
            string currentDescriptionPadded = CurrentDescription.PadRight(20) + currentPrice;            
            e.Value = currentDescriptionPadded ;
            
        }

        private void createTabbedPanel()
        {
            foreach( tblProductType pt in jde.tblProductTypes)
            {
                tabControl1.TabPages.Add(pt.ProductType.ToString(), pt.Description);
            }
        }

        private void AddProductsToTab()
        {
            int i = 1;           

            foreach (TabPage tp in tabControl1.TabPages)
            {
                ObjectContext contex = new ObjectContext("name=DatabaseEntities");
               contex.DefaultContainerName = "DatabaseEntities";

                ObjectQuery<tblProduct> proQuery = new ObjectQuery<tblProduct>
                 ("SELECT VALUE v FROM tblProducts as v where v.ProductType = " + i.ToString(), contex);

                FlowLayoutPanel flp = new FlowLayoutPanel();
                flp.Dock = DockStyle.Fill;
                flp.AutoScroll = true;

                foreach (tblProduct pr in proQuery)
                {
                    Button btn = new Button();
                    btn.Size = new Size(100, 100);
                    btn.Text = pr.ProductName;
                    btn.Tag = pr;
                    btn.Click += new EventHandler(updateProductList);
                    flp.Controls.Add(btn);
                }
                tp.Controls.Add(flp);
                i++;
            }
        }

        string currentPrice;
        string currentDescriptionPadded;
        private void updateCustomerPanel(tblProduct product)
        {
            string CurrentDescription = product.Description;
            var formatter = new System.Globalization.CultureInfo("HA-LATN-NG");
            currentPrice = string.Format(formatter.NumberFormat.CurrencySymbol = "₦", product.Price) +product.Price ;
            currentDescriptionPadded = CurrentDescription.PadRight(30);

            textBox1.Text = currentDescriptionPadded + currentPrice;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            tblProduct selectedProd = (tblProduct)listBox1.SelectedItem;
            proList.Remove(selectedProd);           
        }

        void updateProductList(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            tblProduct tp = (tblProduct)b.Tag;
            proList.Add(tp);
            updateCustomerPanel(tp);            
            TransactionTotal = transactionTotal + (decimal)tp.Price;
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            Payment p = new Payment();                
           
            p.PaymentAmount = transactionTotal;
            p.paymentmade += new Payment.PaymentMadeEvent(p_PaymentMade);
            p.ShowDialog();
        }

        void p_PaymentMade(object sender, PaymentMadeEventArgs e)
        {
            ObjectContext oc = new ObjectContext("name=DatabaseEntities");
            oc.DefaultContainerName = "DatabaseEntities";

            tblTransaction transact = new tblTransaction();
            transact.TransactionDate = DateTime.Now;
            transact.TotalBill = (int)transactionTotal;

            if (e.PaymentSuccess == true )
            {
                  foreach (tblProduct pro in proList)
                {
                    pro.tblTransactionItems.Add(new tblTransactionItem()
                    {                        
                        TransactionItemID = transact.TransactionID,
                        ProductID = pro.ProductID,
                        ProductName = pro.Description,
                        Quantity = 0,
                        Price = (int)pro.Price
                    });
                    //oc.AddObject("tblTransactionItems", pro);
                    //oc.SaveChanges();

                    //var test = new tblTransactionItem()
                    //{
                    //    TransactionID = pro.ProductID,
                    //    TransactionItemID = transact.TransactionID,
                    //    ProductID = pro.ProductID,
                    //    ProductName = pro.Description,
                    //    Quantity = 0,
                    //    Price = (int)pro.Price
                    //};
                    //oc.AddObject("tblTransactionItems", test);
                    oc.SaveChanges();
                }

                oc.AddObject("tblTransactionItems", transact);
                oc.SaveChanges();

            }

            proList.Clear();         
            txtTotal.Clear();
            textBox1.Text = "Next Customer";
            transactionTotal = 0;
        }

       
    }
}
