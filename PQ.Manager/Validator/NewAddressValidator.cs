using FluentValidation;
using PQ.CoreShared.ModelViews;
using System;
using System.Collections.Generic;
using System.Text;

namespace PQ.Manager.Validator
{
    public class NewAddressValidator : AbstractValidator<NewAddress>
    {
        public NewAddressValidator()
        {
            RuleFor(p => p.CEP).NotEmpty().NotNull();
            RuleFor(p => p.State).NotEmpty().NotNull().MaximumLength(100);
            RuleFor(p => p.District).NotEmpty().NotNull().MaximumLength(100);
            RuleFor(p => p.City).NotEmpty().NotNull().MaximumLength(200);
            RuleFor(p => p.PublicPlace).NotEmpty().NotNull().MaximumLength(200);
            RuleFor(p => p.Number).NotEmpty().NotNull().MaximumLength(10);
            RuleFor(p => p.Complement).MaximumLength(200);
        }
    }
}
