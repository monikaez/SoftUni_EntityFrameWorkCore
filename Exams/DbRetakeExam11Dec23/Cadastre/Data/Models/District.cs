
using Cadastre.Common;
using Cadastre.Data.Enumerations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace Cadastre.Data.Models;

public class District
{
    [Key]
    public int Id { get; set; }
    //    •	Id – integer, Primary Key

    [Required]
    [MaxLength(GlobalConstants.DistrictNameMaxLength)]
    public string Name { get; set; } = null!;
    //•	Name – text with length[2, 80] (required)

    [Required]
    public string PostalCode { get; set; } = null!;
    //•	PostalCode – text with length 8. All postal codes must have the following structure:starting with two capital letters, followed by e dash '-', followed by five digits.Example: SF-10000 (required)

    [Required]
    public Region Region { get; set; }
    //•	Region – Region enum (SouthEast = 0, SouthWest, NorthEast, NorthWest) (required)
    public virtual ICollection<Property> Properties { get; set; }

    public District()
    {
        Properties = new HashSet<Property>();
    }

    //•	Properties - collection of type Property

}
