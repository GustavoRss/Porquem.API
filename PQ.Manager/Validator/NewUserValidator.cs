using FluentValidation;
using PQ.CoreShared.ModelViews.Usuario;
using PQ.Manager.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PQ.Manager.Validator
{
    public class NewUserValidator : AbstractValidator<NewUser>
    {
        private readonly IUserRepository repository;

        public NewUserValidator(IUserRepository repository)
        {
            this.repository = repository;
     
            RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress().MustAsync(async (email, cancelar) =>
            {
                bool exist = await AlreadyExist(email);
                return exist;
            }).WithMessage("Esse email já está em uso");
            RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage("A senha não pode ser vazia");
            RuleFor(x => x.Roles).NotNull().NotEmpty();
            RuleForEach(x => x.Roles).Must(x => x.Id != 4).WithMessage("Infelizmente não existe essa role cadastrada").NotNull().NotEmpty();
        }
        private async Task<bool> AlreadyExist(string email)
        {
            return await repository.AlreadyExist(email);
        }
    }
}
