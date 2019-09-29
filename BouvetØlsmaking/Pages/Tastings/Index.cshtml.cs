using BouvetØlsmaking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BouvetØlsmaking.Pages.Tastings
{
    public class IndexModel : BeerPageModel
    {
        public Tasting CurrentTasting { get; set; }
        public IndexModel(TastingContext context) : base(context)
        {
            
        }

        public IList<Tasting> Tasting { get;set; }

        public async Task OnGetAsync()
        {
            CheckAndReportAdmin();

            Tasting = await _context.Tasting.OrderByDescending(x => x.TastingDate).ToListAsync();
        }

        public async Task<IActionResult> OnPostCalculateAsync(int id)
        {
            CurrentTasting = await _context.Tasting.FirstOrDefaultAsync(x => x.TastingId == id);
            if (CurrentTasting == null)
            {
                TempData["CustomError"] = "Invalid tasting, calculation aborted";
                return Page();
            }

            await PopulateResultsAsync();
            TempData["Calculate"] = $"Calculated results for {CurrentTasting.Name}";
            return Page();
        }

        private async Task PopulateResultsAsync()
        {
            var tasting = _context.Tasting.FirstOrDefault(x => x.TastingId == CurrentTasting.TastingId);
            if (tasting == null)
            {
                TempData["CustomError"] = "Invalid tasting, go home";
                return;
            }

            var tastingResults = new List<TastingBeerResult>();

            var votes = _context.Vote.Where(x => x.TastingId == CurrentTasting.TastingId).GroupBy(x => x.BeerId);
            foreach (var beerVotes in votes)
            {
                var avgAppearance = beerVotes.Select(x => x.Appearance).Average();
                var avgTaste = beerVotes.Select(x => x.Taste).Average();
                var avgOverall = beerVotes.Select(x => x.Overall).Average();
                var finalScore = (avgAppearance + avgOverall + (avgTaste * 2)) / 4;
                var beer = _context.Beer.FirstOrDefault(x => x.BeerId == beerVotes.Key);
                var beerClass = _context.Beerclass.FirstOrDefault(x => x.BeerClassId == beer.BeerClassId);
                var beerStyle = _context.Beerstyle.FirstOrDefault(x => x.BeerstyleId == beer.BeerStyleId);
                var brewery = _context.Brewery.FirstOrDefault(x => x.BreweryId == beer.BreweryId);

                tastingResults.Add(new TastingBeerResult()
                {
                    TastingId = CurrentTasting.TastingId,
                    BeerClassId = beerClass.BeerClassId,
                    BeerId = beer.BeerId,                    
                    BeerName = beer.Name,
                    BeerClassName = beerClass.Name,
                    BeerStyle = beerStyle.Name,
                    BreweryName = brewery.Name,
                    BreweryUrl = brewery.Website,
                    Abv = Math.Round(beer.ABV, 2),
                    RateBeerUrl = beer.RateBeerLink,
                    ScoreAppearance = Math.Round(avgAppearance, 2),
                    ScoreTaste = Math.Round(avgTaste, 2),
                    ScoreOverall = Math.Round(avgOverall, 2),
                    ScoreFinal = Math.Round(finalScore, 2)
                });
            }

            var oldResults = await _context.TastingResult.Where(x => x.TastingId == CurrentTasting.TastingId).ToListAsync();
            _context.TastingResult.RemoveRange(oldResults);
            await _context.TastingResult.AddRangeAsync(tastingResults);

            await _context.SaveChangesAsync();
        }
    }
}
