using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BouvetØlsmaking.Models;

namespace BouvetØlsmaking.Pages.Tasters
{
    public class DeleteModel : BeerPageModel
    {
        public DeleteModel(TastingContext context) : base(context)
        {

        }

        [BindProperty]
        public Taster Taster { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (!CheckAndReportAdmin())
                return Page();

            if (id == null)
            {
                return NotFound();
            }

            Taster = await _context.Taster.FirstOrDefaultAsync(m => m.TasterId == id);

            if (Taster == null)
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

            Taster = await _context.Taster.FindAsync(id);

            if (Taster != null)
            {
                _context.Taster.Remove(Taster);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
