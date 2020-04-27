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

        public bool? DokazOUplacenojNaknadi
        {
            get;set;
        }
    }

     public class PravnaOsobaValidator : AbstractValidator<PravnaOsoba>
    {
        public PravnaOsobaValidator()
        {
            RuleFor(pravnaOsoba => pravnaOsoba.DokazOUplacenojNaknadi).Must(x => x == false || x == true).WithMessage("Dokaz o uplaćenoj naknadi je obavezan podatak.");
            RuleFor(pravnaOsoba => pravnaOsoba.DokazOUplacenojNaknadi).NotEqual(false).WithMessage("Potvrdite dokaz o uplaćenoj naknadi");
            RuleFor(pravnaOsoba => pravnaOsoba.Naziv).NotEmpty().WithMessage("Unesite naziv pravne osobe.");
            RuleFor(pravnaOsoba => pravnaOsoba.OIB).NotEmpty().WithMessage("Unesite OIB pravne osobe.").Matches("[0-9]{11}$").WithMessage("Unesite ispravan OIB pravne osobe");
            RuleFor(pravnaOsoba => pravnaOsoba.Mjesto).NotEmpty().WithMessage("Unesite mjesto pravne osobe.");
            RuleFor(pravnaOsoba => pravnaOsoba.UlicaIKucniBroj).NotEmpty().WithMessage("Unesite ulicu i kućni broj pravne osobe.");
        }
    }
}