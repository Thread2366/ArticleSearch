using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine
{
    public class Tag
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Tag name is required!")]
        [Index(IsUnique = true), StringLength(450)]
        [RegularExpression(@"^[\w\d- ]+$", 
            ErrorMessage = "Only letters, digits, spaces and '-' character are allowed")]
        public string Name { get; set; }
    }
}
