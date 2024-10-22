using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RightFood
{
    public class Users
    {
        public List<User> users;
        public ToolStripMenuItem activeMenuItem;
        public User activeUser;

        public Users() {
            users = new List<User>();
        }

        public void AddUser(User user)
        {
            users.Add(user);
        }

        public User FindUserByName(string userName) 
        {
            foreach (User user in users)
            { 
                if (user.Name == userName)
                    return user;
            }
            return null;
        }

        public List<ToolStripMenuItem> GetUsersList() {
            List<ToolStripMenuItem> list = new List<ToolStripMenuItem>();
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
            activeMenuItem = ((ToolStripMenuItem)sender);
            activeMenuItem.Checked = true;
            foreach (var u in users)
                if (u.Name == activeMenuItem.Text)
                    activeUser = u;
        }
    }

    public class User
    {
        public string Name { get; }
        public string Address { get; }

        public User(string name, string address)
        {
            Name = name;
            Address = address;
        }
    }

}
