using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SEDemo.Models
{
    class Token
    {
        [Key]
        public string TokenId { set; get; }
        
       // [StringLength(50)]
       // public string TokenString { set; get; }
    }
}
