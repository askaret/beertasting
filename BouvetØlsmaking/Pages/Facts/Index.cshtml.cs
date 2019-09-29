using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BouvetØlsmaking.Models;

namespace BouvetØlsmaking.Pages.Facts
{
    public class IndexModel : PageModel
    {
        private readonly BouvetØlsmaking.Models.TastingContext _context;

        public IndexModel(BouvetØlsmaking.Models.TastingContext context)
        {
            _context = context;
        }

        public IList<RandomFact> RandomFact { get;set; }

        public async Task OnGetAsync()
        {
            RandomFact = await _context.RandomFact.ToListAsync();
        }
    }
}
