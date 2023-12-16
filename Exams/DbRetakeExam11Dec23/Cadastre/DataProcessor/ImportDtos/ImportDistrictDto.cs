
using Cadastre.Common;
using Cadastre.Data.Enumerations;
using Cadastre.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Cadastre.DataProcessor.ImportDtos;
//<Districts>
[XmlType("District")]
public class ImportDistrictDto
{
    //	<District Region = "SouthWest" >
    [XmlAttribute("Region")]
    [Required]
    public string Region { get; set; } = null!;
    //•	Region – Region enum (SouthEast = 0, SouthWest, NorthEast, NorthWest) (required)


    //        < Name > Sofia </ Name >
    [XmlElement("Name")]
    [Required]
    [MaxLength(GlobalConstants.DistrictNameMaxLength)]
    [MinLength(GlobalConstants.DistrictNameMinLength)]
    public string Name { get; set; } = null!;
    //•	Name – text with length[2, 80] (required)

    //        < PostalCode > SF - 10000 </ PostalCode >
    [XmlElement("PostalCode")]
    [Required]
    [RegularExpression(GlobalConstants.DistrictPostalCodeRegex)]
    public string PostalCode { get; set; } = null!;
    //•	PostalCode – text with length 8. All postal codes must have the following structure:starting with two capital letters, followed by e dash '-', followed by five digits.Example: SF-10000 (required)


    //        < Properties >
    [XmlArray("Properties")]
    public ImportPropertyDto[] Properties { get; set; } = null!;


}

//            < Property >
[XmlType("Property")]
public class ImportPropertyDto
{
    //                < PropertyIdentifier > SF - 10000.001.001.001</PropertyIdentifier>
    [XmlElement("PropertyIdentifier")]
    [Required]
    [MaxLength(GlobalConstants.PropertyIdentifierMaxLength)]
    [MinLength(GlobalConstants.PropertyIdentifierMinLength)]
    public string PropertyIdentifier { get; set; } = null!;
    //•	PropertyIdentifier – text with length[16, 20] (required)


    //				<Area>71</Area>
    [XmlElement("Area")]
    [Required]
    [Range(GlobalConstants.PropertyAreaMinRange, int.MaxValue)]
    public int Area { get; set; }
    //•	Area – int not negative(required)

    //				<Details>One-bedroom apartment</Details>
    [XmlElement("Details")]
    [Required]
    [MaxLength(GlobalConstants.PropertyDetailsMaxLength)]
    [MinLength(GlobalConstants.PropertyDetailsMinLength)]
    public string Details { get; set; } = null!;
    //•	Details - text with length[5, 500] (not required)

    //				<Address>Apartment 5, 23 Silverado Street, Sofia</Address>
    [XmlElement("Address")]
    [Required]
    [MaxLength(GlobalConstants.PropertyAddressMaxLength)]
    [MinLength(GlobalConstants.PropertyAddressMinLength)]
    public string Address { get; set; } = null!;
    //•	Address – text with length[5, 200] (required)

    //				<DateOfAcquisition>15/03/2022</DateOfAcquisition>
    [XmlElement("DateOfAcquisition")]
    [Required]
    public string DateOfAcquisition { get; set; } = null!;
    //•	DateOfAcquisition – DateTime(required)
    //			</Property>		
}







