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

        public string PravnaIliFizickaOsoba { get; set; }
        public FizickaOsoba FizickaOsoba { get; set; }
        public PravnaOsoba PravnaOsoba { get; set; }
    }
}