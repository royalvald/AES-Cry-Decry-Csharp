using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDemo.testAlgorithm
{
    class Test
    {
        public static void Start(int num)
        {
            int[] testSize = new int[3];
            int temp;
            testSize[0] = 500;
            testSize[1] = 1000;
            testSize[2] = 3000;
            //testSize[3] = 5000;
            //testSize[4] = 7000;
            //testSize[5] = 10000;
            string filePath = @"D:\test1.txt";
            using (StreamWriter sr = new StreamWriter(File.Create(@"D:\result.txt")))
            {
                foreach (var item in testSize)
                {
                    System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
                    stopwatch.Start();
                    TestFunctions.noEncryFind(filePath, item);
                    stopwatch.Stop();
                    TimeSpan time = stopwatch.Elapsed;
                    Console.WriteLine(time.TotalSeconds);
                    sr.WriteLine("没有加密情况下，在数据规模为"+num+",关键字字数为"+item+"时，时间消耗为"+time.TotalSeconds);
                    Console.WriteLine("没有加密情况下，在数据规模为" + num + ",关键字字数为" + item + "时，时间消耗为" + time.TotalSeconds);

                    System.Diagnostics.Stopwatch stopwatch1 = new System.Diagnostics.Stopwatch();
                    stopwatch1.Start();
                    TestFunctions.basicEncryFind(filePath, item);
                    stopwatch1.Stop();
                    TimeSpan time1 = stopwatch1.Elapsed;
                    Console.WriteLine(time1.TotalSeconds);
                    sr.WriteLine("原始加密情况下，在数据规模为" + num + ",关键字字数为" + item + "时，时间消耗为" + time1.TotalSeconds);
                    Console.WriteLine("原始加密情况下，在数据规模为" + num + ",关键字字数为" + item + "时，时间消耗为" + time1.TotalSeconds);

                    System.Diagnostics.Stopwatch stopwatch2 = new System.Diagnostics.Stopwatch();
                    stopwatch2.Start();
                    TestFunctions.basicEncryFindWithInverted(filePath, item);
                    stopwatch2.Stop();
                    TimeSpan time2 = stopwatch2.Elapsed;
                    Console.WriteLine(time2.TotalSeconds);
                    sr.WriteLine("反向索引情况下，在数据规模为" + num + ",关键字字数为" + item + "时，时间消耗为" + time2.TotalSeconds);
                    Console.WriteLine("反向索引情况下，在数据规模为" + num + ",关键字字数为" + item + "时，时间消耗为" + time2.TotalSeconds);
                    
                    System.Diagnostics.Stopwatch stopwatch3 = new System.Diagnostics.Stopwatch();
                    stopwatch3.Start();
                    TestFunctions.NewMethod(filePath, item);
                    stopwatch3.Stop();
                    TimeSpan time3 = stopwatch3.Elapsed;
                    Console.WriteLine(time3.TotalSeconds);
                    sr.WriteLine("基于区域属性的新方法，在数据规模为" + num + ",关键字字数为" + item + "时，时间消耗为" + time3.TotalSeconds);
                    Console.WriteLine("基于区域属性的新方法，在数据规模为" + num + ",关键字字数为" + item + "时，时间消耗为" + time3.TotalSeconds);
                }
            }
        }
    }
}
