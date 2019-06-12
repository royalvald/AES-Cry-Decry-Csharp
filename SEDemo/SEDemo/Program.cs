using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
#region
using SEDemo.Models;
using SEDemo.test;
using SEDemo.testAlgorithm;
using SEDemo.SE.Method;
using System.Threading;
#endregion
namespace SEDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            // stopwatch.Start();
             //testAlgorithm.TestFunctions.basicEncryFind(@"D:\test1.txt",5000);
             //testAlgorithm.TestFunctions.NewMethod(@"D:\test1.txt", 1000);
             //test.TestCreat.WordListCreat(1000);
             //testAlgorithm.TestFunctions.basicEncryFindWithInverted(@"D:\test1.txt", 1000);
             //stopwatch.Stop();
             //TimeSpan time = stopwatch.Elapsed;
            // Console.WriteLine(time.TotalSeconds);
            
            for (int i = 1; i < 10; i++)
            {
                ThreadInfo info = new ThreadInfo(i * 1000,i, @"D:\test"+i+".txt",@"D:\result" + i + ".txt");
                Thread thread = new Thread(new ParameterizedThreadStart(Test.Start));
                thread.Start(info);
                //testAlgorithm.Test.Start(1000);
                //testAlgorithm.Test.Start(3000);
                //testAlgorithm.Test.Start(5000);
                ///testAlgorithm.Test.Start(7000);
                //testAlgorithm.Test.Start(10000);
                Console.WriteLine("第" + i + 1 + "次运行完毕");
            }
            Console.WriteLine("completed");
            //test.TestCreat.WordListCreat(1000);
        }
    }
}
