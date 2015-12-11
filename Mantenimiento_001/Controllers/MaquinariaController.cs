using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mantenimiento_001.Models;

namespace Mantenimiento_001.Controllers
{
    [LoginFilter]
    public class MaquinariaController : Controller
    {

        databaseEntities db = new databaseEntities();

        // GET: Maquinaria
        public ActionResult Maquinaria()
        {
            ViewBag.user = Session["user"];
            return View(db.maquinas.ToList());
        }

        public ActionResult Delete(int id)
        {
            db.maquinas.Remove(db.maquinas.Find(id));
            db.SaveChanges();

            return RedirectToAction("Maquinaria");
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            maquina m = new maquina();
            m.maq_name = collection["maq_name"];
            db.maquinas.Add(m);
            db.SaveChanges();
            return RedirectToAction("Maquinaria");
        }
    }
}