using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#region
using Cry_AES_File;
using Cry_AES_File.Utils;
using Cry_AES_File.PublicCry;
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
        public int DecryptionFile(string filePath,string DesFilePath)
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
            byte[] tokenArray= HmacHash.Sign(k1, wordArray);

            string token = tool.ByteToHex(tokenArray);

            return token;
        }
    }
}
