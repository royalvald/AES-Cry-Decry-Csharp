using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SEDemo.SE.Method;

namespace SEDemo.testAlgorithm
{
    class TestFunctions
    {
        delegate void Method(string filePath);
        //没有加密情况下的索引建立过程
        public static Dictionary<string, List<string>> noEncry(string filePath)
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
            /* foreach (var item in rf)
             {
                 string[] agrs = item.Value.ToArray<string>();
                 Console.Write(item.Key + ":");
                 foreach (var items in agrs)
                 {
                     Console.Write(items + " ");
                 }
                 Console.Write("\n");
             }*/
            return rf;
        }

        public static Dictionary<int, List<saveInfo>> basicEncry(string filePath,byte[] key)
        {
            Dictionary<int, List<saveInfo>> dic = new Dictionary<int, List<saveInfo>>();
            if (File.Exists(filePath))
            {
                
                using (FileStream fs = File.Open(filePath, FileMode.Open)) 
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        string s = null;
                        while ((s = sr.ReadLine()) != null)
                        {
                            if (s != "" && s != " ")
                            {
                                string[] args = s.Split(' ');
                                List<byte[]> list = new List<byte[]>();
                                for(int i=1;i<6;i++)
                                {
                                    list.Add(tool.WordToHash());
                                }
                                List<saveInfo> infoList = new List<saveInfo>();
                                foreach (var item in list)
                                {
                                    saveInfo info = new saveInfo(item, tool.HmacHashByte(key, item));
                                    infoList.Add(info);
                                }
                                dic.Add(Convert.ToInt32(args[0]), infoList);
                            }
                        }
                    }
                }
            }
            return dic;
        }

        public static List<string> searchFile(string filePath,int count)
        {
            List<string> list = new List<string>();
            if(File.Exists(filePath))
            {
                string s = null;
                using (FileStream stream = File.Open(filePath, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        while((s=sr.ReadLine())!=null)
                        {
                            if(s!=""&&s!=" ")
                            {
                                string[] args = s.Split(' ');
                                for(int i=1;i<6;i++)
                                {
                                    list.Add(args[i]);
                                    if (list.Count == count) return list;
                                }
                            }
                        }
                    }
                }
            }
            return list;
        }

        public static int noEncryFind(string filePath,int count)
        {
            if(File.Exists(filePath))
            {
                Dictionary<string, List<string>> dic = noEncry(filePath);
                List<string> list = searchFile(filePath,count);
                foreach (var item in list)
                {
                    foreach (var items in dic)
                    {
                        foreach (var element in items.Value)
                        {
                            if (element == item) continue;
                        }
                    }
                }
            }
            return 1;
        }

        public static int basicEncryFind(string filePath,int count1)
        {
            byte[] key = tool.WordToHash();
            if(File.Exists(filePath))
            {
                System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
                stopwatch.Start();
                Dictionary<int, List<saveInfo>> dic = basicEncry(filePath,key);
                List<string> list = searchFile(filePath,count1);
                stopwatch.Stop();
                TimeSpan time = stopwatch.Elapsed;
                Console.WriteLine(time.TotalSeconds);
                Console.WriteLine("查询关键词个数：" + list.Count);
                int count = 0;
                foreach (var item in list)
                {
                    foreach (var items in dic)
                    {
                        foreach (var element in items.Value)
                        {
                            if (BytesCompare(element.result, tool.HmacHashByte(element.random, key))) continue;
                            count++;
                            if (count % 10000000 == 0) Console.WriteLine(count);
                        }
                    }
                }
            }
            return 1;
        }

        public static int basicEncryFindWithInverted(string filePath, int count1)
        {
            byte[] key = tool.WordToHash();
            if (File.Exists(filePath))
            {
                System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
                stopwatch.Start();
                Dictionary<int, List<saveInfo>> dic = basicEncry(filePath, key);
                List<string> list = searchFile(filePath, count1);
                stopwatch.Stop();
                TimeSpan time = stopwatch.Elapsed;
                Console.WriteLine(time.TotalSeconds);
                Console.WriteLine("查询关键词个数：" + list.Count);
                int count = 0;
                List<string> inverted = new List<string>();
                bool tag = false;
                foreach (var item in list)
                {
                    tag = false;
                    foreach (var s in inverted)
                    {
                        if (s == item) tag = true;
                    }
                    if (tag) continue;
                    foreach (var items in dic)
                    {
                        foreach (var element in items.Value)
                        {
                            if (BytesCompare(element.result, tool.HmacHashByte(element.random, key))) continue;
                            count++;
                            if (count % 100000 == 0) Console.WriteLine(count);
                        }
                    }
                }
                //第二次执行
                foreach (var item in list)
                {
                    tag = false;
                    foreach (var s in inverted)
                    {
                        if (s == item) tag = true;
                    }
                    if (tag) continue;
                    foreach (var items in dic)
                    {
                        foreach (var element in items.Value)
                        {
                            if (BytesCompare(element.result, tool.HmacHashByte(element.random, key))) continue;
                            count++;
                            if (count % 100000 == 0) Console.WriteLine(count);
                        }
                    }
                }
            }
            return 1;
        }
        public static bool BytesCompare(byte[]b1,byte[]b2)
        {
            if (b1.Length != b2.Length) return false;
            for (int i = 0; i < b1.Length;i++)
                if (b1[i] != b2[i]) return false;
            return true;
        }
    }
}
