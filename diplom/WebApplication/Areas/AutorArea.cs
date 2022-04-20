using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ApplicationUsers.Areas
{
    public class AutorArea : AreaRegistration
    {
        public override string AreaName 
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            /*
            context.MapRoute(
                "Autor_default",
                "Autor/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
            */
        }
    }
}
