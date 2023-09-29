using System.ComponentModel.DataAnnotations;

namespace WebSwager.Model;

public class PackageCDEK
{
    [Required]
    public string dateExecute { get; set; } = String.Empty;
    
    [Required]
    public string senderCityId { get; set; } = String.Empty;

    [Required] public string receiverCityId { get; set; } = String.Empty;

    [Required] public string tariffId { get; set; } = String.Empty;

    [Required]
    public GoodCDEK[] goods { get; set; }
    
    [Required]
    public ServiceCDEK[] services { get; set; }
}