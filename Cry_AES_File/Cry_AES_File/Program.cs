using Cry_AES_File.PublicCry;
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
            //cryTool tool = new cryTool(cryTool.Cry_KeySize.Key_128);
            RSA tool = new RSA(8192);
            tool.EncryptFile(@"D://x1.pdf");
            tool.DecryptFile(@"D://x1.dat");
            Console.WriteLine("finshed");
            Console.ReadLine();
        }
    }
}
