using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace Cry_AES_File.Utils
{
    public class cryTool
    {
        public enum Cry_KeySize { Key_128, Key_192, Key_256 };

        private AesManaged managed = new AesManaged();

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
            //生成密钥和随机向量
            managed.GenerateKey();
            managed.GenerateIV();
            //byte[] IVBlock = managed.IV;
            //byte[] KeyBlock = managed.Key;
        }


        /// <summary>
        /// 返回加密密钥
        /// </summary>
        /// <returns></returns>
        public byte[] GenerateKey()
        {
            return managed.Key;
        }

        /// <summary>
        /// 返回初始向量
        /// </summary>
        /// <returns></returns>
        public byte[] GenerateIV()
        {
            return managed.IV;
        }

        /// <summary>
        /// 重新生成密钥
        /// </summary>
        /// <param name="keySize"></param>
        /// <returns></returns>
        public byte[] reGenerateKey(Cry_KeySize keySize)
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
            managed.GenerateIV();

            return managed.Key;
        }

        public byte[] reGenerateKey()
        {
            managed.GenerateKey();

            return managed.Key;
        }

        /// <summary>
        /// 重新生成随机向量
        /// </summary>
        /// <returns></returns>
        public byte[] reGenerateIV()
        {
            managed.GenerateIV();

            return managed.IV;
        }
        /// <summary>
        /// 信息的加密与解密
        /// </summary>
        /// <param name="Info"></param>
        /// <returns></returns>
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
                fs.Close();
                return 0;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 文件解密
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public int DecryFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                using (FileStream fs = File.Open(filePath, FileMode.Open))
                {
                    using (CryptoStream crypto = new CryptoStream(fs, managed.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        byte[] NameCount = new byte[4];
                        crypto.Read(NameCount, 0, 4);
                        int NameLength = BitConverter.ToInt32(NameCount, 0);
                        byte[] NameBlock = new byte[NameLength];
                        crypto.Read(NameBlock, 0, NameLength);
                        string fileName = Encoding.UTF8.GetString(NameBlock);

                        byte[] infoBytes = new byte[1024];
                        using (FileStream writeStream = File.Create(@"E://" + fileName))
                        {
                            int count = 4 + NameLength, size = 0;
                            int FileLength = (int)fs.Length;
                            do
                            {
                                size = crypto.Read(infoBytes, 0, 1024);
                                writeStream.Write(infoBytes, 0, size);
                                count += size;
                            } while (size > 0);
                        }
                    }
                }
                return 0;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 将随机向量写入文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public int EncryFileNoIV(string filePath)
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

                //文件较大只能进行循环读取
                using (FileStream writeStream = File.Create(Encry_Full_Name))
                {
                    writeStream.Write(managed.IV, 0, managed.IV.Length);
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
                fs.Close();
                return 0;
            }
            else
            {
                return -1;
            }
        }

        public int DecryFileNoIV(string filePath,byte[] key)
        {
            if (File.Exists(filePath))
            {
                using (FileStream fs = File.Open(filePath, FileMode.Open))
                {
                    byte[] IVArray = new byte[16];
                    fs.Read(IVArray, 0, 16);
                    managed.IV = IVArray;
                    managed.Key = key;
                    using (CryptoStream crypto = new CryptoStream(fs, managed.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        byte[] NameCount = new byte[4];
                        crypto.Read(NameCount, 0, 4);
                        int NameLength = BitConverter.ToInt32(NameCount, 0);
                        byte[] NameBlock = new byte[NameLength];
                        crypto.Read(NameBlock, 0, NameLength);
                        string fileName = Encoding.UTF8.GetString(NameBlock);

                        byte[] infoBytes = new byte[1024];
                        using (FileStream writeStream = File.Create(@"E://" + fileName))
                        {
                            int count = 4 + NameLength, size = 0;
                            int FileLength = (int)fs.Length;
                            do
                            {
                                size = crypto.Read(infoBytes, 0, 1024);
                                writeStream.Write(infoBytes, 0, size);
                                count += size;
                            } while (size > 0);
                        }
                    }
                }
                return 0;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 给定密钥加密文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="key"></param>
        /// <param name="IV"></param>
        /// <returns></returns>
        public int EncryFile(string filePath, byte[] key, byte[] IV)
        {
            if (File.Exists(filePath))
            {
                managed.Key = key;
                managed.IV = IV;
                EncryFile(filePath);
                return 0;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 给定密钥进行解密
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="key"></param>
        /// <param name="IV"></param>
        /// <returns></returns>
        public int DecryFile(string filePath, byte[] key, byte[] IV)
        {
            if (File.Exists(filePath))
            {
                managed.Key = key;
                managed.IV = IV;
                DecryFile(filePath);
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
        public  byte[] AESEncryption(SymmetricAlgorithm sym, byte[] info)
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
        public  byte[] AESDecryption(SymmetricAlgorithm sym, byte[] info)
        {
            ICryptoTransform crypto = sym.CreateDecryptor(managed.Key, managed.IV);
            byte[] block = crypto.TransformFinalBlock(info, 0, info.Length);

            return block;
        }


    }
}
