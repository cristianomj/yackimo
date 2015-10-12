using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yackimo.Infrastructure;

namespace Yackimo.Controllers
{
    public class CategoryController : Controller
    {
        private readonly YackimoDb db = new YackimoDb();

        //
        // GET: /Category/Details

        public ActionResult Details(int id)
        {
            var category = db.Categories.Find(id);
            return View(category);
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
