using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BouvetØlsmaking.Models;

namespace BouvetØlsmaking.Pages.Tasters
{
    public class EditModel : BeerPageModel
    {        
        public EditModel(TastingContext context) : base(context)
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Taster).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TasterExists(Taster.TasterId))
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

        private bool TasterExists(int id)
        {
            return _context.Taster.Any(e => e.TasterId == id);
        }
    }
}
