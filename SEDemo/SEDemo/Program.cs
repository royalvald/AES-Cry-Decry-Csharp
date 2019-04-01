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
            using (SEContext context = new SEContext())
            {
                Token token = new Token { TokenId = "6541"  };
                context.Token.Add(token);
                context.SaveChanges();
            }
        }
    }
}
