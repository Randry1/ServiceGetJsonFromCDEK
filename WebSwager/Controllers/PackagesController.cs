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

    [HttpGet]
    public IEnumerable<PackageCDEK> Get() => _packages;


    // [HttpGet("GetPriceJson")]
    // public IActionResult GetPriceJson(Package package)
    // {
    //     
    //     var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://api.cdek.ru/calculator/calculate_price_by_json.php");
    //     httpWebRequest.ContentType = "application/json";
    //     httpWebRequest.Method = "POST";
    //
    //     using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
    //     {
    //         Package tmp_packege = _packages.FirstOrDefault(p => p.dateExecute == "2023-09-28");
    //         var jsonRequest =  JsonConvert.SerializeObject(tmp_packege);
    //
    //         string json = jsonRequest.ToString();
    //         streamWriter.Write(json);
    //     }
    //
    //     var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
    //     using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
    //     {
    //         var result = streamReader.ReadToEnd();
    //         var json_respond = JsonConvert.DeserializeObject(result);
    //         
    //         Debug.WriteLine( "\n\n\n\n\n"+ JsonConvert.SerializeObject(json_respond).ToString() + "\n\n\n\n\n");
    //         
    //         return Ok(new {Message = result});
    //     }
    // }

    [HttpGet("GetPriceJson")]
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
            var answer_CDEK = JsonConvert.DeserializeObject<RootObject>(response_CDEK_string);
        }

        return Ok();
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
    
}