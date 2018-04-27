using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace lab_6_Web_Api.Models
{
    public class AirportContext : DbContext
    {
        public AirportContext(DbContextOptions options) : base(options) { }

        public DbSet<Aircraft> Aircrafts { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Ticket> Tickets { get; set; }  
    }
}
