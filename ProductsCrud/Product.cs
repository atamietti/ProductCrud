using System.ComponentModel.DataAnnotations;

namespace ProductsCrud;
public class Product
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "The price must be grater than 0.0")]
    public decimal Price { get; set; }

}


