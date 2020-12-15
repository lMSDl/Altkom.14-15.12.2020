using System.Collections.Generic;

namespace Models
{
    public class Order : Entity
    {
        public User User {get; set;}
        public ICollection<Product> Products {get; set;}
    }
}