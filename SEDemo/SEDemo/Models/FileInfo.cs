using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SEDemo.Models
{
    class FileInfo
    {
        [Key]
        public string FileInfoId { set; get; }

        [StringLength(50)]
        public string FileName { set; get; }

        public decimal FileSize { set; get; }

        [StringLength(50)]
        public string LastModifyDate { set; get; }
    }
}
