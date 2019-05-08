using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Cry_AES_File
{
    public class HmacHash
    {
        public static byte[] Sign(byte[] key, byte[] info)
        {
            HMAC hmac = new HMACSHA512(key);

            return hmac.ComputeHash(info, 0, info.Length);
        }

        public static bool Verify(byte[] key, byte[] info, byte[] hash)
        {
            HMAC hmac = new HMACSHA512(key);
            byte[] tempHash = hmac.ComputeHash(info, 0, info.Length);
            if (tempHash.Length != hash.Length)
                return false;
            int i = 0;
            while (i < tempHash.Length)
            {
                if (tempHash[i] != hash[i]) return false;
                i++;
            }

            return true;
        }

        public int GetHashSize()
        {
            HMAC hMAC = new HMACSHA512();
            return hMAC.HashSize;
        }
    }
}
