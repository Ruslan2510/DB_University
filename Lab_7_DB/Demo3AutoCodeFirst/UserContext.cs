using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo3AutoCodeFirst
{
    class UserContext : DbContext
    {
        public UserContext() : base("UserContext")
        { }
        public DbSet<User> Users { get; set; }
    }
}
