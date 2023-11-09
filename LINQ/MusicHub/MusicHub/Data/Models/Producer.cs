using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MusicHub.Data.Models
{
    public class Producer
    {
        //Constructor
        //public Producer()
        //{
        //    Albums = new HashSet<Album>();
        //}
        //Properties
        //•	Id – integer, Primary Key
       [Key]
        public int Id { get; set; }

        //•	Name – text with max length 30 (required)/
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        //•	Pseudonym – text
        public string? Pseudonym { get; set; }

        //•	PhoneNumber – text
        public string? PhoneNumber { get; set; }

        //•	Albums – a collection of type Album
        public virtual ICollection<Album> Albums { get; set; }


    }
}
