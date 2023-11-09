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
    public class Song
    {
        //Constructor
        //public Song()
        //{
        //    SongPerformers = new HashSet<SongPerformer>();
        //}
        //•Id – integer, Primary Key
        [Key]
        public int Id { get; set; }

        //•	Name – text with max length 20 (required)
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        //•	Duration – TimeSpan(required)
        [Required]
        public TimeSpan Duration { get; set; }

        //•	CreatedOn – date(required)
        [Required]
        public DateTime CreatedOn { get; set; }

        //•	Genre ¬– genre enumeration with possible values: "Blues, Rap, PopMusic, Rock, Jazz" (required)
        [Required]
        public Genre Genre { get; set; }

        //•	Price – decimal (required)
        [Required]
        public decimal Price { get; set; }

        //Album
        //•	AlbumId – integer, Foreign key
       
        public int? AlbumId { get; set; }

        //•	Album – the Song's Album
        [ForeignKey(nameof(AlbumId))]
        public  Album Album { get; set; }


        //Writer
        //•	WriterId – integer, Foreign key(required)
        [Required]
        public int WriterId { get; set; }

        //•	Writer – the Song's Writer
              
        [ForeignKey(nameof(WriterId))]
        public  Writer  Writer { get; set; }

        //•	SongPerformers – a collection of type SongPerformer
        public ICollection<SongPerformer> SongPerformers { get; set; }




    }
}
