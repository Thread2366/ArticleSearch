using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine
{
    public class Authorship
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Author is required")]
        [Index("IX_Authorship", 0, IsUnique = true)]
        public virtual Author Author { get; set; }

        [Required(ErrorMessage = "Article is required!")]
        [Index("IX_Authorship", 1, IsUnique = true)]
        public virtual Article Article { get; set; }
    }
}
