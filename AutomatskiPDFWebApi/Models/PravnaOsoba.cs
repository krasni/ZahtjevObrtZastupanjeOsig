using FluentValidation;
using FluentValidation.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutomatskiPDFWebApi.Models
{
    public class PravnaOsoba
    {
        public string Naziv { get; set; }

        public string OIB { get; set; }

        public string Mjesto { get; set; }

        public string UlicaIKucniBroj { get; set; }

        public bool DokazOUplacenojNaknadi { get; set; }
    }
}