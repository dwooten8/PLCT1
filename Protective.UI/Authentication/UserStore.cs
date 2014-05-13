using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Protective.Core.Entity.Authentication;
using Protective.Core.Interfaces.Repository;
using StructureMap;

namespace Protective.UI.Authentication
{
    /// <summary>
    /// Class that implements the key ASP.NET Identity user store interfaces
    /// </summary>
    public class UserStore : IUserStore<IdentityUser>,
                             IUserLoginStore<IdentityUser>,
                             IUserRoleStore<IdentityUser>,
                             IUserPasswordStore<IdentityUser>
    {

        private IAuthenticationRepository _authenticationRepository;

        public UserStore()
        {
            _authenticationRepository = ObjectFactory.GetInstance<IAuthenticationRepository>();
        }


        /// <summary>
        /// Returns an IdentityUser instance based on a userName query 
        /// </summary>
        /// <param name="userName">The user's name</param>
        /// <returns></returns>
        public Task<IdentityUser> FindByNameAsync(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("Null or empty argument: userName");
            }

            IdentityUser user = _authenticationRepository.GetUserByName(userName);
            return Task.FromResult<IdentityUser>(user);
        }

        public Task<IdentityUser> FindByIdAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("Null or empty argument: userId");
            }

            int id = Convert.ToInt32(userId);
            IdentityUser result = _authenticationRepository.GetUserById(id);
            if (result != null)
            {
                return Task.FromResult<IdentityUser>(result);
            }

            return Task.FromResult<IdentityUser>(null);
        }

        public Task<string> GetPasswordHashAsync(IdentityUser user)
        {
            string passwordHash = _authenticationRepository.GetPasswordHash(user.UserId);
            return Task.FromResult<string>(passwordHash);
        }

        public void Dispose()
        {
            if (_authenticationRepository != null)
            {
                _authenticationRepository = null;
            }
        }

        public Task CreateAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task AddToRoleAsync(IdentityUser user, string role)
        {
            throw new NotImplementedException();
        }

        public Task<IList<string>> GetRolesAsync(IdentityUser user)
        {
            return Task.FromResult<IList<string>>(user.UserRoles.Select(r => r.Name).ToList());
        }

        public Task<bool> IsInRoleAsync(IdentityUser user, string role)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromRoleAsync(IdentityUser user, string role)
        {
            throw new NotImplementedException();
        }


        public Task AddLoginAsync(IdentityUser user, UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityUser> FindAsync(UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task RemoveLoginAsync(IdentityUser user, UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasPasswordAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task SetPasswordHashAsync(IdentityUser user, string passwordHash)
        {
            throw new NotImplementedException();
        }
    }
}