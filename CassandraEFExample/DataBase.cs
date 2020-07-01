using CassandraEFExample.Models;
using Microsoft.EntityFrameworkCore;

namespace CassandraEFExample
{
    public class DataBase:DbContext
    {
        private DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseCassandra("Contact Points=127.0.0.1");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ForCassandraAddKeyspace("hl", new KeyspaceReplicationSimpleStrategyClass(2));
            modelBuilder.Entity<User>().ToTable("Users", "hl").HasKey(p => p.Name);
            modelBuilder.Entity<User>().ForCassandraSetClusterColumns(p => p.Name);
        }
    }
}