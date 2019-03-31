using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SEDemo.Models
{
    class Token_File
    {
        [Key]
        public int Token_FileId { set; get; }

        public int TokenId { set; get; }

        public string FileIdList { set; get; }
    }
}
