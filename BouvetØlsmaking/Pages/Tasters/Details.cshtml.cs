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
    public class DetailsModel : BeerPageModel
    {   
        public DetailsModel(TastingContext context) : base(context)
        {

        }

        public Taster Taster { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (CheckAndReportAdmin())
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
    }
}
