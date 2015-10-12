using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using Yackimo.Models;

namespace Yackimo.Controllers
{
    public class MessagesController : Controller
    {

        YackimoDb db = new YackimoDb();

        //
        // GET: /Messages/

        public ActionResult Index()
        {
            var userId = WebSecurity.GetUserId(User.Identity.Name);

            var model = db.Messages.Where(m => m.UserId == userId)
                                   .OrderByDescending(m => m.DateCreated)
                                   .ToList();

            return View(model);
        }

        public ActionResult Compose(int id)
        {
            Message model = new Message
            {
                UserId = id,
                SenderName = User.Identity.Name,
                isRead = false
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Compose(Message message)
        {
            message.DateCreated = DateTime.Now;
            db.Messages.Add(message);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Read(int id)
        {
            var model = db.Messages.Find(id);
            model.isRead = true;
            db.SaveChanges();

            return View(model);
        }

        public ActionResult Notify()
        {
            var userId = WebSecurity.GetUserId(User.Identity.Name);

            var model = db.Messages.Where(m => m.UserId == userId && m.isRead == false).Count();

            return Content(string.Format("New Messages: {0}", model));
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
