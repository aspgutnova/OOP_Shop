using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RightFood
{
    public partial class AddProductForm : Form
    {
        public AddProductForm()
        {
            InitializeComponent();
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            //c.ProductPlus(tbProduct.Text,
            //              Convert.ToDecimal(tbPrice.Text),
            //              cbProductType.Text,
            //              Convert.ToInt32(tbQty.Text));
            //ShowProducts();
        }

        private void bDelete_Click(object sender, EventArgs e)
        {
            //string s = c.ProductMinus(tbProduct.Text, Convert.ToInt32(tbQty.Text));
            //if (s.Length > 0) MessageBox.Show(s);
            //else ShowProducts();
        }

        private void AddProductForm_Load(object sender, EventArgs e)
        {
            //cbProductType.Items.Clear();
            //foreach (var item in ProductType.Names)
            //    cbProductType.Items.Add(item);
        }
    }
}
