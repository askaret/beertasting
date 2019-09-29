using BouvetØlsmaking.Models;

namespace BouvetØlsmaking.Pages
{
    public class AdminModel : BeerPageModel
    {
        public AdminModel(TastingContext context) : base(context)
        {
        }

        public void OnGet()
        {
            CheckAndReportAdmin();
        }
    }
}