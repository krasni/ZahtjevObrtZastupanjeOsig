using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZahtjevObrtZastupanjeOsig.Models
{
    [Validator(typeof(OvlasteniZastupnikValidator))]
    public class OvlasteniZastupnik
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Oib { get; set; }
        public string BrojURegistruHanfe { get; set; }
        public string Mjesto { get; set; }
        public string UlicaIKucniBroj { get; set; }
        public bool KontrolaOvlastenja { get; set; }
    }

    public class OvlasteniZastupnikValidator : AbstractValidator<OvlasteniZastupnik>
    {
        public OvlasteniZastupnikValidator()
        {
            RuleFor(ovlasteniZastupinik => ovlasteniZastupinik.KontrolaOvlastenja).Must(x => x == false || x == true).WithMessage("Kontrola ovlastenja je obavezan podatak.");

            When(ovlasteniZastupnik => ovlasteniZastupnik.KontrolaOvlastenja == true, () => {
                RuleFor(ovlasteniZastupnik => ovlasteniZastupnik.Ime).NotEmpty().WithMessage("Unesite ime ovlaštenog zastupnika.");
                RuleFor(ovlasteniZastupnik => ovlasteniZastupnik.Prezime).NotEmpty().WithMessage("Unesite prezime ovlaštenog zastupnika.");
                RuleFor(ovlasteniZastupnik => ovlasteniZastupnik.Oib).NotEmpty().WithMessage("Unesite OIB ovlaštenog zastupnika.").Matches("[0-9]{11}$").WithMessage("Unesite ispravan OIB ovlaštene osobe");
                RuleFor(ovlasteniZastupnik => ovlasteniZastupnik.BrojURegistruHanfe).NotEmpty().WithMessage("Unesite broj u registru Hanfe ovlaštenog zastupnika.");
                RuleFor(ovlasteniZastupnik => ovlasteniZastupnik.Mjesto).NotEmpty().WithMessage("Unesite mjesto ovlaštenog zastupnika.");
                RuleFor(ovlasteniZastupnik => ovlasteniZastupnik.UlicaIKucniBroj).NotEmpty().WithMessage("Unesite ulicu i kućni broj ovlaštenog zastupnika.");
            });
        }
    }
}