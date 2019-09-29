using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BouvetØlsmaking.Models;

namespace BouvetØlsmaking.Pages.Breweries
{
    public class IndexModel : BeerPageModel
    {
        public IndexModel(TastingContext context) : base(context)
        {

        }

        public IList<Brewery> Brewery { get;set; }

        public async Task OnGetAsync()
        {
            if(CheckAndReportAdmin())
                Brewery = await _context.Brewery.OrderBy(x => x.Name).ToListAsync();
        }
    }
}
