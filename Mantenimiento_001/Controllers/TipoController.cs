using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using Mantenimiento_001.Models;


namespace Mantenimiento_001.Controllers
{
    [LoginFilter]
    public class TipoController : Controller
    {
        databaseEntities db = new databaseEntities();

        // GET: Home
        public ActionResult Tipo()
        {
            ViewBag.user = Session["user"];
            
            return View(db.inc_type.ToList());
        }

        public ActionResult Delete(int id)
        {
            var inc = db.inc_type.Find(id);
            db.inc_type.Remove(inc);
            db.SaveChanges();

            return RedirectToAction("Tipo");
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            inc_type t = new inc_type();
            t.ty_name = collection["ty_name"];
            db.inc_type.Add(t);
            db.SaveChanges();
            return RedirectToAction("Tipo");
        }
    }
}
