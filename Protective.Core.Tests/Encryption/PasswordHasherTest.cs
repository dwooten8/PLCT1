using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using NUnit.Framework;
using Protective.Core.Encryption;

namespace Protective.Core.Tests.Encryption
{
    [TestFixture]
    public class PasswordHasherTest
    {
        #region HashPassword Tests

        [Test]
        public void Verify_HashPassword_Hashes_a_Password()
        {
            PasswordEncryptor hasher = new PasswordEncryptor();
            string password = "password";
            string hash = hasher.HashPassword(password);
            Assert.That(hash.Length, Is.GreaterThan(0));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void HashPassword_Should_Return_an_Exception_if_the_Provided_Password_is_Empty()
        {
            PasswordEncryptor hasher = new PasswordEncryptor();
            string hash = hasher.HashPassword(string.Empty);
        }

        #endregion

        #region HashPassword Tests

        [Test]
        public void VerifyHashedPassword_Should_Verify_Two_Passwords_That_Are_Equal()
        {
            PasswordEncryptor hasher = new PasswordEncryptor();
            PasswordVerificationResult result = hasher.VerifyHashedPassword("ADAXH+oycHxLcBpQbDFIyi3HqJKRJsAuzbJGPKEF1AxmKhfjKH14xkXYiPowfoQTRQ==", "password");
            Assert.That(result, Is.EqualTo(PasswordVerificationResult.Success));
        }

        [Test]
        public void VerifyHashedPassword_Should_Not_Verify_Two_Passwords_That_Are_Not_Equal()
        {
            PasswordEncryptor hasher = new PasswordEncryptor();
            PasswordVerificationResult result = hasher.VerifyHashedPassword("ADAXH+oycHxLcBpQbDFIyi3HqJKRJsAuzbJGPKEF1AxmKhfjKH14xkXYiPowfoQTRQ==", "PassWord");
            Assert.That(result, Is.EqualTo(PasswordVerificationResult.Failed));
        }

        [Test]
        public void VerifyHashedPassword_Should_Not_Verify_a_Password_if_the_Hash_is_Empty()
        {
            PasswordEncryptor hasher = new PasswordEncryptor();
            PasswordVerificationResult result = hasher.VerifyHashedPassword(string.Empty, "PassWord");
            Assert.That(result, Is.EqualTo(PasswordVerificationResult.Failed));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void VerifyHashedPassword_Should_Return_an_Exception_if_the_Provided_Password_is_Empty()
        {
            PasswordEncryptor hasher = new PasswordEncryptor();
            PasswordVerificationResult result = hasher.VerifyHashedPassword("ADAXH+oycHxLcBpQbDFIyi3HqJKRJsAuzbJGPKEF1AxmKhfjKH14xkXYiPowfoQTRQ==", string.Empty);
        }
        #endregion
    }
}
