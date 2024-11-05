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
    public partial class FormUserRegister : Form
    {
        public FormUserRegister()
        {
            InitializeComponent();
        }

        private void bRegister_Click(object sender, EventArgs e)
        {
            if (tbName.Text.Trim().Length > 0 && tbAddress.Text.Trim().Length > 0)
            {
                Users users = new Users();
                users = users.Deserialize("data/users.dat");
                User user = new User(tbName.Text.Trim(), tbAddress.Text.Trim());
                if (users.AddUser(user))
                {
                    users.Serialize("data/users.dat");
                    Close();
                } else MessageBox.Show("Пользователь с таким именем уже существует");
            }
            else MessageBox.Show("Все поля обязательные");
        }
    }
}
