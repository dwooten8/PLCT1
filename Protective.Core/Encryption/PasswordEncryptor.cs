using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Protective.Core.Encryption
{
    public class PasswordEncryptor : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException("password");

            byte[] salt;
            byte[] buffer2;
            
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }

        public PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            if (string.IsNullOrEmpty(hashedPassword))
                return PasswordVerificationResult.Failed;

            if (string.IsNullOrEmpty(providedPassword))
                throw new ArgumentNullException("password");


            byte[] buffer4;            
            byte[] src = Convert.FromBase64String(hashedPassword);
            if ((src.Length != 0x31) || (src[0] != 0))
                return PasswordVerificationResult.Failed;

            byte[] dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            byte[] buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(providedPassword, dst, 0x3e8))
            {
                buffer4 = bytes.GetBytes(0x20);
            }
            return ByteArraysEqual(buffer3, buffer4);
        }

        private PasswordVerificationResult ByteArraysEqual(byte[] a1, byte[] a2)
        {
            System.Collections.IStructuralEquatable eqa1 = a1;
            return eqa1.Equals(a2, System.Collections.StructuralComparisons.StructuralEqualityComparer) ? PasswordVerificationResult.Success : PasswordVerificationResult.Failed;
        }
    }
}
