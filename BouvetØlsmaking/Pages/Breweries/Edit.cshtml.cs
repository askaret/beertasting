using BouvetØlsmaking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BouvetØlsmaking.Pages.Breweries
{
    public class EditModel : BeerPageModel
    {
        public EditModel(TastingContext context) : base(context)
        {

        }

        [BindProperty]
        public Brewery Brewery { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (CheckAndReportAdmin())
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Brewery).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BreweryExists(Brewery.BreweryId))
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

        private bool BreweryExists(int id)
        {
            return _context.Brewery.Any(e => e.BreweryId == id);
        }
    }
}
