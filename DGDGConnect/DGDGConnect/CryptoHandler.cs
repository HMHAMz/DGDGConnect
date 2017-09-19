using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCLCrypto;

namespace DGDGConnect
{
    public static class CryptoHandler
    {
        public static String Hash(string toHash)
        {
            /*byte[] salt = new byte[] { 85, 4, 5, 12, 46, 86, 72, 22 };
            int iterations = 5;
            int keyLengthInBytes = 16;
            byte[] key = NetFxCrypto.DeriveBytes.GetBytes(toHash, salt, iterations, keyLengthInBytes);*/

            /* The following cryptographic method is an adaptation of the 'recipe' provided by AArnott
             * Here: https://github.com/AArnott/PCLCrypto/wiki/Crypto-Recipes 
             */
            if (toHash != "" && toHash != null) //if passed is in fact a value
            {
                byte[] data = System.Text.Encoding.UTF8.GetBytes(toHash); // convert passed data to byte array - using UTF8 encoding
                var hashMach = WinRTCrypto.HashAlgorithmProvider.OpenAlgorithm(HashAlgorithm.Sha256);
                byte[] hash = hashMach.HashData(data);
                string hashBase64 = Convert.ToBase64String(hash);

                return hashBase64;
            }

            return "invalid";
            
        }
    }
}
