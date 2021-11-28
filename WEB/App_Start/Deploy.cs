using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB
{
    public static class Deploy
    {
        static string _servidor = "local";

        public static string servidor {
            get { return _servidor; }
            set { _servidor = value; }
        }
    }
}