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
    public partial class Payment : Telerik.WinControls.UI.RadForm
    {

        public delegate void PaymentMadeEvent(object sender, PaymentMadeEventArgs e);
        public event PaymentMadeEvent paymentmade;
        private decimal paymentAmount;

        public decimal PaymentAmount
        {
            get { return paymentAmount; }
            set
            {
                var formatter = new System.Globalization.CultureInfo("HA-LATN-NG");
                paymentAmount = value;
                txtPaymentAmount.Text = string.Format(formatter.NumberFormat.CurrencySymbol = "₦", paymentAmount) +paymentAmount;
            }
        }

        public Payment()
        {
            InitializeComponent();
        }

        private void PaymentHasBeenMade(object sender, EventArgs e)
        {
            decimal Total = 0;
            try
            {
                Total = decimal.Parse(txtAmountToPay.Text) - decimal.Parse(txtPaymentAmount.Text.TrimStart('₦'));
            }
            catch
            {
                MessageBox.Show("Please Enter Valid Amount");
                return;
            }            

            if (Total < 0)
            {
               // txtAmountToPay.Text = Total.ToString();
            }
            else
            {
                 var formatter = new System.Globalization.CultureInfo("HA-LATN-NG");
                string naira = formatter.NumberFormat.CurrencySymbol = "₦";
                MessageBox.Show("Please give "+naira.ToString() +Total + " change", "Change");
                paymentmade(this, new PaymentMadeEventArgs() { PaymentSuccess = true });
                this.Hide();
            }            
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Payment_Load(object sender, EventArgs e)
        {

        }
    }

    public class PaymentMadeEventArgs: EventArgs
    {
        private bool paymentSuccess;
        public bool PaymentSuccess
        {
            get { return paymentSuccess; }
            set { paymentSuccess = value; }
        }
    }
}
