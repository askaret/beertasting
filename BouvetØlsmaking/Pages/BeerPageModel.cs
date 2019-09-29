using BouvetØlsmaking.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BouvetØlsmaking.Pages
{
    public abstract class BeerPageModel : PageModel
    {
        public Taster CurrentTaster { get; set; }
        protected readonly TastingContext _context;

        public BeerPageModel(TastingContext context) :base()
        {
            _context = context;
            CurrentTaster = SessionHelper.Instance().GetTaster();
        }

        public bool CheckAndReportLogin()
        {
            CurrentTaster = SessionHelper.Instance().GetTaster();

            if (CurrentTaster == null || CurrentTaster.TasterId == -1)
            {
                TempData["CustomError"] = "You must be logged in";
                return false;
            }

            return true;
        }

        public bool CheckAndReportAdmin()
        {
            if (CurrentTaster == null || CurrentTaster.TasterId == -1 || CurrentTaster.IsAdmin == false)
            {
                TempData["CustomError"] = "You must be an administrator to do this";
                return false;
            }

            return true;
        }
    }
}
