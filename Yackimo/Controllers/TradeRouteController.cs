using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using Yackimo.Models;

namespace Yackimo.Controllers
{
    [Authorize]
    public class TradeRouteController : Controller
    {
        YackimoDb db = new YackimoDb();

        //
        // GET: /TradeRoute/

        public ActionResult Index()
        {
            var userId = WebSecurity.GetUserId(User.Identity.Name);

            var model = db.TradeRoutes.Where(r => r.RequestorId == userId).ToList();
            return View(model);
        }

        //
        // GET: /TradeRoute/RouteProducts

        public ActionResult RouteProducts(int id)
        {
            var model = db.TradeRoutes.Find(id);

            return View(model);
        }

        //
        // GET: /TradeRoute/Create

        public ActionResult Create(int id)
        {
            TradeRouteViewModel model = new TradeRouteViewModel
            {
                ProductId = id
            };

            return View(model);
        }

        //
        // POST: /TradeRoute/Create

        [HttpPost]
        public ActionResult Create(TradeRouteViewModel model)
        {
            if (ModelState.IsValid)
            {
                TradeRoutes newRoute = new TradeRoutes
                {
                    RequestorId = WebSecurity.GetUserId(User.Identity.Name),
                    RequesteeId = db.Products.Where(p => p.ProductId == model.ProductId).Select(p => p.UserId).Single(),
                    RouteName = model.RouteName
                };

                db.TradeRoutes.Add(newRoute);

                RouteProducts products = new RouteProducts
                {
                    TradeRoutesId = newRoute.TradeRoutesId,
                    Product = model.ProductId
                };

                db.RouteProducts.Add(products);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(model);
        }

        //
        // GET: /TradeRoute/AddToRoute

        public ActionResult AddToRoute(int id)
        {
            ViewBag.Product = id;

            return RedirectToAction("Index");
        }

        #region Helpers
        protected override void Dispose(bool disposing)
        {
            if (db != null)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion
    }
}
