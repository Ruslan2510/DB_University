using Layer.Models;
using System.Data.Entity;

namespace Layer.Contexts
{
    public class StorageUnitContext : DbContext
    {
        public DbSet<Material> Materials { get; set; }
        public DbSet<MeasureInfo> MeasureInfo { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<StorageUnit> StorageUnits { get; set; }

        //static StorageUnitContext()
        //{
        //    Database.SetInitializer<StorageUnitContext>(new ContextInitializer());
        //}

        public StorageUnitContext() : base("ConnectionString") { }
    }
}
