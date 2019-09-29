using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BouvetØlsmaking.Models;

namespace BouvetØlsmaking.Pages.Facts
{
    public class DeleteModel : PageModel
    {
        private readonly BouvetØlsmaking.Models.TastingContext _context;

        public DeleteModel(BouvetØlsmaking.Models.TastingContext context)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RandomFact = await _context.RandomFact.FindAsync(id);

            if (RandomFact != null)
            {
                _context.RandomFact.Remove(RandomFact);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
