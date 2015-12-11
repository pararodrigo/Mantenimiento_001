using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Mantenimiento_001.Models;

namespace Mantenimiento_001.Controllers
{
    public class apiEstadisticasController : ApiController
    {

        databaseEntities db = new databaseEntities();


        // GET: api/apiEstadisticas
        public dynamic Get()
        {
      
            datos d = new datos();
            //recuento de incidencias
            d.espera = db.incidencias.Where(i => i.estado_id == 1).Count();
            d.proceso = db.incidencias.Where(i => i.estado_id == 2).Count();
            d.reparadas = db.incidencias.Where(i => i.estado_id == 3).Count();
            d.detenidas = db.incidencias.Where(i => i.estado_id == 4).Count();

            //recuento por tipo
            List<tipeData> lista = new List<tipeData>();
            var tipos = db.inc_type.ToList();
            foreach(var t in tipos)
            {
                tipeData type = new tipeData();
                type.label = t.ty_name;
                type.value = db.incidencias.Where(i => i.ty_id == t.ty_id).Count();
                lista.Add(type);
            }

            d.porcentaje = lista;

            // tiempos
            d.tiempoEspera = db.esperatime.FirstOrDefault().media;
            d.tiempointervencion = db.reparacionTime.FirstOrDefault().media;
            d.tiempoTotal = db.totalTime.FirstOrDefault().media;

            

            return Json(d);
        }
    }
}
