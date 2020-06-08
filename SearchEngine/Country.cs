using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine
{
    public class Country
    {
        [Key]
        [JsonProperty("Code")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Country code is required")]
        public string CountryCode { get; set; }

        [JsonProperty("Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Country name is required")]
        [Index(IsUnique = true), StringLength(450)]
        public string CountryName { get; set; }
    }
}
