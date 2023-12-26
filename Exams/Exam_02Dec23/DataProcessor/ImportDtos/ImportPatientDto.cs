

using Medicines.Data.Models.Enums;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Medicines.DataProcessor.ImportDtos;


public class ImportPatientDto
{
    //  "FullName": "Ivan Petrov",
    [JsonProperty("FullName")]
    [Required]
    [MaxLength(100)]
    [MinLength(5)]
    public string FullName { get; set; } = null!;
    //•	FullName – text with length[5, 100] (required)

    //  "AgeGroup": "1",
    [JsonProperty("AgeGroup")]
    [Required]
    [Range(0,2)]
    public int AgeGroup { get; set; }
    //•	AgeGroup – AgeGroup enum (Child = 0, Adult, Senior) (required)

    //  "Gender": "0"
    [Required]
    [Range(0,1)]
    public int Gender { get; set; }
    //•	Gender – Gender enum (Male = 0, Female) (required)

    //  "Medicines": 
    [JsonProperty("Medicines")]
    public int[] Medicines { get; set; } = null!;

}




