using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZahtjevObrtZastupanjeOsig.Models
{
 [Validator(typeof(OsnivacObrtValidator))]
    public class OsnivacObrt
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string OIB { get; set; }
        public string Email { get; set; }
        public string KontaktBroj { get; set; }
        public string Mjesto { get; set; }
        public string UlicaIKucniBroj { get; set; }
    }

    public class OsnivacObrtValidator : AbstractValidator<OsnivacObrt>
    {
        public OsnivacObrtValidator()
        {
            RuleFor(osnivacObrt => osnivacObrt.Ime).NotEmpty().WithMessage("Unesite ime osnivača obrta.");
            RuleFor(osnivacObrt => osnivacObrt.Prezime).NotEmpty().WithMessage("Unesite prezime osnivača obrta.");
            RuleFor(osnivacObrt => osnivacObrt.OIB).NotEmpty().WithMessage("Unesite OIB osnivača obrta.").Matches("[0-9]{11}$").WithMessage("Unesite ispravan OIB osnivača obrta");
            RuleFor(osnivacObrt => osnivacObrt.Email).NotEmpty().WithMessage("Unesite email osnivača obrta").EmailAddress().WithMessage("Unesite ispravan email osnivača obrta");
            RuleFor(osnivacObrt => osnivacObrt.KontaktBroj).NotEmpty().WithMessage("Unesite kontakt broj osnivača obrta");
            RuleFor(osnivacObrt => osnivacObrt.Mjesto).NotEmpty().WithMessage("Unesite mjesto osnivača obrta.");
            RuleFor(osnivacObrt => osnivacObrt.UlicaIKucniBroj).NotEmpty().WithMessage("Unesite ulicu i kućni broj osnivača obrta.");
        }
    }
}