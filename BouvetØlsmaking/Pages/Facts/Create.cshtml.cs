using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BouvetØlsmaking.Models;

namespace BouvetØlsmaking.Pages.Facts
{
    public class CreateModel : PageModel
    {
        private readonly BouvetØlsmaking.Models.TastingContext _context;

        public CreateModel(BouvetØlsmaking.Models.TastingContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public RandomFact RandomFact { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.RandomFact.Add(RandomFact);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}