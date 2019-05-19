using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDemo.SE.Method.PackClass
{
    class PackFileInfo
    {
        //文件标识符
        public string FileID { set; get; }

        //文件关键字映射
        public List<string> tokenList { set; get; }

        //已检索过的token
        public List<string> historyList { set; get; }

    }
}
