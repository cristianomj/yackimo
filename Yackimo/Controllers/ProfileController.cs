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
    public class ProfileController : Controller
    {
        YackimoDb db = new YackimoDb();

        //
        // GET: /Profile/

        public ActionResult Index()
        {
            UserProfile user = db.UserProfiles.Find(WebSecurity.GetUserId(User.Identity.Name));

            return View(user);
        }

        //
        // GET: /Profile/Details

        [AllowAnonymous]
        public ActionResult Details(string username)
        {
            UserProfile user = db.UserProfiles.FirstOrDefault(u => u.UserName.ToLower() == username.ToLower());

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        //
        // GET: /Profile/Edit

        public ActionResult Edit()
        {
            UserProfile user = db.UserProfiles.Find(WebSecurity.GetUserId(User.Identity.Name));

            if (user == null)
            {
                return Content("profile not found");
            }
            return View(user);
        }

        //
        // POST: /Profile/Edit

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserProfile model, HttpPostedFileBase image)
        {
            UserProfile user = db.UserProfiles.Find(model.UserId);

            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    user.MimeType = image.ContentType;
                    user.Picture = new byte[image.ContentLength];
                    image.InputStream.Read(user.Picture, 0, image.ContentLength);
                }

                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }

        [ChildActionOnly]
        public ActionResult GetUser(int id = 0)
        {
            // Get user for summary page
            var profile = db.UserProfiles.Find(id);

            return PartialView("_PublicProfile", profile);
        }

        [ChildActionOnly]
        public ContentResult GetUserName(int id = 0)
        {
            // Get user name associated with user id
            var profile = db.UserProfiles.Find(id);

            return Content(profile.UserName);
        }

        [AllowAnonymous]
        public FileContentResult GetImage(int id = 0)
        {
            UserProfile user = db.UserProfiles.Find(id);

            if (user != null)
            {
                return File(user.Picture, user.MimeType);
            }
            else
            {
                return null;
            }
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
