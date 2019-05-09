using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SEDemo.SE.Method
{
    class tool
    {
        //字节数组转化为16进制字符串
        public static string ByteToHex(byte[] array)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var item in array)
            {
                builder.AppendFormat("{0:x2}", item);
            }

            return builder.ToString();
        }

        //16进制字符串转化为字符数组
        public static byte[] HexToByte(string hex)
        {
            byte[] infoByte = new byte[hex.Length / 2];
            int temp = 0;
            for(int i=0;i<infoByte.Length;i++)
            {
                temp = Convert.ToInt32(hex.Substring(i * 2, 2), 16);
                infoByte[i] = (byte)temp;
            }

            return infoByte;
        }

        //HMAC数据映射检查
        public bool HmacCheck(string s1)
        {
            return true;
        }
    }
}