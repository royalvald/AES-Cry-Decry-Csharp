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

        [StringLength(50)]
        public string TokenId { set; get; }

        public string FileId { set; get; }
    }
}
