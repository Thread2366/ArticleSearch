using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine
{
    public class ArticleTag
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tag is required")]
        [Index("IX_ArticleTag", 0, IsUnique = true)]
        public virtual Tag Tag { get; set; }

        [Required(ErrorMessage = "Article is required")]
        [Index("IX_ArticleTag", 1, IsUnique = true)]
        public virtual Article Article { get; set; }
    }
}
