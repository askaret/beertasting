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
    public class IndexModel : BeerPageModel
    {
        public IndexModel(TastingContext context) : base(context)
        {
            
        }

        public IList<Taster> Taster { get;set; }

        public async Task OnGetAsync()
        {
            CheckAndReportAdmin();
            Taster = await _context.Taster.ToListAsync();
        }
    }
}
