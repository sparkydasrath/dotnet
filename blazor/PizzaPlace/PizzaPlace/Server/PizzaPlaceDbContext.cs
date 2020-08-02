using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PizzaPlace.Shared;

namespace PizzaPlace.Server
{
    public class PizzaPlaceDbContext : DbContext
    {
        public PizzaPlaceDbContext(DbContextOptions<PizzaPlaceDbContext> options) : base(options)
        {
        }

        // tables
        public DbSet<Pizza> Pizzas { get; set; }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            EntityTypeBuilder<Pizza> pizzaEntity = modelBuilder.Entity<Pizza>();
            pizzaEntity.HasKey(pizza => pizza.Id);
            pizzaEntity
                .Property(pizza => pizza.Price)
                .HasColumnType("money");

            EntityTypeBuilder<Customer> customerEntity = modelBuilder.Entity<Customer>();
            customerEntity.HasKey(customer => customer.Id);
            customerEntity.HasOne(customer => customer.Order)
                .WithOne(order => order.Customer)
                .HasForeignKey<Order>(
                    order => order.CustomerId);

            EntityTypeBuilder<Order> orderEntity = modelBuilder.Entity<Order>();
            orderEntity.HasKey(order => order.Id);
            orderEntity.HasMany(order => order.PizzaOrders)
                .WithOne(pizzaOrder => pizzaOrder.Order);

            modelBuilder.Entity<PizzaOrder>()
                .HasOne(po => po.Pizza)
                .WithMany();
        }
    }
}