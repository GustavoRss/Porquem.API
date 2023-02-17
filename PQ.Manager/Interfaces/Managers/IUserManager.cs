using PQ.Core.Domain;
using PQ.CoreShared.ModelViews;
using PQ.CoreShared.ModelViews.Email;
using PQ.CoreShared.ModelViews.User;
using PQ.CoreShared.ModelViews.Usuario;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PQ.Manager.Interfaces
{
    public interface IUserManager
    {
        Task<IEnumerable<UserView>> GetAsync();

        Task<UserView> GetAsync(string login);

        Task<UserView> InsertAsync(NewUser user);

        Task<UserView> UpdatePhilanEntityAsync(User user);

        Task<LoggedUser> ValidateUserAndGenerateToken(User user);

        bool ForgotPassword(EmailModel model, string origin);

        bool ResetPassword(ResetPasswordModel model);

        bool verifyToken(ResetPasswordModel model);
    }
}
