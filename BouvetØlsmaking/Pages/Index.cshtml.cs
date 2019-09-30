using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using BouvetØlsmaking.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BouvetØlsmaking.Pages
{
    public class IndexModel : PageModel
    {
        private readonly BouvetØlsmaking.Models.TastingContext _context;

        public IndexModel(BouvetØlsmaking.Models.TastingContext context)
        {
            _context = context;
        }
        public Taster CurrentTaster { get; set; }
        public IList<Tasting> Tasting { get; set; }

        public async Task OnGetAsync()
        {
            Tasting = await _context.Tasting.OrderByDescending(x => x.TastingDate).ToListAsync();
            CurrentTaster = SessionHelper.Instance().GetTaster();            
        }
        public IActionResult OnPostLogout()
        {
            var taster = new Taster()
            {
                DisplayName = string.Empty,
                EmailAddress = string.Empty,
                IsAdmin = false,
                TasterId = -1,
            };
            
            SessionHelper.Instance().SaveTaster(taster);
            return RedirectToPage("./Index");
        }
        public IActionResult OnPostSendPassword(string email)
        {
            MailMessage message = new MailMessage(new MailAddress("beertasting@skarets.com", "Beertasting"), new MailAddress(email));
            message.Subject = "Tetsmail fra c#";
            message.Body = @"Jada, dette funker";

            SmtpClient client = new SmtpClient("send.one.com");
            client.EnableSsl = false;
            client.Port = 587;
            client.EnableSsl = true;

            var cred = new System.Net.NetworkCredential("<username>", "<password>");

            client.Credentials = cred;

            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in CreateTestMessage2(): {0}",
                            ex.ToString());
            }

            return RedirectToPage("./Index");
        }
    }
}
