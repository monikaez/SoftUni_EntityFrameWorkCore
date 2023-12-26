

using System.ComponentModel.DataAnnotations;

namespace Medicines.Data.Models;

public class Pharmacy
{
    [Key]
    public int Id { get; set; }
    //    •	Id – integer, Primary Key

    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = null!;
    //•	Name – text with length[2, 50] (required)
    [Required]
    [MaxLength(14)]
    public string PhoneNumber { get; set; } = null!;
    //•	PhoneNumber – text with length 14. (required)
    //o   All phone numbers must have the following structure: three digits enclosed in parentheses, followed by a space, three more digits, a hyphen, and four final digits: 
    //	Example -> (123) 456-7890 

    [Required]
    public bool IsNonStop { get; set; }
    //•	IsNonStop – bool (required)

    public virtual  ICollection<Medicine> Medicines { get; set; } = new List<Medicine>();
    //•	Medicines - collection of type Medicine

}
