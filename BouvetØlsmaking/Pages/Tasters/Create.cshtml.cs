using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BouvetØlsmaking.Models;

namespace BouvetØlsmaking.Pages.Tasters
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
        public Taster Taster { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (Taster.EmailAddress.ToLower().Equals("andreas.skaret@bouvet.no"))
                Taster.IsAdmin = true;
            else
                Taster.IsAdmin = false;

            _context.Taster.Add(Taster);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}