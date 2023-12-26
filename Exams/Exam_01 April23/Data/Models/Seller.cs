

using Boardgames.Shared;
using System.ComponentModel.DataAnnotations;

namespace Boardgames.Data.Models;

public class Seller
{
    public Seller()
    {
        BoardgamesSellers = new List<BoardgameSeller>();
    }

    [Key]
    public int Id { get; set; }
    //•	Id – integer, Primary Key
    [Required]
    [MaxLength(GlobalConstants.SellerNameMaxLength)]
    public string Name { get; set; }
    //•	Name – text with length[5…20] (required)
    [Required]
    [MaxLength(GlobalConstants.SellerAddressMaxLength)]
    public string ?Address { get; set; }
    //•	Address – text with length[2…30] (required)
    [Required]
    public string ?Country { get; set; }
    [Required]
    //•	Country – text(required)
    public string ?Website { get; set; }
    //•	Website – a string (required). First four characters are "www.", followed by upper and lower letters, digits or '-' and the last three characters are ".com".

    public ICollection<BoardgameSeller> BoardgamesSellers { get; set; }

    //•	BoardgamesSellers – collection of type BoardgameSeller

}





