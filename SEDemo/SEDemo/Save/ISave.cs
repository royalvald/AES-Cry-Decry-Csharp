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
        string CreatRecord(string filePath);
    }
}
