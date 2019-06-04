using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace SEDemo.testAlgorithm
{
    class TestFunctions
    {
        delegate void Method(string filePath);
        //没有加密情况下的索引建立过程
        public static void noEncry(string filePath)
        {
            Dictionary<string, List<string>> rf = new Dictionary<string, List<string>>();
            string s = null;

            if(File.Exists(filePath))
            {
                using (FileStream stream = File.Open(filePath, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        while((s=sr.ReadLine())!=null)
                        {
                            if (s != "" && s != " ")
                            {
                                string[] args = s.Split(' ');
                                List<string> list = new List<string>();
                                for (int i = 1; i < 6; i++)
                                    list.Add(args[i]);
                                rf.Add(args[0], list);
                            }
                        }
                    }
                }
            }
            foreach (var item in rf)
            {
                string[] agrs = item.Value.ToArray<string>();
                Console.Write(item.Key + ":");
                foreach (var items in agrs)
                {
                    Console.Write(items + " ");
                }
                Console.Write("\n");
            }
        }

        public static void basicEncry(string filePath)
        {
            if(File.Exists(filePath))
            {

            }
        }
    }
}
