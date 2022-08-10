using Microsoft.EntityFrameworkCore;

namespace aspnetserver.Data
{
    internal sealed class AppDBContext : DbContext
    {
        public DbSet<Sweater> Sweaters { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder) => dbContextOptionsBuilder.UseSqlite("Data Source=./Data/AppDB.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Sweater[] sweatersToSeed = new Sweater[6];

            for (int i = 1; i <= 6; i++) {
                sweatersToSeed[i - 1] = new Sweater
                {
                    SweaterId = i,
                    manufacturer = $"Sweater {i}",
                    quantity = i
                };

            }

            modelBuilder.Entity<Sweater>().HasData(sweatersToSeed);
        }


    }
}
