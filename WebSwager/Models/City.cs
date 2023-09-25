using System.Diagnostics;

namespace WebSwager.Model;


public class City
{
    public string cityName { get; set; }
    public string cityCode { get; set; }
    public string cityUuid { get; set; }
    public string country { get; set; }
    public string countryCode { get; set; }
    public string region { get; set; }
    public string regionCode { get; set; }
    public string regionFiasGuid { get; set; }
    public string subRegion { get; set; }
    public double latitude { get; set; }
    public double longitude { get; set; }
    public string kladr { get; set; }
    public string fiasGuid { get; set; }
    public double paymentLimit { get; set; }
    public string timezone { get; set; }
}



// public class City
// {
//     public string cityName { get; set; }
//     public string cityCode { get; set; }
//     public string cityUuid { get; set; }
//     public string country { get; set; }
//     public string countryCode { get; set; }
//     public string region { get; set; }
//     public string regionCode { get; set; }
//     public string regionFiasGuid { get; set; }
//     public string subRegion { get; set; }
//     public string latitude { get; set; }
//     public string longitude { get; set; }
//     public string fiasGuid { get; set; }
//     public string paymentLimit { get; set; }
//     public string timezone { get; set; }
// }

// class ProgramMy
// {
//     static HttpClient httpClient = new HttpClient();
//     static async Task Main()
//     {
//         // отправляемый объект 
//         // отправляем запрос
//         var respond = httpClient.GetAsync("http://integration.cdek.ru/v1/location/cities/json?");
//         Debug.WriteLine(respond);
//         // using var response = await GetFromJsonAsync<List<City>>(httpClient, "http://integration.cdek.ru/v1/location/cities/json?");
//         // City? person = await response.Content.ReadFromJsonAsync<City>();
//         // Console.WriteLine($"{person?.Id} - {person?.Name}");
//     }
// }

    





















//     {
//         "cityName":"Усинск",
//         "cityCode":"5",
//         "cityUuid":"318ce2b1-d866-465d-90ec-e70000dcec45",
//         "country":"Россия",
//         "countryCode":"RU",
//         "region":"Республика Коми",
//         "regionCode":"1",
//         "regionFiasGuid":"c20180d9-ad9c-46d1-9eff-d60bc424592a",
//         "subRegion":"городской округ Усинск",
//         "latitude":65.994148,
//         "longitude":57.55701,"kladr":"1100000700000",
//         "fiasGuid":"267fd50d-0077-4041-9594-18b1de5f2acb",
//         "paymentLimit":-1.0,
//         "timezone":"Europe/Moscow"
// }