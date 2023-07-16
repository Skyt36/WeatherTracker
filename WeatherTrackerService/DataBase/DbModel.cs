using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WeatherTrackerService.DataBase
{
    public partial class DbModel : DbContext
    {
        public DbModel()
            : base("name=DbModel")
        {
        }

        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Weather> Weather { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<City>()
                .HasMany(e => e.Weather)
                .WithRequired(e => e.City)
                .WillCascadeOnDelete(false);
        }
    }
}
