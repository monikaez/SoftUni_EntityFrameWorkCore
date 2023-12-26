

using Medicines.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace Medicines.Data.Models;

public class Patient
{
    public Patient()
    {
        this.PatientsMedicines = new List<PatientMedicine>();
    }

    [Key]
    public int Id { get; set; }
    //•	Id – integer, Primary Key

    [Required]
    [MaxLength(100)]
    public string FullName { get; set; } = null!;
    //•	FullName – text with length[5, 100] (required)

    [Required]
    public AgeGroup AgeGroup { get; set; }
    //•	AgeGroup – AgeGroup enum (Child = 0, Adult, Senior) (required)

    [Required]
    public Gender Gender { get; set; }
    //•	Gender – Gender enum (Male = 0, Female) (required)

    public virtual ICollection<PatientMedicine> PatientsMedicines { get; set; }
    //•	PatientsMedicines - collection of type PatientMedicine

}
