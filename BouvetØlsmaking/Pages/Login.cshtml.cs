using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BouvetØlsmaking.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BouvetØlsmaking.Pages
{
    public class LoginModel : PageModel
    {
        private readonly BouvetØlsmaking.Models.TastingContext _context;

        [BindProperty]
        [Required]
        public string EmailAddress { get; set; }
        
        [BindProperty]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public LoginModel(BouvetØlsmaking.Models.TastingContext context)
        {
            _context = context;
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

            if (EmailAddress == null || EmailAddress.ToLower().Contains("bouvet.no") == false)
            {
                TempData["CustomError"] = "Invalid e-mail address";
                return OnGet();
            }

            var taster = await _context.Taster.FirstOrDefaultAsync(x =>
                string.Equals(x.EmailAddress.ToLower(), EmailAddress.ToLower(), StringComparison.InvariantCultureIgnoreCase) && x.Password == Password);
            
            if(taster != null)
            {
                SessionHelper.Instance().SaveTaster(taster);
                return RedirectToPage("./Index");
            }
            else
            {
                TempData["CustomError"] = "Invalid username or password, click forgotten password if you've got dementia";
                return OnGet();
            }
        }
    }
}