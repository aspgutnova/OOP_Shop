using System;
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
    public class CartItem
    {
        private int quantity;
        public Product Product { get; set; }
        public int Quantity {
            get { return quantity; }
            set { if (value > 0) quantity = value; }
        }

        public CartItem(Product item, int quantity)
        {
            Product = item;
            Quantity = quantity;
        }
    }

    [Serializable]
    public class Cart
    {
        public User user { get; }
        List<CartItem> Items { get; set; }
        
        public Cart(User user) 
        { 
            this.user = user;
            Items = new List<CartItem>();
        }

        public void Add(Product prod, int quantity, Catalog cat) 
        {
            foreach (var item in Items)
            {
                if (item.Product == prod)
                {
                    Increase(prod.ID, cat, quantity);
                    return;
                }
            }
            Items.Add(new CartItem(prod, quantity));
        }

        public void ShowCart(DataGridView dg)
        { 
            dg.Rows.Clear();
            foreach (var item in Items)
            {
                dg.Rows.Add(item.Product.ID, item.Product.Name, "-", item.Quantity, "+");
            }
        }

        public void Increase(int id, Catalog cat, int quantity = 1)
        {
            foreach (var item in Items)
            {
                if (item.Product.ID == id)
                {
                    if (cat.GetQuantityByID(id) > item.Quantity)
                        item.Quantity += quantity;
                    break;
                }
            }
        }

        public void Decrease(int id, int quantity = 1)
        {
            foreach (var item in Items)
            {
                if (item.Product.ID == id)
                {
                    if (item.Quantity <= quantity)
                        Items.Remove(item);
                    else
                        item.Quantity -= quantity;
                    break;
                }
            }
        }
    }

    [Serializable]
    public class CartList
    {
        public List<Cart> Carts { get; set; }

        public CartList()
        {
            Carts = new List<Cart>();
        }

        public void Add(Cart cart) 
        { 
            Carts.Add(cart);
        }

        public Cart FindCartByUsername(string userName)
        {
            foreach (Cart c in Carts)
            { 
                if (c.user.Name == userName)
                    return c;
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

        public CartList Deserialize(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.OpenOrCreate);
            BinaryFormatter bf = new BinaryFormatter();
            CartList loaded_carts = (CartList)bf.Deserialize(fs);
            fs.Close();

            return loaded_carts;
        }

    }
}
