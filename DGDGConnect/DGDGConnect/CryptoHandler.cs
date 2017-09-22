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
