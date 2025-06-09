using System.ComponentModel.DataAnnotations;


public class AddOnCreateDto
{
    [Required]
    public string Name { get; set; }


    public decimal Price { get; set; }


    public bool IsActive { get; set; } = true;
}