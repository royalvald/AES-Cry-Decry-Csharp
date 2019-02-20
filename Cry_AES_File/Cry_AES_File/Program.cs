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
            cryTool tool = new cryTool(cryTool.Cry_KeySize.Key_128);
            tool.EncryFile(@"F://x1.txt");
            tool.DecryFile(@"F://x1.dat");
            Console.ReadLine();
        }
    }
}
