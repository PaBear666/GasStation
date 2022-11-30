using System;
using System.Collections.Generic;
using System.Data.Entity;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GasStation.DB
{
    public class DataBaseContext:DbContext
    {
        public DataBaseContext()
       : base("DefaultConnection")
        { }
        public DbSet<User> Users { get; set; }
        public DbSet<Fuel> Fuels { get; set; }
        public DbSet<Transport> Transports { get; set; }
        public DbSet<Topology> Topologies { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // ...
            modelBuilder.Entity<User>().HasIndex(u => u.Name).IsUnique();
            // ...
        }
    }

}
