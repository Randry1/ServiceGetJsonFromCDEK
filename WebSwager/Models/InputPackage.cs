using System.ComponentModel.DataAnnotations;

namespace WebSwager.Model;

public class InputPackage
{
    [Required]
    public string fiasSenderCity { get; set; }

    [Required] 
    public string fiasReceiverCity { get; set; }
    
    [Required] 
    public string weight { get; set; }
    
    [Required] 
    public string length { get; set; }
    
    [Required]
    public string width { get; set; }
    
    [Required]
    public string height { get; set; }
}