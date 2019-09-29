using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BouvetØlsmaking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BouvetØlsmaking.Pages.Tastings
{
    public class TastingBeersModel : BeerPageModel
    {
        
        [BindProperty]
        public Tasting SelectedTasting { get; set; }

        [BindProperty]
        public List<TastingBeer> Beers{ get; set; }
        public TastingBeersModel(TastingContext context) : base(context)
        {
            
        }

        public IList<Tasting> Tastings { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (!CheckAndReportAdmin())
                return Page();

            if (id == null)
            {
                return NotFound();
            }

            SelectedTasting = await _context.Tasting.FirstOrDefaultAsync(m => m.TastingId == id);

            if (SelectedTasting == null)
            {
                return NotFound();
            }

            Beers = await _context.TastingBeer.Where(x => x.TastingId == id).OrderBy(x => x.SortOrder)
                                              .Include(x => x.Beer)
                                                .ThenInclude(x => x.Brewery)
                                              .Include(x => x.Beer)
                                                .ThenInclude(x => x.Beerstyle).AsNoTracking().ToListAsync();
            
            return Page();
        }

        [HttpPost]
        public async Task<IActionResult> OnPostDelete(int? id)
        {
            var tb = _context.TastingBeer.FirstOrDefault(x => x.TastingBeerId == id);
            var tbId = tb.TastingId;

            _context.TastingBeer.Remove(tb);
            _context.SaveChanges();

            var beers = await _context.TastingBeer.Where(x => x.TastingId == tbId).OrderBy(x => x.SortOrder).ToListAsync();
            for(int i = 0; i < beers.Count; i++)
            {
                beers[i].SortOrder = i + 1;
            }

            _context.TastingBeer.UpdateRange(beers);
            _context.SaveChanges();

            return RedirectToAction("index", new { id = tbId });            
        }

        [HttpPost]
        public async Task<IActionResult> OnPostMoveUp(int? id)
        {
            var tb = _context.TastingBeer.FirstOrDefault(x => x.TastingBeerId == id);
            var tbId = tb.TastingId;

            if (tb.SortOrder > 1)
            {                
                var beers = await _context.TastingBeer.Where(x => x.TastingId == tbId).OrderBy(x => x.SortOrder).ToListAsync();
                var b1 = beers.FirstOrDefault(x => x.SortOrder == tb.SortOrder - 1);
                b1.SortOrder += 1;
                tb.SortOrder -= 1;

                _context.TastingBeer.Update(b1);
                _context.TastingBeer.Update(tb);
                _context.SaveChanges();
            }

            return RedirectToAction("index", new { id = tbId });
        }
        [HttpPost]
        public async Task<IActionResult> OnPostMoveDown(int? id)
        {
            var tb = _context.TastingBeer.FirstOrDefault(x => x.TastingBeerId == id);
            var tbId = tb.TastingId;
            var beers = await _context.TastingBeer.Where(x => x.TastingId == tbId).ToListAsync();

            if (tb.SortOrder < beers.Count)
            {
                var b1 = beers.FirstOrDefault(x => x.SortOrder == tb.SortOrder + 1);
                b1.SortOrder -= 1;
                tb.SortOrder += 1;

                _context.TastingBeer.Update(b1);
                _context.TastingBeer.Update(tb);
                _context.SaveChanges();
            }

            return RedirectToAction("index", new { id = tbId });
        }
    }
}