using Microsoft.EntityFrameworkCore;
using SuperHeroAPI;

namespace SupperHeroAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext>options) : base(options) { }

        public DbSet<SupperHero>SupperHeroes { get; set; }
    }
}
