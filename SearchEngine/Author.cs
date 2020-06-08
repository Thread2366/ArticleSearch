using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace SearchEngine
{
    public class Author
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Author name is requred!")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = true, ErrorMessage = "Bio is required! Use empty string for empty bio")]
        public string Bio { get; set; }

        public virtual Country Country { get; set; }
    }
}
