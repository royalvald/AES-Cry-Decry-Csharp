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
        static void Main(string[] args)
        {
            cryTool tool = new cryTool(128);
            /*byte[] cryBlock= tool.InfoCry("hello world");
            Console.WriteLine(Encoding.UTF8.GetString(cryBlock));

            string s1 = tool.BytesDecry(cryBlock);
            Console.WriteLine(s1);*/
            tool.CompareKeyChange();

            Console.ReadLine();
        }
    }
}
