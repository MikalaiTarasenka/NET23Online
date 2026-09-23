using Microsoft.EntityFrameworkCore;

namespace AnimalWorld.Api.DbStuff
{
    public class ApiContext : DbContext
    {
        public DbSet<InterestingFact> InterestingFacts { get; set; }

        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }
    }
}
