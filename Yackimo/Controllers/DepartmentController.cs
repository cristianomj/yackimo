using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yackimo.Infrastructure;

namespace Yackimo.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly YackimoDb db = new YackimoDb();

        //
        // GET: /Department/Details

        public ActionResult Details(int id)
        {
            var department = db.Departments.Find(id);
            return View(department);
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
