using System.Diagnostics;
using System.Text;
using Newtonsoft.Json;

namespace WebSwager.Model;

public class MainOrder
{
    public MainOrder()
    {
        _cities = GetAllCities();
    }
    public static CityCDEK[] _cities;
    
    public CityCDEK[] GetAllCities()
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

    public string GetCityCode(string fiasGuid)
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
            _cities = GetAllCities();
        }
        return cityCode;
    }

    public void print_cities()
    {
        if (_cities != null)
        {
            foreach (CityCDEK city in _cities)
            {
                Debug.WriteLine(city.fiasGuid + "\t" + city.cityCode + "\t" + city.cityName);
            }
        }
    }
    

}