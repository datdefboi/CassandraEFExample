using CassandraEFExample.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Cassandra.Storage;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CassandraEFExample
{
    public class DataBase:DbContext
    {
        private DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseCassandra("Contact Points=127.0.0.1;", opt =>
            {
                opt.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "hl");
            });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ForCassandraAddKeyspace("hl", new KeyspaceReplicationSimpleStrategyClass(2));
            modelBuilder.Entity<User>().ToTable("Users", "hl")
                .HasKey(p => new { p.Name });
        }
    }
}