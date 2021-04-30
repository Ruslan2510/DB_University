using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo2CodeFirst
{
    class UserContext : DbContext
    {
        public UserContext() : base("DBConnectionEF")
        { }

        public DbSet<User> Users { get; set; }
    }
}
