using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebSwager.Model;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using Newtonsoft.Json;


namespace WebSwager.Controllers;

[Route("api/[controller]")]
public class PackagesController : Controller
{
    private static List<PackageCDEK> _packages = new List<PackageCDEK>(
        new []
        {
            new PackageCDEK() { dateExecute = "2023-09-28", senderCityId = "72", receiverCityId = "73", tariffId = "2", 
                goods = new []
                    {
                        new GoodCDEK(){weight = "0.3", length = "5", width = "20", height = "10"}
                    },

                services = new [] { new ServiceCDEK() { id = "7" } }
            }
            
        }
    );

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

    public static CityCDEK[] _cities = InitAllCities();
    public UserPackage _user_package;
    public MainOrder _main_order = new MainOrder(){};
    public PackageCDEK _package_Cdek = new PackageCDEK(){
        dateExecute = "2023-09-28", 
        senderCityId = "72", 
        receiverCityId = "73", 
        tariffId = "2", 
        goods = new []
        {
            new GoodCDEK(){weight = "1", length = "1", width = "1", height = "1"}
        }
        
    };
    
    [HttpGet]
    public IEnumerable<UserPackage> Get() => _userPackages;
    

    [HttpGet("GetPrice")]
    public IActionResult GetPrice()
    {
        using (var client = new HttpClient())
        {
            var uri = new Uri("http://api.cdek.ru/calculator/calculate_price_by_json.php");
            var new_package_CDEK = _packages.FirstOrDefault();
            var new_package_CDEK_json = JsonConvert.SerializeObject(new_package_CDEK);
            var request = new StringContent(new_package_CDEK_json, Encoding.UTF8, "application/json");
            var response_CDEK = client.PostAsync(uri, request).Result;
            var response_CDEK_string = response_CDEK.Content.ReadAsStringAsync().Result;
            var answer_CDEK = JsonConvert.DeserializeObject<AnswerCDEK>(response_CDEK_string);
            
            return Ok(answer_CDEK);
        }

    }
    
    [HttpGet("GetAllCities")]
    public IActionResult GetAllCities()
    {
        using (var client = new HttpClient())
        {
            var endpoint = new Uri("http://integration.cdek.ru/v1/location/cities/json?");
            var result = client.GetAsync(endpoint).Result;
            var json = result.Content.ReadAsStringAsync().Result;
            var all_city = JsonConvert.DeserializeObject<CityCDEK[]>(json);
            if (all_city != null)
            {
            return Ok(all_city);
            } else
            {
                return NotFound();
            }
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
    public IActionResult GetPrice(UserPackage package)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        package.Id = GetNextProductId();
        _userPackages.Add(package);
        _package_Cdek.dateExecute = "2023-10-24";
        _package_Cdek.senderCityId = GetCityCode(package.fiasSenderCity);
        _package_Cdek.receiverCityId = GetCityCode(package.fiasReceiverCity);
        _package_Cdek.goods[0] = new GoodCDEK() { weight = package.ConvertWeight(), length = package.ConvertLength(), height = package.ConvertHeight(), width = package.ConvertWidth() };
        _package_Cdek.tariffId = "480";
        var answerCdek_sting = GetAnswerToCDEK(_package_Cdek);
        var answer_CDEK = JsonConvert.DeserializeObject<AnswerCDEK>(answerCdek_sting);
        if (answer_CDEK != null)
        {
            return Ok(answer_CDEK);
        }
        else
        {
            return Ok(new { Message = answerCdek_sting});
        }
    }
    
    private string GetAnswerToCDEK(PackageCDEK new_package_CDEK)
    {
        using (var client = new HttpClient())
        {
            var uri = new Uri("http://api.cdek.ru/calculator/calculate_price_by_json.php");
            var new_package_CDEK_json = JsonConvert.SerializeObject(new_package_CDEK);
            var request = new StringContent(new_package_CDEK_json, Encoding.UTF8, "application/json");
            var response_CDEK = client.PostAsync(uri, request).Result;
            var response_CDEK_string = response_CDEK.Content.ReadAsStringAsync().Result;
            // var answer_CDEK = JsonConvert.DeserializeObject<AnswerCDEK>(response_CDEK_string);
            
            return response_CDEK_string;
        }

    }
    
    private static CityCDEK[] InitAllCities()
    {
        using (var client = new HttpClient())
        {
            var endpoint = new Uri("http://integration.cdek.ru/v1/location/cities/json?");
            var result = client.GetAsync(endpoint).Result;
            var json = result.Content.ReadAsStringAsync().Result;
            var all_cities = JsonConvert.DeserializeObject<CityCDEK[]>(json);
            
            return all_cities;
        }
    }

    private string GetCityCode(string fiasGuid)
    {
        string cityCode = "0";
        if (_cities != null)
        {
            foreach (CityCDEK city in _cities)
            {
                if (city.fiasGuid == fiasGuid)
                {
                    cityCode = city.cityCode;
                }
            }
        }
        else
        {
            _cities = InitAllCities();
        }
        return cityCode;
    }
    

}