using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yackimo.Filters;

namespace Yackimo.Controllers
{
    // [Authorize]
    [Log]
    public class DemostrationController : Controller
    {
        //
        // GET: /Demostration/

        public ActionResult Index(string id = "default")
        {
            throw new Exception("something terrible has happened");

            return Content("Hello");
        }

    }
}
