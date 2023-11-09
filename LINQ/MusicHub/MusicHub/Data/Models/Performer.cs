using Castle.Components.DictionaryAdapter;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace MusicHub.Data.Models
{

    public class Performer
    {//Constructor
        //public Performer()
        //{
        //    PerformerSongs = new HashSet<SongPerformer>();
        //}

        //Properties
        //•Id – integer, Primary Key
        [Key]
        public int Id { get; set; }
        //•	FirstName – text with max length 20 (required) 
        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }

        //•	LastName – text with max length 20 (required) 
        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }

        //•	Age – integer(required)
        [Required]
        public int Age { get; set; }

        //•	NetWorth – decimal (required)
        [Required]
        public decimal NetWorth { get; set; }

        //•	PerformerSongs – a collection of type SongPerformer
        public virtual ICollection<SongPerformer> PerformerSongs { get; set; }




    }
}
