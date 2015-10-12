using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yackimo.Models;
using PagedList;
using WebMatrix.WebData;

namespace Yackimo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult View1()
        {
            var name = User.Identity.Name;
            var model = db.Products.Select(p => new ProductViewModel
            {
                ProductId = p.ProductId,
                ProductName = p.Name,
                Description = p.Description,
                Category = p.Category,
                OwnerName = name,
                OwnerId = p.UserId
            });
 
            return View(model);
        }

        YackimoDb db = new YackimoDb();

        public ActionResult Index(int page = 1)
        {
            ViewBag.Message = "Featured Products";

            // Display featured products
            var model = db.Products.OrderByDescending(p => p.Views)
                                   .ToPagedList(page, 5);
            
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Products", model);
            }

            return View(model);
        }

        //
        // GET: /Home/Autocomplete
        public ActionResult AutoComplete(string term) /* jquery will send a request to server
                                                       * and return a parameter called term */
        {
            var userId = WebSecurity.GetUserId(User.Identity.Name);
            var status = WebSecurity.IsAuthenticated;

            // Query db with autocomplete term
            var model = db.Products
                .Where(p => (p.Name.StartsWith(term) && (p.UserId != userId && status))
                    || (p.Name.StartsWith(term) && !status))
                .Take(10)
                .Select(p => new { label = p.Name });

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Home/Search

        public ActionResult Search(string searchTerm = null, int page = 1)
        {
            var userId = WebSecurity.GetUserId(User.Identity.Name);

            // Search database for products matching searchTerm
            var model = db.Products
                        .OrderByDescending(p => p.Views)
                        .Where(p => (searchTerm == null ||
                            (p.Name.StartsWith(searchTerm) || p.Description.StartsWith(searchTerm)
                            || p.Category.StartsWith(searchTerm)))
                            && p.UserId != userId);

            var count = model.Count();

            if (count == 0)
            {
                ViewBag.NotFound = string.Format("Your search '{0}' did not match any products.", searchTerm);
                ViewBag.Message = "More Items to Consider";
                // Items to consider
                var toConsider = db.Products.OrderByDescending(p => p.Views)
                                   .ToPagedList(page, 5);

                return View("Index", toConsider);
            }
            else
            {
                ViewBag.Message = "'" + searchTerm + "'";

                if (count > 10)
                {
                    ViewBag.Showing = string.Format("Showing 1 - 10 of {0} results. ", count);
                }
                else
                {
                    ViewBag.Showing = string.Format("Showing 1 - {0} of {0} results.", count);
                }
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_SearchResults", model.ToPagedList(page, 10));
            }

            return View(model.ToPagedList(page, 10));
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
