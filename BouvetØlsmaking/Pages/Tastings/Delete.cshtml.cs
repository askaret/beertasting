using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BouvetØlsmaking.Models;

namespace BouvetØlsmaking.Pages.Tastings
{
    public class DeleteModel : BeerPageModel
    {
        public DeleteModel(TastingContext context) : base(context)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Tasting = await _context.Tasting.FindAsync(id);

            if (Tasting != null)
            {
                _context.Tasting.Remove(Tasting);

                var votes = await _context.Vote.Where(x => x.TastingId == id).ToArrayAsync();
                _context.Vote.RemoveRange(votes);

                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
