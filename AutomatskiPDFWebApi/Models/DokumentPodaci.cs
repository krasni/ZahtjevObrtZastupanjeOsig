using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomatskiPDFWebApi.Models
{
    [Validator(typeof(DokumentPodaciValidator))]
    public class DokumentPodaci
    {
        public string MjestoIzrade { get; set; }
        public DateTime DatumIzrade { get; set; }
    }

    public class DokumentPodaciValidator : AbstractValidator<DokumentPodaci>
    {
        public DokumentPodaciValidator()
        {
            RuleFor(dokumentPodaci => dokumentPodaci.MjestoIzrade).NotEmpty().WithMessage("{PropertyName} je obavezan podatak.");
            RuleFor(dokumentPodaci => dokumentPodaci.DatumIzrade).NotEmpty().WithMessage("{PropertyName} je obavezan podatak.");
        }
    }
}