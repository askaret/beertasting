using BouvetØlsmaking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BouvetØlsmaking.Pages.Tastings
{
    public class EditModel : BeerPageModel
    {   
        public EditModel(TastingContext context) : base(context)
        {
            
        }

        [BindProperty]
        public Tasting Tasting { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (!CheckAndReportAdmin())
                return Page();

            if (id == null)
            {
                return NotFound();
            }

            Tasting = await _context.Tasting.FirstOrDefaultAsync(m => m.TastingId == id);

            if (Tasting == null)
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

            _context.Attach(Tasting).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TastingExists(Tasting.TastingId))
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

        private bool TastingExists(int id)
        {
            return _context.Tasting.Any(e => e.TastingId == id);
        }
    }
}
