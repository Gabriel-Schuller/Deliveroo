using Deliveroo.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Deliveroo.Service
{
    public class DataContext : DbContext
    {
        private readonly IConfiguration _config;

        public DataContext(DbContextOptions options, IConfiguration config) : base(options)
        {
            _config = config;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Package> Packages { get; set; }
    }
}
