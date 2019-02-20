using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace Cry_AES_File.Utils
{
    class cryTool
    {
        public enum Cry_KeySize { Key_128, Key_192, Key_256 };

        static AesManaged managed = new AesManaged();

        public cryTool(Cry_KeySize keySize)
        {
            int k = 0;
            switch (keySize)
            {
                case Cry_KeySize.Key_128:
                    k = 128;
                    break;
                case Cry_KeySize.Key_192:
                    k = 192;
                    break;
                case Cry_KeySize.Key_256:
                    k = 256;
                    break;
            }

            if (k != 0)
            {
                managed.KeySize = k;
            }
            else
            {
                managed.KeySize = 128;
            }
            managed.GenerateKey();
            //byte[] IVBlock = managed.IV;
            //byte[] KeyBlock = managed.Key;
        }



        public byte[] InfoCry(string Info)
        {
            byte[] tempInfoBytes = Encoding.UTF8.GetBytes(Info);
            byte[] CryBlock = AESEncryption(managed, tempInfoBytes);

            return CryBlock;
        }

        public string BytesDecry(byte[] bytes)
        {
            byte[] InfoBlock = AESDecryption(managed, bytes);

            return Encoding.UTF8.GetString(InfoBlock);
        }

        //test
        public void CompareKeyChange()
        {
            Console.WriteLine(Encoding.UTF8.GetString(managed.Key));
            managed.KeySize = 137;
            managed.GenerateKey();
            Console.WriteLine(Encoding.UTF8.GetString(managed.Key));
        }

        /// <summary>
        /// 文件加密
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public int EncryFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                //获取文件名和路径
                string fileName = Path.GetFileName(filePath);
                string fileDir = Path.GetDirectoryName(filePath);
                //加密后文件名和文件存放路径
                string Encry_File_Name = fileName.Substring(0, fileName.LastIndexOf(".")) + ".dat";
                string Encry_Full_Name = fileDir + Encry_File_Name;

                FileStream fs = File.Open(filePath, FileMode.Open);

                if (fs.Length < 1024 * 1024)
                {
                    using (FileStream writeStream = File.Create(Encry_Full_Name))
                    {
                        using (CryptoStream crypto = new CryptoStream(writeStream, managed.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            byte[] NameBlock = Encoding.UTF8.GetBytes(fileName);
                            byte[] InfoBlock = new byte[fs.Length + NameBlock.Length + 4];
                            byte[] NameCount = BitConverter.GetBytes(NameBlock.Length);

                            Array.Copy(NameCount, 0, InfoBlock, 0, 4);
                            Array.Copy(NameBlock, 0, InfoBlock, 4, NameBlock.Length);

                            fs.Read(InfoBlock, NameBlock.Length, (int)fs.Length);
                            crypto.Write(InfoBlock, 0, InfoBlock.Length);
                        }
                    }
                }
                else
                {
                    //文件较大只能进行循环读取
                    using (FileStream writeStream = File.Create(Encry_Full_Name))
                    {
                        using (CryptoStream crypto = new CryptoStream(writeStream, managed.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            byte[] NameBlock = Encoding.UTF8.GetBytes(fileName);
                            byte[] InfoBlcok = new byte[1024];
                            byte[] NameCount = BitConverter.GetBytes(NameBlock.Length);

                            crypto.Write(NameCount, 0, NameCount.Length);
                            crypto.Write(NameBlock, 0, NameBlock.Length);

                            int count = 0, size = 0;

                            while (count < fs.Length)
                            {
                                size = fs.Read(InfoBlcok, 0, 1024);
                                crypto.Write(InfoBlcok, 0, size);
                                count += size;
                            }
                        }
                    }
                }

                fs.Close();
                return 0;
            }
            else
            {
                return -1;
            }
        }

        public int DecryFile(string filePath)
        {
            if(File.Exists(filePath))
            {

                return 0;
            }
            else
            {
                return -1;
            }
        }


        /// <summary>
        /// 给定加密实例进行AES加密
        /// </summary>
        /// <param name="sym"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static byte[] AESEncryption(SymmetricAlgorithm sym, byte[] info)
        {
            ICryptoTransform crypto = sym.CreateEncryptor(managed.Key, managed.IV);
            byte[] block = crypto.TransformFinalBlock(info, 0, info.Length);

            return block;
        }

        /// <summary>
        /// 给定加密实例进行解密
        /// </summary>
        /// <param name="sym"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static byte[] AESDecryption(SymmetricAlgorithm sym, byte[] info)
        {
            ICryptoTransform crypto = sym.CreateDecryptor(managed.Key, managed.IV);
            byte[] block = crypto.TransformFinalBlock(info, 0, info.Length);

            return block;
        }


    }
}
