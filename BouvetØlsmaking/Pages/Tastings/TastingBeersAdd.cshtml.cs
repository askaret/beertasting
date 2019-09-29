using BouvetØlsmaking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BouvetØlsmaking.Pages.Tastings
{
    public class TastingBeersAddModel : BeerPageModel
    {
        [BindProperty]
        public List<SelectableHelper> SelectableBeers { get; set; }

        [BindProperty]
        public Tasting SelectedTasting { get; set; }

        [BindProperty]
        public List<Beer> Beers { get; set; }
        public TastingBeersAddModel(TastingContext context) : base(context)
        {

        }

        public IList<Tasting> Tastings { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (!CheckAndReportAdmin())
            {
                return Page();
            }

            if (id == null)
            {
                return NotFound();
            }

            SelectedTasting = await _context.Tasting.FirstOrDefaultAsync(m => m.TastingId == id);

            var query = from beer in _context.Beer where !(from bt in _context.TastingBeer.Where(x => x.TastingId == id) select bt.BeerId).Contains(beer.BeerId) select beer;
            Beers = await query.OrderBy(x => x.Name).ToListAsync();

            foreach (var b in Beers)
            {
                b.Brewery = _context.Brewery.FirstOrDefault(x => x.BreweryId == b.BreweryId);
                b.Beerstyle = _context.Beerstyle.FirstOrDefault(x => x.BeerstyleId == b.BeerStyleId);
            }

            SelectableBeers = new List<SelectableHelper>(from beer in Beers select new SelectableHelper(beer.BeerId, beer.Name, beer.ABV.ToString(), beer.Brewery.Name, beer.Beerstyle.Name));
            return Page();
        }

        [HttpPost]
        public async Task<IActionResult> OnPostAddBeer(int? id, int? tastingId)
        {
            if (id == null || tastingId == null)
            {
                return Page();
            }

            var maxSort = 0;
            if (_context.TastingBeer.Where(x => x.TastingId == tastingId).Any())
            {
                maxSort = _context.TastingBeer.Where(x => x.TastingId == tastingId).Max(x => x.SortOrder);
            }

            _context.TastingBeer.Add(new TastingBeer()
            {
                BeerId = id.Value,
                TastingId = tastingId.Value,
                SortOrder = maxSort + 1,
            });
            await _context.SaveChangesAsync();

            return RedirectToAction("index", new { id = tastingId });
        }
    }
}