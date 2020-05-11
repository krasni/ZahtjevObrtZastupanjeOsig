using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutomatskiPDFWebApi.Models
{
    public class FizickaOsoba
    {
        public string Ime { get; set; }

        public string Prezime { get; set; }

        public string OIB { get; set; }

        public string Mjesto { get; set; }

        public string UlicaIKucniBroj { get; set; }
    }
}