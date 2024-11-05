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
    public partial class FormUserManage : Form
    {
        public FormUserManage()
        {
            InitializeComponent();
        }

        private void FormUserManage_Load(object sender, EventArgs e)
        {
            Users users = new Users();
            users = users.Deserialize("data/users.dat");
            users.Reset();
            foreach (var u in users)
                dgvUsers.Rows.Add((u as User).Name, (u as User).Address, 'X');
        }

        private void dgvUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex==2 && e.RowIndex!=dgvUsers.RowCount-1)
            {
                dgvUsers.Rows.RemoveAt(e.RowIndex);
            }
        }

        private void FormUserManage_FormClosed(object sender, FormClosedEventArgs e)
        {
            Users newUsers = new Users();
            for (int i = 0; i < dgvUsers.RowCount-1; i++)
            {
                User u = new User(dgvUsers.Rows[i].Cells[0].Value.ToString(), dgvUsers.Rows[i].Cells[1].Value.ToString());
                if (!newUsers.AddUser(u)) MessageBox.Show($"Пользователь с именем {0} существует", dgvUsers.Rows[i].Cells[0].Value.ToString());
            }
            newUsers.Serialize("data/users.dat");
        }
    }
}
