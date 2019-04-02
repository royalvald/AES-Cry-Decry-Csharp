using SEDemo.Interface;
using SEDemo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDemo
{
    class Service
    {
        public List<string> searchFile(string tokenString)
        {
            //存储文件ID信息的list
            List<string> fileList = null;

            using (SEContext context = new SEContext())
            {
                var IsTokenExisted = from token in context.Token
                                     where token.TokenId == tokenString
                                     select token;
                if (IsTokenExisted != null)
                {
                    var fileListInfo = from file in context.Token_File
                                       where file.TokenId == tokenString
                                       select file;

                    foreach (var item in fileListInfo)
                    {
                        fileList.Add(item.FileId);
                    }
                }
            }

            return fileList;
        }

        //保存文件信息
        public int SaveFileId(FileInfo fileInfo)
        {
            using (SEContext context = new SEContext())
            {
                string fileId = fileInfo.FileInfoId;
                var info = from file in context.FileInfo
                           where file.FileInfoId == fileId
                           select file;
                if (info != null)
                    return -1;
                else
                {
                    context.FileInfo.Add(fileInfo);
                }
                context.SaveChanges();
            }
            return 0;

        }

        //存储token和fileId对应信息
        public int SaveToken(string tokenString, FileInfo file)
        {
            using (SEContext context = new SEContext())
            {
                var IsTokenExisted = from token in context.Token
                                     where token.TokenId == tokenString
                                     select token;
                if (IsTokenExisted == null)
                {
                    context.Token.Add(
                        new Token { TokenId = tokenString });
                    context.SaveChanges();
                   
                }

                var IsFileExisted = from fileInfo in context.FileInfo
                                    where fileInfo.FileInfoId == file.FileInfoId
                                    select fileInfo;
                if(IsFileExisted==null)
                {
                    context.FileInfo.Add(file);
                    context.SaveChanges();
                }

                var IsRecordExisted = from item in context.Token_File
                                      where item.TokenId == tokenString
                                      where item.FileId == file.FileInfoId
                                      select item;
                if(IsRecordExisted==null)
                {
                    context.Token_File.Add(
                        new Token_File
                        {
                            FileId = file.FileInfoId,
                            TokenId = tokenString
                        }
                        );
                    context.SaveChanges();
                }

            }
            return 0;
        }
    }
}
