using BouvetØlsmaking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BouvetØlsmaking.Pages.Tastings
{
    public class ResultModel : BeerPageModel
    {
        [BindProperty]
        public int CurrentTastingId { get; set; }
        public List<TastingBeerResult> ResultHomebrew { get; set; }

        public List<TastingBeerResult> ResultRegular { get; set; }

        public List<TastingBeerResult> ResultHighAbv { get; set; }

        public List<TastingBeerResult> ResultAll { get; set; }

        public string CurrentTastingName { get; set; }

        public ResultModel(BouvetØlsmaking.Models.TastingContext context) : base(context)
        {
            ResultRegular = new List<TastingBeerResult>();
            ResultHomebrew = new List<TastingBeerResult>();
            ResultHighAbv = new List<TastingBeerResult>();
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            TempData["EmailSent"] = null;

            if (!CheckAndReportLogin())
                return Page();

            CurrentTastingId = id;

            await GetResults();
            return Page();            
        }

        private async Task GetResults()
        {
            var tasting = await _context.Tasting.FirstOrDefaultAsync(x => x.TastingId == CurrentTastingId);
            CurrentTastingName = tasting?.Name ?? "N/A";

            var tastingResults = await _context.TastingResult.Where(x => x.TastingId == CurrentTastingId).ToListAsync();            

            if(tastingResults.Count == 0)
            {
                TempData["CustomError"] = "No votes cast or calculation hasn't been run";
                return;
            }

            ResultAll = new List<TastingBeerResult>();
            ResultAll.AddRange(tastingResults.OrderByDescending(x => x.ScoreFinal).ToList());
            ResultRegular.AddRange(ResultAll.Where(x => x.BeerClassName == "Regular").OrderByDescending(x => x.ScoreFinal));
            ResultHomebrew.AddRange(ResultAll.Where(x => x.BeerClassName == "Homebrew").OrderByDescending(x => x.ScoreFinal));
            ResultHighAbv.AddRange(ResultAll.Where(x => x.BeerClassName == "High ABV").OrderByDescending(x => x.ScoreFinal));
        }

        private async Task<string> GenerateEmailBodyAsync()
        {
            await GetResults();

            var retString = $"<html><body><h1>Results for {CurrentTastingName}</h1>";

            retString += "<table style=\"width: 100 %\">";
            retString += "  <tr>";
            retString += "    <th style=\"border-bottom: 1px solid #ddd;\">#</th>";
            retString += "    <th style=\"border-bottom: 1px solid #ddd;\">Beername</th>";
            retString += "    <th style=\"border-bottom: 1px solid #ddd;\">BreweryName</th>";
            retString += "    <th style=\"border-bottom: 1px solid #ddd;\">ABV</th>";
            retString += "    <th style=\"border-bottom: 1px solid #ddd;\">Taste</th>";
            retString += "    <th style=\"border-bottom: 1px solid #ddd;\">Appearance</th>";
            retString += "    <th style=\"border-bottom: 1px solid #ddd;\">Overall</th>";
            retString += "    <th style=\"border-bottom: 1px solid #ddd;\">FINAL</th>";
            retString += "  </tr>";
            for(int i = 0; i < ResultAll.Count; i++)
            {
                retString += "  <tr>";
                retString += $"    <td style=\"border-bottom: 1px solid #ddd;\">{i+1}</td>";
                retString += $"    <td style=\"border-bottom: 1px solid #ddd;\">{ResultAll[i].BeerName}</td>";
                retString += $"    <td style=\"border-bottom: 1px solid #ddd;\">{ResultAll[i].BreweryName}</td>";
                retString += $"    <td style=\"border-bottom: 1px solid #ddd;\">{ResultAll[i].Abv}</td>";
                retString += $"    <td style=\"border-bottom: 1px solid #ddd;\">{ResultAll[i].ScoreTaste}</td>";
                retString += $"    <td style=\"border-bottom: 1px solid #ddd;\">{ResultAll[i].ScoreAppearance}</td>";
                retString += $"    <td style=\"border-bottom: 1px solid #ddd;\">{ResultAll[i].ScoreOverall}</td>";
                retString += $"    <td style=\"border-bottom: 1px solid #ddd;\">{ResultAll[i].ScoreFinal}</td>";
                retString += "  </tr>";
            }

            retString += "</table><br><br>";

            var myVotes = await _context.Vote.Where(x => x.TastingId == CurrentTastingId && x.TasterId == CurrentTaster.TasterId).OrderByDescending(x => x.Taste).ThenByDescending(x => x.Overall).ThenByDescending(x => x.Appearance).ToListAsync();
            var beers = await _context.Beer.Where(x => ResultAll.Any(y => y.BeerId == x.BeerId)).ToListAsync();
            var breweries = await _context.Brewery.Where(x => beers.Any(y => y.BreweryId == x.BreweryId)).ToListAsync();

            if (!myVotes.Any())
            {
                retString += $"<h3>No votes found for {CurrentTaster.DisplayName}";
                return retString;
            }

            retString += $"<h1>Results for {CurrentTaster.DisplayName}</h1>";
            retString += "<table style=\"width: 100 %\">";
            retString += "  <tr>";
            retString += "    <th style=\"border-bottom: 1px solid #ddd;\">#</th>";
            retString += "    <th style=\"border-bottom: 1px solid #ddd;\">Beername</th>";
            retString += "    <th style=\"border-bottom: 1px solid #ddd;\">BreweryName</th>";
            retString += "    <th style=\"border-bottom: 1px solid #ddd;\">ABV</th>";
            retString += "    <th style=\"border-bottom: 1px solid #ddd;\">Taste</th>";
            retString += "    <th style=\"border-bottom: 1px solid #ddd;\">Appearance</th>";
            retString += "    <th style=\"border-bottom: 1px solid #ddd;\">Overall</th>";
            retString += "  </tr>";

            for(int j = 0; j < myVotes.Count; j++)
            {
                var beer = beers.FirstOrDefault(x => x.BeerId == myVotes[j].BeerId);
                var brewery = breweries.FirstOrDefault(x => x.BreweryId == beer.BreweryId);

                retString += "  <tr>";
                retString += $"    <td style=\"border-bottom: 1px solid #ddd;\">{j + 1}</td>";
                retString += $"    <td style=\"border-bottom: 1px solid #ddd;\">{beer.Name}</td>";
                retString += $"    <td style=\"border-bottom: 1px solid #ddd;\">{brewery.Name}</td>";
                retString += $"    <td style=\"border-bottom: 1px solid #ddd;\">{beer.ABV}</td>";

                var taste = myVotes[j].Taste;
                var appearance = myVotes[j].Appearance;
                var overall = myVotes[j].Overall;

                retString += $"    <td style=\"border-bottom: 1px solid #ddd;\">{Math.Round(taste, 2)}</td>";
                retString += $"    <td style=\"border-bottom: 1px solid #ddd;\">{Math.Round(appearance, 2)}</td>";
                retString += $"    <td style=\"border-bottom: 1px solid #ddd;\">{Math.Round(overall, 2)}</td>";          
                retString += "  </tr>";
            }
            retString += "</table></body></html>";
            return retString;
        }

        [HttpPost]
        public async Task<IActionResult> OnPostEmailResults(int? tastingId)
        {
            if (tastingId == null)
                return Page();

            CurrentTastingId = tastingId.Value;
            var body = await GenerateEmailBodyAsync();
            MailSender.Send(CurrentTaster.EmailAddress, $"Results for {CurrentTastingName}", body);
            TempData["EmailSent"] = "sent";
            return Page();
        }
    }
}