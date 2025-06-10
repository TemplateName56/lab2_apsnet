using lab2.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace lab2.Utils
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Trip> Trips => Set<Trip>();
        public DbSet<Booking> Bookings => Set<Booking>();
        public DbSet<Question> Questions => Set<Question>();
        public DbSet<Destination> Destinations => Set<Destination>();
        public DbSet<ProposedDestination> ProposedDestinations => Set<ProposedDestination>();
    }
}
