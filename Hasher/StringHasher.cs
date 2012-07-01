﻿using System;
using System.Security.Cryptography;
using System.Text;

namespace Hasher
{
    /// <summary>
    /// Class implementing various string hashing algorithms.
    /// </summary>
    public class StringHasher
    {
        /// <summary>
        /// Delegate represents hash method implementing certain hashing algorithm.
        /// </summary>
        /// <param name="stringToHash">The string to hash.</param>
        /// <returns>The hashed string.</returns>
        private delegate string HashAlgorithMethod(string stringToHash);

        /// <summary>
        /// Hashes a string using MD5 Algorithm.
        /// </summary>
        /// <param name="stringToHash">The string to hash.</param>
        /// <returns>The hashed string.</returns>
        public string MD5ComputeHash(string stringToHash)
        {
            string results;
            using (var md5Algorithm = MD5.Create())
            {
                results = ComputeHash(md5Algorithm, stringToHash);
                DisposeAlgorithm(md5Algorithm);
            }

            return results;
        }

        /// <summary>
        /// Adds randomly generated SALT to the string then hashes the string using MD5 Algorithm.
        /// The salt has the same length as the one generated by the algorithm.
        /// </summary>
        /// <param name="stringToHash">The string to hash.</param>
        /// <returns>The hashed string</returns>
        /// <remarks>The returned string has the salt used concatinated with output hash.</remarks>
        public string MD5SaltComputeHash(string stringToHash)
        {
            var salt = GenerateRandomSalt(16);
            return salt + MD5ComputeHash(salt + stringToHash);
        }

        /// <summary>
        /// Verifies string against another MD5 hashed string.
        /// </summary>
        /// <param name="stringToVerify">The string to verify.</param>
        /// <param name="hash">The MD5 generated hash to verify against.</param>
        /// <returns>Boolean indicating whether the string match the hash or not.</returns>
        public bool MD5VerifyHash(string stringToVerify, string hash)
        {
            var hashedString = MD5ComputeHash(stringToVerify);
            return AreTwoStringsEqual(hash, hashedString);
        }

        /// <summary>
        /// Verifies string against another MD5 SALT hashed string.
        /// </summary>
        /// <param name="stringToVerify">The string to verify.</param>
        /// <param name="hash">The MD5 SALT generated hash to verify against.</param>
        /// <returns>Boolean indifcating whether the string match the SALT hash or not.</returns>
        public bool MD5SaltVerifyHash(string stringToVerify, string hash)
        {
            return VerifySaltHash(stringToVerify, hash, MD5ComputeHash);
        }

        /// <summary>
        /// Hashes a string using SHA256 Algorithm.
        /// </summary>
        /// <param name="stringToHash">The string to hash.</param>
        /// <returns>The hashed string.</returns>
        public string SHA256ComputeHash(string stringToHash)
        {
            string results;
            using (var sha256Algorithm = new SHA256Managed())
            {
                results = ComputeHash(sha256Algorithm, stringToHash);
                DisposeAlgorithm(sha256Algorithm);
            }

            return results;
        }

        /// <summary>
        /// Adds randomly generated SALT to the string then hashes the string using SHA256 Algorithm.
        /// The salt has the same length as the one generated by the algorithm.
        /// </summary>
        /// <param name="stringToHash">The string to hash.</param>
        /// <returns>The hashed string</returns>
        /// <remarks>The returned string has the salt used concatinated with output hash.</remarks>
        public string SHA256SaltComputeHash(string stringToHash)
        {
            var salt = GenerateRandomSalt(32);
            return salt + SHA256ComputeHash(salt + stringToHash);
        }

        /// <summary>
        /// Verifies string against another SHA256 hashed string.
        /// </summary>
        /// <param name="stringToVerify">The string to verify.</param>
        /// <param name="hash">The SHA256 generated hash to verify against.</param>
        /// <returns>Boolean indicating whether the string match the hash or not.</returns>
        public bool SHA256VerifyHash(string stringToVerify, string hash)
        {
            var hashedString = SHA256ComputeHash(stringToVerify);
            return AreTwoStringsEqual(hash, hashedString);
        }

        /// <summary>
        /// Verifies string against another SHA256 SALT hashed string.
        /// </summary>
        /// <param name="stringToVerify">The string to verify.</param>
        /// <param name="hash">The SHA256 SALT generated hash to verify against.</param>
        /// <returns>Boolean indifcating whether the string match the SALT hash or not.</returns>
        public bool SHA256SaltVerifyHash(string stringToVerify, string hash)
        {
            return VerifySaltHash(stringToVerify, hash, SHA256ComputeHash);
        }

        /// <summary>
        /// Hashes a string using SHA512 Algorithm.
        /// </summary>
        /// <param name="stringToHash">The string to hash.</param>
        /// <returns>The hashed string.</returns>
        public string SHA512ComputeHash(string stringToHash)
        {
            string results;
            using (var sha512Algorithm = new SHA512Managed())
            {
                results = ComputeHash(sha512Algorithm, stringToHash);
                DisposeAlgorithm(sha512Algorithm);
            }

            return results;
        }

        /// <summary>
        /// Adds randomly generated SALT to the string then hashes the string using SHA512 Algorithm.
        /// The salt has the same length as the one generated by the algorithm.
        /// </summary>
        /// <param name="stringToHash">The string to hash.</param>
        /// <returns>The hashed string</returns>
        /// <remarks>The returned string has the salt used concatinated with output hash.</remarks>
        public string SHA512SaltComputeHash(string stringToHash)
        {
            var salt = GenerateRandomSalt(64);
            return salt + SHA512ComputeHash(salt + stringToHash);
        }

        /// <summary>
        /// Verifies string against another SHA512 hashed string.
        /// </summary>
        /// <param name="stringToVerify">The string to verify.</param>
        /// <param name="hash">The SHA512 generated hash to verify against.</param>
        /// <returns>Boolean indicating whether the string match the hash or not.</returns>
        public bool SHA512VerifyHash(string stringToVerify, string hash)
        {
            var hashedString = SHA512ComputeHash(stringToVerify);
            return AreTwoStringsEqual(hash, hashedString);
        }

        /// <summary>
        /// Verifies string against another SHA512 SALT hashed string.
        /// </summary>
        /// <param name="stringToVerify">The string to verify.</param>
        /// <param name="hash">The SHA512 SALT generated hash to verify against.</param>
        /// <returns>Boolean indifcating whether the string match the SALT hash or not.</returns>
        public bool SHA512SaltVerifyHash(string stringToVerify, string hash)
        {
            return VerifySaltHash(stringToVerify, hash, SHA512ComputeHash);
        }

        /// <summary>
        /// Computes the hash of string using the passed hashed algorithm.
        /// </summary>
        /// <param name="hashAlgorithm">The hash algorithm to use in computing.</param>
        /// <param name="stringToHash">The string to hash.</param>
        /// <returns>The hashed string.</returns>
        private static string ComputeHash(HashAlgorithm hashAlgorithm, string stringToHash)
        {
            var results = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(stringToHash));
            return BytesToHexadecimalString(results);
        }

        /// <summary>
        /// Verifies string against another SALT hashed string using the passed hashing algorithm.
        /// </summary>
        /// <param name="stringToVerify">The string to verify.</param>
        /// <param name="hash">The generated SALT hash to verify against.</param>
        /// <param name="algorithmToInvoke">The hash algorithm to use in verifying.</param>
        /// <returns>Boolean indicating whether the string match the SALT hash or not</returns>
        private static bool VerifySaltHash(string stringToVerify, string hash, HashAlgorithMethod algorithmToInvoke)
        {
            var saltLength = hash.Length / 2;
            var salt = hash.Substring(0, saltLength);
            var validHash = hash.Substring(saltLength);
            var hashedString = algorithmToInvoke(salt + stringToVerify);
            return AreTwoStringsEqual(validHash, hashedString);
        }

        /// <summary>
        /// Disposes hashing algorithm.
        /// </summary>
        /// <param name="hashAlgorithm">The hash algorithm to dispose.</param>
        private static void DisposeAlgorithm(HashAlgorithm hashAlgorithm)
        {
            hashAlgorithm.Clear();
        }

        /// <summary>
        /// Generates random SALT with the specified length.
        /// </summary>
        /// <param name="saltLength">Length of SALT to generate.</param>
        /// <returns>The randomly generated SALT.</returns>
        private static string GenerateRandomSalt(int saltLength)
        {
            var random = new RNGCryptoServiceProvider();
            var salt = new byte[saltLength];
            random.GetBytes(salt);
            return BytesToHexadecimalString(salt);
        }

        /// <summary>
        /// Converts given bytes array to their corresponding hexadecimal string.
        /// </summary>
        /// <param name="bytesToConvert">The bytes array to convert.</param>
        /// <returns>The corresponding string representation.</returns>
        private static string BytesToHexadecimalString(byte[] bytesToConvert)
        {
            var hexaDecimalString = new StringBuilder();

            for (var i = 0; i < bytesToConvert.Length; i++)
            {
                hexaDecimalString.Append(bytesToConvert[i].ToString("x2"));
            }

            return hexaDecimalString.ToString();
        }

        /// <summary>
        /// Compares two hashes ignoring their cases.
        /// </summary>
        /// <param name="hash">The first hash to compare.</param>
        /// <param name="hashedString">The second hash to compare.</param>
        /// <returns>Boolean indicating whether hashes are equal or not.</returns>
        private static bool AreTwoStringsEqual(string hash, string hashedString)
        {
            return hash.Equals(hashedString, StringComparison.OrdinalIgnoreCase);
        }
    }
}