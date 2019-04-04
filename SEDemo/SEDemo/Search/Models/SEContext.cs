using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace SEDemo.Models
{
    class SEContext:DbContext
    {
        public SEContext() : base("name=SEContext") { }

        public DbSet<FileInfo> FileInfo { set; get; }

        public DbSet<Token_File> Token_File { set; get; }

        public DbSet<Token> Token { set; get; }
    }
}
