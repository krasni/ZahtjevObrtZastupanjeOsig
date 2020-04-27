using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutomatskiPDFMVC.Models
{
    public class EmailPodaci
    {
        [Display(Name = "Pošalji dokument na email")]
        public bool PosaljiNaEmail { get; set; }

        public string EmailAdresaZaDostavu { get; set; }
    }
}