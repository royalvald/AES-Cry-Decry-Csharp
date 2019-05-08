using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Cry_AES_File
{
    class Program
    {
        public static void Main(string[] args)
        {
            //string s1 = "hello world";
            //string s2 = "bye bye";
            //byte[] key = Encoding.UTF8.GetBytes(s2);
            //byte[] info = Encoding.UTF8.GetBytes(s1);
            //byte[] signInfo = HmacHash.Sign(key, info);
            //Console.WriteLine(HmacHash.Verify(key, info, signInfo));
            //HmacHash hmac = new HmacHash();
            //Console.WriteLine(hmac.GetHashSize()/8);
            string s1 = @"F:\test\code\test";
            Directory.CreateDirectory(s1);
            Console.WriteLine(Directory.Exists(s1));
        }
    }
}