using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine
{
    public class Article
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Article title is required!")]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Abstract is required!")]
        public string Abstract { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Text is required!")]
        public string Text { get; set; }


        public virtual ICollection<Authorship> Authorships { get; set; }
        public virtual ICollection<ArticleTag> ArticleTags { get; set; }
    }
}
