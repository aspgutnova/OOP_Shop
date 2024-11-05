using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace RightFood
{
    [Serializable]
    public class Users : IEnumerable, IEnumerator
    {
        private List<User> users;
        private int currentIndex = -1;
        [NonSerialized]
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
        
        public bool AddUser(User user)
        {
            foreach (User u in users)
                if (u.Equals(user))
                    return false;
            users.Add(user);
            return true;
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
    public class User: IEquatable<User>
    {
        public string Name { get; }
        public string Address { get; }

        public User(string name, string address)
        {
            Name = name;
            Address = address;
        }

        public bool Equals(User other)
        {
            return Name.ToLower() == other.Name.ToLower();
        }
    }

}
