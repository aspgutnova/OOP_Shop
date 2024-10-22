using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            catalog.GetDataFromFile("warehouse.csv");
            lvCatalog.Items.AddRange(catalog.GetProductsList().ToArray());

            User alina = new User("Алина", "Ватутина 46");
            users.AddUser(alina);
            users.AddUser(new User("Вова", "Церетели 16"));

            tsmiUsers.DropDownItems.AddRange(users.GetUsersList().ToArray());
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

        private void rbAlina_CheckedChanged(object sender, EventArgs e)
        {
            users.activeUser = users.FindUserByName(((RadioButton)sender).Text);
            tslUsername.Text = users.activeUser.Name;
            if ((cart = listofcarts.FindCartByUsername(users.activeUser.Name)) == null)
            {
                cart = new Cart(users.activeUser);
                listofcarts.Add(cart);
            }
            cart.ShowCart(dgvCart);
        }
    }
}
