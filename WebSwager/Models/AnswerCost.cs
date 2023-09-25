using System.ComponentModel.DataAnnotations;


namespace WebSwager.Model;

public class AnswerCost
{
    [Required]
    public string price { get; set; }
    
    [Required]
    public string deliveryPeriodMin { get; set; }
    
    [Required]
    public string deliveryPeriodMax { get; set; }
    
    [Required]
    public string deliveryDateMin { get; set; }
    
    [Required]
    public string deliveryDateMax { get; set; }
    
    [Required]
    public string tariffId { get; set; }
    
    [Required]
    public string priceByCurrency { get; set; }
    
    [Required]
    public string currency { get; set; }
    
    [Required]
    public string percentVAT { get; set; }

    public List<Service> services { get; set; }
}