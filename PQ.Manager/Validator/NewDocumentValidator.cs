using FluentValidation;
using PQ.CoreShared.ModelViews;
using System;
using System.Collections.Generic;
using System.Text;

namespace PQ.Manager.Validator
{
    public class NewDocumentValidator : AbstractValidator<NewDocument> 
    {
        public NewDocumentValidator()
        {
            RuleFor(p => p.DocumentPath).MinimumLength(5).WithMessage("A url deve ter no mínimo 5 caracteres");
        }
    }
}
