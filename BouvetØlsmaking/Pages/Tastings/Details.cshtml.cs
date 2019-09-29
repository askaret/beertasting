using BouvetØlsmaking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BouvetØlsmaking.Pages.Tastings
{
    public class DetailsModel : BeerPageModel
    {
        public DetailsModel(TastingContext context) : base(context)
        {
            
        }

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
    }
}
