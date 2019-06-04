using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDemo.testAlgorithm
{
    class saveInfo
    {
        public byte[] random { set; get; }
        public byte[] result { set; get; }
        public saveInfo(byte[] random,byte[] result)
        {
            this.random = random;
            this.result = result;
        }
    }
}
