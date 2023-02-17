using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PQ.Core.Domain;
using PQ.CoreShared.ModelViews.Email;
using PQ.CoreShared.ModelViews.User;
using PQ.Data.Context;
using PQ.Manager.Interfaces.Repositories;
using PQ.Manager.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PQ.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly PQContext context;
        private readonly IEmailService _emailService;

        public UserRepository(PQContext context, IEmailService email)
        {
            this.context = context;
            this._emailService = email;
        }

        public async Task<IEnumerable<User>> GetAsync()
        {
            return await context.Users.AsNoTracking().ToListAsync();
        }

        public async Task<User> GetAsync(string login)
        {
            return await context.Users
                .Include(p => p.Roles)
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.Email == login);
        }

        public async Task<User> InsertAsync(User user)
        {
            await InsertUserRoleAsync(user);
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            return user;
        }

        private async Task InsertUserRoleAsync(User user)
        {
            var consultedRoles = new List<Role>();
            foreach(var role in user.Roles)
            {
                var consultedRole = await context.Roles.FindAsync(role.Id);
                consultedRoles.Add(consultedRole);
            }
            user.Roles = consultedRoles;
        }

        public async Task<User> UpdateAsync(User user)
        {
            var consultedUser = await context.Users.FindAsync(user.Email);
            if (consultedUser == null)
            {
                return null;
            }
            context.Entry(consultedUser).CurrentValues.SetValues(user);
            await context.SaveChangesAsync();
            return consultedUser;
        }

        public async Task<bool> AlreadyExist(string email)
        {
            var consultedUser = await context.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (consultedUser == null)
            {
                return true;
            } else
            {
                return false;
            }
        }

        private string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            // convert random bytes to hex string
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }

        public bool ForgotPassword(EmailModel model, string origin)
        {
            var account = context.Users.SingleOrDefault(x => x.Email == model.Email);

            // always return ok response to prevent email enumeration
            if (account == null) return false;

            // create reset token that expires after 1 day
            account.ResetToken = RandomTokenString();
            account.ResetTokenExpires = DateTime.UtcNow.AddDays(1);

            context.Users.Update(account);
            context.SaveChanges();

            // send email
            SendPasswordResetEmail(account, origin);
            return true;
        }

        public bool verifyToken(ResetPasswordModel model)
        {
            var account = context.Users.SingleOrDefault(x =>
               x.ResetToken == model.Token &&
               x.ResetTokenExpires > DateTime.UtcNow);

            if (account == null)
            {
                return false;
            } else
            {
                return true;
            }
                
        }

        public bool ResetPassword(ResetPasswordModel model)
        {
            var account = context.Users.SingleOrDefault(x =>
                x.ResetToken == model.Token &&
                x.ResetTokenExpires > DateTime.UtcNow);

            if (account == null)
                return false;

            // update password and remove reset token
            var passwordHasher = new PasswordHasher<User>();
            account.Password = passwordHasher.HashPassword(account, model.Password);

            context.Users.Update(account);
            context.SaveChanges();
            return true;
        }

        private void SendPasswordResetEmail(User account, string origin)
        {
            string message;
            if (!string.IsNullOrEmpty(origin))
            {
                var resetUrl = $"https://porquem.com/account/reset-password?token={account.ResetToken}";
                message = $@"<p>Por favor, clique no link abaixo para alterar a sua senha, o link estará válido por apenas um 1 dia</p>
                             <p><a href=""{resetUrl}"">Clique aqui</a></p><br>
                        <strong>Atenção! Não compartilhe este link com mais ninguém. Caso essa solicitação não tenha sido criada por você, envie um e-mail
                        urgentemente para porquem.dev@gmail.com</strong>";
            }
            else
            {
                message = $@"<p>Please use the below token to reset your password with the <code>/accounts/reset-password</code> api route:</p>
                             <p><code>{account.ResetToken}</code></p>";
            }

            _emailService.Send(
                to: account.Email,
                subject: "Instruções para recuperação de senha",
                html: $@"<h4>Recuperação de senha</h4>
                         {message}"
            );
        }

    }
}
