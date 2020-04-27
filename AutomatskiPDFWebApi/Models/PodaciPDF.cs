using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutomatskiPDFWebApi.Models
{
    [Validator(typeof(PodaciPDFValidator))]
    public class PodaciPDF
    {
        public OsnivacObrt OsnivacObrt { get; set; }

        public Obrt Obrt { get; set; }

        public OvlasteniZastupnik OvlasteniZastupnik { get; set; }

        public KontrolaPovezanosti KontrolaPovezanosti { get; set; }

        public EmailPodaci EmailPodaci { get; set; }

        public DokumentPodaci DokumentPodaci { get; set; }
    }

    public class PodaciPDFValidator : AbstractValidator<PodaciPDF>
    {
        public PodaciPDFValidator()
        {
            RuleFor(podaciPDF => podaciPDF.OsnivacObrt).NotEmpty().WithMessage("{PropertyName} je obavezan podatak."); ;
            RuleFor(podaciPDF => podaciPDF.OsnivacObrt).SetValidator(new OsnivacObrtValidator());

            RuleFor(podaciPDF => podaciPDF.Obrt).NotEmpty().WithMessage("{PropertyName} je obavezan podatak."); ;
            RuleFor(podaciPDF => podaciPDF.Obrt).SetValidator(new ObrtValidator());

            RuleFor(podaciPDF => podaciPDF.OvlasteniZastupnik).NotEmpty().WithMessage("{PropertyName} je obavezan podatak."); ;
            RuleFor(podaciPDF => podaciPDF.OvlasteniZastupnik).SetValidator(new OvlasteniZastupnikValidator());

            RuleFor(podaciPDF => podaciPDF.KontrolaPovezanosti).NotEmpty().WithMessage("{PropertyName} je obavezan podatak."); ;
            RuleFor(podaciPDF => podaciPDF.KontrolaPovezanosti).SetValidator(new KontrolaPovezanostiValidator());

            RuleFor(podaciPDF => podaciPDF.EmailPodaci).NotEmpty().WithMessage("{PropertyName} je obavezan podatak.");
            RuleFor(podaciPDF => podaciPDF.EmailPodaci).SetValidator(new EmailPodaciValidator());
        }
    }
}