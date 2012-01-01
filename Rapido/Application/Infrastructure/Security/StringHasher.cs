using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Dynamic;

namespace Rapido.Infrastructure.Security
{
   
    public class StringHasher
    {
        //RequireAuthenticatedUser();
        //call base controller method top of controller action or call from virtual overrode onaction() will redirect
        //have only=> "actiona","actionb" that it applies to
        public bool ComputedHashUsingGivenSaltMatchesGivenHash(string plain, string salt, string hashed)
        {
            string computedHash = ComputeHashUsingGivenSalt(plain, salt);

            return hashed == computedHash;
        }

        public dynamic ComputeHashUsingRandomSalt(string plain)
        {
            string salt = GenerateRandomSalt();

            string hashed = ComputeHashUsingGivenSalt(plain, salt);

            if (!ComputedHashUsingGivenSaltMatchesGivenHash(plain, salt, hashed))
                throw new Exception("The computed hash value does ont match the hash value that it is compared to.");

            dynamic hash_salt = new ExpandoObject();
            hash_salt.Salt = salt;
            hash_salt.HashedValue = hashed;

            return hash_salt;
        }


        public string ComputeHashUsingGivenSalt(string plain, string salt)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);

            byte[] passwordBytes = System.Text.UnicodeEncoding.Unicode.GetBytes(plain);

            byte[] combinedBytes = new byte[passwordBytes.Length + saltBytes.Length];

            //Array src , int srcOffset, Array dst, int dstOffset, int count
            System.Buffer.BlockCopy(passwordBytes, 0, combinedBytes, 0, passwordBytes.Length);

            System.Buffer.BlockCopy(saltBytes, 0, combinedBytes, passwordBytes.Length, saltBytes.Length);

            HashAlgorithm hashAlgo = new SHA512Cng();

            byte[] hashBytes = hashAlgo.ComputeHash(combinedBytes);

            return Convert.ToBase64String(hashBytes);
        }

        private string GenerateRandomSalt(int saltSize = 32)
        {
            byte[] saltBytes = new byte[saltSize];

            RNGCryptoServiceProvider.Create().GetBytes(saltBytes);

            return Convert.ToBase64String(saltBytes);
        }

        private string GenerateRandomSaltMinMax(int minSaltSize = 32, int maxSaltSize = 64)
        {
            Random random = new Random();

            int saltSize = random.Next(minSaltSize, maxSaltSize);

            byte[] saltBytes = new byte[saltSize];

            RNGCryptoServiceProvider.Create().GetBytes(saltBytes);

            string salt = Convert.ToBase64String(saltBytes);

            //Validate just to be sure
            if (!AreBytesArraysEqual(saltBytes, Convert.FromBase64String(salt)))
                throw new Exception("Password hasher Salt does not validate.");

            return salt;
        }

        private bool AreBytesArraysEqual(byte[] saltBytes, byte[] saltBytes2)
        {

            if (saltBytes.Length != saltBytes2.Length) return false;

            int i = 0;
            foreach (byte b in saltBytes)
            {
                if (saltBytes[i] != saltBytes2[i])
                {

                    return false;
                }

                i++;
            }

            return true;
        }
    }
}