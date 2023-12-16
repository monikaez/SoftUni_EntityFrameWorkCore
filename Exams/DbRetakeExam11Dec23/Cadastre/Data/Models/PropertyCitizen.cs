
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cadastre.Data.Models;

public class PropertyCitizen
{
    [Required]
    public int PropertyId { get; set; }
    //    •	PropertyId – integer, Primary Key, foreign key(required)
    [ForeignKey(nameof(PropertyId))]
    public Property Property { get; set; } = null!;
    //•	Property – Property

    [Required]
    public int CitizenId { get; set; }
    //•	CitizenId – integer, Primary Key, foreign key(required)
    [ForeignKey(nameof(CitizenId))]
    public Citizen Citizen { get; set; } = null!;
    //•	Citizen – Citizen

}
