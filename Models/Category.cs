using System;

namespace ProductCRUD.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Products { get; set; }
        public DateTime Created { get; set; }
    }
}