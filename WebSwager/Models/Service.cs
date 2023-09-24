using System.ComponentModel.DataAnnotations;

namespace WebSwager.Model;

public class Service
{
    [Required]
    public string id { get; set; }
}