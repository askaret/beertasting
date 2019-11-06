using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BouvetØlsmaking.Models;

namespace BouvetØlsmaking.Pages.Beers
{
    public class EditModel : BeerPageModel
    {
        public List<Brewery> Breweries { get; set; }
        public List<Beerstyle> BeerStyles { get; set; }
        public List<BeerClass> Beerclasses { get; set; }

        [BindProperty]
        public Beer Beer { get; set; }

        [BindProperty]
        public Brewery SelectedBrewery { get; set; }

        [BindProperty]
        public BeerClass SelectedBeerclass { get; set; }

        [BindProperty]
        public Beerstyle SelectedBeerstyle { get; set; }

        public EditModel(TastingContext context) : base(context)
        {
            BeerStyles = _context.Beerstyle.OrderBy(x => x.Name).ToList();
            Beerclasses = _context.Beerclass.OrderBy(x => x.Name).ToList();
            Breweries = _context.Brewery.OrderBy(x => x.Name).ToList();
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (!CheckAndReportAdmin())
                return Page();

            if (id == null)
            {
                return NotFound();
            }

            Beer = await _context.Beer.FirstOrDefaultAsync(m => m.BeerId == id);

            if (Beer == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Beer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BeerExists(Beer.BeerId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BeerExists(int id)
        {
            return _context.Beer.Any(e => e.BeerId == id);
        }
    }
}
