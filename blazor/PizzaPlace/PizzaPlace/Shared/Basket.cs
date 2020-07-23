using System.Collections.Generic;

namespace PizzaPlace.Shared
{
    public class Basket
    {
        public Customer Customer { get; set; } = new Customer();
        public List<int> Orders { get; set; } = new List<int>();
        public bool HasPaid { get; set; } = false;

        public void Add(in int pizzaId)
        {
            Orders.Add(pizzaId);
        }
    }
}