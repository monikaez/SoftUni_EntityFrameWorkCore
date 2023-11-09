using Castle.Components.DictionaryAdapter;
using MusicHub.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace MusicHub.Data.Models
{

    public class Album
    {
        //public Album()
        //{
        //    Songs = new HashSet<Song>();
        //}
        //Id – integer, Primary Key
        [Key]
        public int Id { get; set; }

        //•	Name – text with max length 40 (required)
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }

        //•	ReleaseDate – date(required)
        [Required]
        public DateTime ReleaseDate { get; set; }

        //•	Price – calculated property(the sum of all song prices in the album)
        public decimal Price => Songs.Sum(s => s.Price);

        //•	ProducerId – integer, foreign key
        [ForeignKey(nameof(ProducerId))]
        public int? ProducerId { get; set; }

        //•	Producer – the Album's Producer
        public virtual Producer? Producer { get; set; }

        //•	Songs – a collection of all Songs in the Album
        public  ICollection<Song> Songs { get; set; }
    }
}
