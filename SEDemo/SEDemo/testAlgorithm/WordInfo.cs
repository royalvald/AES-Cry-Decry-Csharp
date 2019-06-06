using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDemo.testAlgorithm
{
    class WordInfo
    {
        public string word { set; get; }
        public byte[] bytes { set; get; }
        public WordInfo(string word,byte[] bytes)
        {
            this.word = word;
            this.bytes = bytes;
        }
    }
}
