using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Protective.Core.Entity.Authentication;
using Protective.Core.Interfaces.Repository;

namespace Protective.DAL.Repository
{
    public class AuthenticationRepository : RepositoryBase, IAuthenticationRepository
    {
        public IdentityUser GetUserByName(string userName)
        {
            string commandText = @"Select u.Id, u.UserName, u.FirstName, u.LastName, u.Email, u.IsApproved, u.IsLockedOut, u.IsDeleted,
	                            u.CreateDate, u.PasswordReset, r.Name, r.Id 
                                from Users u 
                                Inner Join UserRoles ur on u.Id = ur.UserId 
                                inner join Roles r on ur.RoleId = r.Id 
                                where u.UserName = @name";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@name", userName);

            return GetUser(parameters, commandText);
        }

        public IdentityUser GetUserById(int userId)
        {
            string commandText = @"Select u.Id, u.UserName, u.FirstName, u.LastName, u.Email, u.IsApproved, u.IsLockedOut, u.IsDeleted,
	                            u.CreateDate, u.PasswordReset, r.Name, r.Id 
                                from Users u 
                                Inner Join UserRoles ur on u.Id = ur.UserId 
                                inner join Roles r on ur.RoleId = r.Id 
                                where u.Id = @id";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@id", userId);

            return GetUser(parameters, commandText);
        }

        private IdentityUser GetUser(DynamicParameters parameters, string commandText)
        {
            using (IDbConnection connection = GetConnection(RepositoryBase.ConnectionString))
            {
                connection.Open();

                var lookup = new Dictionary<int, IdentityUser>();
                IdentityUser user = connection.Query<IdentityUser, IdentityRole, IdentityUser>
                    (commandText,
                        (u, r) =>
                        {
                            IdentityUser identityUser;

                            if (!lookup.TryGetValue(u.UserId, out identityUser))
                            {
                                lookup.Add(u.UserId, identityUser = u);
                            }

                            identityUser.UserRoles.Add(r);
                            return identityUser;
                        },
                        parameters,
                        commandType: CommandType.Text,
                        splitOn: "Name").FirstOrDefault();

                return user;
            }
        }


        public string GetPasswordHash(int userId)
        {
            string commandText = "Select PasswordHash from Users where Id = @id";
            using (IDbConnection connection = GetConnection(RepositoryBase.ConnectionString))
            {
                connection.Open();
                IdentityUser user = connection.Query<IdentityUser>(commandText, new { Id = userId }).FirstOrDefault();
                return user.PasswordHash;
            }
        }
    }
}
