using Microsoft.EntityFrameworkCore;
using SarajevoFilmFestival.Models;

namespace SarajevoFilmFestival.Data;


    public class AppDbContext : DbContext
    {

        public AppDbContext()
        {
            
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=sff.db");
        }

        public DbSet<Customer> Customers { get; set; }
            public DbSet<Screening> Screenings { get; set; }
            public DbSet<Booking> Bookings { get; set; }
        }
    
    
