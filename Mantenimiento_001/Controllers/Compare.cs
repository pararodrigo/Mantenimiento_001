using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mantenimiento_001.Controllers
{
    public static class Compare
    {
        public static bool CompareMd5(byte[] md51, byte[] md52)
        {
            bool igual = false;

            if (md51.Length == md52.Length)
            {
                int i = 0;
                while ((i < md51.Length) && (md51[i] == md52[i]))
                {
                    i++;
                }
                if(i == md52.Length)
                {
                    igual = true;
                }
            }
            else
            {
                igual = false;
            }

            return igual;
        }
    }
}