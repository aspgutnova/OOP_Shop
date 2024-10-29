using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace RightFood
{
    public partial class FormMain : Form
    {
        Catalog catalog = new Catalog();
        Users users = new Users();
        Cart cart;
        CartList listofcarts = new CartList();
        public ToolStripMenuItem activeMenuItem;

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            catalog.GetDataFromFile("data/warehouse.csv");
            lvCatalog.Items.AddRange(catalog.GetProductsList().ToArray());
            users = users.Deserialize("data/users.dat");
            listofcarts = listofcarts.Deserialize("data/carts.dat");

            tsmiUsers.DropDownItems.AddRange(GetUsersList().ToArray());
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            if (users.activeUser != null)
            {
                try
                {
                    int id = Convert.ToInt32(lvCatalog.SelectedItems[0].SubItems[2].Text);
                    Product selProduct = catalog.FindByID(id);
                    cart.Add(selProduct, 1, catalog);
                    cart.ShowCart(dgvCart);
                }
                catch
                {
                    MessageBox.Show("Выберите продукт!");
                }
            }
            else MessageBox.Show("Выберите пользователя!");
        }

        private void dgvCart_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = Convert.ToInt32(dgvCart.Rows[e.RowIndex].Cells[0].Value);
            if (id > 0)
            {
                switch (e.ColumnIndex)
                {
                    case 2:
                        cart.Decrease(id);
                        break;
                    case 4:
                        cart.Increase(id, catalog);
                        break;
                }
                cart.ShowCart(dgvCart);
            }
        }

        public List<ToolStripMenuItem> GetUsersList()
        {
            List<ToolStripMenuItem> list = new List<ToolStripMenuItem>();
            users.Reset();
            foreach (User user in users)
            {
                ToolStripMenuItem item = new ToolStripMenuItem(user.Name) { CheckOnClick = true };
                item.Click += new System.EventHandler(MenuItem_Click);
                list.Add(item);
            }
            return list;
        }

        private void MenuItem_Click(object sender, EventArgs e)
        {
            if (activeMenuItem != null) activeMenuItem.Checked = false;
            activeMenuItem = (sender as ToolStripMenuItem);
            activeMenuItem.Checked = true;
            users.Reset();
            foreach (var u in users)
                if ((u as User).Name == activeMenuItem.Text)
                {
                    users.activeUser = (u as User);
                    tslUsername.Text = users.activeUser.Name;

                    if ((cart = listofcarts.FindCartByUsername(users.activeUser.Name)) == null)
                    {
                        cart = new Cart(users.activeUser);
                        listofcarts.Add(cart);
                    }
                    cart.ShowCart(dgvCart);
                }
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            users.Serialize("data/users.dat");
            listofcarts.Serialize("data/carts.dat");
        }

        private void FormMain_Activated(object sender, EventArgs e)
        {
            
        }
    }
}
