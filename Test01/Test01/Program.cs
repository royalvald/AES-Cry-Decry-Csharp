using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using edu.pku;

namespace Test01
{
    class Program
    {
        static void Main(string[] args)
        {
            //Pair<BigInteger> test = new Pair<BigInteger>();
            Util test = new Util();
            string s = "hello world";
            BigInteger integer = test.StringToBigInteger(s);
            //Console.WriteLine(integer);
            string temp = test.IntegerToBiginteger(integer);
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            //Console.WriteLine(temp);
            
            Accumulator accu1 = new Accumulator();
            java.math.BigInteger mem = new EnhancedRandom().nextBigInteger();
            java.math.BigInteger a1=accu1.add(mem);
            java.math.BigInteger witness1 = accu1.proveMembership(mem);
            java.math.BigInteger nonce1 = accu1.getNonce(mem);
            java.math.BigInteger n1 = accu1.getN();
            stopwatch.Start();
            
            
            bool tag;
            for (int i = 0; i < 1000; i++)
            {
                 tag = Accumulator.verifyMembership(a1, mem, nonce1, witness1, n1);
            }
            stopwatch.Stop();
            TimeSpan span = stopwatch.Elapsed;
            Console.WriteLine(span.Milliseconds);
            Console.WriteLine("completed");
        }
    }
}
