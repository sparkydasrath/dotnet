using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PizzaPlace.Shared;

namespace PizzaPlace.Server
{
    public class PizzaPlaceDbContext : DbContext
    {
        public PizzaPlaceDbContext(DbContextOptions<PizzaPlaceDbContext> options): base(options)
        {
            
        }

        public DbSet<Pizza> Pizzas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            EntityTypeBuilder<Pizza> pizzaEntity = modelBuilder.Entity<Pizza>();
            pizzaEntity.HasKey(pizza => pizza.Id);
            pizzaEntity
                .Property(pizza => pizza.Price)
                .HasColumnType("money");

        }
    }
}