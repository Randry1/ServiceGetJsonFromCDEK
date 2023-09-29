using System.ComponentModel.DataAnnotations;

namespace WebSwager.Model;

public class GoodCDEK
{
    [Required]
    public string weight { get; set; }  = String.Empty;
    
    [Required]
    public string length { get; set; }  = String.Empty;

    [Required] public string width { get; set; }  = String.Empty;
    
    [Required] public string height { get; set; }  = String.Empty;
}