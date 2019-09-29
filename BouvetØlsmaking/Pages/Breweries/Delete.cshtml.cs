using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BouvetØlsmaking.Models;

namespace BouvetØlsmaking.Pages.Breweries
{
    public class DeleteModel : BeerPageModel
    {
        public DeleteModel(TastingContext context) : base(context)
        {

        }

        [BindProperty]
        public Brewery Brewery { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (!CheckAndReportAdmin())
                return Page();

            if (id == null)
            {
                return NotFound();
            }

            Brewery = await _context.Brewery.FirstOrDefaultAsync(m => m.BreweryId == id);

            if (Brewery == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Brewery = await _context.Brewery.FindAsync(id);

            if (Brewery != null)
            {
                _context.Brewery.Remove(Brewery);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
