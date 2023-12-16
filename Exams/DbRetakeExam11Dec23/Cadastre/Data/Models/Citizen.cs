using Cadastre.Common;
using Cadastre.Data.Enumerations;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace Cadastre.Data.Models;

public class Citizen
{
    [Key]
    public int Id { get; set; }
    //    •	Id – integer, Primary Key

    [Required]
    [MaxLength(GlobalConstants.CitizenFirstNameMaxLength)]
    public string FirstName { get; set; } = null!;
    //•	FirstName – text with length[2, 30] (required)

    [Required]
    [MaxLength(GlobalConstants.CitizenLastNameMaxLength)]
    public string LastName { get; set; } = null!;
    //•	LastName – text with length[2, 30] (required)

    [Required]
    public DateTime BirthDate { get; set; }
    //•	BirthDate – DateTime(required)

    [Required]
    public MaritalStatus MaritalStatus { get; set; }
    //•	MaritalStatus - MaritalStatus enum (Unmarried = 0, Married, Divorced, Widowed) (required)


    public ICollection<PropertyCitizen> PropertiesCitizens { get; set; } = new HashSet<PropertyCitizen>();

    //public Citizen()
    //{
    //    PropertiesCitizens = new HashSet<PropertyCitizen>();
    //}
    //•	PropertiesCitizens - collection of type PropertyCitizen

}
