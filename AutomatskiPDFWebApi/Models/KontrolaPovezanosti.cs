using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomatskiPDFWebApi.Models
{
    public class KontrolaPovezanosti
    {
        public bool KontrolaPovlastenosti { get; set; }

        public List<FizickaOsoba> FizickeOsobe { get; set; }
        public List<PravnaOsoba> PravneOsobe { get; set; }
    }
}