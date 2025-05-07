using ZuZuVirtual.core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ZuZuVirtual.Core.Entities;

//A base class that allows the application to interact with the database and manages the database schema.

namespace ZuZuVirtual.data
{
    public class DatabaseContext : DbContext
    {
         public DbSet<Address> Addresses { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-2NVQBV6\SQLEXPRESS;Database=ZuZuVirtualDb2; Trusted_Connection=True; TrustServerCertificate=True;");
            optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            //modelBuilder.ApplyConfiguration(new BrandConfiguration());
            // modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            //modelBuilder.ApplyConfiguration(new ContactConfiguration());
            // modelBuilder.ApplyConfiguration(new NewsConfiguration());
            // modelBuilder.ApplyConfiguration(new ProductConfiguration());
            // modelBuilder.ApplyConfiguration(new SliderConfiguration());

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); //The code line finds the classes form where it runs
            base.OnModelCreating(modelBuilder);
        }
    }
}
