using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SEDemo.test
{
    class TestCreat
    {
        public static int WordListCreat(int num)
        {
            char[] testCharArray = {'a','b', 'c' , 'd' , 'e' , 'f' , 'g' , 'h' , 'i' , 'j' , 'k' ,
            'l','m','n','o','p','q','r','s','t','u','v','w','x','y','z','A','B','C','D','E',
            'F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y',
            'Z'};
           // Console.WriteLine(testCharArray.Length);
            List<int> list = new List<int>();
            List<int> numList = new List<int>();
            char[] word = null;
            Random random = new Random(System.DateTime.Now.Millisecond);
            //单词长度随机
            for (int i = 0; i < num * 5; i++)
                list.Add(random.Next(1, 11));
            int sum = list.Sum();
            //生成字符
            for (int i = 0; i < sum; i++)
                numList.Add(random.Next(1, 53));
            using (FileStream stream = File.Create(@"D:\test.txt"))
            {
                using (StreamWriter sw = new StreamWriter(stream))
                {
                    foreach (var item in numList)
                    {
                        sw.WriteLine(item);
                    }
                }
            }
            int count = 0;
            int tag = 0;
            using (StreamWriter sw = new StreamWriter(File.Create(@"D:\test1.txt")))
            {

                foreach (var item in list)
                {
                    if (tag % 5 == 0)
                    {
                        sw.Write("\r\n");
                        sw.Write(count);
                    }
                    tag++;
                    word = new char[item];
                    for (int i = 0; i < item; i++)
                        word[i] = testCharArray[numList[count++] - 1];
                    sw.Write(" ");
                    sw.Write(word);
                }
            }
            return 0;
        }

         
        
    //public static int randomNum()

}
}
