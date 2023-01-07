using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstDemoCS.Models;

[Table("Test")]
public class Test
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    [MaxLength(200)]
    public string? Summary { get; set; }

    [Range(0, 9999)]
    public int PinCode { get; set; } = 0000;

}
