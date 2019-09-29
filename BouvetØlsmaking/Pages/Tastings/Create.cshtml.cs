using BouvetØlsmaking.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BouvetØlsmaking.Pages.Tastings
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
        public Tasting Tasting { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Tasting.Add(Tasting);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}