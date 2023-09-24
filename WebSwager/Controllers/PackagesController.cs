using Microsoft.AspNetCore.Mvc;
using WebSwager.Model;

namespace WebSwager.Controllers;

[Route("api/[controller]")]
public class PackagesController : Controller
{
    private static List<Package> _packages = new List<Package>(
        new []
        {
            new Package() { dateExecute = "2023-09-28", senderCityId = "72", receiverCityId = "73", tariffId = "2", 
                goods = new List<Good>(
                    new []
                    {
                        new Good(){weight = "0.3", length = "5", width = "20", height = "10"}
                    }),

                services = new List<Service>()
                {
                    new Service()
                    {
                        id = "7"
                    }
                }
                    
            }
            
        }
    );

    [HttpGet]
    public IEnumerable<Package> Get() => _packages;


    [HttpGet("GetPrice")]
    public Package GetPrice(Package package)
    {
        return package;
    }
}