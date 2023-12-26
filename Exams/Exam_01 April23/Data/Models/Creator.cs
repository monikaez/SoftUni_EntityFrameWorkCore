
using Boardgames.Shared;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace Boardgames.Data.Models;

public class Creator
{
    [Key]
    public int Id { get; set; }
    //•	Id – integer, Primary Key
    [Required]
    [MaxLength(GlobalConstants.CreatorNameMaxLength)]
    public string ?FirstName { get; set; }
    //•	FirstName – text with length[2, 7] (required) 
    [Required]
    [MaxLength(GlobalConstants.CreatorNameMaxLength)]
    public string ?LastName { get; set; }
    //•	LastName – text with length[2, 7] (required)
    public virtual ICollection<Boardgame> ?Boardgames { get; set; }

    //•	Boardgames – collection of type Boardgame


}




