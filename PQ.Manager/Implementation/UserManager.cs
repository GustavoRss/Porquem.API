using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PQ.Core.Domain;
using PQ.CoreShared.ModelViews;
using PQ.CoreShared.ModelViews.Email;
using PQ.CoreShared.ModelViews.User;
using PQ.CoreShared.ModelViews.Usuario;
using PQ.Manager.Interfaces;
using PQ.Manager.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PQ.Manager.Implementation
{
        public class UserManager : IUserManager
        {
        private readonly IUserRepository repository;
        private readonly IMapper mapper;
        private readonly IJWTService jwt;

        public UserManager(IUserRepository repository, IMapper mapper, IJWTService jwt)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.jwt = jwt;
        }

        public async Task<IEnumerable<UserView>> GetAsync()
        {
            return mapper.Map<IEnumerable<User>, IEnumerable<UserView>>(await repository.GetAsync());
        }

        public async Task<UserView> GetAsync(string login)
        {
            return mapper.Map<UserView>(await repository.GetAsync(login));
        }

        public async Task<UserView> InsertAsync(NewUser newUser)
        {
            var user = mapper.Map<User>(newUser);
            ConvertPasswordToHash(user);
            return mapper.Map<UserView>(await repository.InsertAsync(user));
        }

        private void ConvertPasswordToHash(User user)
        {
            var passwordHasher = new PasswordHasher<User>();
            user.Password = passwordHasher.HashPassword(user, user.Password);
        }

        public async Task<UserView> UpdatePhilanEntityAsync(User user)
        {
            ConvertPasswordToHash(user);
            return mapper.Map<UserView>(await repository.UpdateAsync(user));
        }

        public async Task<LoggedUser> ValidateUserAndGenerateToken(User user)
        {
            var consultedUser = await repository.GetAsync(user.Email);
            if (consultedUser == null)
            {
                return null;
            }
            if (await ValidateAndUpdateHashAsync(user, consultedUser.Password))
            {
                var loggedUser = mapper.Map<LoggedUser>(consultedUser);
                loggedUser.Token = jwt.GerarToken(consultedUser);
                return loggedUser;
            }
            return null;
        }

        private async Task<bool> ValidateAndUpdateHashAsync(User user, string hash)
        {
            var passwordHasher = new PasswordHasher<User>();
            var status = passwordHasher.VerifyHashedPassword(user, hash, user.Password);
            switch (status)
            {
                case PasswordVerificationResult.Failed:
                    return false;

                case PasswordVerificationResult.Success:
                    return true;

                case PasswordVerificationResult.SuccessRehashNeeded:
                    await UpdatePhilanEntityAsync(user);
                    return true;

                default:
                    throw new InvalidOperationException();
            }
        }

        public bool ForgotPassword(EmailModel model, string origin)
        {
           return repository.ForgotPassword(model, origin);
        }

        public bool ResetPassword(ResetPasswordModel model)
        {
          return repository.ResetPassword(model);
        }

        public bool verifyToken(ResetPasswordModel model)
        {
           return repository.verifyToken(model);
        }
    }
}

