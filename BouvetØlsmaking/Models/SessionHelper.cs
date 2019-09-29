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

namespace BouvetØlsmaking.Models
{
    public class SessionHelper
    {
        private static SessionHelper instance = new SessionHelper();
        private static IHttpContextAccessor contextAccessor = new HttpContextAccessor();

        private ISession Session => new HttpContextAccessor().HttpContext.Session;
        
        private SessionHelper()
        {

        }

        public static SessionHelper Instance()
        {
            if (instance == null)
                instance = new SessionHelper();

            return instance;
        }

        public bool SaveTaster(Taster taster)
        {
            if (Session == null || Session.IsAvailable == false)
                return false;

            Session.SetString(nameof(taster.DisplayName), taster.DisplayName);
            Session.SetString(nameof(taster.EmailAddress), taster.EmailAddress);
            Session.SetInt32(nameof(taster.IsAdmin), taster.IsAdmin ? 1 : 0);
            Session.SetInt32(nameof(taster.TasterId), taster.TasterId);

            return true;
        }

        public Taster GetTaster()
        {
            var taster = new Taster() { TasterId = -1 };

            if (Session == null ||Session.IsAvailable == false)
                return taster;

            taster.DisplayName = Session.GetString(nameof(taster.DisplayName));
            taster.EmailAddress= Session.GetString(nameof(taster.EmailAddress));
            taster.IsAdmin = Session.GetInt32(nameof(taster.IsAdmin)) == 1 ? true : false;

            var id = Session.GetInt32(nameof(taster.TasterId));

            taster.TasterId = id.HasValue ? id.Value : -1;

            return taster;
        }

        public bool IsLoggedIn()
        {
            return GetTaster().TasterId != -1;
        }

        public bool IsAdmin()
        {
            return GetTaster().IsAdmin;
        }
    }
}
