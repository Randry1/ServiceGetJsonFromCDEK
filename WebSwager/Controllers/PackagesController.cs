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


    [HttpGet("GetPriceJson")]
    public IActionResult GetPriceJson(Package package)
    {
        
        var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://api.cdek.ru/calculator/calculate_price_by_json.php");
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "POST";

        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
            Package tmp_packege = _packages.FirstOrDefault(p => p.dateExecute == "2023-09-28");
            var jsonRequest =  JsonConvert.SerializeObject(tmp_packege);

            string json = jsonRequest.ToString();
            streamWriter.Write(json);
        }

        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            var result = streamReader.ReadToEnd();
            var json_respond = JsonConvert.DeserializeObject(result);
            
            Debug.WriteLine( "\n\n\n\n\n"+ JsonConvert.SerializeObject(json_respond).ToString() + "\n\n\n\n\n");
            
            return Ok(new {Message = result});
        }
    }
    
}