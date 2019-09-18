using System;
using System.Security.Cryptography;

namespace FreelanceService.Common.Salt
{
    public class GenerateSalt
    {
        private static int saltLengthLimit = 32;
        public static string GetDinamicSalt()
        {
            return GetDinamicSalt(saltLengthLimit);
        }
        public static string GetDinamicSalt(int maximumSaltLength)
        {
            var salt = new byte[maximumSaltLength];
            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }
            string stringSalt = string.Empty;
            foreach (byte x in salt)
            {
                stringSalt += String.Format("{0:x2}", x);
            }
            return stringSalt;
        }
    }
}
