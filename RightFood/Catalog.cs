using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RightFood
{
    public class Catalog
    {
        Dictionary<Product, int> Products { get; set; }

        public Catalog()
        {
            Products = new Dictionary<Product, int>();
        }

        public void ProductPlus(string product, decimal price, string producttype, int qty)
        {
            foreach (var item in Products)
            {
                if (item.Key.Name.ToLower() == product.Trim().ToLower() && item.Key.Price == price)
                {
                    Products[item.Key] += qty;
                    return;
                }
            }
            Product p = new Product(product, price, producttype);
            Products.Add(p, qty);
        }

        public string ProductMinus(string product, int qty)
        {
            foreach(var item in Products)
            {
                if (item.Key.Name.ToLower() == product.Trim().ToLower() && item.Value >= qty)
                {
                    Products[item.Key] -= qty;
                    return "";
                }
            }
            return "Извините, товару нет, приходите завтра";
        }

        public void GetDataFromFile(string filename)
        { 
            StreamReader sr= new StreamReader(filename);
            while (!sr.EndOfStream)
            { 
                string[] data = sr.ReadLine().Split(',');
                Product p = new Product(data[0], Convert.ToDecimal(data[1]), data[2]);
                Products.Add(p, Convert.ToInt32(data[3]));
            }
            sr.Close();
        }

        public List<ListViewItem> GetProductsList()
        {
            List<ListViewItem> list = new List<ListViewItem>();

            foreach (var item in Products)
            {
                ListViewItem listviewitem = new ListViewItem(item.Key.ToString());
                listviewitem.SubItems.Add(item.Value.ToString());
                listviewitem.SubItems.Add(item.Key.ID.ToString());
                list.Add(listviewitem);
            }

            return list;
        }

        public Product FindByID(int id)
        {
            foreach (var item in Products)
            {
                if (item.Key.ID == id) return item.Key;
            }
            return null;
        }

        public int GetQuantityByID(int id)
        {
            foreach (var item in Products)
            {
                if (item.Key.ID == id) return item.Value;
            }
            return 0;
        }

    }
}
