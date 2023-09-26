using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace WebSwager.Model;

public class UserPackage
{
    public string Id { get; set; }
    
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

    public string ConvertWeight()
    {
        return (float.Parse(weight, CultureInfo.InvariantCulture.NumberFormat) / 1000).ToString();
    }
    
    public string ConvertLength()
    {
        return (float.Parse(length, CultureInfo.InvariantCulture.NumberFormat) / 10).ToString();
    }
    
    public string ConvertWidth()
    {
        return (float.Parse(weight, CultureInfo.InvariantCulture.NumberFormat) / 10).ToString();
    }
    
    public string ConvertHeight()
    {
        return (float.Parse(height, CultureInfo.InvariantCulture.NumberFormat) / 10).ToString();
    }
}