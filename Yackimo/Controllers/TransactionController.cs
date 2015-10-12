using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using Yackimo.Models;
using PagedList;

namespace Yackimo.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
        YackimoDb db = new YackimoDb();

        //
        // GET: /Transaction/

        public ActionResult TradeRequest(int id)
        {
            // Requestor Info
            var requestorId = WebSecurity.GetUserId(User.Identity.Name);
            var RequestorProfile = db.UserProfiles.Find(requestorId);

            // Requestee Info
            var requesteeId = db.Products.Where(p => p.ProductId == id).Select(p => p.UserId).Single();
            var RequesteeProfile = db.UserProfiles.Find(requesteeId);

            TransactionViewModel model = new TransactionViewModel
            {
                Requestor = RequestorProfile,
                Requestee = RequesteeProfile,
                RequestedProduct = id
            };

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
