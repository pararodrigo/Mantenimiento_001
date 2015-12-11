using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mantenimiento_001.Models;
using Mantenimiento_001.Controllers;
using System.Security.Cryptography;
using System.Text;

namespace Mantenimiento_001.Controllers
{
    [LoginFilter]
    public class PersonalController : Controller
    {
        databaseEntities db = new databaseEntities();

        

        // GET: Personal
        public ActionResult Personal()
        {
            ViewBag.user = Session["user"];

            return View(db.users_list.ToList());
        }


        // GET: Default/Create
        public ActionResult Create()
        {
            ViewBag.roll = db.rolls.ToList();
            return View();
        }

        // POST: Default/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection, int roll)
        {
            try
            {

                user u = new user();
                u.users_name = collection["users_name"];
                u.users_fname = collection["users_fname"];
                u.users_lname = collection["users_lname"];
                u.users_alias = collection["users_alias"];
                u.users_pass = null;
                u.roll_id = roll;
                u.act = true;
                db.users.Add(u);
                db.SaveChanges();
                return RedirectToAction("Personal");
            }
            catch
            {
                ViewBag.mensaje = "No se ha podido crear un nuevo usuario";
                return RedirectToAction("Personal");
            }
        }

        // GET: Default/Edit/5
        public ActionResult Edit(int id)
        {
            var us = db.users.Find(id);
            ViewBag.user = Session["user"];
            ViewBag.rollList = db.rolls.ToList();

            ViewBag.Change = Session["reintentar"];
            Session["reintentar"] = false;
            
            var puesto = from r in db.rolls where r.roll_id == us.roll_id select r;
            ViewBag.puesto = puesto.FirstOrDefault();

            return View(us);
        }

        // POST: Default/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, int rollSelected, FormCollection collection)
        {
            try
            {

                var user = from us in db.users where us.users_id == id select us;
                var u = user.FirstOrDefault();
                u.users_name = collection["users_name"];
                u.users_fname = collection["users_fname"];
                u.users_lname = collection["users_lname"];
                u.users_alias = collection["users_alias"];


                u.roll_id = rollSelected;
                u.act = bool.Parse(collection["act"]);

                db.SaveChanges();
                Session["user"] = u;
                return RedirectToAction("Edit", new { id = id });
            }
            catch
            {
                return View();
            }
        }
        // POST: Default/EditPassword/5
        [HttpPost]
        public ActionResult EditPassword(int id, string users_password0, string users_password, string users_password2)
        {
            var user = db.users.Find(id);

            if (users_password0 == "")
            {
                
                user.users_pass = Md5Encode.EncodeMd5(users_password);
                db.SaveChanges();
                Session["user"] = user;
                Session["reintentar"] = false;
            }
            else
            {
                if (Compare.CompareMd5( user.users_pass, Md5Encode.EncodeMd5(users_password0)))
                {
                    user.users_pass = Md5Encode.EncodeMd5(users_password);
                    db.SaveChanges();
                    Session["user"] = user;
                    Session["reintentar"] = false;
                }
                else
                {
                    Session["reintentar"] = true;
                }

            }




            return RedirectToAction("Edit", new { id = id });
        }
        //Delete
        public ActionResult Delete(int id)
        {
            try
            {
                var user = db.users.Find(id);
                db.users.Remove(user);
                db.SaveChanges();
                return RedirectToAction("Personal");
            }
            catch
            {
                return RedirectToAction("Personal");
            }
        }
    }
}