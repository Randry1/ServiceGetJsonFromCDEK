using System.ComponentModel.DataAnnotations;

namespace WebSwager.Model;

public class Package
{
    [Required]
    public string dateExecute { get; set; }
    
    [Required]
    public string senderCityId { get; set; }

    [Required] public string receiverCityId { get; set; }
    
    [Required] public string tariffId { get; set; }
    
    [Required]
    public Good[] goods { get; set; }
    
    [Required]
    public Service[] services { get; set; }
}