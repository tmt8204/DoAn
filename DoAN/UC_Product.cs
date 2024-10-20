using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAN
{
    public partial class UC_Product : UserControl
    {
        public event EventHandler onslect = null;
        public UC_Product()
        {
            InitializeComponent();
        }

        private void picProduct_Click(object sender, EventArgs e)
        {
            onslect?.Invoke(this, e);
        }

        public int id {  get; set; }
        public string total { get; set; }
        public string name
        {
            get { return labelProductName.Text; }
            set { labelProductName.Text = value; }
        }

        public string UniPrice
        {
            get { return labelPrice.Text; }
            set { labelPrice.Text = value; }
        }

        public Image Image
        {
            get { return picProduct.Image; }
            set { picProduct.Image = value; }
        }
    }
}
