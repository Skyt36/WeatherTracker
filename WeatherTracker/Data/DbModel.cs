using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WeatherTracker.Data
{
    public partial class DbModel : DbContext
    {
        public DbModel()
            : base("name=ModelConnect")
        {
        }

        public virtual DbSet<City> City { get; set; }
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
