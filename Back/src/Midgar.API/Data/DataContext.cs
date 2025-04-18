using Microsoft.EntityFrameworkCore;
using Midgar.API.Models;

namespace Midgar.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}

        public DbSet<Event> Events { get; set; }
    }
}