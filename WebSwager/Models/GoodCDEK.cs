using System.ComponentModel.DataAnnotations;

namespace WebSwager.Model;

public class GoodCDEK
{
    [Required]
    public string weight { get; set; }
    
    [Required]
    public string length { get; set; }

    [Required] public string width { get; set; }
    
    [Required] public string height { get; set; }
}