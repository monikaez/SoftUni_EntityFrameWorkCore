
using Cadastre.Common;
using Cadastre.Data.Enumerations;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Cadastre.DataProcessor.ImportDtos;

public class ImportCitizenDto
{
    //  "FirstName": "Ivan",
    [JsonProperty("FirstName")]
    [Required]
    [MaxLength(GlobalConstants.CitizenFirstNameMaxLength)]
    [MinLength(GlobalConstants.CitizenFirstNameMinLength)]
    public string FirstName { get; set; } = null!;
    //•	FirstName – text with length[2, 30] (required)

    //  "LastName": "Georgiev",
    [JsonProperty("LastName")]
    [Required]
    [MaxLength(GlobalConstants.CitizenLastNameMaxLength)]
    [MinLength(GlobalConstants.CitizenLastNameMinLength)]
    public string LastName { get; set; } = null!;
    //•	LastName – text with length[2, 30] (required)


    //  "BirthDate": "12-05-1980",
    [JsonProperty("BirthDate")]
    [Required]
    public string BirthDate { get; set; } = null!;
    //•	BirthDate – DateTime(required)


    //  "MaritalStatus": "Married",
    [JsonProperty("MaritalStatus")]
    [Required]
    public string MaritalStatus { get; set; }
    //•	MaritalStatus - MaritalStatus enum (Unmarried = 0, Married, Divorced, Widowed) (required)


    //  "Properties": [ 17, 29 ]
    [JsonProperty("Properties")]
    public int[] PropertiesIds { get; set; }

}







