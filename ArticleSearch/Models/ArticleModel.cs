using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SearchEngine;

namespace ArticleSearch.Models
{
    public class ArticleModel
    {
        public int Id { get; set; }

        [Display(Name = "Заголовок")]
        public string Title { get; set; }

        [Display(Name = "Аннотация")]
        public string Abstract { get; set; }

        public string AbstractPreview
        {
            get => Abstract.Length > 30 ? Abstract.Substring(0, 30) + "..." : Abstract;
        }

        [Display(Name = "Текст")]
        public string Text { get; set; }

        public string TextPreview
        {
            get => Text.Length > 30 ? Text.Substring(0, 30) + "..." : Text;
        }

        [Display(Name = "Авторы")]
        public string AuthorsNames { get; set; }

        [Display(Name = "Ключевые слова")]
        public string Keywords { get; set; }
    }
}