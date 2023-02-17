using FluentValidation;
using PQ.CoreShared.ModelViews;
using System;
using System.Collections.Generic;
using System.Text;

namespace PQ.Manager.Validator
{
    public class ChangeEntityValidator:AbstractValidator<UpdatePhilanthropicEntity>
    {
        public ChangeEntityValidator()
        {
            RuleFor(p => p.Id).NotNull().NotEmpty().GreaterThan(0);
            Include(new NewEntityValidator());
        }
        
    }
}
