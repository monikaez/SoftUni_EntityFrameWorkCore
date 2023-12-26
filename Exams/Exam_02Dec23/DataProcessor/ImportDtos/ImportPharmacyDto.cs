

using Medicines.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Medicines.DataProcessor.ImportDtos;


[XmlType("Pharmacy")]
public class ImportPharmacyDto
{//	<Pharmacy non-stop="true">      
    [XmlAttribute("non-stop")]
    [Required]
    public string IsNonStop { get; set; } = null!;
    //•	IsNonStop – bool (required)

    //		<Name>Vitality</Name>
    [XmlElement("Name")]
    [Required]
    [MaxLength(50)]
    [MinLength(2)]
    public string Name { get; set; } = null!;
    //•	Name – text with length[2, 50] (required)

    //		<PhoneNumber>(123) 456-7890</PhoneNumber>
    [XmlElement("PhoneNumber")]
    [Required]
    [MaxLength(14)]
    [MinLength(14)]
    [RegularExpression("^\\(\\d{3}\\) \\d{3}-\\d{4}$")]
    public string PhoneNumber { get; set; } = null!;
    //•	PhoneNumber – text with length 14. (required)
    //o   All phone numbers must have the following structure: three digits enclosed in parentheses, followed by a space, three more digits, a hyphen, and four final digits: 
    //	Example -> (123) 456-7890 

    //		<Medicines>
    [XmlArray("Medicines")]
    public ImportMedicineDto[] Medicines { get; set; } = null!;
}
[XmlType("Medicine")]
public class ImportMedicineDto
{
    //			<Medicine category = "1" >
    [XmlAttribute("category")]
    [Required]
    [Range(0, 4)]
    public int Category { get; set; }
    //•	Category – Category enum (Analgesic = 0, Antibiotic, Antiseptic, Sedative, Vaccine) (required)

    //				< Name > Ibuprofen </ Name >
    [XmlElement("Name")]
    [Required]
    [MinLength(3)]
    [MaxLength(150)]
    public string Name { get; set; } = null!;

    //                < Price > 8.50 </ Price >
    [XmlElement("Price")]
    [Required]
    [Range(0.01, 1000.00)]
    public decimal Price { get; set; }
    //•	Price – decimal in range[0.01…1000.00] (required)

    //                < ProductionDate > 2022 - 02 - 10 </ ProductionDate >
    [XmlElement("ProductionDate")]
    [Required]
    public string ProductionDate { get; set; } = null!;
    //•	ProductionDate – DateTime(required)

    //                < ExpiryDate > 2025 - 02 - 10 </ ExpiryDate >
    [XmlElement("ExpiryDate")]
    [Required]
    public string ExpiryDate { get; set; } = null!;

    //                < Producer > ReliefMed Labs</Producer>
    [XmlElement("Producer")]
    [Required]
    [MaxLength(100)]
    [MinLength(3)]
    public string Producer { get; set; } = null!;
    //•	Producer – text with length[3, 100] (required)
    //			</Medicine>

}

















