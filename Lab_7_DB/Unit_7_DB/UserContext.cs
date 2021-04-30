using System.Data.Entity;

namespace Code_First
{
    class UserContext : DbContext
    {
        public UserContext() : base("DbConnection")
        { }
        public DbSet Users { get; set; }
    }
}
