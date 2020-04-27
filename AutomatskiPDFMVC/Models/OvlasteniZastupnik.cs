using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutomatskiPDFMVC.Models
{
    public class OvlasteniZastupnik
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Oib { get; set; }
        public string BrojURegistruHanfe { get; set; }
        public string Mjesto { get; set; }
        public string UlicaIKucniBroj { get; set; }

        [Display(Name = "Kontrola ovlaštenja")]
        public bool KontrolaOvlastenja { get; set; }
    }
}