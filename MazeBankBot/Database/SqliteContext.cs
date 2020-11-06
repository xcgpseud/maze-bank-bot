using MazeBankBot.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace MazeBankBot.Database
{
    public class SqliteContext : DbContext
    {
        public DbSet<TestEntity> Tests { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=mazebank.db");
        }
    }
}