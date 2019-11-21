using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BouvetØlsmaking.Models
{
    public class BeerViewModel
    {
        public int BeerId { get; set; }
        public string BeerStyle { get; set; }
        public string BeerClass { get; set; }
        public string Brewery { get; set; }
        public string Name { get; set; }
        public double ABV { get; set; }        
        public string RateBeerLink { get; set; }

        public ICollection<TastingBeer> TastingBeers { get; set; }
    }
}
