using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BouvetØlsmaking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BouvetØlsmaking.Pages
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly BouvetØlsmaking.Models.TastingContext _context;
        public ForgotPasswordModel(BouvetØlsmaking.Models.TastingContext context)
        {
            _context = context;
        }
        [BindProperty]
        public string EmailAddress { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(EmailAddress == null || EmailAddress.Contains("@") == false || EmailAddress.Contains(".") == false)
            {
                TempData["CustomError"] = $"Try again Einstein";
                return OnGet();
            }

            var taster = _context.Taster.FirstOrDefault(x => x.EmailAddress.ToLowerInvariant().Equals(EmailAddress));

            if (taster == null)
            {
                TempData["CustomError"] = $"No registration found for {EmailAddress}";
                return OnGet();
            }                

            MailSender.Send(taster.EmailAddress, "You're probably drunk and should go home", taster.Password);
            return RedirectToPage("./Login");
        }
    }
}