using System.ComponentModel.DataAnnotations;


namespace WebSwager.Model;

public class AnswerCost

{
    public Result result { get; set; }
}

public class Result
{
    public string price { get; set; }
    public int deliveryPeriodMin { get; set; }
    public int deliveryPeriodMax { get; set; }
    public string deliveryDateMin { get; set; }
    public string deliveryDateMax { get; set; }
    public string tariffId { get; set; }
    public int priceByCurrency { get; set; }
    public string currency { get; set; }
    public AnswerServices[] services { get; set; }
}

public class AnswerServices
{
    public int id { get; set; }
    public string title { get; set; }
    public int price { get; set; }
}

//
// public class AnswerCost
// {
//     [Required]
//     public string price { get; set; }
//     
//     [Required]
//     public string deliveryPeriodMin { get; set; }
//     
//     [Required]
//     public string deliveryPeriodMax { get; set; }
//     
//     [Required]
//     public string deliveryDateMin { get; set; }
//     
//     [Required]
//     public string deliveryDateMax { get; set; }
//     
//     [Required]
//     public string tariffId { get; set; }
//     
//     [Required]
//     public string priceByCurrency { get; set; }
//     
//     [Required]
//     public string currency { get; set; }
//     
//     [Required]
//     public string percentVAT { get; set; }
//
//     public List<Service> services { get; set; }
// }