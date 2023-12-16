

using Cadastre.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cadastre.Data.Models;

public class Property
{
   
    [Key]
    public int Id { get; set; }
    //    •	Id – integer, Primary Key

    [Required]
    [MaxLength(GlobalConstants.PropertyIdentifierMaxLength)]
    public string PropertyIdentifier { get; set; } = null!;
    //•	PropertyIdentifier – text with length[16, 20] (required)

    [Required]
    [Range(GlobalConstants.PropertyAreaMinRange,int.MaxValue)]
    public int Area { get; set; }
    //•	Area – int not negative(required)

    [Required]
    [MaxLength(GlobalConstants.PropertyDetailsMaxLength)]
    public string Details { get; set; } = null!;
    //•	Details - text with length[5, 500] (not required)

    [Required]
    [MaxLength(GlobalConstants.PropertyAddressMaxLength)]
    public string Address { get; set; } = null!;
    //•	Address – text with length[5, 200] (required)

    [Required]
    public DateTime DateOfAcquisition { get; set; }
    //•	DateOfAcquisition – DateTime(required)

    //Distict
    [Required]
     public int DistrictId { get; set; }

    [ForeignKey(nameof(DistrictId))]
    //•	DistrictId – integer, foreign key(required)
    public District District { get; set; } = null!;
    //•	District – District

    public virtual ICollection<PropertyCitizen> PropertiesCitizens { get; set; } = new HashSet<PropertyCitizen>();
    //•	PropertiesCitizens - collection of type PropertyCitizen

}
