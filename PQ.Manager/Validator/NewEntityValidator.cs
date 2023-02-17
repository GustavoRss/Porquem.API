using FluentValidation;
using PQ.Core.Domain;
using PQ.CoreShared.ModelViews;
using System;
using System.Collections.Generic;
using System.Text;

namespace PQ.Manager.Validator
{
    public class NewEntityValidator : AbstractValidator<NewPhilanthropicEntity>
    {
        public NewEntityValidator()
        {
            RuleFor(x => x.FantasyName).NotNull().NotEmpty().MinimumLength(10).MaximumLength(150);
            // RuleFor(x => x.DtOpening).NotNull().NotEmpty().LessThan(DateTime.Now).GreaterThan(DateTime.Now.AddYears(-200));
            RuleFor(x => x.CorporateName).NotNull().NotEmpty();
            RuleFor(x => x.Cnpj).NotNull().NotEmpty();
            RuleFor(x => x.StateRegistration).NotNull().NotEmpty();
            RuleFor(x => x.Telephone).NotNull().NotEmpty();
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Address).SetValidator(new NewAddressValidator());

        }
        /* private bool IsMorF(char sexo)
        {
            return sexo == 'M' || sexo == 'F';
        }*/
    }
}
