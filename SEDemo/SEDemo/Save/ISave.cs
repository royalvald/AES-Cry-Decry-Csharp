using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDemo.Save
{

    public interface ISave
    {
        //获取所有驱动器存储空间
        Dictionary<string,long> GetDriverCapacity();

        //获取指定驱动器剩余存储空间
        long GetDriverCapacity(string driverName);

        //传入文件大小获取文件存储地址
        string GetSavePathByFileSize(long fileSize);

        //存储文件
        int SaveFile(string filePath);

        //接下来需要根据文件版本控制提供相应接口
        /// <summary>
        /// 生成版本信息
        /// </summary>
        /// <param name="filePath">传入文件xml记录</param>
        /// <returns>返回文件版本（xml记录）地址/识别码</returns>
        string CreatRecord(string filePath);

        /// <summary>
        /// 回溯版本
        /// </summary>
        /// <param name="filePath">传入文件识别码</param>
        /// <returns>返回识别码</returns>
        string GetRecord(string filePath);
    }
}
