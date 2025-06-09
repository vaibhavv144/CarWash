using System.ComponentModel.DataAnnotations;

public class CarAddDTO
{
    [Required]
    public string Make { get; set; }

    [Required]
    public string Model { get; set; }
    [Required]
    public int Year { get; set; }
}
