using BouvetØlsmaking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BouvetØlsmaking.Pages
{

    public class VoteModel : BeerPageModel
    {           
        public int BeerNumber { get; set; }
        [BindProperty]
        public Vote CurrentVote { get; set; }                
        public Beer CurrentBeer { get; set; }                
        public Tasting CurrentTasting { get; set; }
        public TastingBeer CurrentTastingBeer { get; set; }
        public bool CanGoBack { get; set; }
        public bool CanGoForward { get; set; }
        public Brewery CurrentBrewery { get; set; }
        public Beerstyle CurrentStyle { get; set; }

        public VoteModel(BouvetØlsmaking.Models.TastingContext context) : base(context) { }

        #region Get
        public async Task<IActionResult> OnGetAsync(int id, int beernumber = 0)
        {
            TempData["CustomError"] = null;
            BeerNumber = beernumber;

            if (!CheckAndReportLogin())
                return Page();

            var tasting = _context.Tasting.FirstOrDefault(x => x.TastingId == id);
            if (tasting == null)
            {
                TempData["CustomError"] = "Invalid tasting, go home";
                return Page();
            }

            if (!tasting.IsActive)
            {
                TempData["CustomError"] = "Tasting is not active, no votes allowed";
                return Page();
            }

            if (tasting.TastingDate != null && DateTime.Now.Date < tasting.TastingDate.Date)
            {
                TempData["CustomError"] = "Tasting is not yet active";
                return Page();
            }

            CurrentTasting = tasting;

            var beers = await _context.TastingBeer.Where(x => x.TastingId == id).OrderBy(x => x.SortOrder).ToListAsync();

            var actualTastingBeer = beers.ElementAtOrDefault(beernumber);
            if (actualTastingBeer == null)
            {
                TempData["CustomError"] = $"Something went wrong, no beer at index {beernumber}";
                return Page();
            }
            CurrentTastingBeer = actualTastingBeer;
            CurrentBeer = await _context.Beer.FirstOrDefaultAsync(x => x.BeerId == actualTastingBeer.BeerId);
            CurrentBeer.ABV = System.Math.Round(CurrentBeer.ABV, 2);
            if (actualTastingBeer == null)
            {
                TempData["CustomError"] = $"Something went wrong, unable to find beer with id {actualTastingBeer.BeerId}";
                return Page();
            }

            CurrentBrewery = await _context.Brewery.FirstOrDefaultAsync(x => x.BreweryId == CurrentBeer.BreweryId);
            CurrentStyle = await _context.Beerstyle.FirstOrDefaultAsync(x => x.BeerstyleId == CurrentBeer.BeerStyleId);

            if (CurrentTasting.IsBlind)
            {
                CurrentBrewery.Country = "(N/A)";
                CurrentBrewery.Name = "Hidden";
                CurrentBrewery.Website = "_null";

                CurrentBeer.Name = "Hidden";
                CurrentBeer.ABV = 0.0;
                CurrentBeer.Beerstyle.Name = "Hidden";
                CurrentBeer.RateBeerLink = "_null";
            }

            CanGoBack = beernumber > 0;
            CanGoForward = beers.Count > beernumber + 1;

            var vote = _context.Vote.FirstOrDefault(x => x.BeerId == CurrentBeer.BeerId && x.TasterId == CurrentTaster.TasterId && x.TastingId == CurrentTasting.TastingId);

            if (vote == null)
            {
                CurrentVote = new Vote()
                {
                    VoteId = -1,
                    BeerId = CurrentBeer.BeerId,
                    TasterId = CurrentTaster.TasterId,
                    TastingId = id,
                    Appearance = 0,
                    Overall = 0,
                    Taste = 0,
                    Note = string.Empty
                };
            }
            else
            {
                CurrentVote = vote;
            }

            return Page();
        }
        #endregion

        #region Post
        [HttpPost]
        public async Task<IActionResult> OnPostSaveVote(int beerNumber)
        {
            return await saveVote(beerNumber);
        }

        [HttpPost]
        public async Task<IActionResult> OnPostMovePrevious(int beerNumber)
        {
            return await saveVote(beerNumber - 1);            
        }
        [HttpPost]
        public async Task<IActionResult> OnPostMoveNext(int beerNumber)
        {
            return await saveVote(beerNumber + 1);
        }
        #endregion

        private async Task<IActionResult> saveVote(int nextBeer)
        {
            if (CurrentVote.VoteId == -1)
            {
                var vote = new Vote()
                {
                    Appearance = CurrentVote.Appearance,
                    Note = CurrentVote.Note,
                    Taste = CurrentVote.Taste,
                    Overall = CurrentVote.Overall,
                    BeerId = CurrentVote.BeerId,
                    TasterId = CurrentVote.TasterId,
                    TastingId = CurrentVote.TastingId
                };

                if (!vote.IsBlank())
                {
                    _context.Vote.Add(vote);
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                var vote = await _context.Vote.FirstOrDefaultAsync(x => x.VoteId == CurrentVote.VoteId);
                vote.Note = CurrentVote.Note;
                vote.Appearance = CurrentVote.Appearance;
                vote.BeerId = CurrentVote.BeerId;
                vote.Overall = CurrentVote.Overall;
                vote.Taste = CurrentVote.Taste;
                vote.TasterId = CurrentVote.TasterId;
                vote.TastingId = CurrentVote.TastingId;

                _context.Vote.Update(vote);
                await _context.SaveChangesAsync();
            }

            var tastingId = CurrentVote.TastingId;
            return RedirectToAction("OnGetAsync", new { id = tastingId, beernumber = nextBeer });
        }
    }
}