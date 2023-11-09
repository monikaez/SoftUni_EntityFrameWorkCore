using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MusicHub.Data.Models
{
    public class Writer
    {
        //Constructor
        //public Writer()
        //{
        //    Songs = new HashSet<Song>();
        //}
        //•	Id – integer, Primary Key
        //Properties
        [Key]
        public int Id { get; set; }

        //•	Name – text with max length 20 (required)
        [Required]
        [MaxLength(20)]
        public string? Name { get; set; }

        //•	Pseudonym – text
        public string? Pseudonym { get; set; }

        //•	Songs – a collection of type Song
        public ICollection<Song>? Songs { get; set; }
    }
}
