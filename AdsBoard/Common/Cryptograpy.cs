using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace AdsBoard.Common
{
    class Cryprograpy
    {
        static string Key = "vfhnsik.[f";

        public static string Encrypt(string decryptString)
        {
            if(decryptString!=null)
            {
                using (var md5 = MD5.Create())
                {
                    using (var tdes = System.Security.Cryptography.TripleDESCryptoServiceProvider.Create())
                    {
                        tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(Key));
                        tdes.Mode = CipherMode.ECB;
                        tdes.Padding = PaddingMode.PKCS7;

                        using (var encryptor = tdes.CreateEncryptor())
                        {
                            byte[] strBytes = UTF8Encoding.UTF8.GetBytes(decryptString);
                            byte[] bytes = encryptor.TransformFinalBlock(strBytes, 0, strBytes.Length);
                            return Convert.ToBase64String(bytes, 0, bytes.Length);
                        }
                    }
                }
            }
            else
            {
                return string.Empty;
            }
        }
        public static string Decrypt(string encryptString)
        {
            if(encryptString !=null)
            {
                using (var md5 = new MD5CryptoServiceProvider())
                {
                    using (var tdes = new TripleDESCryptoServiceProvider())
                    {
                        tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(Key));
                        tdes.Mode = CipherMode.ECB;
                        tdes.Padding = PaddingMode.PKCS7;

                        using (var transform = tdes.CreateDecryptor())
                        {
                            byte[] encryptBytes = Convert.FromBase64String(encryptString);
                            byte[] bytes = transform.TransformFinalBlock(encryptBytes, 0, encryptBytes.Length);
                            return UTF8Encoding.UTF8.GetString(bytes);
                        }
                    }
                }
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
