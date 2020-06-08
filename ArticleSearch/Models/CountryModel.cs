using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArticleSearch.Models
{
    public class CountryModel
    {
        [Display(Name = "Код страны")]
        public string CountryCode { get; set; }

        [Display(Name = "Страна")]
        public string CountryName { get; set; }
    }
}