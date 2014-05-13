using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Protective.Core.Entity.Authentication;
using Protective.Core.Interfaces.Repository;
using Protective.DAL.Repository;

namespace Protective.DAL.Test.Repository
{
    public class AuthenticationRepositoryTest
    {
        private IAuthenticationRepository _authenticationRepository;

        #region Setup

        [TestFixtureSetUp]
        public void Setup()
        {
            _authenticationRepository = new AuthenticationRepository();
        }

        #endregion

        #region Get User By Name Tests

        [Test]
        public void GetUserByName_Should_Return_an_IdentityUser_Entity_By_UserName()
        {
            IdentityUser user = _authenticationRepository.GetUserByName("admin");
            Assert.That(user.UserId, Is.GreaterThan(0));
        }
        #endregion

        #region Get Password Hash Tests

        [Test]
        public void GetPasswordHash_Should_Return_a_Users_Hashed_Password()
        {
            IdentityUser user = _authenticationRepository.GetUserByName("admin");
            string hash = _authenticationRepository.GetPasswordHash(user.UserId);
            Assert.That(hash.Length, Is.GreaterThan(0));
        }
        #endregion
    }
}
