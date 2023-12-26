

using Medicines.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;

namespace Medicines.Data.Models;

public class Medicine
{
    [Key]
    public int Id { get; set; }
    //    •	Id – integer, Primary Key

    [Required]
    [MaxLength(150)]
    public string Name { get; set; } = null!;
    //•	Name – text with length[3, 150] (required)

    [Required]
    public decimal Price { get; set; }
    //•	Price – decimal in range[0.01…1000.00] (required)

    [Required]
    public Category Category { get; set; }
    //•	Category – Category enum (Analgesic = 0, Antibiotic, Antiseptic, Sedative, Vaccine) (required)

    [Required]
    public DateTime ProductionDate { get; set; }
    //•	ProductionDate – DateTime(required)

    [Required]
    public DateTime ExpiryDate { get; set; }
    //•	ExpiryDate – DateTime(required)

    [Required]
    [MaxLength(100)]
    public string Producer { get; set; } = null!;
    //•	Producer – text with length[3, 100] (required)

    [ForeignKey(nameof(PharmacyId))]
    public int PharmacyId { get; set; }
    //•	PharmacyId – integer, foreign key(required)
    public virtual Pharmacy Pharmacy { get; set; } = null!;
    //•	Pharmacy – Pharmacy

    public  virtual ICollection<PatientMedicine> PatientsMedicines { get; set; }

    public Medicine()
    {
        this.PatientsMedicines = new List<PatientMedicine>();
    }
    //•	PatientsMedicines - collection of type PatientMedicine

}
