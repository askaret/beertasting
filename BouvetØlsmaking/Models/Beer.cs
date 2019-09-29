using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BouvetØlsmaking.Models
{
    public class Beer
    {
        public int BeerId { get; set; }
        public int BeerStyleId { get; set; }
        public int BeerClassId { get; set; }
        public Beerstyle Beerstyle { get; set; }
        public int BreweryId { get; set; }
        public Brewery Brewery { get; set; }
        public string Name { get; set; }
        public double ABV { get; set; }        
        public string RateBeerLink { get; set; }

        public ICollection<TastingBeer> TastingBeers { get; set; }
    }
}
