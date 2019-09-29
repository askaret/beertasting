using BouvetØlsmaking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BouvetØlsmaking.Pages.Beers
{
    public class DeleteModel : BeerPageModel
    {   
        public DeleteModel(TastingContext context) : base(context)
        {

        }

        [BindProperty]
        public Beer Beer { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (CheckAndReportAdmin())
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Beer = await _context.Beer.FindAsync(id);

            if (Beer != null)
            {
                _context.Beer.Remove(Beer);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
