using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BouvetØlsmaking.Models;

namespace BouvetØlsmaking.Pages.Beers
{
    public class IndexModel : BeerPageModel
    {   
        public IndexModel(TastingContext context) : base(context)
        {

        }

        public IList<Beer> Beer { get;set; }

        public async Task OnGetAsync()
        {
            CheckAndReportAdmin();

            Beer = await _context.Beer.OrderBy(x => x.Name).ToListAsync();
        }
    }
}
