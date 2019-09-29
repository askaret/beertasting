using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BouvetØlsmaking.Models
{
    public class TastingBeerResult
    {
        public int TastingBeerResultId { get; set; }
        public int TastingId { get; set; }
        public int BeerId { get; set; }
        public int BeerClassId { get; set; }
        public string BeerName { get; set; }
        public string BeerClassName { get; set; }
        public string BreweryName { get; set; }
        public string BeerStyle { get; set; }
        public double Abv { get; set; }
        public string BreweryUrl { get; set; }
        public string RateBeerUrl { get; set; }
        public double ScoreTaste { get; set; }
        public double ScoreAppearance { get; set; }
        public double ScoreOverall { get; set; }
        public double ScoreFinal { get; internal set; }
    }
}
