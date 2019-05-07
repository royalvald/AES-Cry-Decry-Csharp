using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Cry_AES_File.PublicCry
{
    public class RSA
    {
        private int RsaSize = 0;
        private int DecryptionSize = 0;
        private int EncryptionSize = 0;

        public RSAParameters PublicKey { private set; get; }
        private RSAParameters SecrectKey { set; get; }

        RSACryptoServiceProvider rsa;
        public RSA(int keySize = 400)
        {

            rsa = new RSACryptoServiceProvider(keySize);
            this.RsaSize = keySize;
            this.SecrectKey = rsa.ExportParameters(true);
            this.PublicKey = rsa.ExportParameters(false);
            this.DecryptionSize = keySize / 8;
            this.EncryptionSize = keySize / 8 - 11;
        }

        public byte[] GetPublicKey()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            formatter.Serialize(stream, this.PublicKey);
            byte[] objectBytes = new byte[stream.Length];
            stream.Read(objectBytes, 0, (int)stream.Length);
            stream.Close();

            return objectBytes;
        }

        public byte[] GetSecrectKey()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            formatter.Serialize(stream, this.SecrectKey);
            byte[] objectBytes = new byte[stream.Length];
            stream.Read(objectBytes,0,)
        }

        public byte[] EncryptInfo(string Info)
        {
            byte[] InfoBlock = Encoding.UTF8.GetBytes(Info);
            byte[] EncryptBlcok = rsa.Encrypt(InfoBlock, false);

            return EncryptBlcok;
        }

        public string DecryptInfo(byte[] ToEncryptBlock)
        {
            string Info = null;
            byte[] InfoBlock = rsa.Decrypt(ToEncryptBlock, false);
            Info = Encoding.UTF8.GetString(InfoBlock);

            return Info;
        }

        /// <summary>
        /// 加密信息
        /// </summary>
        /// <param name="Info"></param>
        /// <param name="parameters"></param>
        /// <param name="fOAEP"></param>
        /// <returns></returns>
        public byte[] EncryptInfo(string Info, RSAParameters parameters, bool fOAEP)
        {
            byte[] EncryptBlock = null;
            try
            {
                rsa.ImportParameters(parameters);
                EncryptBlock = rsa.Encrypt(Encoding.UTF8.GetBytes(Info), fOAEP);
            }
            catch (Exception e)
            {
                EncryptBlock = null;
            }
            return EncryptBlock;
        }

        /// <summary>
        /// 解密信息
        /// </summary>
        /// <param name="EncrypyBlock"></param>
        /// <param name="parameters"></param>
        /// <param name="fOAEP"></param>
        /// <returns></returns>
        public string DecryptInfo(byte[] EncrypyBlock, RSAParameters parameters, bool fOAEP)
        {
            string Info = null;
            try
            {
                rsa.ImportParameters(parameters);
                byte[] infoBlcok = rsa.Decrypt(EncrypyBlock, fOAEP);
                Info = Encoding.UTF8.GetString(infoBlcok);
            }
            catch (Exception e)
            {
                Info = null;
                return Info;
            }
            return Info;
        }

        public byte[] Encrypt(byte[] ToEncryptBlock, bool fOAEP = false)
        {
            return rsa.Encrypt(ToEncryptBlock, fOAEP);
        }

        public byte[] Decrypt(byte[] EncryptBlock, bool fOAEP = false)
        {
            return rsa.Decrypt(EncryptBlock, fOAEP);
        }

        public byte[] Encrypt(byte[] ToEncryptBlock, int offset, int count, bool fOAEP = false)
        {
            byte[] bytes = new byte[count];
            Array.Copy(ToEncryptBlock, offset, bytes, 0, count);
            return rsa.Encrypt(bytes, fOAEP);
        }

        public byte[] Decrypt(byte[] EncryptBlock, int offset, int count, bool fOAEP = false)
        {
            byte[] bytes = new byte[count];
            Array.Copy(EncryptBlock, offset, bytes, 0, count);

            return rsa.Decrypt(bytes, fOAEP);
        }

        public int EncryptFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                using (FileStream fs = File.Open(filePath, FileMode.Open))
                {
                    string fileDir = Path.GetDirectoryName(filePath);
                    string fileName = Path.GetFileName(filePath);
                    string saveName = fileDir + fileName.Substring(0, fileName.LastIndexOf(".")) + ".dat";

                    byte[] NameBlock = Encoding.UTF8.GetBytes(fileName);
                    byte[] NameLength = BitConverter.GetBytes(NameBlock.Length);

                    rsa = new RSACryptoServiceProvider(RsaSize);
                    rsa.ImportParameters(this.PublicKey);

                    byte[] readBlock = new byte[EncryptionSize];
                    int size = 0, count = 0;
                    byte[] writeBlcok;
                    using (FileStream writeStream = File.Create(saveName))
                    {
                        writeStream.Write(NameLength, 0, 4);
                        writeStream.Write(NameBlock, 0, NameBlock.Length);
                        while (count < fs.Length)
                        {
                            size = fs.Read(readBlock, 0, EncryptionSize);
                            writeBlcok = Encrypt(readBlock, 0, size, false);
                            writeStream.Write(writeBlcok, 0, writeBlcok.Length);
                            count += size;
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

        public int DecryptFile(string filePath)
        {
            if(File.Exists(filePath))
            {
                using (FileStream fs = File.Open(filePath,FileMode.Open))
                {
                    //获取文件名
                    byte[] NameLength = new byte[4];
                    fs.Read(NameLength, 0, 4);
                    int Length = BitConverter.ToInt32(NameLength, 0);
                    byte[] NameBlock = new byte[Length];
                    fs.Read(NameBlock, 0, Length);
                    string fileName = Encoding.UTF8.GetString(NameBlock);
                    string fileDir = Path.GetDirectoryName(filePath);

                    rsa = new RSACryptoServiceProvider(RsaSize);
                    rsa.ImportParameters(this.SecrectKey);


                    //解密文件
                    byte[] readBlock = new byte[DecryptionSize];
                    byte[] writeBlock;
                    int count = 0, size = 0;
                    using (FileStream writeStream = File.Create(@"D://" + fileName))
                    {
                        while (count < fs.Length - 4 - Length)
                        {
                            size = fs.Read(readBlock, 0, DecryptionSize);
                            writeBlock = Decrypt(readBlock, 0, size, false);
                            writeStream.Write(writeBlock, 0, writeBlock.Length);
                            count += size;
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

        public int EncryptFile(string filePath, RSAParameters parameters)
        {
            return 0;
        }

        public int DecryptFile(string filePath, RSAParameters parameters)
        {
            return 0;
        }

        public int SetKeySize(int size)
        {
            this.RsaSize = size;
            rsa = new RSACryptoServiceProvider(size);

            this.DecryptionSize = size / 8;
            this.EncryptionSize = size / 8 - 11;
            return 0;
        }
    }
}
