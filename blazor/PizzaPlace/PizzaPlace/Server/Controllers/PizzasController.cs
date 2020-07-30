using Microsoft.AspNetCore.Mvc;
using PizzaPlace.Shared;
using System.Collections.Generic;
using System.Linq;

namespace PizzaPlace.Server.Controllers
{
    [ApiController]
    public class PizzasController : ControllerBase
    {
        private static readonly List<Pizza> Pizzas = new List<Pizza>
        {
            new Pizza(1, "Pepperoni", 8.99M, Spiciness.Spicy),
            new Pizza(2, "Margarita", 7.99M, Spiciness.None),
            new Pizza(3, "Diabolo", 9.99M, Spiciness.Hot)
        };

        [HttpGet("pizzas")]
        public IQueryable<Pizza> GetPizzas() 
            => Pizzas.AsQueryable();
    }
}