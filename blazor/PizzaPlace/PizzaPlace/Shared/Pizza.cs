namespace PizzaPlace.Shared
{
    public class Pizza
    {
        public int Id { get; }
        public string Name { get; }
        public decimal Price { get; }
        public Spiciness Spiciness { get; }

        public Pizza(int id, string name, decimal price, Spiciness spiciness)
        {
            Id = id;
            Name = name;
            Price = price;
            Spiciness = spiciness;
        }
    }
}