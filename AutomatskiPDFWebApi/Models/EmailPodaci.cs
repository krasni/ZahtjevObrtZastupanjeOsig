using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomatskiPDFWebApi.Models
{
    [Validator(typeof(EmailPodaciValidator))]
    public class EmailPodaci
    {
        public bool? PosaljiNaEmail { get; set; }
        public string EmailAdresaZaDostavu { get; set; }
    }

    public class EmailPodaciValidator : AbstractValidator<EmailPodaci>
    {
        public EmailPodaciValidator()
        {
            RuleFor(emailPodaci => emailPodaci.PosaljiNaEmail).Must(x => x == false || x == true).WithMessage("{PropertyName} je obavezan podatak.");

            When(emailPodaci => emailPodaci.PosaljiNaEmail == true, () =>
            {
                RuleFor(emailPodaci => emailPodaci.EmailAdresaZaDostavu).NotEmpty().WithMessage("Unesite email adresu za dostavu").EmailAddress().WithMessage("Unesite ispravnu email adresu za dostavu.");
            });
        }
    }
}