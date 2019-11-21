using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BouvetØlsmaking.Models;

namespace BouvetØlsmaking.Pages.Beers
{
    public class IndexModel : BeerPageModel
    {   
        public IndexModel(TastingContext context) : base(context)
        {

        }

        public IList<BeerViewModel> Beer { get;set; }

        public async Task OnGetAsync()
        {
            CheckAndReportAdmin();

            var beerList = await _context.Beer.OrderBy(x => x.Name).ToListAsync();
            
            var breweries = await _context.Brewery.ToListAsync();
            var beerStyles = await _context.Beerstyle.ToListAsync();
            var beerClass = await _context.Beerclass.ToListAsync();

            var actualList = new List<BeerViewModel>();

            foreach (var beer in beerList)
            {
                actualList.Add(new BeerViewModel()
                {
                    Name = beer.Name,
                    ABV = beer.ABV,
                    Brewery = breweries.SingleOrDefault(x => x.BreweryId == beer.BreweryId)?.Name ?? "N/A",
                    BeerClass = beerClass.SingleOrDefault(x => x.BeerClassId == beer.BeerClassId)?.Name ?? "N/A",
                    BeerId = beer.BeerId,
                    BeerStyle = beerStyles.SingleOrDefault(x => x.BeerstyleId == beer.BeerStyleId)?.Name ?? "N/A",
                    RateBeerLink = beer.RateBeerLink,
                });
            }

            Beer = actualList;
        }
    }
}
