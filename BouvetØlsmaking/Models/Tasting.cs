using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BouvetØlsmaking.Models
{
    public class Tasting
    {
        public int TastingId { get; set; }
        public string Name { get; set; }
        public DateTime TastingDate { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsBlind { get; set; }
        public ICollection<TastingBeer> TastingBeers { get; set; }
    }
}
