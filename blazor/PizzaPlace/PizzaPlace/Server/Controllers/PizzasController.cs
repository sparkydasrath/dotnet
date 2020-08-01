using Microsoft.AspNetCore.Mvc;
using PizzaPlace.Shared;
using System.Collections.Generic;
using System.Linq;

namespace PizzaPlace.Server.Controllers
{
    [ApiController]
    public class PizzasController : ControllerBase
    {
        private readonly PizzaPlaceDbContext dbContext;

        public PizzasController(PizzaPlaceDbContext dbContext) => this.dbContext = dbContext;

        /*
        // getting pizza from static list 
        private static readonly List<Pizza> Pizzas = new List<Pizza>
        {
            new Pizza(1, "Pepperoni", 8.99M, Spiciness.Spicy),
            new Pizza(2, "Margarita", 7.99M, Spiciness.None),
            new Pizza(3, "Diabolo", 9.99M, Spiciness.Hot)
        };

        [HttpGet("pizzas")]
        public IQueryable<Pizza> GetPizzas() => Pizzas.AsQueryable(); */

        // now getting pizzas from the entity created database
        [HttpGet("pizzas")]
        public IQueryable<Pizza> GetPizzas() => dbContext.Pizzas;

        [HttpPost("pizzas")]
        public IActionResult InsertPizza([FromBody] Pizza pizza)
        {
            dbContext.Pizzas.Add(pizza);
            dbContext.SaveChanges();
            return Created($"pizzas/{pizza.Id}", pizza);
        }
    }
}