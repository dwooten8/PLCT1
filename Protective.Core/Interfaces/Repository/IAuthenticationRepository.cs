using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protective.Core.Entity.Authentication;

namespace Protective.Core.Interfaces.Repository
{
    public interface IAuthenticationRepository
    {
        IdentityUser GetUserByName(string userName);

        IdentityUser GetUserById(int userId);

        string GetPasswordHash(int userId);
    }
}
