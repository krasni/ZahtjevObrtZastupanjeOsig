using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutomatskiPDFMVC.Models
{
    public class PodaciPDF
    {
        public string DownloadToken { get; set; }
        public OsnivacObrt OsnivacObrt { get; set; }
        public Obrt Obrt { get; set; }
        public OvlasteniZastupnik OvlasteniZastupnik { get; set; }
        public KontrolaPovezanosti KontrolaPovezanosti { get; set; }
        public EmailPodaci EmailPodaci { get; set; }
        public DokumentPodaci DokumentPodaci {get;set;}

        [Display(Name = "Dokaz o uplaćenoj naknadi")]
        public bool DokazOUplacenojNaknadi
        {
            get; set;
        }
    }   
}