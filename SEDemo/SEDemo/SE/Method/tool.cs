using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
#region
using Cry_AES_File.Utils;
using Cry_AES_File.PublicCry;
#endregion

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

        //HMAC数据映射检查格式为“H(s)||s”
        public static bool HmacCheck(string s1)
        {
            return true;
        }

        /// <summary>
        /// 字符串映射为强随机值
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static byte[] WordToHash(string s)
        {
            byte[] temp = new byte[64];
            
            return temp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string HmacHash(string s,byte[] key)
        {
            byte[] stringBytes = Encoding.UTF8.GetBytes(s);
            HmacHash(stringBytes, key);
                
            return " ";
        }

        /// <summary>
        /// token生成
        /// </summary>
        /// <param name="s"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string HmacHash(byte[] s,byte[] key)
        {
            byte[] temp= Cry_AES_File.HmacHash.Sign(key, s);
            return ByteToHex(temp);
        }

        /// <summary>
        /// 将s映射为H(s)，输出H(s)||s
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string HmacAdd(string s)
        {
           
            return " ";
        }

        /// <summary>
        /// 根据文件路径添加文件到系统
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static int FileAdd(string filePath)
        {
            return 1;
        }

        /// <summary>
        /// 根据文件ID删除文件
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        public static int RemoveFile(string fileId)
        {
            return 1;
        }
    }
}