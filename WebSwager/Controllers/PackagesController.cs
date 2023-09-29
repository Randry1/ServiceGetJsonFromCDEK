using Microsoft.AspNetCore.Mvc;
using WebSwager.Model;
using System.Text;
using Newtonsoft.Json;


namespace WebSwager.Controllers;

[Route("api/[controller]")]
public class PackagesController : Controller
{
    
    private static List<UserPackage> _userPackages = new List<UserPackage>(
        new [] {
            new UserPackage()
            {
                Id = "1",
                fiasSenderCity = "52297f9a-df7f-4d97-9260-342aed1f0718",
                fiasReceiverCity = "394a840f-9502-406f-a8be-3a2aa9e8f075",
                weight = "500",
                length = "500",
                width = "500",
                height = "500",
            },
            new UserPackage()
            {
                Id = "2",
                fiasSenderCity = "4e503ad8-0372-4434-888c-f111ed78040c",
                fiasReceiverCity = "394a840f-9502-406f-a8be-3a2aa9e8f075",
                weight = "500",
                length = "500",
                width = "500",
                height = "500",
            }
        }
        );

    public static CityCDEK[] cities = UserPackage.InitAllCities();
    
    [HttpGet]
    public IEnumerable<UserPackage> Get() => _userPackages;
    
     
     [HttpPost("GetPrice")]
     public IActionResult GetPrice(UserPackage user_package)
     {
         if (!ModelState.IsValid)
         {
             return BadRequest(ModelState);
         }

         user_package.Id = GetNextProductId();
         _userPackages.Add(user_package);

         return Ok(user_package.GetPrice());

     }
    
    [HttpGet("GetAllCities")]
    public IActionResult GetAllCities()
    {
            CityCDEK[] allCity = UserPackage.InitAllCities();
            if (allCity != null)
            {
                return Ok(allCity);
            } 
            else
            {
                return NotFound();
            }
    }
    
    
    private string NextUserPackageId =>
        _userPackages.Count() == 0 ? "1" : (_userPackages.Max(x => Int32.Parse(x.Id)) + 1).ToString();

    [HttpGet("GetNextProductId")]
    public string GetNextProductId()
    {
        return NextUserPackageId;
    }
    
    [HttpPost]
    public IActionResult Post(UserPackage package)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        package.Id = GetNextProductId();
        _userPackages.Add(package);
        return CreatedAtAction(nameof(Get), new { id = package.Id }, package);
    }

    [HttpPost("GetPriceJson")]
    public IActionResult GetPriceJson(UserPackage user_package)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        user_package.Id = GetNextProductId();
        _userPackages.Add(user_package);

        var answerCdek = user_package.GetPriceJson();
        return Ok(answerCdek);
    }
}