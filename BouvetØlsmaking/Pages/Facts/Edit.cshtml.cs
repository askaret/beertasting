using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BouvetØlsmaking.Models;

namespace BouvetØlsmaking.Pages.Facts
{
    public class EditModel : PageModel
    {
        private readonly BouvetØlsmaking.Models.TastingContext _context;

        public EditModel(BouvetØlsmaking.Models.TastingContext context)
        {
            _context = context;
        }

        [BindProperty]
        public RandomFact RandomFact { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RandomFact = await _context.RandomFact.FirstOrDefaultAsync(m => m.RandomFactId == id);

            if (RandomFact == null)
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

            _context.Attach(RandomFact).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RandomFactExists(RandomFact.RandomFactId))
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

        private bool RandomFactExists(int id)
        {
            return _context.RandomFact.Any(e => e.RandomFactId == id);
        }
    }
}
