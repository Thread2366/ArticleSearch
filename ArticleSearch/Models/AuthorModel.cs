using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArticleSearch.Models
{
    public class AuthorModel
    {
        public int Id { get; set; }
        
        [Display(Name = "Автор")]
        public string Name { get; set; }

        [Display(Name = "Биография")]
        public string Bio { get; set; }

        public string BioPreview
        {
            get => Bio.Length > 30 ? Bio.Substring(0, 30) + "..." : Bio;
        }

        [Display(Name = "Страна")]
        public string Country { get; set; }
    }
}