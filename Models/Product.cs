using System;

namespace ProductCRUD.Models
{
    public class Product
    {
        public int Id { get; set; }
        public int Category { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public DateTime Created { get; set; }
    }
}