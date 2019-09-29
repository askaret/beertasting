using BouvetØlsmaking.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BouvetØlsmaking.Pages.Beers
{
    public class CreateModel : BeerPageModel
    {   
        public List<Brewery> Breweries{ get; set; }
        public List<Beerstyle> BeerStyles { get; set; }
        public List <BeerClass> Beerclasses { get; set; }
        [BindProperty]
        public Beerstyle SelectedBeerstyle { get; set; }

        [BindProperty]
        public BeerClass SelectedBeerclass { get; set; }

        [BindProperty]
        public Brewery SelectedBrewery { get; set; }
        public CreateModel(TastingContext context) : base(context)
        {

        }

        public IActionResult OnGet()
        {
            if (!CheckAndReportAdmin())
                return Page();

            BeerStyles = _context.Beerstyle.OrderBy(x => x.Name).ToList();
            Beerclasses = _context.Beerclass.OrderBy(x => x.Name).ToList();
            Breweries = _context.Brewery.OrderBy(x => x.Name).ToList();

            
            return Page();
        }

        [BindProperty]
        public Beer Beer { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Beer.Add(Beer);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}