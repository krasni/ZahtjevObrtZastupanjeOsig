using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomatskiPDFWebApi.Models
{
    [Validator(typeof(KontrolaPovezanostiValidator))]
    public class KontrolaPovezanosti
    {
        public bool? KontrolaPovlastenosti { get; set; }
        public string PravnaIliFizickaOsoba { get; set; }
        public FizickaOsoba FizickaOsoba { get; set; }
        public PravnaOsoba PravnaOsoba { get; set; }
    }

    public class KontrolaPovezanostiValidator : AbstractValidator<KontrolaPovezanosti>
    {
        public KontrolaPovezanostiValidator()
        {
            RuleFor(kontrolaPovezanosti => kontrolaPovezanosti.KontrolaPovlastenosti).Must(x => x == false || x == true).WithMessage("Kontrola povezanosti je obavezan podatak.");
            RuleFor(kontrolaPovezanosti => kontrolaPovezanosti.PravnaIliFizickaOsoba).Must(x => x=="pravnaOsoba" || x =="fizickaOsoba").WithMessage("{PropertyName}: Dozvoljene vrijednosti: pravnaOsoba, fizickaOsoba.");

            When(kontrolaPovezanosti => (kontrolaPovezanosti.KontrolaPovlastenosti == true) && (kontrolaPovezanosti.PravnaIliFizickaOsoba == "fizickaOsoba"), () =>
            {
                RuleFor(kontrolaPovezanosti => kontrolaPovezanosti.FizickaOsoba).NotEmpty();
                RuleFor(kontrolaPovezanosti => kontrolaPovezanosti.FizickaOsoba).SetValidator(new FizickaOsobaValidator());
            });

            When(kontrolaPovezanosti => (kontrolaPovezanosti.KontrolaPovlastenosti == true) && (kontrolaPovezanosti.PravnaIliFizickaOsoba == "pravnaOsoba"), () =>
            {
                RuleFor(kontrolaPovezanosti => kontrolaPovezanosti.PravnaOsoba).NotEmpty();
                RuleFor(kontrolaPovezanosti => kontrolaPovezanosti.PravnaOsoba).SetValidator(new PravnaOsobaValidator());
            });
        }
    }
}