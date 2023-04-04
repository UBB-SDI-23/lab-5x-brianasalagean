using lab5.Models;
using Microsoft.EntityFrameworkCore;

namespace lab5.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DataContext() { }

        public virtual DbSet<SuperHero> SuperHeroes { get; set; } = null!;

        public virtual DbSet<Manager> Managers { get; set; } = null!;

        public virtual DbSet<SuperPower> SuperPowers { get; set; } = null!;

        public virtual DbSet<SuperHeroPower> SuperHeroPowers { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SuperHeroPower>()
                .HasKey(nameof(SuperHeroPower.SuperHeroId), nameof(SuperHeroPower.SuperPowerId));
        }
    }
}
