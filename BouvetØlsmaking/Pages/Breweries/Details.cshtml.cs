using BouvetØlsmaking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BouvetØlsmaking.Pages.Breweries
{
    public class DetailsModel : BeerPageModel
    {
        public DetailsModel(TastingContext context) : base(context)
        {

        }

        public Brewery Brewery { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (!CheckAndReportAdmin())
                return Page();

            if (id == null)
            {
                return NotFound();
            }

            Brewery = await _context.Brewery.FirstOrDefaultAsync(m => m.BreweryId == id);

            if (Brewery == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
