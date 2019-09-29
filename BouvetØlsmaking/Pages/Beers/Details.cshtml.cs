using BouvetØlsmaking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BouvetØlsmaking.Pages.Beers
{
    public class DetailsModel : BeerPageModel
    {
        public DetailsModel(TastingContext context) : base(context)
        {

        }

        public Beer Beer { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (!CheckAndReportAdmin())
                return Page();

            if (id == null)
            {
                return NotFound();
            }

            Beer = await _context.Beer.FirstOrDefaultAsync(m => m.BeerId == id);

            if (Beer == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
