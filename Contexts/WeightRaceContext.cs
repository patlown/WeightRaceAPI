using WeightRaceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace WeightRaceAPI.Data
{
    public class WeightRaceContext : DbContext
    {
        public WeightRaceContext(DbContextOptions<WeightRaceContext> options) : base(options)
        {
        }

        public DbSet<User>? Users { get; set; }
        public DbSet<Weight>? Weights { get; set; }
    }
}
