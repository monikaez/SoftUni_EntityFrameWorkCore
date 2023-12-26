

using Boardgames.Data.Models.Enums;
using Boardgames.Shared;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boardgames.Data.Models;

public class Boardgame
{
  
    [Key]
    public int Id { get; set; }
    //•	Id – integer, Primary Key
    [Required]
    [MaxLength(GlobalConstants.BoardgameNameMaxLength)]
    public string Name { get; set; }
    //•	Name – text with length[10…20] (required)
    [Required]

    public double Rating { get; set; }
    //•	Rating – double in range[1…10.00] (required)
    [Required]
    public int YearPublished { get; set; }
    //•	YearPublished – integer in range[2018…2023] (required)
    [Required]
    public CategoryType CategoryType { get; set; }
    //•	CategoryType – enumeration of type CategoryType, with possible values(Abstract, Children, Family, Party, Strategy) (required)
    [Required]
    public string ?Mechanics { get; set; }
    //•	Mechanics – text(string, not an array) (required)
    [Required]
    [ForeignKey(nameof(CreatorId))]
    public int CreatorId { get; set; }
    //•	CreatorId – integer, foreign key(required)
    public virtual Creator Creator { get; set; }
    //•	Creator – Creator
    public ICollection<BoardgameSeller> BoardgamesSellers { get; set; } = new List<BoardgameSeller>();
    //•	BoardgamesSellers – collection of type BoardgameSeller

}








