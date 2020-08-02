using Microsoft.AspNetCore.Mvc;
using PizzaPlace.Shared;
using System.Collections.Generic;
using System.Linq;

namespace PizzaPlace.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly PizzaPlaceDbContext dbContext;

        public OrdersController(PizzaPlaceDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost("/orders")]
        public IActionResult CreateOrder([FromBody] Basket basket)
        {
            Customer customer = basket.Customer;
            Order order = new Order() { PizzaOrders = new List<PizzaOrder>() };
            customer.Order = order;

            foreach (int pizzaId in basket.Orders)
            {
                Pizza pizza = dbContext.Pizzas.Single(p => p.Id == pizzaId);
                order.PizzaOrders.Add(new PizzaOrder { Pizza = pizza, Order = order });
            }

            order.TotalPrice = order.PizzaOrders.Sum(po => po.Pizza.Price);
            dbContext.Customers.Add(customer);
            dbContext.SaveChanges();
            return Ok();
        }
    }
}