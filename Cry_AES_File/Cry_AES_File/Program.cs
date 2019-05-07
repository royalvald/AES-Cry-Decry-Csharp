using Cry_AES_File.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cry_AES_File
{
    class Program
    {
        public static void Main(string[] args)
        {
            cryTool tool = new cryTool(cryTool.Cry_KeySize.Key_192);
            byte[] IVArray = tool.GenerateIV();
            byte[] keyArray = tool.GenerateKey();
            Console.WriteLine("IV长度为" + IVArray.Count<byte>());
            Console.WriteLine("Key长度为" + keyArray.Count<byte>());

        }
    }
}
