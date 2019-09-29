using System;
using BouvetØlsmaking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BouvetØlsmaking.Pages
{
    public class RegisterModel : BeerPageModel
    {
        [BindProperty]
        [Required]
        public string EmailAddress { get; set; }
        [BindProperty]
        [Required]
        public string DisplayName { get; set; }
        [BindProperty]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public RegisterModel(TastingContext context) : base(context)
        {
            
        }

        public IActionResult OnGet()
        {
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var taster = new Taster()
            {
                EmailAddress = this.EmailAddress,
                DisplayName = this.DisplayName,
                Password = this.Password,
            };

            if (CheckAndReportLogin())
                return Page();

            if (taster.EmailAddress.ToLower().Contains("bouvet.no") == false)
            {
                TempData["CustomError"] = "You're not allowed to register here with that e-mail address";
                return OnGet();
            }                

            if (_context.Taster.Any(x => x.EmailAddress.ToLower() == taster.EmailAddress.ToLower()))
            {
                TempData["CustomError"] = "E-mail is already registered";
                return OnGet();
            }

            taster.IsAdmin = taster.EmailAddress.ToLower().Equals("andreas.skaret@bouvet.no");
            
            _context.Taster.Add(taster);
            await _context.SaveChangesAsync();

            SessionHelper.Instance().SaveTaster(taster);
            SessionHelper.Instance().GetTaster();

            return RedirectToPage("./Index");
        }
    }
}