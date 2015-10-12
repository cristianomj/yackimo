using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using Yackimo.Models;
using PagedList;

namespace Yackimo.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        YackimoDb db = new YackimoDb();

        public ActionResult OpenModel(int id)
        {
            var model = db.Products.Find(id);

            return PartialView("_OpenModel", model);
        }

        //
        // GET: /Product/
        
        public ActionResult Index(string category = null, string searchTerm = null, int page = 1)
        {
            // Get user's products
            var userId = WebSecurity.GetUserId(User.Identity.Name);

            // Get list of categories from products
            var categoryList = db.Products
                .Where(p => p.UserId == userId)
                .Select(p => p.Category)
                .ToList().Distinct();

            ViewBag.category = new SelectList(categoryList); 

            // Get products from db
            var model = db.Products
                .OrderByDescending(p => p.Views)
                .Where(p => (searchTerm == null && p.UserId == userId)
                    || (p.Name.Contains(searchTerm) && p.Category.Contains(category) && p.UserId == userId));

            ViewBag.Qty = model.Count();

            if (Request.IsAjaxRequest())
            {
                if (model.Count() == 0) return RedirectToAction("Index");
                return PartialView("_MyProducts", model.ToPagedList(page, 10));
            }

            return View(model.ToPagedList(page, 10));
        }

        //
        // GET: /Product/Create

        [HttpGet]
        public ActionResult Create(int userId)
        {
            return View();
        }

        //
        // POST: /Product/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    product.ImageMimeType = image.ContentType;
                    product.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(product.ImageData, 0, image.ContentLength);
                }
                else
                {
                    byte[] imageByte = System.IO.File.ReadAllBytes(Server.MapPath("~/Images/defaultProduct.gif"));
                    product.ImageData = imageByte;
                    product.ImageMimeType = "image/gif";
                }

                // Date product was created
                product.UserId = WebSecurity.GetUserId(User.Identity.Name);
                product.DataCreated = DateTime.Now;

                db.Products.Add(product);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(product);
        }

        //
        // GET: /Product/Edit

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = db.Products.Find(id);
            return View(model);
        }
        
        //
        // POST: /Product/Edit

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                Product dbEntry = db.Products.Find(product.ProductId);

                if (image != null)
                {
                    dbEntry.ImageMimeType = image.ContentType;
                    dbEntry.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(dbEntry.ImageData, 0, image.ContentLength);
                }

                dbEntry.Name = product.Name;
                dbEntry.Description = product.Description;
                dbEntry.Category = product.Category;

                db.SaveChanges();

                return RedirectToAction("index");
            }
            return View(product);
        }

        //
        // POST: /Product/Delete
        public ActionResult Delete(int productId)
        {
            var toDelete = db.Products.Find(productId);

            if (toDelete != null)
            {
                db.Products.Remove(toDelete);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        
        //
        // GET: /Product/PublicList

        [AllowAnonymous]
        public ActionResult PublicList([Bind(Prefix="id")] int profileId, int page = 1, string searchTerm = null)
        {
            UserProfile user = db.UserProfiles.Find(profileId);
            ViewBag.UserName = user.UserName;

            var model = db.Products
                .OrderByDescending(p => p.Views)
                .Where(p => (searchTerm == null && p.UserId == user.UserId)
                    || (p.Name.Contains(searchTerm) && p.UserId == user.UserId))
                .ToPagedList(page, 10);

            if (user == null)
            {
                return HttpNotFound();
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_ProductList", model);
            }

            return View(model);
        }

        //
        // GET: /Product/Details

        [AllowAnonymous]
        public ActionResult Details(int id = 0)
        {
            Product p = db.Products.Find(id);
            var userId = WebSecurity.GetUserId(User.Identity.Name);

            var routes = db.TradeRoutes.Where(r => r.RequesteeId == p.UserId && r.RequestorId == userId)
                                       .Select(r => r.RouteName).ToList();

            ViewBag.routes = new SelectList(routes);

            ProductViewModel model = new ProductViewModel
            {
                ProductId = p.ProductId,
                ProductName = p.Name,
                Description = p.Description,
                Category = p.Category,
                OwnerName = db.UserProfiles.Find(p.UserId).UserName
            };

            p.Views++;
            db.SaveChanges();

            if (p == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        #region Helpers
        [AllowAnonymous]
        public FileContentResult GetImage(int id)
        {
            var product = db.Products.Find(id);

            // If product found
            if (product != null)
            {
                return File(product.ImageData, product.ImageMimeType);
            }
            else
            {
                return null;
            }
        }

        //
        // GET: /Home/Autocomplete
        public ActionResult AutoComplete(string term) /* jquery will send a request to server
                                                       * and return a parameter called term */
        {
            var userId = WebSecurity.GetUserId(User.Identity.Name);

            // Query db with autocomplete term
            var model = db.Products
                .Where(p => p.Name.StartsWith(term) && p.UserId == userId)
                .Take(10)
                .Select(p => new { label = p.Name });

            return Json(model, JsonRequestBehavior.AllowGet);
        }

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
