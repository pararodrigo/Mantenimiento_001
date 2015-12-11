using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace Mantenimiento_001.Controllers
{
    public static class Md5Encode
    {
        public static Byte[] EncodeMd5(string password)
        {
            MD5 md5 = MD5.Create();
            var pass = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            //Encoding.UTF8.GetString(pass)
            return pass;
        }
    }
}