using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#region
using Cry_AES_File;
using Cry_AES_File.Utils;
using Cry_AES_File.PublicCry;
using SEDemo.SE.Method;
using SEDemo.SE.Method.PackClass;
#endregion
using System.Security.Cryptography;


namespace SEDemo.SE.Method
{
    class SEFun
    {
        //指定key大小
        public enum KeySize { size_128, size_192, size_256 };
        public cryTool.Cry_KeySize symmetry;
        //随机密钥
        public byte[] k1;
        //对称密钥
        public byte[] k2;
        //对称加密的随机向量
        public byte[] k3;
        //哈希表
        Dictionary<string, List<string>> rw;
        Dictionary<string, List<string>> rf;
        //搜索历史集合
        List<string> history;
        //密钥生成
        public int GenKey(KeySize size)
        {
            int keySize = 0;

            switch (size)
            {
                case KeySize.size_128:
                    keySize = 128;
                    symmetry = cryTool.Cry_KeySize.Key_128;
                    break;
                case KeySize.size_192:
                    keySize = 192;
                    symmetry = cryTool.Cry_KeySize.Key_192;
                    break;
                case KeySize.size_256:
                    keySize = 256;
                    symmetry = cryTool.Cry_KeySize.Key_256;
                    break;
                default:
                    keySize = 128;
                    symmetry = cryTool.Cry_KeySize.Key_128;
                    break;
            }

            k1 = new byte[keySize / 8];


            //生成k1密钥
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(k1);
            }

            //生成K2密钥
            cryTool tool = new cryTool(symmetry);
            k2 = tool.GenerateKey();
            k3 = tool.GenerateIV();

            //初始化
            history = new List<string>();

            rw = new Dictionary<string, List<string>>();
            rf = new Dictionary<string, List<string>>();
            return 0;
        }

        //文件加密
        public int EncryptionFile(string filePath, string DesFilePath)
        {
            cryTool tool = new cryTool(symmetry);
            tool.SetKey(k2);
            tool.SetIV(k3);
            tool.EncryFileNoIV(filePath, DesFilePath);
            return 0;
        }

        //文件解密
        public int DecryptionFile(string filePath, string DesFilePath)
        {
            cryTool tool = new cryTool(symmetry);
            tool.SetKey(k2);
            tool.SetIV(k3);
            tool.DecryFileNoIV(filePath, DesFilePath);

            return 0;
        }

        //SearchToken生成
        public string SearchToken(string word)
        {
            byte[] wordArray = Encoding.UTF8.GetBytes(word);
            byte[] tokenArray = HmacHash.Sign(k1, wordArray);

            string token = tool.ByteToHex(tokenArray);
            history.Add(token);

            return token;
        }

        //Search过程
        public List<string> Search(string token)
        {
            if (rw.ContainsKey(token)) return rw[token];
            List<string> searchList = null;
            searchList = new List<string>();
            foreach (var item in rf.Keys)
            {
                foreach (var tip in rf[item])
                {
                    if (tool.HmacCheck(tip))
                    {
                        searchList.Add(tip);
                    }
                }
            }
            return searchList;
        }

        //添加Token
        public PackFileInfo AddToken(List<string> keyWords, string fileID)
        {
            List<byte[]> random = new List<byte[]>();
            byte[] temp = null;
            List<string> C = new List<string>();
            List<string> searched = new List<string>();
            foreach (var item in keyWords)
            {
                temp = tool.WordToHash(item);
                random.Add(temp);
                string s1 = tool.HmacHash(temp,k1);
                if (history.Contains(s1))
                {
                    searched.Add(s1);
                }
                C.Add(tool.HmacAdd(s1));
            }
            //sort比较器实现
            C.Sort();
            PackFileInfo info = new PackFileInfo();
            info.FileID = fileID;
            info.historyList = searched;
            info.tokenList = C;         

            return info;
        }


        public int AddFile(PackFileInfo info, string filePath)
        {
            rf.Add(info.FileID, info.tokenList);
            foreach (var item in info.historyList)
            {
                rw[item].Add(info.FileID);
            }
            tool.FileAdd(filePath);
            return 1;
        }

        public bool DeleteFile(string fileId)
        {
            foreach (string item in rw.Keys)
            {
                if (rw[item].Contains(fileId)) rw[item].Remove(fileId);
            }
            tool.RemoveFile(fileId);
            if (rf.Keys.Contains<string>(fileId)) rf.Remove(fileId);
            return true;
        }

    }
}
