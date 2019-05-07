using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Cry_AES_File.Hash
{
    class HMACTool
    {
        public static byte[] SignInfo(byte[] key, byte[] info)
        {
            byte[] newArray = null;
            using (HMAC hmac = new HMACSHA512(key))
            {
                byte[] hashValue = hmac.ComputeHash(info);
                int arrayLength = info.Length + hashValue.Length;
                newArray = new byte[arrayLength];
                Array.Copy(hashValue, 0, newArray, 0, hashValue.Length);
                Array.Copy(info, 0, newArray, hashValue.Length, info.Length);
            }

            return newArray;
        }

        public static bool VerifyInfo(byte[] key,byte[] info)
        {
            byte[] newArray = null;
            using (HMAC hmac = new HMACSHA512(key))
            {
                int hashSize = hmac.HashSize/8;
                if (info.Length < hashSize / 8) return false;
                byte[] hashValue = new byte[hashSize];
                Array.Copy(info, 0, hashValue, 0, hashSize);
                byte[] infoHash = hmac.ComputeHash(info, hashSize, info.Length - hashSize);

                int minLength = Math.Min(infoHash.Length, hashSize);
                for(int i=0;i<minLength;i++)
                {
                    if (infoHash[i] != hashValue[i]) return false;
                }

                return true;
            }

        }
    }
}