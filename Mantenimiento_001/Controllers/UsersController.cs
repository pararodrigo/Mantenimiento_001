using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using Mantenimiento_001.Models;

namespace Mantenimiento_001.Controllers
{
    public class UsersController : ApiController
    {
        databaseEntities db = new databaseEntities();

        // GET: api/users
        
        public dynamic Get()
        {
            //ESTO FUNCIONA
            var lista = db.users.Include("u").Select(u => new { u.users_name, u.users_fname });
         
            return Json(lista) ;
        }

        // GET: api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/values/5
        public void Delete(int id)
        {
        }
    }
}
