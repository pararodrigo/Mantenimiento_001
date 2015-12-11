using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Mantenimiento_001.Models;

namespace Mantenimiento_001.Controllers

{
    [LoginFilter]
    public class HomeController : Controller
    {

        databaseEntities db = new databaseEntities();
        
        // GET: Home
         public ActionResult Home()
        {
            ViewBag.user = Session["user"];
            var incidencias = db.incidencia_list.ToList();
            return View(incidencias);
        }

        // GET: Default/Create
        public ActionResult Create()
        {
            
                ViewBag.type = db.inc_type.ToList();
                ViewBag.priority = db.priorities.ToList();
                ViewBag.user = Session["user"];
                ViewBag.maquina = db.maquinas.ToList();
                ViewBag.estado = db.estadoes.ToList();
                
                return View();
            
          
        }

        // POST: Default/Create
        [HttpPost]
        public ActionResult Create(int priority, int type, int maquina, int user_s, string comentarios)
        {
            try
            {
                incidencia inc = new incidencia();
                inc.priority_id = priority;
                inc.ty_id = type;
                inc.maq_id = maquina;
                inc.s_date = DateTime.Now;
                inc.f_date = null;
                inc.s_user = user_s;
                inc.f_user = null;
                inc.coment = comentarios;
                inc.estado_id = 1;
                ViewBag.mensaje = "";
                db.incidencias.Add(inc);
                db.SaveChanges();

                // TODO: Add insert logic here

                return RedirectToAction("Home");
            }
            catch
            {
                ViewBag.mensaje = "No se han podido crear una nueva incidencia";
                return View();
            }
        }
        
        //Borrar incidencia(solo disponible para administradores)
        public ActionResult Delete(int id)
        {
            try
            {
                db.incidencias.Remove(db.incidencias.Find(id));
                db.SaveChanges();
                ViewBag.mensaje = "";
                return RedirectToAction("Home");

            }
            catch (Exception)
            {
                ViewBag.mensaje = "No se han podido borrar la incidencia";
                return RedirectToAction("Home");
                throw;
            }
        }

        // GET: Default/Edit/5
        public ActionResult Edit(int id)
        {
           
                var inc = db.incidencias.Find(id);

                ViewBag.user = Session["user"];
                ViewBag.type = db.inc_type.ToList();
                ViewBag.priority = db.priorities.ToList();
                ViewBag.users = db.users.ToList();
                ViewBag.maquina = db.maquinas.ToList();
                ViewBag.estadoList = db.estadoes.ToList();
                ViewBag.archivos = db.archivos.Where(f => f.inc_id == id).ToList();
                ViewBag.incidenciaInList = db.incidencia_list.Find(id);

                //los tecnicos se pasan en una lista para que se seleccionen en la lista de tecnicos
                var tec = db.tecnicos.Where(t => t.inc_id == id).ToList();

                IQueryable<users_list> tecnic = from u in db.users_list
                                                join t in db.tecnicos on u.users_id equals t.users_id
                                                where t.inc_id == id
                                                select u;
                //lista de todos los tecnicos
                ViewBag.tecnicosList = tecnic.ToList();

                List<String> tecnicos = new List<String>();

                foreach (var t in tec)
                {
                    tecnicos.Add(t.users_id.ToString());

                }
                //tecnico de la incidencia
                ViewBag.tecnicos = tecnicos;

                //busco la incidencia y la pasamos a la vista
                ViewBag.mensaje = "";
                return View(inc);
           

     
        }

        //editar parametros incidencia
        [HttpPost]
        public ActionResult EditIncidencia(int inc_id, int prioridad, int tipo, int maquinaSelect, string comentarios)
        {
            var inc = db.incidencias.Find(inc_id);
            inc.priority_id = prioridad;
            inc.ty_id = tipo;
            inc.maq_id = maquinaSelect;
            inc.coment = comentarios;
            db.SaveChanges();

            return RedirectToAction("Edit", "Home", new { id = inc_id });
        }

        //Añadir personal a la incidencia
        [HttpPost]
        public ActionResult EditPersonal(int inc_id, List<string> selector)
        {
            var borrar = from t in db.tecnicos where (t.inc_id == inc_id) select t;
            var del = db.tecnicos.Where(c => c.inc_id == inc_id).ToList();
            var incidencia = db.incidencias.Find(inc_id);

            //borramos los tecnicos añadidos anteriormente
            foreach (var d in del)
            {
                db.tecnicos.Remove(d);
            }

            //si añadimos personal 
            if (selector != null)
            {
                //añdimos el personal a la incidencia
                foreach (var t in selector)
                {

                    tecnico tec = new tecnico();
                    tec.inc_id = inc_id;
                    tec.users_id = int.Parse(t);
                    db.tecnicos.Add(tec);
                }

                // cambio el estado de la incidencia a "En proceso"
                incidencia.estado_id = 2; //2 = en proceso

                //añado la fecha y hora en la que pasa a "en proceso"
                incidencia.p_date = DateTime.Now;
            }
            else
            {
                incidencia.estado_id = 1; //1 = pendiente
                incidencia.p_date = null;
            }
            db.SaveChanges();
            return RedirectToAction("Edit", "Home", new { id = inc_id });
        }

        //Añadir ficheros a la incidencia
        [HttpPost]
        public ActionResult EditFile(string file_coment, HttpPostedFileBase file, int inc_id)
        {

            archivo f = new archivo();

            MemoryStream ms = new MemoryStream();
            file.InputStream.CopyTo(ms);
            byte[] data = ms.ToArray();

            f.file_name = file.FileName;
            f.file_ext = file.ContentType;
            f.file_coment = file_coment;
            f.file = data;
            f.inc_id = inc_id;
            db.archivos.Add(f);
            db.SaveChanges();
            return RedirectToAction("Edit", "Home", new { id = inc_id });
        }

        //descargar fichero
        public FileStreamResult Download(int id)
        {

            archivo file = db.archivos.Find(id);


            Response.Clear();

            MemoryStream ms = new MemoryStream(file.file);
            Response.ContentType = file.file_coment;

            Response.AddHeader("content-disposition", "attachment;filename=" + file.file_name);
            Response.Buffer = true;
            ms.WriteTo(Response.OutputStream);
            Response.End();

            return new FileStreamResult(ms, "'" + file.file_coment + "'");


        }

        //cerrar incidencia
        [HttpPost]
        public ActionResult cerrarIncidencia(int inc_id, int estado, int user_id)
        {
            var incidencia = db.incidencias.Find(inc_id);
            incidencia.estado_id = estado;
            incidencia.f_user = user_id;
            incidencia.f_date = DateTime.Now;
            db.SaveChanges();
            return RedirectToAction("Edit", "Home", new { id = inc_id });
        }
    }
}