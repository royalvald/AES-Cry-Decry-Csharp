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
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();
            testAlgorithm.TestFunctions.basicEncryFind(@"D:\test1.txt",5000);
            //test.TestCreat.WordListCreat(10000);
            stopwatch.Stop();
            TimeSpan time = stopwatch.Elapsed;
            Console.WriteLine(time.TotalSeconds);
           
        }
    }
}
