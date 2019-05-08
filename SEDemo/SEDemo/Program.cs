using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SEDemo.Models;

namespace SEDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //using (SEContext context = new SEContext())
            //{
            //    Token token = new Token { TokenId = "6541"  };
            //    context.Token.Add(token);
            //    context.SaveChanges();
            //

            var str = DateTime.Now.ToString();
            var encode = Encoding.UTF8;
            var bytes = encode.GetBytes(str);
            StringBuilder ret = new StringBuilder();
            foreach (byte b in bytes)
            {
                //{0:X2} 大写
                ret.AppendFormat("{0:x2}", b);
            }
            var hex = ret.ToString();
            Console.WriteLine(hex);

        }
    }
}
