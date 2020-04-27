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

    public class FizickaOsobaValidator : AbstractValidator<FizickaOsoba>
    {
        public FizickaOsobaValidator()
        {
            RuleFor(fizickaOsoba => fizickaOsoba.Ime).NotEmpty().WithMessage("Unesite naziv fizičke osobe.");
            RuleFor(fizickaOsoba => fizickaOsoba.Prezime).NotEmpty().WithMessage("Unesite prezime fizičke osobe.");
            RuleFor(fizickaOsoba => fizickaOsoba.OIB).NotEmpty().WithMessage("Unesite OIB fizičke osobe.").Matches("[0-9]{11}$").WithMessage("Unesite ispravan OIB fizičke osobe");
            RuleFor(fizickaOsoba => fizickaOsoba.Mjesto).NotEmpty().WithMessage("Unesite mjesto fizičke osobe.");
            RuleFor(fizickaOsoba => fizickaOsoba.UlicaIKucniBroj).NotEmpty().WithMessage("Unesite ulicu i kućni broj fizičke osobe.");
        }
    }
}