using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BouvetØlsmaking.Models;

namespace BouvetØlsmaking.Pages.Breweries
{
    public class CreateModel : BeerPageModel
    {
        public CreateModel(TastingContext context) : base(context)
        {

        }

        public IActionResult OnGet()
        {
            CheckAndReportAdmin();
            return Page();
        }

        [BindProperty]
        public Brewery Brewery { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Brewery.Add(Brewery);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}