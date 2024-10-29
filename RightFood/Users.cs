using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RightFood
{
    [Serializable]
    public class Users : IEnumerable, IEnumerator
    {
        private List<User> users;
        private int currentIndex = -1;
        public User activeUser;

        public Users() {
            users = new List<User>();
        }

        public IEnumerator GetEnumerator()
        {
            return this;
        }

        public object Current => users[currentIndex];

        public bool MoveNext()
        {
            currentIndex++;
            if (currentIndex >= users.Count)
                return false;
            return true;
        }

        public void Reset()
        { 
            currentIndex = -1;
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

        public void Serialize(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, this);
            fs.Close();
        }

        public Users Deserialize(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.OpenOrCreate);
            BinaryFormatter bf = new BinaryFormatter();
            Users loaded_users = (Users)bf.Deserialize(fs);
            fs.Close();

            return loaded_users;
        }
    }

    [Serializable]
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
