using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Product
    {
        public int IdProduct { get; set; }

        public string ProductName { get; set; }

        public string Description { get; set; }

        public float Price { get; set; }

        public string Picture { get; set; }

        public bool Disponibility { get; set; }

        public int IdRestaurant { get; set; }

        public int IdProductType { get; set; }

        public override string ToString()
        {
            return "__________________________" +
                   "\nIdProduct: " + IdProduct +
                   "\nProductName: " + ProductName +
                   "\nDescription: " + Description +
                   "\nPrice: " + Price +
                   "\nPicture: " + Picture +
                   "\nDisponibility: " + Disponibility +
                   "\nIdRestaurant: " + IdRestaurant +
                   "\nIdProductType: " + IdProductType +
                   "\n__________________________";
        }
    }
}
