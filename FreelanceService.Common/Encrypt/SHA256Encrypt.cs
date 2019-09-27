using System;
using System.Security.Cryptography;
using System.Text;

namespace FreelanceService.Common.Encrypt
{
    public class SHA256Encrypt
    {
        public static string getHashSha256WithSalt(string text, string dynamicSaltArg)
        {
            var hashWithSalt = text + dynamicSaltArg;
            byte[] bytes = Encoding.Unicode.GetBytes(hashWithSalt);
            SHA256Managed hashString = new SHA256Managed();
            byte[] hashByte = hashString.ComputeHash(bytes);
            string hash = string.Empty;
            foreach (byte x in hashByte)
            {
                hash += String.Format("{0:x2}", x);
            }
            return hash;
        }

        public static string getHashSha256(string text)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(text);
            SHA256Managed hashString = new SHA256Managed();
            byte[] hashByte = hashString.ComputeHash(bytes);
            string hash = string.Empty;
            foreach (byte x in hashByte)
            {
                hash += String.Format("{0:x2}", x);
            }
            return hash;
        }

        public static bool checkHashSha256(string pass, string passHash, string dynamicSalt)
        {
            var hashWithSalt = pass + dynamicSalt;
            byte[] bytes = Encoding.Unicode.GetBytes(hashWithSalt);
            SHA256Managed hashString = new SHA256Managed();
            byte[] hashByte = hashString.ComputeHash(bytes);
            string hash = string.Empty;
            foreach (byte x in hashByte)
            {
                hash += String.Format("{0:x2}", x);
            }
            if (Equals(hash, passHash))
                return true;
            else return false;
        }
    }
}
