

using System.ComponentModel.DataAnnotations.Schema;

namespace Medicines.Data.Models;

public class PatientMedicine
{
    public int PatientId { get; set; }
    //•	PatientId – integer, Primary Key, foreign key(required)
    [ForeignKey(nameof(PatientId))]
    public virtual Patient Patient { get; set; } = null!;
    //•	Patient – Patient

    public int MedicineId { get; set; }
    //•	MedicineId – integer, Primary Key, foreign key(required)
    [ForeignKey(nameof(MedicineId))]
    public virtual Medicine Medicine { get; set; } = null!;
    //•	Medicine – Medicine
}
