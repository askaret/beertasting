using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BouvetØlsmaking.Pages.Tastings
{
    public class LiveModel : BeerPageModel
    {
        public int TastingId { get; set; }
        public LiveModel(Models.TastingContext context) : base(context)
        {
        
        }

        public void OnGet(int id)
        {
            if (!CheckAndReportAdmin())
                return;

            TastingId = id;
        }

        public async Task<JsonResult> OnGetVotesAsync(int id)
        {
            var votes = await _context.Vote.Where(x => x.TastingId == id).ToListAsync();
            if (votes.Count == 0)
                return new JsonResult("No votes cast yet");

            var numVotes = votes.Count;
            var avgTaste = Math.Round(votes.Average(x => x.Taste), 2);
            var avgAppearance = Math.Round(votes.Average(x => x.Appearance), 2);
            var avgOverall = Math.Round(votes.Average(x => x.Overall), 2);

            var retString = $"{numVotes} vote(s) cast so far  [Taste: {avgTaste} - Appearance: {avgAppearance} - Overall: {avgOverall}]";

            return new JsonResult(retString);
        }

        public async Task<JsonResult> OnGetNotesAsync(int id)
        {
            var notes = await _context.Vote.Where(x => x.TastingId == id).Select(y => y.Note).ToListAsync();

            if (notes.Count == 0)
                return new JsonResult("No notes yet, come on people!");

            var rand = new Random((int)DateTime.Now.Ticks);
            var note = notes.ElementAt(rand.Next(0, notes.Count - 1));
            return new JsonResult(note);
        }

        public async Task<JsonResult> OnGetFactsAsync(int id)
        {
            var facts = await _context.RandomFact.ToListAsync();
            var rand = new Random((int)DateTime.Now.Ticks);
            var fact = facts.ElementAt(rand.Next(0, facts.Count - 1));

            return new JsonResult(fact.FactText);
        }


        public JsonResult OnGetFilter(int id)
        {
            return new JsonResult("fuck me");
        }        
    }
}