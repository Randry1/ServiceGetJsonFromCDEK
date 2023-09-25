using System.ComponentModel.DataAnnotations;

namespace WebSwager.Model;

public class PackageCDEK
{
    [Required]
    public string dateExecute { get; set; }
    
    [Required]
    public string senderCityId { get; set; }

    [Required] public string receiverCityId { get; set; }
    
    [Required] public string tariffId { get; set; }
    
    [Required]
    public GoodCDEK[] goods { get; set; }
    
    [Required]
    public ServiceCDEK[] services { get; set; }
}