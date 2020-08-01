using System;

namespace PizzaPlace.Shared
{
    public class Pizza
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Spiciness Spiciness { get; set; }

        public Pizza() { /* empty constructor for entity framework  */}
        public Pizza(int id, string name, decimal price, Spiciness spiciness)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name),"A pizza needs a name!");
            Price = price;
            Spiciness = spiciness;
        }
    }
}