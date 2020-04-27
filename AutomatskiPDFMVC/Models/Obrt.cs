using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomatskiPDFMVC.Models
{
    public class Obrt
    {
        public string Naziv { get; set; }
        public string Oznaka { get; set; }
        public string ImeObrtnika { get; set; }
        public string PrezimeObrtnika { get; set; }
        public string Mjesto { get; set; }
        public string UlicaIKucniBroj { get; set; }
    }
}