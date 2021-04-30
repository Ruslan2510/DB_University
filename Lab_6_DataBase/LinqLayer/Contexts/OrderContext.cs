using System.Data.Entity;

namespace LinqLayer.Contexts
{
    public class OrderContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Broker> Brokers { get; set; }
        public DbSet<Order> Orders { get; set; }
        //static OrderContext()
        //{
        //    Database.SetInitializer<OrderContext>(new ContextInitializer());
        //}

        public OrderContext() : base("ConnectionString") { }
    }
}