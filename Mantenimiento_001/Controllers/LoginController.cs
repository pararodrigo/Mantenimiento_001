using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mantenimiento_001.Models;
using System.Security.Cryptography;
using System.Text;

namespace Mantenimiento_001.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            Session.Clear();
            return View();
        }


        // POST: Login/Create
        [HttpPost]
        public ActionResult Login(String user, String password)
        {
            try
            {
                // TODO: Add insert logic here
                databaseEntities db = new databaseEntities();


                var us = from u in db.users where u.users_alias == user select u;
                var p = Md5Encode.EncodeMd5(password);
               if ( us != null)
                {

                    var pu = us.FirstOrDefault().users_pass;
                    
                    // si es la primera vez que accede no tiene contraseña y es redirigido a edit (Personal) para introducir su password
                    if(us.FirstOrDefault().users_pass == null)
                    {
                        Session["user"] = us.FirstOrDefault();
                       
                        return RedirectToAction("Edit","Personal", new { id = us.FirstOrDefault().users_id });
                    }
                    else if(Compare.CompareMd5(us.FirstOrDefault().users_pass , p)== true)
                    {
                        Session["user"] = us.FirstOrDefault();
                        return RedirectToAction("Home", "Home");
                    }
                    else
                    {
                        ViewBag.mensaje = "Contraseña incorrecta";
                        return View();
                    }

                }
                else
                {

                ViewBag.mensaje = "El usuario no esta registrado";
                return View();
                }
            }
            catch
            {
                ViewBag.mensaje = "No se han podido verificar los datos";
                return View();
            }
        }

     
    }
}
