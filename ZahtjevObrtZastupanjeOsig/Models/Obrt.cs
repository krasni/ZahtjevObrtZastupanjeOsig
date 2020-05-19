using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZahtjevObrtZastupanjeOsig.Models
{
    [Validator(typeof(Obrt))]
    public class Obrt
    {
        public string Naziv { get; set; }
        public string Oznaka { get; set; }
        public string ImeObrtnika { get; set; }
        public string PrezimeObrtnika { get; set; }
        public string Mjesto { get; set; }
        public string UlicaIKucniBroj { get; set; }
    }

    public class ObrtValidator : AbstractValidator<Obrt>
    {
        public ObrtValidator()
        {
            RuleFor(obrt => obrt.Naziv).NotEmpty().WithMessage("Unesite naziv obrta.");
            RuleFor(obrt => obrt.Oznaka).NotEmpty().WithMessage("Unesite oznaku obrta.");
            RuleFor(obrt => obrt.ImeObrtnika).NotEmpty().WithMessage("Unesite ime obrtnika.");
            RuleFor(obrt => obrt.PrezimeObrtnika).NotEmpty().WithMessage("Unesite prezime obrtnika.");
            RuleFor(obrt => obrt.Mjesto).NotEmpty().WithMessage("Unesite mjesto obrta.");
            RuleFor(obrt => obrt.UlicaIKucniBroj).NotEmpty().WithMessage("Unesite ulicu i kućni broj obrta.");
        }
    }
}