using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BouvetØlsmaking.Models
{
    public class Taster
    {
        public int TasterId { get; set; }
        public string EmailAddress { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }

        public bool IsLoggedIn => TasterId != -1;
    }
}
