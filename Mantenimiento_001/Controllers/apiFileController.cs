using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Mantenimiento_001.Models;
using System.Web;
using System.IO;

namespace Mantenimiento_001.Controllers
{
    public class apiFileController : ApiController
    {
        databaseEntities db = new databaseEntities();

        // GET: api/apiFile/4
        public dynamic Get(int id)
        {
            var files = db.archivos.Include("f").Where(f => f.inc_id == id).Select(f => new { f.file_id, f.file_name, f.file_ext, f.file, f.file_coment });
            
            return Json(files);
        }
        //// POST: api/apiFile
        //public void Post(string file_coment, HttpPostedFileBase file, int inc_id)
        //{
        //    archivo f = new archivo();

        //    MemoryStream ms = new MemoryStream();
        //    file.InputStream.CopyTo(ms);
        //    byte[] data = ms.ToArray();

        //    f.file_name = file.FileName;
        //    f.file_ext = file.ContentType;
        //    f.file_coment = file_coment;
        //    f.file = data;
        //    f.inc_id = inc_id;
        //    db.archivos.Add(f);
        //    db.SaveChanges();
        //}
    }
}
