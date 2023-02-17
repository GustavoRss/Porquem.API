using PQ.Core.Domain;
using PQ.CoreShared.ModelViews.Email;
using PQ.CoreShared.ModelViews.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PQ.Manager.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAsync();

        Task<User> GetAsync(string login);

        Task<User> InsertAsync(User user);

        Task<User> UpdateAsync(User user);

        Task<bool> AlreadyExist(string email);

        bool ForgotPassword(EmailModel model, string origin);

        bool ResetPassword(ResetPasswordModel model);

        bool verifyToken(ResetPasswordModel model);

    }
}
