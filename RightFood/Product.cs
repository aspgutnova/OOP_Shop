using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightFood
{
    static class ProductType
    {
        static public List<string> Names = new List<string>
        {
            "бакалея", "мясные", "хлебобулочные", "кисломолочные"
        };
    }

    [Serializable]
    public class Product
    {
        public int ID { get; }
        static int count = 0;
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }

        public Product(string name, decimal price, string type)
        {
            ID = ++count;
            Name = name;
            Price = price;
            if (ProductType.Names.Contains(type))
                Type = type;
        }

        public override string ToString()
        {
            return $"{Name} ({Type}) - {Price} руб.";
        }
    }


}
