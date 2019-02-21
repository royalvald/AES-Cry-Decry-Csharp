using System.Text;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Security.Cryptography;
using System;

public class AESEncryption
{

    static string strKey = "dongbinhuiasxiny";//密钥,128位，也可以改为192位（24字节）或256位（32字节）。
    /*static void Main(string[] args)
    {
        RijndaelManaged rij = new RijndaelManaged();
        rij.KeySize = 128;//指定密钥长度

        string fp = @"...";//待加密文件
        string sPhysicalFilePath = @"...";//加密后的文件
        string fw = @"...";//解密后的文件
        Console.WriteLine("Encrypting begin...");
        encryption(rij, fp, sPhysicalFilePath);
        decryption(rij, sPhysicalFilePath, fw);

    }*/
    //用于加密的函数
    public static void encryption(RijndaelManaged rij, string readfile, string writefile)
    {
        try
        {
            //byte[] key = rij.Key;
            byte[] key = Encoding.UTF8.GetBytes(strKey);
            byte[] iv = rij.IV;
            byte[] buffer = new byte[4096];
            Rijndael crypt = Rijndael.Create();
            ICryptoTransform transform = crypt.CreateEncryptor(key, iv);
            //写进文件
            FileStream fswrite = new FileStream(writefile, FileMode.Create);
            CryptoStream cs = new CryptoStream(fswrite, transform, CryptoStreamMode.Write);
            //打开文件
            FileStream fsread = new FileStream(readfile, FileMode.Open);

            /*------------------定位要加密的部分-----------------*/
            long _file_size = fsread.Length;
            byte[] _header = new byte[8];
            //定位GUID
            fsread.Seek(16, SeekOrigin.Begin);
            //读取header size
            fsread.Read(_header, 0, _header.Length);
            //头部长度
            long _header_size = (long)BitConverter.ToInt32(_header, 0);
            byte[] _header_buffer = new byte[_header_size];
            fsread.Seek(0, SeekOrigin.Begin);
            fsread.Read(_header_buffer, 0, _header_buffer.Length);
            //头部写入新文件
            fswrite.Write(_header_buffer, 0, _header_buffer.Length);
            //定位到头部，准备读取需要加密的部分
            fsread.Seek(_header_size, SeekOrigin.Begin);
            /*-----------------定位加密部分完成-------------------*/
            int length;
            //while ((length = fsread.ReadByte()) != -1)
            //cs.WriteByte((byte)length);
            while ((length = fsread.Read(buffer, 0, 4096)) > 0)
            {
                cs.Write(buffer, 0, (int)length);
            }

            fsread.Close();
            cs.Close();
            fswrite.Close();
            Console.WriteLine("Encrypt Success");
        }
        catch (Exception e)
        {
            Console.WriteLine("Encrypt Faile" + e.ToString());
        }
    }
    //用于解密的函数
    public static void decryption(RijndaelManaged rij, string readfile, string writefile)
    {
        try
        {
            //byte[] key = rij.Key;
            byte[] key = Encoding.UTF8.GetBytes(strKey);
            byte[] iv = rij.IV;
            byte[] buffer = new byte[4096];
            Rijndael crypt = Rijndael.Create();
            ICryptoTransform transform = crypt.CreateDecryptor(key, iv);
            //读取加密后的文件 
            FileStream fsopen = new FileStream(readfile, FileMode.Open);
            CryptoStream cs = new CryptoStream(fsopen, transform, CryptoStreamMode.Read);
            //把解密后的结果写进文件
            FileStream fswrite = new FileStream(writefile, FileMode.OpenOrCreate);
            /*------------------定位要解密的部分-----------------*/
            long _file_size = fsopen.Length;
            byte[] _header = new byte[8];
            //定位GUID
            fsopen.Seek(16, SeekOrigin.Begin);
            //读取header size
            fsopen.Read(_header, 0, _header.Length);
            //头部长度
            long _header_size = (long)BitConverter.ToInt32(_header, 0);
            byte[] _header_buffer = new byte[_header_size];
            fsopen.Seek(0, SeekOrigin.Begin);
            fsopen.Read(_header_buffer, 0, _header_buffer.Length);
            //头部写入新文件
            fswrite.Write(_header_buffer, 0, _header_buffer.Length);
            //定位到头部，准备读取需要加密的部分
            fsopen.Seek(_header_size, SeekOrigin.Begin);
            /*-----------------定位要解密的部分完成-------------------*/

            int length;
            //while ((length = cs.ReadByte()) != -1)
            //fswrite.WriteByte((byte)length);
            while ((length = cs.Read(buffer, 0, 4096)) > 0)
            {
                fswrite.Write(buffer, 0, (int)length);
            }
            fswrite.Close();
            cs.Close();
            fsopen.Close();
            Console.WriteLine("Decrypt Success");
        }
        catch (Exception e)
        {
            Console.WriteLine("Decrypt Failed" + e.ToString());
        }
    }
}