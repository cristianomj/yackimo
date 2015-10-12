using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using Yackimo.Entities;
using Yackimo.Infrastructure;
using Yackimo.Models;

namespace Yackimo.Controllers
{
    public class TradeController : Controller
    {
        private YackimoDb db = new YackimoDb();

        //
        // GET: /Trade/

        public ActionResult Index(int id)
        {
            var model = db.Products.Find(id);

            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (db != null)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
