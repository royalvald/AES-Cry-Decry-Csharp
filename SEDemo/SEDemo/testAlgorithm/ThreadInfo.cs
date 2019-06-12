using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDemo.testAlgorithm
{
    class ThreadInfo
    {
        public int Num { set; get; }
        public string filePath { set; get; }
        public string savePath { set; get; }
        public int Tag { set; get; }
        public ThreadInfo(int Num, int tag,string filePath,string savePath)
        {
            this.Num = Num;
            this.Tag = tag;
            this.filePath = filePath;
            this.savePath = savePath;
        }
    }
}
