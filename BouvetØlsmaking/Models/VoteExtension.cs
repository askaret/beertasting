using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BouvetØlsmaking.Models
{
    public static class VoteExtension
    {
        public static bool IsValid(this Vote v)
        {
            if (v.Appearance >= 0 && v.Appearance <= 10 && v.Overall >= 0 && v.Overall <= 10 && v.Taste >= 0 && v.Taste <= 10)
                return true;

            return false;
        }
    }
}
