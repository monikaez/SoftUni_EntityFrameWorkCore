using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHub.Data.Models
{
    public class SongPerformer
    {

        //Songs
        //•	SongId – integer, Primary Key
        
        public int SongId { get; set; }
        [Required]
        [ForeignKey(nameof(SongId))]
        //•	Song – the performer's Song (required)
        public virtual Song Song { get; set; }

        //•	PerformerId – integer, Primary Key
       
        public int PerformerId { get; set; }
        [Required]
        [ForeignKey(nameof(PerformerId))]
        //•	Performer – the Song's Performer (required)
        public virtual Performer Performer { get; set; }

    }
}
