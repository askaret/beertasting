using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BouvetØlsmaking.Models
{
    public class TastingBeer
    {
        public int TastingBeerId { get; set; }
        public int TastingId { get; set; }
        public Tasting Tasting { get; set; }
        public int BeerId { get; set; }
        public Beer Beer { get; set; }
        public int SortOrder { get; set; }
    }
}
