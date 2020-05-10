using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutomatskiPDFMVC.Models
{
    public class KontrolaPovezanosti
    {
        [Display(Name = "Kontrola povezanosti")]
        public bool KontrolaPovlastenosti { get; set; }

        public List<FizickaOsoba> FizickeOsobe  { get; set; }
        public List<PravnaOsoba> PravneOsobe { get; set; }
    }
}