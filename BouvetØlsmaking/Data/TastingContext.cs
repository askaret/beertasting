using Microsoft.EntityFrameworkCore;

namespace BouvetØlsmaking.Models
{
    public class TastingContext : DbContext
    {
        public TastingContext (DbContextOptions<TastingContext> options)
            : base(options)
        {
        }

        public DbSet<Brewery> Brewery { get; set; }
        public DbSet<Beer> Beer { get; set; }
        public DbSet<Taster> Taster { get; set; }
        public DbSet<Tasting> Tasting { get; set; }
        public DbSet<Beerstyle> Beerstyle { get; set; }
        public DbSet<BeerClass> Beerclass { get; set; }
        public DbSet<TastingBeer> TastingBeer { get; set; }
        public DbSet<Vote> Vote { get; set; }
        public DbSet<RandomFact> RandomFact { get; set; }
        public DbSet<TastingBeerResult> TastingResult { get; set; }
    }
}
