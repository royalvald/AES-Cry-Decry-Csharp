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
        /// <summary>
        /// 给定密钥下，对指定数据进行hamc映射
        /// </summary>
        /// <param name="key"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static byte[] Sign(byte[] key, byte[] info)
        {
            HMAC hmac = new HMACSHA512(key);

            return hmac.ComputeHash(info, 0, info.Length);
        }

        /// <summary>
        /// 给定数据确认验证信息是否是数据的映射
        /// </summary>
        /// <param name="key"></param>
        /// <param name="info"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
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
