using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutomatskiPDFMVC.Models
{
    public class PravnaOsoba
    {
        public string Naziv { get; set; }
        public string OIB { get; set; }
        public string Mjesto { get; set; }
        public string UlicaIKucniBroj { get; set; }
    }
}