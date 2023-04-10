using WeightRaceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace WeightRaceAPI.Data
{
    public class WeightRaceContext : DbContext
    {
        public WeightRaceContext(DbContextOptions<WeightRaceContext> options) : base(options)
        {
        }

        public virtual DbSet<User>? Users { get; set; }
        public virtual DbSet<Weight>? Weights { get; set; }

        public virtual void SetModified(object entity)
        {
            Entry(entity).State = EntityState.Modified;
        }
    }
}
