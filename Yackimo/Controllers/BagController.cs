using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using Yackimo.Infrastructure;

namespace Yackimo.Controllers
{
    public class BagController : Controller
    {
        private YackimoDb db = new YackimoDb();

        //
        // GET: /Bag/

        public ActionResult Index()
        {
            var userId = WebSecurity.GetUserId(User.Identity.Name);

            var bag = db.Bags.Where(b => b.UserId == userId).Single();
 
            return View(bag);
        }

        //
        // POST: /Bag/Add

        [HttpGet]
        public ActionResult Add(int id)
        {
            var product = db.Products.Find(id);

            var userId = WebSecurity.GetUserId(User.Identity.Name);

            var bag = db.Bags.Where(b => b.UserId == userId).Single();

            bag.Products.Add(product);

            db.SaveChanges();

            return RedirectToAction("Index");
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
