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

    public static CityCDEK[] cities = InitAllCities();
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
    

    [HttpPost("GetPrice")]
    public IActionResult GetPrice(UserPackage user_package)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        user_package.Id = GetNextProductId();
        _userPackages.Add(user_package);
        _package_Cdek = ConvertUserPackageInCdek(user_package, _package_Cdek);
        var answerCdek_sting = GetAnswerToCDEK(_package_Cdek);
        var answer_CDEK = JsonConvert.DeserializeObject<AnswerCDEK>(answerCdek_sting);
        Price price;
        if (answer_CDEK.result != null)
        {
            price = new Price() { price = answer_CDEK.result.price };
            return Ok(price);
        }
        else
        {
            return Ok(new { Error = "Невозможно доставить"});
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
    public IActionResult GetPriceJson(UserPackage user_package)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        user_package.Id = GetNextProductId();
        _userPackages.Add(user_package);
        _package_Cdek = ConvertUserPackageInCdek(user_package, _package_Cdek);
        var answerCdek_sting = GetAnswerToCDEK(_package_Cdek);
        var answer_CDEK = JsonConvert.DeserializeObject<AnswerCDEK>(answerCdek_sting);
        if (answer_CDEK != null)
        {
            return Ok(answer_CDEK);
        }
        else
        {
            return Ok(new { Error = answerCdek_sting});
        }
    }
    
    private string GetAnswerToCDEK(PackageCDEK new_package_CDEK)
    {
        string result = "";
        using (var client = new HttpClient())
        {
            var uri = new Uri("http://api.cdek.ru/calculator/calculate_price_by_json.php");
            var new_package_CDEK_json = JsonConvert.SerializeObject(new_package_CDEK);
            var request = new StringContent(new_package_CDEK_json, Encoding.UTF8, "application/json");
            var response_CDEK = client.PostAsync(uri, request);
            if (response_CDEK.Result.IsSuccessStatusCode)
            {
                var response_CDEK_string = response_CDEK.Result.Content.ReadAsStringAsync().Result;
                result =  response_CDEK_string;
            }
        }

        return result;
    }
    
    private static CityCDEK[] InitAllCities()
    {
        using (var client = new HttpClient())
        {
            var endpoint = new Uri("http://integration.cdek.ru/v1/location/cities/json?");
            var result = client.GetAsync(endpoint).Result;
            if (!result.IsSuccessStatusCode)
            {
                throw new Exception("Нет связи сервером СДЕК.");
            }
            var json = result.Content.ReadAsStringAsync().Result;
            var all_cities = JsonConvert.DeserializeObject<CityCDEK[]>(json);
            
            return all_cities;
        }
    }

    private string GetCityCode(string fiasGuid)
    {
        string cityCode = "0";
        
        if (cities != null)
        {
            foreach (CityCDEK city in cities)
            {
                if (city.fiasGuid == fiasGuid)
                {
                    cityCode = city.cityCode;
                }
            }
        }
        else
        {
            cities = InitAllCities();
        }
        return cityCode;
    }

    private string GetDate()
    {
        return DateTime.Now.ToString("yyyy-MM-dd");
    }

    private PackageCDEK ConvertUserPackageInCdek(UserPackage user_package, PackageCDEK package_cdek)
    {
        _package_Cdek.dateExecute = GetDate();
        _package_Cdek.senderCityId = GetCityCode(user_package.fiasSenderCity);
        _package_Cdek.receiverCityId = GetCityCode(user_package.fiasReceiverCity);
        _package_Cdek.goods[0] = new GoodCDEK() { weight = user_package.ConvertWeight(), length = user_package.ConvertLength(), height = user_package.ConvertHeight(), width = user_package.ConvertWidth() };
        _package_Cdek.tariffId = "480";
        return package_cdek;
    }

}