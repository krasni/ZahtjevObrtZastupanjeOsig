using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutomatskiPDFWebApp.Models
{
    public class Adresa
    {
        [Required(AllowEmptyStrings = true)]
        public string Mjesto { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string UlicaIKucniBroj { get; set; }
    }
}